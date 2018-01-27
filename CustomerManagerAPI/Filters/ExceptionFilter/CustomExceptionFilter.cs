using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Model.Models;

namespace CustomerManagerAPI.ExceptionFilter
{   // https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/understanding-action-filters-cs
    // Exception filters handle unhandled exceptions
    /*exceptions thrown by your controller methods are captured by the custom exception 
    filter and then converted to HttpStatusResponse objects with appropriate HttpStatusCode.*/
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {


        public override void OnException(ExceptionContext context)
        {
            string message = context.Exception.Message;

            if (context.Exception.InnerException != null)
            {
                message = $"Exception: {message} InnerException: {context.Exception.InnerException.Message}";
            }
            var result = new ObjectResult(new Error{ErrorMessage = message})
            {
                StatusCode = 500
            };
            Debug.WriteLine(message);
            Debug.WriteLine("#######################################################", message);
            context.Result = result;
        }





        //// method is what is called when the application encounters an exception
        //public override void OnException(ExceptionContext context)
        //{
        //    string message = context.Exception.Message;
        //    var StatusCode = context.HttpContext.Response.StatusCode = 500;

        //    if (context.Exception.InnerException != null) { 


        //        message = $"Exception: {message} InnerException: {context.Exception.InnerException.Message}";

        //    }

        //    var result = new ObjectResult(new JsonResult(new { message, StatusCode }));




        //    //Log Critical errors
        //    Debug.WriteLine("#######################################################", message);
        //    context.Result = result;


        //}

    }
}

