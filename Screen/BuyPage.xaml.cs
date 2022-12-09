using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VendingDisplay.Resource;
using VendingDisplay.Screen.PaymentScreen;

namespace VendingDisplay.Screen
{
    /// <summary>
    /// Interaction logic for BuyPage.xaml
    /// </summary>
    public partial class BuyPage : Page
    {
        GenerateElement generate = new GenerateElement();
        PaymentMethod pm = new PaymentMethod();
        List<Button> pmList;
        public BuyPage()
        {

            InitializeComponent();
            pmList = generate.GenerateButton("payment_method");
            _buyPage.Loaded += _buyPage_Loaded;
            _buyPage.Unloaded += _buyPage_Unloaded;
            payBtn.Click += PayBtn_Click;
            ascBtn.Click += AscBtn_Click;
            dscBtn.Click += DscBtn_Click;
            cancelPayBtn.Click += CancelPayBtn_Click;
            tujuanCB.SelectionChanged += TujuanCB_SelectionChanged;
        }

        private void TujuanCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbBorder.BorderBrush = Brushes.Transparent;
        }

        private void CancelPayBtn_Click(object sender, RoutedEventArgs e)
        {
            tiketContainer.IsEnabled = true;
            paymentContainer.Visibility = Visibility.Collapsed;
        }

        private void DscBtn_Click(object sender, RoutedEventArgs e)
        {
            int valTicket = int.Parse(valTB.Text);
            if (valTicket > 1)
            {
                valTicket--;
                valTB.Text = valTicket.ToString();
            }
            else return;
        }

        private void AscBtn_Click(object sender, RoutedEventArgs e)
        {

            int valTicket = int.Parse(valTB.Text);
            valTicket++;
            valTB.Text = valTicket.ToString();
        }
        int price;
        private void PayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tujuanCB.SelectedIndex == 0)
            {
                cbBorder.BorderBrush = Brushes.Red;
                return;
            }
            tiketContainer.IsEnabled = false;
            paymentContainer.Visibility = Visibility.Visible;
            int valTicket = int.Parse(valTB.Text);
            price = valTicket * 3000;
        }

        private void _buyPage_Unloaded(object sender, RoutedEventArgs e)
        {
            pmSP.Children.Clear();
        }

        private void _buyPage_Loaded(object sender, RoutedEventArgs e)
        {
            string[] stasiun = { "Pilih Stasiun Tujuan", "Manggarai", "Jatinegara", "Durenkalibata", "Pasar Senen" };
            foreach (string s in stasiun)
            {
                tujuanCB.Items.Add(s);
            }
            foreach (Button i in pmList)
            {
                i.Style = FindResource("ButtonStyle") as Style;
                pmSP.Children.Add(i);
                i.Click += NewBtn_Click;
            }
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            Button cBtn = (Button)sender;
            MainWindow mWindow = (MainWindow)Application.Current.MainWindow;
            _ = cBtn.Name.ToString().Contains("cash")
                ? mWindow._mainFrame.NavigationService.Navigate(
                    new CashPage(
                    tujuanCB.Text,
                    valTB.Text,
                    price,
                    0))
                : mWindow._mainFrame.NavigationService.Navigate(new OnlinePage(cBtn.Content.ToString(), price));
        }
    }
}

