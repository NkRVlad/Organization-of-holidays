using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.ModelsDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int Duration { get; set; }
         public int Price { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
