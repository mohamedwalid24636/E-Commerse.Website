using AutoMapper;
using DomanLayer.Models;
using Shared.MachineReportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class MachineInfoProfile : Profile
    {
        public MachineInfoProfile()
        {
            CreateMap<MachineReportDto, MachineReport>().ReverseMap();
            CreateMap<SoftwareDto, Software>().ReverseMap();
            CreateMap<ErrorDto, Error>().ReverseMap();
        }
    }
}
