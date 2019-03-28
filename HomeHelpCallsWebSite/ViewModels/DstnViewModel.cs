using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using AutoMapper.Configuration.Conventions;
using HomeHelpCallsWebSite.Infrastructure.Data.Models;

namespace HomeHelpCallsWebSite.ViewModels
{
    public class DstnViewModel
    {
        [Column("DSTN_KEY")]
        [MapTo("DSTN_KEY")]
        public string DSTN_KEY { get; set; }
        [Column("DIRA")]
        [MapTo("DIRA")]
        public string DIRA { get; set; }
        [Column("DSTN_CODE")]
        [MapTo("DSTN_CODE")]
        public int DSTN_CODE { get; set; }
        [Column("DSTN_NAME")]
        [MapTo("DSTN_NAME")]
        public string DSTN_NAME { get; set; }
        [Column("CELL")]
        [MapTo("CELL")]
        public string CELL { get; set; }
        [Column("EMAIL")]
        [MapTo("EMAIL")]
        public string EMAIL { get; set; }
        [Column("ISPREF")]
        [MapTo("ISPREF")]
        public Nullable<decimal> ISPREF { get; set; }
        [Column("SALE_SUB_AREA")]
        [MapTo("SALE_SUB_AREA")]
        public string SALE_SUB_AREA { get; set; }

    }
}
