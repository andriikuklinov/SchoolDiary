using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.BLL.Converters
{
    internal class DateOnlyToDateTimeConverter : IValueConverter<DateOnly, DateTime>
    {
        public DateTime Convert(DateOnly sourceMember, ResolutionContext context)
        {
            return sourceMember.ToDateTime(TimeOnly.Parse("00:00 AM"));
        }
    }
}
