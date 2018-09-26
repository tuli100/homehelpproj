using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using HomeHelpCallsWebSite.Infrastructure.Data.Models;
using Newtonsoft.Json;

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
        public string txt_dscr { get; set; }

        [Column("QNTY")]
        [Display(Name = "כמות")]
        [Required(ErrorMessage = "חסרה כמות")]
        public decimal qnty { get; set; }

        [Column("PARENT_STRM_CODE")]
        public string parent_strm_code { get; set; }

        [Column("PART_CODE")]
        public string part_code { get; set; }

        [Column("PART_CODE_NAME")]
        [Display(Name = "שם פריט")]
        public string part_code_name { get; set; }

        //[Display (Name = "פריט")]
        //[Required(ErrorMessage = "בחר פריט")]
        //public PartViewModel PartV { get; set; }
        [Column("UNIT_NAME")]
        [Display(Name = "יחידות")]
        public String unit_name {get; set;}

        public IEnumerable<SelectListItem> Parts { get; set; }

        public PartViewModel[] PartsJ { get; set; }

        //public string PartsJSON
        //{
        //    get { return JsonConvert.SerializeObject(Parts); }
        //    set { PartsJ = JsonConvert.DeserializeObject<PartViewModel[]>(value); }
        //}
        public long Id
        {
            get { return this.LINE_ID; }
        }
    }
}