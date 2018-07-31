using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace HomeHelpCallsWebSite.Infrastructure.Data.Models
{
    public class UserModel //: IdentityUser<Guid>
    {
        public string USER_NAME { get; set; }
        public long USER_ID { get; set; }
        public decimal IS_ADMIN { get; set; }
        public string STRM_CODE { get; set; }
        public string SMSUSER { get; set; }
        public string STC_NBR { get; set; }
        public long Id
        {
            get { return this.USER_ID; }
        }
    }
}