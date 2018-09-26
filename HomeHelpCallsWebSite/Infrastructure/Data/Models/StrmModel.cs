using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models
{
    public class StrmModel
    {
        public string code { get; set; }
        public string name { get; set; }
        public string parent_code { get; set; }
        public string parent_name { get; set; }

        public string Id
        {
            get { return this.code; }
        }
    }
}