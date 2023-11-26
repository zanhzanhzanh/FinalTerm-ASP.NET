using FinalTerm.Common;
using FinalTerm.Common.HandlingException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FinalTerm.Filters {
    public class ErrorHandlerFilter : ExceptionFilterAttribute {
        public override void OnException(ExceptionContext context) {
            Exception exception = context.Exception;

            //var problemDetail = new ProblemDetails {
            //    Title = exception.Message,
            //    Status = (int)HttpStatusCode.InternalServerError,
            //};

            if (exception is ApiException apiException) {
                context.Result = new ObjectResult(new ProblemDetails {
                    Status = apiException.ErrorCode,
                    Title = apiException.ErrorMessage,
                });
                context.ExceptionHandled = true;
            }

            //else {
            //    context.Result = new ObjectResult(new ProblemDetails {
            //        Status = (int)HttpStatusCode.InternalServerError,
            //        Title = exception.Message,
            //    });
            //    context.ExceptionHandled = true;
            //}
        }
    }
}
