using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models
{
    public class LineModel
    {
        public long LINE_ID;

        [Column("LINE_NBR")]
        public int line_nbr { get; set; }

        [Column("QNTY")]
        public decimal qnty { get; set; }

        [StringLength(100, ErrorMessage = "הערה עד 100 תווים")]
        [Column("TXT_DSCR")]
        public string rmrk { get; set; }

        [Column("DOC_NBR")]
        public long doc_nbr { get; set; }

        [Column("PART_CODE")]
        public string part_code { get; set; }

        //DataTablesExclude
        //public virtual CallModel Call { get; set; }

        public virtual PartModel Part { get; set; }
        //DataTablesExclude

        public long Id
        {
            get { return this.line_nbr; }
        }
    }
}