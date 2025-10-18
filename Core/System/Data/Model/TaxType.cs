using System;

namespace Core.System.Data.Model
{
    public class TaxType
    {
        public int Id { get; set; }
        public string TaxName { get; set; }
        public decimal Rate {  get; set; }
        public User CreatedBy { get; set; }
        public String CreatedDate { get; set; }
    }
}
