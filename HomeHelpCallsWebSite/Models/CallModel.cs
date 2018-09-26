using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models
{
    public class CallModel
    {
        [Column("DOC_NBR")]
        public long doc_nbr { get; set; }

        [Column("LINE_EVNT_DATE")]
        public Nullable<System.DateTime> line_evnt_date { get; set; }

        [Column("RQSTD_SHIP_DATE")]
        public Nullable<System.DateTime> rqstd_ship_date { get; set; }

        [Column("STRM_CODE")]
        public string strm_code { get; set; }

        [Column("DST_CODE")]
        public long dst_code { get; set; }

        [Column("CALL_DSCR")]
        public string dscr { get; set; }

        [Column("APT_NAME")] 
        public string apt { get; set; }

        [Column("CALL_STAT_FULL")]
        public string stat { get; set; }

        [Column("HAS_IMAGES")]
        public Nullable<decimal> has_images { get; set; }

        //DataTablesExclude
        public virtual ICollection<LineModel> CallLines { get; set; }

        public StrmModel Strm { get; set; }

        public DestinationModel dst { get; set; }
        //DataTablesExclude


        public long Id
        {
            get { return this.doc_nbr; }
        }
    }
}