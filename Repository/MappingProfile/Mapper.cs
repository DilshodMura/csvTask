using AutoMapper;
using Database.Entities;
using Domain.Models;
using Repository.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MappingProfile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserBusiness, UserDb>().ReverseMap();
            CreateMap<IUser,UserBusiness>().ReverseMap();
        }
    }
}
