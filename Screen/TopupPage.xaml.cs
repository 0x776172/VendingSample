using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VendingDisplay.Screen
{
    /// <summary>
    /// Interaction logic for TopupPage.xaml
    /// </summary>
    public partial class TopupPage : Page
    {
        int u1 = 1000;
        int u2 = 2000;
        int u5 = 5000;
        int u10 = 10000;
        public TopupPage()
        {
            InitializeComponent();
            topContainer.MouseLeftButtonUp += TopContainer_MouseLeftButtonUp;
            _topupPage.Loaded += _topupPage_Loaded;
        }

        private void _topupPage_Loaded(object sender, RoutedEventArgs e)
        {
            u1kPaymentBtn.Content = u1.ToString("C");
            u2kPaymentBtn.Content = u2.ToString("C");
            u5kPaymentBtn.Content = u5.ToString("C");
            u10kPaymentBtn.Content = u10.ToString("C");
        }

        private void TopContainer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            paidContainer.Visibility = Visibility.Visible;
            infoTapText.Text = "Informasi Saldo dan Kartu";
        }
    }
}
