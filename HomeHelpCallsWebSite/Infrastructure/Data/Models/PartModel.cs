﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models
{
    public class PartModel
    {
        public string PART_CODE { get; set; }
        public string PART_LONG_NAME { get; set; }
        public string PART_SHRT_NAME { get; set; }
        public Nullable<int> UNIT_CODE { get; set; }
        public string UNIT_NAME { get; set; }
        public string PRMY_STRM_CODE { get; set; }
        public string TMP_PART_NAME_Y_N { get; set; }
        public string PART_GRP_CODE { get; set; }
        public string PART_NAME { get; set; }
        public string PART_CODE_NAME { get; set; }
        public string Id
        {
            get { return this.PART_CODE; }
        }
    }
}