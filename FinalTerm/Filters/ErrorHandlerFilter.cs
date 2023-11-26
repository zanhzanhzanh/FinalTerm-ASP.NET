using FinalTerm.Common.HandlingException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FinalTerm.Filters {
    public class ErrorHandlerFilter : ExceptionFilterAttribute {
        public override void OnException(ExceptionContext context) {
            Exception exception = context.Exception;

            if (exception is ApiException apiException) {
                context.Result = new ObjectResult(new ProblemDetails {
                    Status = apiException.ErrorCode,
                    Title = apiException.ErrorMessage,
                });
                context.ExceptionHandled = true;
            }

            if (exception is ArgumentException argumentException) {
                context.Result = new ObjectResult(new ProblemDetails {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = argumentException.Message,
                });
                context.ExceptionHandled = true;
            }

            else {
                context.Result = new ObjectResult(new ProblemDetails {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = exception.Message,
                });
                context.ExceptionHandled = true;
            }
        }
    }
}
