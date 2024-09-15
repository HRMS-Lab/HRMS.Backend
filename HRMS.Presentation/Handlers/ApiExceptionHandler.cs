using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Presentation.Handlers
{
    public class ApiExceptionHandler : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                if (context.Result is ObjectResult objectResult)
                {
                    var responseObject = new
                    {
                        Success = true,
                        Message = "Entity operation successful.",
                        Data = objectResult.Value
                    };
                    context.Result = new ObjectResult(responseObject)
                    {
                        StatusCode = objectResult.StatusCode ?? 200
                    };
                }
            }
            else
            {
                // Exception occurred.
                var responseObject = new
                {
                    Success = false,
                    Message = "An error occurred.",
                    Error = context.Exception.Message
                };
                context.Result = new ObjectResult(responseObject)
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }

        }
    }
}
