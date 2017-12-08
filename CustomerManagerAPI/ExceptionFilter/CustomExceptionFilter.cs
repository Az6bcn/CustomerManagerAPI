using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CustomerManagerAPI.ExceptionFilter
{
    // Exception filters handle unhandled exceptions
    /*exceptions thrown by your controller methods are captured by the custom exception 
    filter and then converted to HttpStatusResponse objects with appropriate HttpStatusCode.*/
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {

        // method is what is called when the application encounters an exception
        public override void OnException(ExceptionContext context)
        {
            string message = context.Exception.Message;
            var StatusCode = context.HttpContext.Response.StatusCode = 500;

            if (context.Exception.InnerException != null) { 
            

                message = $"Exception: {message} InnerException: {context.Exception.InnerException.Message}";
                
            }

            var result = new ObjectResult(new JsonResult(new { message, StatusCode }));

         


            //Log Critical errors
            Debug.WriteLine("#######################################################", message);
            context.Result = result;
            
            
        }

    }
}

