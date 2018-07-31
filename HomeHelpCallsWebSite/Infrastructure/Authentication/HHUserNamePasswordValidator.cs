using System;
using System.IdentityModel.Selectors;
using System.Security.Principal;
using System.Threading;
using System.Web.Security;

namespace HomeHelpCallsWebSite.Infrastructure
{
    public class HHUserNamePasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }
            if (!Membership.Provider.ValidateUser(userName, password))
            {
                throw new UnauthorizedAccessException();
            }
            Thread.CurrentPrincipal = new RolePrincipal(new GenericIdentity(userName));
        }
    }
}