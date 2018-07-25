using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace HomeHelpCallsWebSite.Models
{
    public class CallsViewModel
    {

        [Display(Name = "מספר קריאה")]
        public long DOC_NBR { get; set; }
        [Display(Name = "מספר שורה")]
        public int LINE_NBR { get; set; }
        [Display(Name = "תאריך פתיחת הקריאה")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> LINE_EVNT_DATE { get; set; }
        [Display(Name = "תאריך יעד")]
        [DataType(DataType.DateTime)]
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public Nullable<System.DateTime> RQSTD_SHIP_DATE { get; set; }
        public string STRM_CODE { get; set; }
        [Display(Name = "מחסן")]
        public string STRM_NAME { get; set; }
        public string PARENT_STRM_CODE { get; set; }
        [Display(Name = "תיאור הקריאה")]
        public string CALL_DSCR { get; set; }
        [Display(Name = "דירה")]
        public string APT_NAME { get; set; }
        public string DSTN_NAME { get; set; }
        [Display(Name = "טלפון")]
        public string CELL_PHONE { get; set; }
        [Display(Name = "אימייל")]
        public string EMAIL { get; set; }
        public string CALL_STAT { get; set; }
        [Display(Name = "סטטוס")]
        public string CALL_STAT_FULL { get; set; }
        public Nullable<decimal> HAS_IMAGES { get; set; }

        public long Id
        {
            get { return this.DOC_NBR; }
        }
    }
    }



