using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model;
using Microsoft.EntityFrameworkCore;
using CustomerManagerAPI.Models;
using BusinessManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthJWT.AuthJWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Swagger;

namespace CustomerManagerAPI
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Register EF Core Context with dependency injection
            services.AddDbContext<DummyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                options2 => options2.MigrationsAssembly("CustomerManagerAPI")));

            services.AddDbContext<AspNetUserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                options2 => options2.MigrationsAssembly("CustomerManagerAPI")));


            // Inject my UnitOf Service
            services.AddTransient<RegisterManager, RegisterManager>(); 
            services.AddTransient<AccountManager, AccountManager>();

            // Inject IJwtFactory Service
            services.AddSingleton<IJWFactory, JWTFactory>();

            services.AddIdentity<AppUser, IdentityRole>
              (options =>
              {
                   // configure identity options
                   options.Password.RequireDigit = false;
                  options.Password.RequireLowercase = false;
                  options.Password.RequireUppercase = false;
                  options.Password.RequireNonAlphanumeric = false;
                  options.Password.RequiredLength = 6;
              })
              .AddEntityFrameworkStores<AspNetUserContext>();   /* if not added this error shows up: 
                                                                    Microsoft.AspNetCore.Identity.IUserStore`1[netCoreWepApiAuthJWT.Model.AppUser]' while attempting
                                                                    to activate 'Microsoft.AspNetCore.Identity.UserManager`1[netCoreWepApiAuthJWT.Model.AppUser]'. */

            /*Read the JwtIssuerOptions settings from the config file to configure the JwtIssuerOptions and set it up for injection. */
           var jwtAppSettingOptions = Configuration.GetSection(nameof(JWTIssuerOptions));

            // Configure JwtIssuerOptions for some properties of JWTIssuerOptions class.
            services.Configure<JWTIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JWTIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JWTIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            /* tell the ASP.NET Core middleware that we want to use JWT authentication on incoming requests with JWTs */
            // var jwtAppSettingOptions = Configuration.GetSection(nameof(JWTIssuerOptions));

            // sets up the validation parameters that we’re going to pass to ASP.NET’s JWT bearer authentication middleware.
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JWTIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JWTIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            // tells the application that we expect JWT tokens as part of the authentication
            // and authorisation processes and to automatically challenge and authenticate.
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });


            // api user claim policy: https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims
            services.AddAuthorization(options =>
            {
                CustomClaimTypes c = new CustomClaimTypes();
                options.AddPolicy("GeneralManager", policy => policy.RequireClaim("GeneralManager", "GeneralManager"));
                options.AddPolicy("SectionManager", policy => policy.RequireClaim("SectionManager", "SectionManager"));
                options.AddPolicy("ProductsManager", policy => policy.RequireClaim("ProductsManager", "ProductsManager"));
            }); /* Adds a policy named 'GeneralManager', GeneralManager policy checks for the presence of an "GeneralManager" claimType,
            and claimValue  on the incoming token payload  with value of "GeneralManager".
                We then apply the policy using the Policy property on the AuthorizeAttribute attribute in our Controller to specify the policy name; 
                Only Identity with the stated claims will be Authorise to access the Controller, Action we apply this Policy.   
             */


            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", //Policy == AllowAngular, Enable policy in Controller: [EnableCors("AllowAngular")]
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            // https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio
            // Register the Swagger generator, defining one or more Swagger documents 
            /*a Swagger generator that builds SwaggerDocument objects directly from your routes, controllers, and models. */
            services.AddSwaggerGen(config =>
            {
                // Creates a Swagger doc named v1
                config.SwaggerDoc("v1", new Info { Title = "CustomerManagerAPI", Version = "v1" });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();

            // To enable CORS for your entire application add the CORS middleware to your request pipeline: before any call to UseMvc
            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
               builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod());

            //https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Add SwaggerUI middleware:  SwaggerUI, creates the UI Endpoint from Json file 
            app.UseSwaggerUI(config => {
                //creates first swagger endpoint url base on the json file and get the description
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerManagerAPI");
            });

            app.UseMvc();
        }
    }
}
