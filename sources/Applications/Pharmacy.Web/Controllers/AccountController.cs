using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Pharmacy.Data.Models;
using Pharmacy.Web.Models;

namespace Pharmacy.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        ////GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }

                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LoginErro", ex.Message);
                return View("Error");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var _db = new PharmacyEntities())
                    {
                        var loginInfo = _db.Users.FirstOrDefault(u => u.Name == model.Name && u.Password == model.Password);
                        if (loginInfo != null)
                        {
                            this.SignInUser(loginInfo.Id.ToString(), false);
                            Session["Email"] = loginInfo.Email.ToString();
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Wrong User ID or Password. Please try again...";
                            ModelState.Remove("Password");
                            return View(model);
                        }
                    }

                }
                ModelState.Remove("Password");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LoginErro", ex.Message);
                return View("Error");
            }

        }

        [HttpGet]
        public ActionResult LogOff()
        {
            try
            {
                Session.Clear();
                // Setting.    
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign Out.    
                authenticationManager.SignOut();
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Login", "Account");
        }

        private void SignInUser(string username, bool isPersistent)
        {
            // Initialization.    
            var claims = new List<Claim>();
            try
            {
                // Setting    
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign In.    
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
        }


    }
}