using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Application.Exceptions;
using System.Collections.Generic;
using FluentValidation;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using FluentValidation.Results;

namespace Web.Ui.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class WebCustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is CustomValidationException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                context.Result = new JsonResult(
                ((CustomValidationException)context.Exception).Failures);

                return;
            }

            var code = HttpStatusCode.InternalServerError;

            if (context.Exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            if (context.Exception is CreationFailureException)
            {
                code = HttpStatusCode.BadRequest;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace
            });
        }
    }

    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                // var ValidationFailureErrors = new ValidationFailure("Validation", errors);

                // throw new CustomValidationException(new List<FluentValidation.Results.ValidationFailure>());

                var responseObj = new
                {
                    // Message = "Bad Request",
                    Errors = errors                    
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
        }
    }
}