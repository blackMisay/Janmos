namespace Core.System.Data.Model
{
    public class CustomerOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public Customer Customer { get; set; }
        public string OrderDate { get; set; }
        public CustomerOrderStatus OrderStatus { get; set; }
        //public StatusType OrderStatus {  get; set; }

        private decimal _totalPrice;
        public string TotalPrice { 
            get {
                return _totalPrice.ToString();
            } 
            set { 
                if (decimal.TryParse(value, out decimal result)) { 
                    _totalPrice = result; 
                } 
            } 
        }

        public StatusRecord.Type Status { get; set; }
    }
    /*public enum StatusType { 
        Pending = 1, 
        Processing = 2, 
        Completed = 3, 
        Cancelled = 4 }*/
}
