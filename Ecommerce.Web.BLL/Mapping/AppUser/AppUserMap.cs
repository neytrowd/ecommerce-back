using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Web.Contract.Models.AppUser;

namespace Ecommerce.Web.BLL.Mapping.AppUser
{
    public class AppUserMap : Profile
    {
        public AppUserMap()
        {
            CreateMap<AppUserEntity, AppUserModel>();
        }
    }
}