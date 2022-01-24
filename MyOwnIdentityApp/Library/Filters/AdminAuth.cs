using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using MyOwnIdentityApp.Context;
using MyOwnIdentityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOwnIdentityApp.Library
{
    public class AdminAuth : ActionFilterAttribute, IAuthorizationFilter
    {
        private IdentityAppContext db;
        public AdminAuth(IdentityAppContext _db)
        {
            db = _db;
            
        }
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext filterContext)
        {
            
            if (filterContext.HttpContext.Session.GetString("AdminSession") == null)
            {
                string cookie = filterContext.HttpContext.Request.Cookies["AdminCookie"];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(cookie))
                    {
                            User admin = db.Users.FirstOrDefault(k => k.RememberMe == cookie);
                            if (admin != null)
                            {
                                filterContext.HttpContext.Session.SetString("AdminSession",admin.UserName);
                            }
                            else 
                            {
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                                            new { action = "Login", controller = "Account" }));
                            }
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                                            new { action = "Login", controller = "Account" }));
                    }
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                                            new { action = "Login", controller = "Account" }));
                }
            }
        }
    }
}