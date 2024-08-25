using AutoMapper;
using SchoolDiary.BLL.DTO;
using SchoolDiary.DAL.Entities;
using SchoolDiary.WEB.Models;

namespace SchoolDiary.WEB.MappingProfile
{
    public class WebLayerMappingProfile: Profile
    {
        public WebLayerMappingProfile()
        {
            CreateMap<StudentModel, StudentDTO>();
            CreateMap<StudentDTO, StudentModel>();
            CreateMap<AuthModel, AuthDTO>();
            CreateMap<ResetPasswordModel, ResetPasswordDTO>();
        }
    }
}
