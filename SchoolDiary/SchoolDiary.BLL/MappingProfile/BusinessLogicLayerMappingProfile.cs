using AutoMapper;
using SchoolDiary.BLL.Converters;
using SchoolDiary.BLL.DTO;
using SchoolDiary.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.BLL.MappingProfile
{
    public class BusinessLogicLayerMappingProfile : Profile
    {
        public BusinessLogicLayerMappingProfile()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(destination => destination.Birthdate, opt => opt.ConvertUsing(new DateOnlyToDateTimeConverter()))
                .ForMember(destination => destination.EnrolnmentDate, opt => opt.ConvertUsing(new DateOnlyToDateTimeConverter()));
            CreateMap<StudentDTO, Student>()
                .ForMember(destination => destination.Birthdate, opt => opt.ConvertUsing(new DateTimeToDateOnlyConverter()))
                .ForMember(destination => destination.EnrolnmentDate, opt => opt.ConvertUsing(new DateTimeToNullableDateOnlyConverter()));
        }
    }
}
