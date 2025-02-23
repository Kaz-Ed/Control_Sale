namespace Class_Sales
{
    public class Sale
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public Sale(string productName, decimal price, int quantity, DateTime date)
        {
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            Date = date;
        }

        public decimal TotalRevenue
        {
            get { return Price * Quantity; }
        }
    }


}
