using Class_Sales;

namespace Control_Sale
{
    public partial class SaleForm : Form
    {
        private SaleManager saleManager;
        private TextBox productNameTextBox;
        private TextBox priceTextBox;
        private TextBox quantityTextBox;
        private DateTimePicker datePicker;
        private Button addSaleButton;
        private Button removeSaleButton;
        private Button generateReportButton;
        private ListBox salesListBox;
        private Label totalRevenueLabel;

        public SaleForm()
        {
            this.Text = "���������� ���������";
            this.Width = 600;
            this.Height = 500;

            productNameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 150,
                PlaceholderText = "�������� ��������"
            };

            priceTextBox = new TextBox
            {
                Location = new System.Drawing.Point(170, 10),
                Width = 100,
                PlaceholderText = "����"
            };

            quantityTextBox = new TextBox
            {
                Location = new System.Drawing.Point(280, 10),
                Width = 80,
                PlaceholderText = "����������"
            };

            datePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(370, 10)
            };

            addSaleButton = new Button
            {
                Location = new System.Drawing.Point(10, 40),
                Text = "��������",
                Width = 100
            };
            addSaleButton.Click += AddSaleButton_Click;

            removeSaleButton = new Button
            {
                Location = new System.Drawing.Point(120, 40),
                Text = "�������",
                Width = 100
            };
            removeSaleButton.Click += RemoveSaleButton_Click;

            generateReportButton = new Button
            {
                Location = new System.Drawing.Point(220, 40),
                Text = "������������ �����",
                Width = 120
            };
            generateReportButton.Click += GenerateReportButton_Click;

            salesListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 70),
                Width = 560,
                Height = 200
            };

            totalRevenueLabel = new Label
            {
                Location = new System.Drawing.Point(10, 280),
                Width = 200,
                Text = "����� �����: "
            };

            this.Controls.Add(productNameTextBox);
            this.Controls.Add(priceTextBox);
            this.Controls.Add(quantityTextBox);
            this.Controls.Add(datePicker);
            this.Controls.Add(addSaleButton);
            this.Controls.Add(removeSaleButton);
            this.Controls.Add(generateReportButton);
            this.Controls.Add(salesListBox);
            this.Controls.Add(totalRevenueLabel);

            saleManager = new SaleManager();
            UpdateSalesList();
            UpdateTotalRevenue();
        }

        private void UpdateSalesList()
        {
            salesListBox.Items.Clear();
            foreach (var sale in saleManager.Sales)
            {
                salesListBox.Items.Add($"{sale.ProductName} - {sale.Price} ���. x {sale.Quantity} = {sale.TotalRevenue} ���.");
            }
        }

        private void UpdateTotalRevenue()
        {
            totalRevenueLabel.Text = $"����� �����: {saleManager.TotalRevenue} ���.";
        }

        private void AddSaleButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(productNameTextBox.Text) ||
    string.IsNullOrEmpty(priceTextBox.Text) || string.IsNullOrEmpty(quantityTextBox.Text))
            {
                MessageBox.Show("��������� ��� ����!");
                return;
            }
            decimal price;
            int quantity;
            if (!decimal.TryParse(priceTextBox.Text, out price) || !int.TryParse(quantityTextBox.Text,
    out quantity))
            {
                MessageBox.Show("�������� ������ ���� ��� ����������!");
                return;
            }
            DateTime date = datePicker.Value;
            Sale newSale = new Sale(productNameTextBox.Text, price, quantity, date);
            try
            {
                saleManager.AddSale(newSale);
                productNameTextBox.Clear();
                priceTextBox.Clear();
                quantityTextBox.Clear();
                UpdateSalesList();
                UpdateTotalRevenue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveSaleButton_Click(object sender, EventArgs e)
        {
            if (salesListBox.SelectedIndex == -1)
            {
                MessageBox.Show("�������� ������� ��� ��������!");
                return;
            }
            string selectedItem = salesListBox.SelectedItem.ToString();
            string[] parts = selectedItem.Split(new[] { '-' }, StringSplitOptions.None);
            if (parts.Length >= 2)
            {
                string productName = parts[0].Trim();
                decimal price;
                if (decimal.TryParse(parts[1].Trim().Split(' ')[0], out price))
                {
                    var saleToRemove = saleManager.Sales.Find(s => s.ProductName ==
    productName && s.Price == price);
                    if (saleToRemove != null)
                    {
                        try
                        {
                            saleManager.RemoveSale(saleToRemove);
                            UpdateSalesList();
                            UpdateTotalRevenue();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void GenerateReportButton_Click(object sender, EventArgs e)
        {
            if (saleManager.Sales.Count == 0)
            {
                MessageBox.Show("��� ������ ��� ��������� ������!");
                return;
            }
            string report = "����� �� ��������:\n";
            foreach (var sale in saleManager.Sales)
            {
                report += $"�������: {sale.ProductName}\n����: {sale.Price} ���.\n����������: {sale.Quantity}\n����: {sale.Date.ToString("yyyy-MM-dd")}\n����� �����: {sale.TotalRevenue}���.\n\n";
            }
            report += $"�����: {saleManager.TotalRevenue} ���.";
            File.WriteAllText("sales_report.txt", report);
            MessageBox.Show("����� ����������� � ������� � ���� sales_report.txt!");
        }
    }
}
