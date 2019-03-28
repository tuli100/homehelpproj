using HomeHelpCallsWebSite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using HomeHelpCallsWebSite.Infrastructure.Data.Models;

namespace HomeHelpCallsWebSite.ViewModels
{
    public class DetailsViewTable
    {
        public CallsViewModel calls { get; set; }
        public IEnumerable<DstnViewModel> dstnPrsn { get; set; }
    }
}