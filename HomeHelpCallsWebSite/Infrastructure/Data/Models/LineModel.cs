using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models
{
    public class LineModel
    {
        public long DOC_NBR { get; set; }
   
        public int LINE_NBR { get; set; }

        public string PART_CODE { get; set; }
      
        public decimal QNTY { get; set; }
       
        public string RMRK { get; set; }

        public long ID { get; set; }

        public virtual CallsModel Call { get; set; }

        public PartModel Part { get; set; }

        public long Id
        {
            get { return this.ID; }
        }
    }
}