using AutoMapper;
using HomeHelpCallsWebSite.Infrastructure.Data;
using HomeHelpCallsWebSite.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HomeHelpCallsWebSite.Controllers.Api
{
    public class LinesController : ApiController
    {
        private ApplicationDbContext _conntext;

        public LinesController()
        {
            _conntext = new ApplicationDbContext();
        }

        // GET api/lines/1
        public IEnumerable<LineModel> GetLines(int docNbr)
        {
            return _conntext.VUMM_HH_CALLS_LINES.Select(Mapper.Map<VUMM_HH_CALLS_LINES, LineModel>).Where(m => m.doc_nbr == docNbr);
        }

        // POST api/lines/1
        //[HttpPost]
        //public LineModel CreateLine(LineModel newLine)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }
        //    var line = Mapper.Map<LineModel, VUMM_HH_CALLS_LINES>(newLine);
        //    _conntext.

        //}
        
    }
}
