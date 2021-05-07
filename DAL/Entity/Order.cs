using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entity
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int Duration { get; set; }
        public int StatusId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Status Status { get; set; }
    }
}
