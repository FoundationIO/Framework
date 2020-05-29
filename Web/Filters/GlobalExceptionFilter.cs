/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Exceptions;
using Framework.Infrastructure.Models.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Framework.Web.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (!(context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
                return;

            if (controllerActionDescriptor.MethodInfo.ReturnType == null)
                return;

            if (!typeof(IReturnModel).IsAssignableFrom(controllerActionDescriptor.MethodInfo.ReturnType))
                return;

            if (exception is RollbackException)
                context.HttpContext.Response.StatusCode = HttpCodeContants.BadRequest;
            else
                context.HttpContext.Response.StatusCode = HttpCodeContants.ErrorOccured;

            context.Result = new JsonResult(new ReturnModel<object>(exception));
        }
    }
}
