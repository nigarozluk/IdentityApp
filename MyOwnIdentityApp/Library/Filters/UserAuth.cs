using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using MyOwnIdentityApp.Context;
using MyOwnIdentityApp.Models;
using System.Linq;

namespace MyOwnIdentityApp.Library
{
    public class UserAuth :ActionFilterAttribute, IAuthorizationFilter
    {
    private IdentityAppContext db;
    public UserAuth(IdentityAppContext _db)
    {
        db = _db;

    }
    public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("UserSession") == null)
            {
                string cookie = filterContext.HttpContext.Request.Cookies["UserCookie"];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(cookie))
                    {
                        User user = db.Users.FirstOrDefault(k => k.RememberMe == cookie);
                        if (user != null)
                        {
                            filterContext.HttpContext.Session.SetString("UserSession", user.UserName);
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
