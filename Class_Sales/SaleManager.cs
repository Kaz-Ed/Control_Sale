using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Sales
{
    public class SaleManager
    {
        public List<Sale> Sales { get; private set; }

        public SaleManager()
        {
            Sales = new List<Sale>();
            LoadSales();
        }

        public void AddSale(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }
            Sales.Add(sale);
            SaveSales();
        }

        public void RemoveSale(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }
            Sales.Remove(sale);
            SaveSales();
        }

        public decimal TotalRevenue
        {
            get { return Sales.Sum(s => s.TotalRevenue); }
        }

        public int Totalcountsale
        {
            get { return Sales.Count; }
        }

        private void SaveSales()
        {
            File.WriteAllLines("sales.txt", Sales.Select(s => $"{s.ProductName}|{s.Price}|{s.Quantity}|{s.Date.ToString("yyyy-MM-dd HH:mm:ss")}"));
        }

        private void LoadSales()
        {
            if (File.Exists("sales.txt"))
            {
                var lines = File.ReadAllLines("sales.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 4)
                    {
                        decimal price;
                        int quantity;
                        DateTime date;
                        if (decimal.TryParse(parts[1], out price) && int.TryParse(parts[2], out quantity) && DateTime.TryParse(parts[3], out date))
                        {
                            Sales.Add(new Sale(parts[0], price, quantity, date));
                        }
                    }
                }
            }
        }
    }
}
