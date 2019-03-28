using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.ViewModels
{
    public class ImagesViewModelcs
    {
        [Display(Name = "מספר קריאה")]
        [Column("DOC_NBR")]
        public long doc_nbr { get; set; }

        //[Display(Name = "קישור לקובץ")]
        //[Column("FILE_NAME")]
        //public string file_name { get; set;}

        public int seq { get; set; }

        public string MIME_TYPE { get; set; }

        public string FILE_EXTENSION { get; set; }

        public byte[] BIN_FILE_DATA { get; set; }

       // public string pic = string.Empty;

    }
}