using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models
{
    public class PartModel 
    {
        [Column("PART_CODE")]
        public string PART_CODE { get; set; }

        public string part_code_name { get; set; }

        [Column("UNIT_CODE")]
        public Nullable<int> unit_code { get; set; }

        [Column("UNIT_NAME")]
        public string unit_name { get; set; }

        [Column("TMP_PART_NAME_Y_N")]
        public bool temp_part_name_y_n { get; set; }
 
        public string part_grp_code { get; set; }

        public string prmy_strm_code { get; set; }

        public string Id
        {
            get { return this.PART_CODE; }
        }
    }
}