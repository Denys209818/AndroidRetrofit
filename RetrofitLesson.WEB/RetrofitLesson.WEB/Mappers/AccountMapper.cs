using AutoMapper;
using RetrofitLesson.WEB.Data.Identity;
using RetrofitLesson.WEB.Models;

namespace RetrofitLesson.WEB.Mappers
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<RegisterModel, AppUser>()
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
                .ForMember(x => x.SecondName, y => y.MapFrom(z => z.SecondName))
                .ForMember(x => x.Phone, y => y.MapFrom(z => z.Phone))
                .ForMember(x => x.Photo, y => y.Ignore());    
        }
    }
}
