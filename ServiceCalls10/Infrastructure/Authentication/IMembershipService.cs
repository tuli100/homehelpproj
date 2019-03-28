using System.Web.Security;

namespace HomeHelpCallsWebSite.Infrastructure
{
    interface IMembershipService
    {

        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool DeleteUser(string userName);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        bool ResetPassword(string userName, string newPassword);
        void AddUserToRoles(string username, params string[] roleNames);
        void AddUsersToRoles(string[] usernames, string[] roleNames);
        void RemoveUserFromRoles(string username, params string[] roleNames);
        void RemoveUsersFromRoles(string[] usernames, string[] roleNames);
    }
}
