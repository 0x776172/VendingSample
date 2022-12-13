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
        private readonly string tujuan, method;
        private readonly string jumlah;
        private readonly int price;
        private readonly int tType;
        private readonly GenerateElement generate = new GenerateElement();
        private readonly MainWindow mWindow;
        private readonly TransactionLog log = new TransactionLog();
        public CashPage(string method, string tujuan, string jumlah, int price, int type)
        {
            InitializeComponent();
            this.method = method;
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
            log.WriteLog(method, tujuan, jumlah, price, paid, totalPaid);
            mWindow._mainFrame.NavigationService.Navigate(new ConfirmPage(tujuan, jumlah, price, totalPaid));
        }

        private int paid = 0;
        private int totalPaid = 0;

        private void _cashPage_Loaded(object sender, RoutedEventArgs e)
        {
            var nBtnList = generate.GenerateButton("nominal_payment", tType);
            foreach (Button newBtn in nBtnList)
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
            tagihanTB.Text = $": {price:C}";
            paidTB.Text = $": {paid:C}";
            totalTB.Text = $": {totalPaid:C}";
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
    }
}
