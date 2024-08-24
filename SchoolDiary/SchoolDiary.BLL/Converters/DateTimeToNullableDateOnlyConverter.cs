using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.BLL.Converters
{
    internal class DateTimeToNullableDateOnlyConverter : IValueConverter<DateTime, DateOnly?>
    {
        public DateOnly? Convert(DateTime sourceMember, ResolutionContext context)
        {
            return DateOnly.FromDateTime(sourceMember);
        }
    }
}
