﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHelpCallsWebSite.Models
{
    public class PartViewModel
    {
        public long doc_nbr { get; set; }

        [Display(Name = "קוד פריט")]
        [Column("PART_CODE")]
        public string PART_CODE { get; set; }

        [Display(Name = "שם פריט ארוך")]
        [Column("PART_LONG_NAME")]
        public string PART_LONG_NAME { get; set; }

        [Display(Name = "קוד יחידה")]
        [Column("UNIT_CODE")]
        public Nullable<int> UNIT_CODE { get; set; }

        [Display(Name = "יחידות")]
        [Column("UNIT_NAME")]
        public string UNIT_NAME { get; set; }

        [Display(Name = "קוד מחסן")]
        [Column("PRMY_STRM_CODE")]
        public string PRMY_STRM_CODE { get; set; }

        //[Display(Name = "פריט מזדמן")]
        //public string TMP_PART_NAME_Y_N { get; set; }
        [Column("DFLT_PART")]
        public int dflt_part { get; set; }
        //[Display(Name = "קבוצת פריט")]
        //public string PART_GRP_CODE { get; set; }

        [Display(Name = "פריט")]
        [Column("PART_CODE_NAME")]
        public string PART_CODE_NAME { get; set; }


        //public PartViewModel()
        //{

        //}

        //public PartViewModel(string part_code, string part_code_name, string unit_name, int unit_code)
        //{
        //    this.PART_CODE = part_code;
        //    this.PART_CODE_NAME = part_code_name;
        //    this.UNIT_CODE = unit_code;
        //    this.UNIT_NAME = unit_name;
        //}
          
    }

    

}