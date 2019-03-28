using HomeHelpCallsWebSite.App_Start;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Script.Serialization;
using HomeHelpCallsWebSite.Infrastructure.Data.Models;
using System.Web.Helpers;
using System.Security.Claims;

namespace HomeHelpCallsWebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Fixing claim issue. https://stack247.wordpress.com/2013/02/22/antiforgerytoken-a-claim-of-type-nameidentifier-or-identityprovider-was-not-present-on-provided-claimsidentity/   
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
        }

          }
}
