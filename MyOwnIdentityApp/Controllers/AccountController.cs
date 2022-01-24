using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOwnIdentityApp.Context;
using MyOwnIdentityApp.Library.Functions;
using MyOwnIdentityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyOwnIdentityApp.Controllers
{
    public class AccountController : Controller
    {
        private IdentityAppContext db;

        public List<User> users = null;
        public AccountController(IdentityAppContext _db)
        {
            db = _db;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login model)
        {
            HttpContext.Session.Clear();
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            string hashpassword = PasswordHasher.PasswordHash(model.Password);
            User user = db.Users.Where(x => x.UserName == model.UserName && x.Password == hashpassword).FirstOrDefault();
            if (user == null)
            {
                return View();
            }

            if (user.Role == RoleType.Admin)
            {

                HttpContext.Session.SetString("AdminSession", model.UserName);
                if (model.RememberLogin)
                {
                    string randomValue = GetStringRandom.GetRandom(12);
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddYears(1);
                    HttpContext.Response.Cookies.Append("AdminCookie", randomValue, cookieOptions);

                    user.RememberMe = randomValue;
                    db.SaveChanges();

                }

                return RedirectToAction("AdminIndex", "Home");
            }
            else if (user.Role == RoleType.User)
            {
                HttpContext.Session.SetString("UserSession", model.UserName);
                if (model.RememberLogin)
                {
                    string randomValue = GetStringRandom.GetRandom(12);
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddYears(1);
                    HttpContext.Response.Cookies.Append("UserCookie", randomValue, cookieOptions);

                    user.RememberMe = randomValue;
                    db.SaveChanges();

                }
                return RedirectToAction("UserIndex", "Home");
            }


            return View();
        }
        public JsonResult LogOut()
        {
            GenericResponse<string> response = new GenericResponse<string>();
            
            User user = db.Users.FirstOrDefault(x => x.UserName == HttpContext.Session.GetString("UserSession"));
            User admin = db.Users.FirstOrDefault(x => x.UserName == HttpContext.Session.GetString("AdminSession"));
            if (user != null)
            {
                db.Users.FirstOrDefault(k => k.Id == user.Id).RememberMe = null;
                if (db.SaveChanges() > 0)
                {
                    response.HasError = false;
                    response.Message = "Logout With Successful!";
                }
            }
            else if (admin != null)
            {
                db.Users.FirstOrDefault(k => k.Id == admin.Id).RememberMe = null;
                if (db.SaveChanges() > 0)
                {
                    response.HasError = false;
                    response.Message = "Logout With Successful!";
                }
            }
            else
            {
                response.HasError = true;
                response.Message = "Error";
            }


            if (HttpContext.Session.GetString("UserSession") != null || HttpContext.Session.GetString("AdminSession") != null)
            {
                HttpContext.Session.Clear();
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
            }

            return Json(response);
        }
        [HttpPost]
        public JsonResult AddUser(User user)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                bool isUser = db.Users.Any(x => x.UserName == user.UserName);
                if (isUser)
                {
                    response.HasError = true;
                    response.Message = "User Already!";
                    return Json(response);
                }
                user.Password = PasswordHasher.PasswordHash(user.Password);
                user.Role = RoleType.User;
                db.Users.Add(user);
                string randomValue = GetStringRandom.GetRandom(12);
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddYears(1);
                HttpContext.Response.Cookies.Append("UserCookie", randomValue, cookieOptions);

                user.RememberMe = randomValue;
                if (db.SaveChanges() > 0)
                {
                    HttpContext.Session.SetString("UserSession", user.UserName);
                    response.HasError = false;
                    response.Message = "Added With Successfull!";
                }
                else
                {
                    response.HasError = true;
                    response.Message = "User not Added!";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        
       
       
    }
}
