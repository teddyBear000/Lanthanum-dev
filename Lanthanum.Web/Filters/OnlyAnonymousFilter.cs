using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lanthanum.Web.Filters
{
    public class OnlyAnonymousFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity is {IsAuthenticated: true})
            {
               context.Result = new LocalRedirectResult("~/");
            }

        }
    }
}
