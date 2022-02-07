using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Policy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal DefaultDeductible { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal DefaultOutOfPocketMax { get; set; }
    }
}