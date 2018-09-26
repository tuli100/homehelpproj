using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Models
{
    public class PartCreateViewModel
    {
        [Display(Name = "קוד פריט"), Editable(false)]
        public string PART_CODE { get; set; }

        [Display(Name = "קוד יחידה"), Editable(false)]
        public Nullable<int> UNIT_CODE { get; set; }

        [Display(Name = "יחידות"), Editable(false)]
        public string UNIT_NAME { get; set; }

        [Display(Name = "קוד מחסן"), Editable(false)]
        public string PRMY_STRM_CODE { get; set; }

        [Display(Name = "פריט מזדמן"), Editable(false)]
        public string TMP_PART_NAME_Y_N { get; set; }

        [Display(Name = "פריט")]
        public string PART_CODE_NAME { get; set; }
    }
}