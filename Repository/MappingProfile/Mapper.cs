using AutoMapper;
using Database.Entities;
using Domain.Models;
using Repository.BusinessModels;
using Service.ServiceModels;

namespace Repository.MappingProfile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Mapper profiles for db, business and service models.
            CreateMap<IUser,UserDb>();
            CreateMap<UserDb, UserBusiness>();
            CreateMap<User, UserBusiness>();
        }
    }
}
