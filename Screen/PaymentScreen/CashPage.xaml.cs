using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VendingDisplay.Resource;

namespace VendingDisplay.Screen.PaymentScreen
{
    /// <summary>
    /// Interaction logic for CashPage.xaml
    /// </summary>
    public partial class CashPage : Page
    {
        private readonly string tujuan;
        private readonly string jumlah;
        private readonly int price;
        private readonly int tType;
        private readonly GenerateElement generate = new GenerateElement();
        private readonly MainWindow mWindow;
        public CashPage(string tujuan, string jumlah, int price, int type)
        {
            InitializeComponent();
            this.price = price;
            this.tujuan = tujuan;
            this.jumlah = jumlah;
            tType = type;
            _cashPage.Loaded += _cashPage_Loaded;
            _cashPage.Unloaded += _cashPage_Unloaded;
            paidBtn.Click += PaidBtn_Click;
            mWindow = (MainWindow)Application.Current.MainWindow;
        }

        private void _cashPage_Unloaded(object sender, RoutedEventArgs e)
        {
            nominalContainer.Children.Clear();
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            Button cBtn = (Button)sender;
            paid += (int)cBtn.Tag;
            updateTB();
        }
        private void PaidBtn_Click(object sender, RoutedEventArgs e)
        {
            writeLog(tujuan, jumlah, price, paid, totalPaid);
            mWindow._mainFrame.NavigationService.Navigate(new ConfirmPage(tujuan, jumlah, price, totalPaid));
        }

        private int paid = 0;
        private int totalPaid = 0;

        private void _cashPage_Loaded(object sender, RoutedEventArgs e)
        {
            var nBtnList = generate.GenerateButton("nominal_payment", tType);
            foreach (var newBtn in nBtnList)
            {
                newBtn.Style = FindResource("ButtonStyle") as Style;
                nominalContainer.Children.Add(newBtn);
                newBtn.Click += NewBtn_Click;
            }
            updateTB();
        }

        private void updateTB()
        {
            totalPaid = paid - price;
            tagihanTB.Text = price.ToString("C");
            paidTB.Text = paid.ToString("C");
            totalTB.Text = totalPaid.ToString("C");
            if (totalPaid < 0)
            {
                totalTB.Foreground = Brushes.Red;
                statusTB.Foreground = Brushes.Red;
                statusTB.Text = "Uang Kurang";
            }
            else
            {
                totalTB.Foreground = Brushes.Green;
                statusTB.Foreground = Brushes.Green;
                statusTB.Text = "Uang Cukup";
                paidBtn.IsEnabled = true;
            }
        }

        private void writeLog(string tujuan, string jumlah, int price, int paid, int totalPaid)
        {
            DateTime date = DateTime.Now;
            string path = $@"D:\K\WPFApps\VendingDisplay\log_file\{date:dd-MM-yyyy}_log.txt";
            try
            {
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine($"Jam {date:HH:mm:ss}");
                    w.WriteLine("========================");
                    w.WriteLine($"Tujuan: {tujuan}");
                    w.WriteLine($"Jumlah: {jumlah} tiket");
                    w.WriteLine($"Harga: {price:C}");
                    w.WriteLine("Metode Pembayaran: Cash");
                    w.WriteLine($"Bayar: {paid:C}");
                    w.WriteLine($"Kembalian: {totalPaid:C}");
                    w.WriteLine("Status: Berhasil");
                    w.WriteLine("========================");
                    w.WriteLine();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
