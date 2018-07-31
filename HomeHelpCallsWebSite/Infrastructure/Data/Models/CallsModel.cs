using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models
{
    public class CallsModel
    {

        public long DOC_NBR { get; set; }
        public int LINE_NBR { get; set; }
        public Nullable<System.DateTime> LINE_EVNT_DATE { get; set; }
        public Nullable<System.DateTime> RQSTD_SHIP_DATE { get; set; }
        public string STRM_CODE { get; set; }
        public string STRM_NAME { get; set; }
        public string PARENT_STRM_CODE { get; set; }
        public string CALL_DSCR { get; set; }
        public string APT_NAME { get; set; }
        public string DSTN_NAME { get; set; }
        public string CELL_PHONE { get; set; }
        public string EMAIL { get; set; }
        public string CALL_STAT { get; set; }
        public string CALL_STAT_FULL { get; set; }
        public Nullable<decimal> HAS_IMAGES { get; set; }
        public virtual ICollection<LineModel> CallLines { get; set; }

        public long Id
        {
            get { return this.DOC_NBR; }
        }
    }
}