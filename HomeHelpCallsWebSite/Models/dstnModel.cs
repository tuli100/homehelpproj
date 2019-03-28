using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Models
{
    public class dstnModel
    {
            [Column("DSTN_KEY")]
            public string DSTN_KEY { get; set; }
            [Column("DIRA")]
            public string DIRA { get; set; }
            [Column("DSTN_CODE")]
            public int DSTN_CODE { get; set; }
            [Column("DSTN_NAME")]
            public string DSTN_NAME { get; set; }
            [Column("CALL")]
            public string CALL { get; set; }
            [Column("EMAIL")]
            public string EMAIL { get; set; }
            [Column("ISPREF")]
            public Nullable<decimal> ISPREF { get; set; }
            public string Id
            {
                get { return this.DSTN_KEY; }
            }
    }
}