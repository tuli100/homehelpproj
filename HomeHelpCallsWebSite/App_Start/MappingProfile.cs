using AutoMapper;
using HomeHelpCallsWebSite.Infrastructure.Data.Models;
using HomeHelpCallsWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<VUMM_HH_CALLS_LINES, LineModel>();
            CreateMap<VUMM_HH_PARTS, PartModel>();
            CreateMap<VUMM_HH_PARTS, PartViewModel>();
            CreateMap<LineModel, LineViewModel>();
            CreateMap<VUMM_HH_CALLS_LINES, LineViewModel>();
            //RecognizePrefixes("m_");

            //var LineMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_CALLS_LINES, LineModel>());
            //linesMapper = config.CreateMapper();
            //var PartMapperConfig2 = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_PARTS, PartModel>());
            //PartsMapper = config2.CreateMapper();
        }

       

    }
}