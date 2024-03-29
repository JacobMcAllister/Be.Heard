﻿using BeHeard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Services;

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

            //var thisSession = context.HttpContext.Session.GetObjectFromJson<SessionModel>("thisSession");
            //if (thisSession == null)
            //{
            //    context.Result = new RedirectResult("~/landing");
            //}

            var service = new SessionService(context.HttpContext);
            if (service.Get() == null)
            {
                context.Result = new RedirectResult("~/landing");
            }

        }
    }
}
