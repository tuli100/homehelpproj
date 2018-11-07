using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using HomeHelpCallsWebSite.Infrastructure.Data;
using AutoMapper;
using HomeHelpCallsWebSite.Models;
using Microsoft.Owin.Security;

namespace HomeHelpCallsWebSite.Controllers
{
         [Authorize]
        public class AccountController : Controller
        {
        #region Private Properties    
        /// <summary>  
        /// Database Store property.    
        /// </summary>  
        private ApplicationDbContext _conntext;
        private IMapper _userMapper;
        #endregion
        #region Default Constructor    
        /// <summary>  
        /// Initializes a new instance of the <see cref="AccountController" /> class.    
        /// </summary>  
        public AccountController()
        {
            _conntext = new ApplicationDbContext();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MM_HH_USERS, LoginViewModel>());
            _userMapper = config.CreateMapper();
        }
        #endregion
        #region Login methods   

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                // Verification.    
                if (this.Request.IsAuthenticated)
                {
                    // Info.    
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            // Info.    
            return this.View();
        }
        /// <summary>  
        /// POST: /Account/Login    
        /// </summary>  
        /// <param name="model">Model parameter</param>  
        /// <param name="returnUrl">Return URL parameter</param>  
        /// <returns>Return login view</returns>  
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                // Verification.    
                if (ModelState.IsValid)
                {
                    // Initialization. 
                    var strms = _conntext.ExecuteFunction<string>("mm_hh.mm_hh_get_roles", model.Username, model.Password);
                  
                    //var loginInfo = this.databaseManager.LoginByUsernamePassword(model.Username, model.Password).ToList();
                    // Verification.    
                    if (strms != null && strms.Count() > 0)
                    {

                        //model.Roles = strms.Split(',');
                        // Initialization.    
                        //var logindetails = loginInfo.First();
                        // Login In.    
                        this.SignInUser(model.Username, strms, false);
                      //  this.SignInUser.
                        
                        // Info.    
                        return this.RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        // Setting.    
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Info    
                //Console.Write(ex);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            // If we got this far, something failed, redisplay form    
            return this.View(model);
        }
        #endregion

        #region Log Out method.    
        /// <summary>  
        /// POST: /Account/LogOff    
        /// </summary>  
        /// <returns>Return log off action</returns>  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
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
        #endregion
        #region Helpers    
        #region Sign In method.    
        /// <summary>  
        /// Sign In User method.    
        /// </summary>  
        /// <param name="username">Username parameter.</param>  
        /// <param name="isPersistent">Is persistent parameter.</param>  
        private void SignInUser(string username, string strms, bool isPersistent)
        {
            // Initialization.    
            var claims = new List<Claim>();
            try
            {
                // Setting    
                claims.Add(new Claim(ClaimTypes.Name, username));
                claims.Add(new Claim(ClaimTypes.Role, strms));
                //claims.Add(new Claim(ClaimTypes.Email, ));
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
        #endregion
        #region Redirect to local method.    
        /// <summary>  
        /// Redirect to local method.    
        /// </summary>  
        /// <param name="returnUrl">Return URL parameter.</param>  
        /// <returns>Return redirection action</returns>  
        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.    
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.    
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Index", "OpenCalls");
        }
        #endregion
        #endregion
    }
}
