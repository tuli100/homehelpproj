using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models
{
    public class DestinationModel
    {
        public long code { get; set; }
        public string name { get; set; }
        public string cell_phone { get; set; }
        public string email { get; set; }
        public string apt { get; set; }
        public string sub_area { get; set; }
        public long Id
        {
            get { return this.code; }
        }
    }
}