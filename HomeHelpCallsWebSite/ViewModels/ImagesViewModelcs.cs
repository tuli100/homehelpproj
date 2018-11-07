using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.ViewModels
{
    public class ImagesViewModelcs
    {
        [Display(Name = "מספר קריאה")]
        [Column("DOC_NBR")]
        public long doc_nbr { get; set; }

        [Display(Name = "קישור לקובץ")]
        [Column("FILE_NAME")]
        public string file_name { get; set;}

        public int seq { get; set; }

    }
}