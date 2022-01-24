using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOwnIdentityApp.Context;
using MyOwnIdentityApp.Library;
using MyOwnIdentityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOwnIdentityApp.Controllers
{
    public class HomeController : Controller
    {
        private IdentityAppContext db;
        public HomeController(IdentityAppContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }
        [TypeFilter(typeof(UserAuth))]
        public IActionResult UserIndex()
        {
            TempData["MessageUser"] = HttpContext.Session.GetString("UserSession");
            ViewBag.CookieUser = HttpContext.Request.Cookies["UserCookie"];
           
            return View();
        }
        [TypeFilter(typeof(AdminAuth))]
        public IActionResult AdminIndex()
        {
            TempData["MessageAdmin"] = HttpContext.Session.GetString("AdminSession");
            ViewBag.CookieAdmin = HttpContext.Request.Cookies["AdminCookie"];
            ViewBag.UserList=db.Users.ToList();

            return View();
        }
        public JsonResult AdminJson()
        {
            GenericResponse<string> response = new GenericResponse<string>();

            var adminSession = HttpContext.Session.GetString("AdminSession");
            if (adminSession != null)
            {
                response.HasError = false;
                response.Message = "";
            }
            else
            {
                response.HasError = true;
                response.Message = "Please Login!";
            }
            return Json(response);
        }
        public JsonResult UserJson()
        {
            GenericResponse<string> response = new GenericResponse<string>();

           var userSession= HttpContext.Session.GetString("UserSession");
            if (userSession != null)
            {
                response.HasError = false;
                response.Message = "";
            }
            else
            {
                response.HasError = true;
                response.Message = "Please Login!";
            }
            return Json(response);
        }
        [HttpPost]
        [TypeFilter(typeof(AdminAuth))]
        public JsonResult AddRoleToUser1(int userId, RoleType roleType)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            User user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (user == null)
            {
                response.HasError = true;
                response.Message = "User not Exist!";
                return Json(response);
            }
            else
            {
                user.Role = roleType;
                db.Users.Update(user);
                if (db.SaveChanges() > 0)
                {
                    response.HasError = false;
                    response.Message = "Role Added With Successfull!";
                }
                else
                {
                    response.HasError = true;
                    response.Message = "Role not Added to User!";
                }
            }


            return Json(response);
        }
    }
}
