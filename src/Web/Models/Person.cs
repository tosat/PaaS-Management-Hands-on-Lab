using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string Postcode { get; set; }
    }
}
