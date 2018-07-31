using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using HomeHelpCallsWebSite.Infrastructure.Data.Models;

namespace HomeHelpCallsWebSite.Models
{
    public class LineViewModel
    {
        [Display(Name = "מספר קריאה")]
        public long DOC_NBR { get; set; }
        [Display(Name = "מספר שורה")]
        public int LINE_NBR { get; set; }
        [Display(Name = "קוד פריט")]
        [Required(ErrorMessage = "בחר פריט")]
        public string PART_CODE { get; set; }
        [Display(Name = "שם פריט")]
        public string PART_NAME { get; set; }
        [Display(Name = "כמות")]
        [Required(ErrorMessage = "חסרה כמות")]
        public decimal QNTY { get; set; }
        [Display(Name = "יחידות")]
        public decimal UNIT_NAME { get; set; }
        [Display(Name = "הערה")]
        public string TXT_DSCR { get; set; }
        [Display (Name = "פריט")]
        public String PART_CODE_NAME { get; set; }
    
        //public IEnumerable<SelectListItem> PartsCodes { get; set; }
        public IEnumerable<SelectListItem> Parts { get; set; }

        public long ID { get; set; }

        public long Id
        {
            get { return this.DOC_NBR; }
        }
    }
}