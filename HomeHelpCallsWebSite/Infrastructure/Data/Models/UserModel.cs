using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace HomeHelpCallsWebSite.Infrastructure.Data.Models
{
    public class UserModel //: IdentityUser<Guid>
    {
        public string name { get; set; }
        public long id { get; set; }
        public bool is_admin { get; set; }
        public StrmModel strm { get; set; }
        public string smsUser { get; set; }
        public string stc { get; set; }
        public long Id
        {
            get { return this.id; }
        }
    }
}