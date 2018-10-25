using HomeHelpCallsWebSite.Infrastructure.Data;
//using HomeHelpCallsWebSite.Infrastructure.Data.Managers;
//using MaaganMichael.Core.DesignPatterns;
//using MaaganMichael.Core.Extensions;
//using MaaganMichael.IoC;
using System;
using System.Linq;
using System.Web.Security;
using System.Collections.Generic;

namespace HomeHelpCallsWebSite.Infrastructure.Authentication
{
    public class HHRoleProvider : RoleProvider
    {
        private string _applicationName;

        public override string ApplicationName { get { return _applicationName; } set { _applicationName = value; } }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return GetUsersEntityManager().Roles.Select(r => r.RoleName).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = this.GetUsersEntityManager().GetUserByName(username);
            if (user == null)
            {
                return new string[0];
            }
            var roles = user.Roles.Select(r => r.RoleName).ToArray();
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = this.GetUsersEntityManager().GetUserByName(username);
            if (user == null)
            {
                return false;
            }
            var isInRole = user.Roles.Any(r => roleName.Equals(r.RoleName, StringComparison.OrdinalIgnoreCase));
            return isInRole;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            usernames.ForEach(u => GetUsersEntityManager().RemoveUserRoles(u, roleNames));
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        private IUsersEntityManager GetUsersEntityManager()
        {
            return Singleton<IEngine>.Instance.Resolve<IUsersEntityManager>();
        }
    }
}