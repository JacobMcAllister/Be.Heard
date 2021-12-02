using BeHeard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BeHeardAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"];

            if (user == null)
            {
                //context.Result = new JsonResult(new { message = "Unauthorized" })
                //{
                //    StatusCode = StatusCodes.Status401Unauthorized
                //};

                context.Result = new RedirectResult("~/landing");
            }
        }
    }
}
