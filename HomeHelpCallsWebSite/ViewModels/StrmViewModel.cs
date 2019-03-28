using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.ViewModels
{
    public class StrmViewModel
    {
        [Column("STRM_CODE")]
        public string strm_code { get; set; }
        public string strm_name { get; set; }
        public string user_name { get; set; }
    }
}