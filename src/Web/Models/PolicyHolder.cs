using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PolicyHolder
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public bool Active { get; set; } = false;
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? EndDate { get; set; }
        public string Username { get; set; }
        public string PolicyNumber { get; set; }
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string FilePath { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal PolicyAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Deductible { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal OutOfPocketMax { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime EffectiveDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime ExpirationDate { get; set; }
    }
}