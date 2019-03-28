using HomeHelpCallsWebSite.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Managers
{
    interface IUsersEntityManager
    {
        IQueryable<UserModel> UsersDisplay { get; }
        IQueryable<UserModel> Users { get; }
       
        UserModel GetUserByCell(string userName, bool untracked = false);
        Task<UserModel> GetUserByCellAsync(string userName, bool untracked = false);

        //SmsUserModel GetSmsUser(string smsUser, bool untracked = false);
       // Task<SmsUser> GetSmsUserAsync(string smsUser, bool untracked = false);

    }
}