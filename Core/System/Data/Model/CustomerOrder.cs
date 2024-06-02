namespace Core.System.Data.Model
{
    public class CustomerOrder
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string OrderDate { get; set; }
        public StatusType Status {  get; set; }
        public decimal TotalPrice { get; set; }
    }
    public enum StatusType { Pending, Processing, Cancelled, Completed }
}
