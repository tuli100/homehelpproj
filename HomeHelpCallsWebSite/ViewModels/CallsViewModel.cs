using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using HomeHelpCallsWebSite.Infrastructure.Data.Models;

namespace HomeHelpCallsWebSite.Models
{
    public class CallsViewModel
    {

        [Display(Name = "מספר קריאה")]
        [Column ("DOC_NBR") ]
        public long doc_nbr { get; set; }
        [Display(Name = "מספר שורה")]
        [Column("LINE_NBR")]
        public int line_nbr { get; set; }
        [Display(Name = "תאריך פתיחת הקריאה")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> LINE_EVNT_DATE { get; set; }
        [Display(Name = "תאריך יעד")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> RQSTD_SHIP_DATE { get; set; }
        [Display(Name = "זמן יעד")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        public string RQSTD_SHIP_TIME { get; set; }
        public string STRM_CODE { get; set; }
        [Display(Name = "קטגוריה")]
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
        [Display(Name = "סטטוס")]
        public int CALL_STAT_CODE { get; set; }
        public string CALL_STAT { get; set; }
        [Display(Name = "סטטוס")]
        public string CALL_STAT_FULL { get; set; }
       
        public Nullable<bool> HAS_IMAGES { get; set; }

        public SelectList StrmList { get; set; }

        [Display(Name = "הערה")]
        [MaxLength(60, ErrorMessage = "ההערה אינה יכולה להיות ארוכה יותר מ-60 תווים.")]
        public string stat_rmrk { get; set; }

        public SelectList StatusList { get; set; }
        //public int status { get; set; }

        public bool isOpen { get; set; }

        //public virtual ICollection<LineModel> CallLines { get; set; }

        public long Id
        {
            get { return this.doc_nbr; }
        }
    }
    }



