namespace Core.System.Data.Model
{
    public class CustomerOrder
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string OrderDate { get; set; }
        public StatusType Status {  get; set; }

        private double _totalPrice;
        public string TotalPrice { get {return _totalPrice.ToString();} set { if (double.TryParse(value, out double result)) { _totalPrice = result; } } }
    }
    public enum StatusType { Pending, Processing, Cancelled, Completed }
}
