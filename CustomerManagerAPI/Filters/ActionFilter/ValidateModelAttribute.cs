using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerAPI.ActionFilter
{
    // https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/understanding-action-filters-cs
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters
    public class ValidateModelAttribute: ActionFilterAttribute
    {
        // method is called before a controller action is executed.
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // base.OnActionExecuting(context);
            if (!context.ModelState.IsValid)
            {
                // If model validation fails, it will return 400 (Bad Request). Won't get to execute the Action in the Controller.
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
