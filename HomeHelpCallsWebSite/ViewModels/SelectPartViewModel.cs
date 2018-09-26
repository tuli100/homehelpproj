using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Models
{
    public class SelectPartViewModel
    {
        [Display(Name = "קוד פריט")]
        public string PART_CODE { get; set; }

        [Display(Name = "פריט")]
        public string PART_CODE_NAME { get; set; }

    }
}