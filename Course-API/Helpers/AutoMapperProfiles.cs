using AutoMapper;
using Course_API.Models;
using Course_API.ViewModels.CategoryViewModels;
using Course_API.ViewModels.CourseViewModels;
using Course_API.ViewModels.StudentViewModels;
using Course_API.ViewModels.TeacherViewModels;

namespace Course_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Course, CourseViewModel>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<Course, CourseOverviewViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.Name));

            CreateMap<PostCourseViewModel, Course>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());


            CreateMap<AppUser, StudentViewModel>()
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src =>
                        string.Concat(src.FirstName, " ", src.LastName)))
                .ForMember(dest => dest.Address, opt =>
                    opt.MapFrom(src =>
                        string.Concat(src.StreetAddress, ", ", src.ZipCode, ", ", src.City)));

            CreateMap<PostStudentViewModel, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));


            CreateMap<AppUser, TeacherViewModel>()
                .ForMember(dest => dest.AreasOfExpertise, opt =>
                    opt.Ignore())
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src =>
                        string.Concat(src.FirstName, " ", src.LastName)))
                .ForMember(dest => dest.Address, opt =>
                    opt.MapFrom(src =>
                        string.Concat(src.StreetAddress, ", ", src.ZipCode, ", ", src.City)));

            CreateMap<PostTeacherViewModel, AppUser>().ForMember(dest =>
                dest.AreasOfExpertise, opt =>
                    opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));


            CreateMap<Category, CategoryViewModel>();
        }
    }
}