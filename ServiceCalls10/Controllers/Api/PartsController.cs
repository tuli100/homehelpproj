using AutoMapper;
using HomeHelpCallsWebSite.Infrastructure.Data;
using HomeHelpCallsWebSite.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace HomeHelpCallsWebSite.Controllers.Api
{
    public class PartsController : ApiController
    {
        private ApplicationDbContext _conntext;
        private IMapper _partsMapper;

        public PartsController()
        {
            _conntext = new ApplicationDbContext();
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_PARTS, PartModel>());
            _partsMapper = config2.CreateMapper();
        }

        //GET Api/parts/1
        //public IEnumerable<PartModel> GetParts()
        // public IEnumerable<SelectListItem> GetParts()
        //{
        //    //var p = _conntext.VUMM_HH_PARTS.ToList();
        //    //return _partsMapper.Map<IEnumerable<PartModel>>(p);

        //    //var res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == parent_strm_code);
        //    //IEnumerable<PartModel> partsl = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, List<PartModel>>(res);
        //    //var partssl = partsl.Select(x => new SelectListItem { Value = x.PART_CODE, Text = x.part_code_name });
        //    //return new SelectList(partssl, "Value", "Text");
        //}

        //GET Api/parts/partId
        //[Route("api/Parts/{id")]
        public PartModel GetPart(string id)
        {
            var part = _conntext.VUMM_HH_PARTS.SingleOrDefault(m => m.PART_CODE == id);
            if (part == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<PartModel>(part);
        }
    }
}
