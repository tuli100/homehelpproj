using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace HomeHelpCallsWebSite.Models
{
    public class UserModel :IPrincipal
    {
        public UserModel(string username, string password, string email, string mobile, string roles)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.mobile = mobile;
            Roles = roles.Split(',');
        }

        string username { get; set; }
        string password { get; set; }
        string email { get; set; }
        string mobile { get; set; }

        public string[] Roles { get; set; }

        IIdentity IPrincipal.Identity { get; }

        bool IPrincipal.IsInRole(string role)
        {
            return Roles.Contains(role);
        }
    }
}