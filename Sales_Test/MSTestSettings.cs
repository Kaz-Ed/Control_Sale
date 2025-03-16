using System.Windows.Forms;
using Class_Sales;

namespace Class_Sales_Test;

[TestClass]

public class Class_SalesTest
{
    [TestMethod]
    public void Test_Save_Sale()
    {
        Sale sale = new Sale("sdgdg", 21.00m, 100, DateTime.Today);
        SaleManager saleManager = new SaleManager();
        saleManager.AddSale(sale);
        Assert.IsTrue(File.Exists("sales.txt"));
        var lines = File.ReadAllLines("sales.txt");
        Assert.AreEqual(2, lines.Length);
    }

    [TestMethod]
    public void Test_Load_Sale()
    {
        SaleManager saleManager = new SaleManager();
        Assert.AreEqual("sdgdg", saleManager.Sales[0].ProductName);
        Assert.AreEqual(21.00m, saleManager.Sales[0].Price);
        Assert.AreEqual(100, saleManager.Sales[0].Quantity);
        Assert.AreEqual(DateTime.Today, saleManager.Sales[0].Date);
    }

    [TestMethod]
    public void Test_Remove_Sale()
    {
        Sale sale = new Sale("sdgdg", 210.00m, 100, DateTime.Today);
        Sale sale1 = new Sale("sdgdg", 21.00m, 100, DateTime.Today);
        SaleManager saleManager = new SaleManager();
        saleManager.AddSale(sale);
        saleManager.AddSale(sale1);
        saleManager.RemoveSale(sale);
        var lines = File.ReadAllLines("sales.txt");
        Assert.AreEqual(1, lines.Length);
        saleManager.RemoveSale(sale1);
        Assert.AreEqual(0, saleManager.Sales.Count);

    }

    [TestMethod]
    public void Test_TotalRevenue_Sale()
    {
        Sale sale = new Sale("sdgdg", 1.00m, 100, DateTime.Today);
        SaleManager saleManager = new SaleManager();
        saleManager.AddSale(sale);
        Assert.AreEqual(100m, (saleManager.Sales[0].Price * saleManager.Sales[0].Quantity));
    }
}

