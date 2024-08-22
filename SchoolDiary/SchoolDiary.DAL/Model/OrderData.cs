using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.DAL.Model
{
    internal class OrderData
    {
        public IEnumerable<OrderModel> Data { get; set; }
    }

    internal class OrderModel
    {
        public string PropertyName { get; set; }
        public string Direction { get; set; }
    }
}
