using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Newtonsoft.Json;
using HomeHelpCallsWebSite.ViewModels;

namespace HomeHelpCallsWebSite.Models
{
    public class LineViewModel
    {
        public long LINE_ID;

        [Display(Name = "מספר קריאה")]
        [Column("DOC_NBR")]
        public long doc_nbr { get; set; }

        [Column("LINE_NBR")]
        [Display(Name = "מספר שורה")]
        public int line_nbr { get; set; }

        [Column("TXT_DSCR")]
        [Display(Name = "הערה")]
        [MaxLength(40, ErrorMessage = "ההערה אינה יכולה להיות ארוכה יותר מ-40 תווים.")]
        public string txt_dscr { get; set; }

        [Column("QNTY")]
        [Display(Name = "כמות")]
        [Required(ErrorMessage = "חסרה כמות")]
        public decimal qnty { get; set; }

        [Column("PARENT_STRM_CODE")]
        public string parent_strm_code { get; set; }

        [Column("PART_CODE")]
        [Display(Name = "קוד פריט")]
        public string part_code { get; set; }

        [Column("PART_CODE_NAME")]
        [Display(Name =  "פריט")]
        public string part_code_name { get; set; }

        [Column("PART_NAME")]
        [Display (Name = "פריט")]
        public string part_name { get; set; }

        [Column("UNIT_NAME")]
        [Display(Name = "יחידות")]
        public string unit_name {get; set;}

        public SelectList WParts { get; set; }

        public CallsViewModel call { get; set; }

        [Display(Name = "הערה")]
        [MaxLength(60, ErrorMessage = "ההערה אינה יכולה להיות ארוכה יותר מ-60 תווים.")]
        public string stat_rmrk { get; set; }

        [Display(Name = "פרטי")]
        public bool private_bill { get; set; }

        [Column("RO")]
        public  bool ro { get; set; }

        [Column("ORD")]
        public int ord { get; set; }

        //public SelectList statusList { get; set { createStatusList(status)} }
        //public int status { get; set; }

        public long Id
        {
            get { return this.LINE_ID; }
        }
    }
}