using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VendingDisplay.Resource;
using VendingDisplay.Screen.PaymentScreen;

namespace VendingDisplay.Screen
{
    /// <summary>
    /// Interaction logic for TopupPage.xaml
    /// </summary>
    public partial class TopupPage : Page
    {
        GenerateElement generate = new GenerateElement();
        List<Button> btnList = new List<Button>();
        List<Button> btnPMList = new List<Button>();
        public TopupPage()
        {
            InitializeComponent();
            _topupPage.Loaded += _topupPage_Loaded;
            _topupPage.Unloaded += _topupPage_Unloaded;
            btnList = generate.GenerateButton("nominal_payment", 0);
            btnPMList = generate.GenerateButton("payment_method");
            correctionBtn.Click += CorrectionBtn_Click;
            bayarBtn.Click += BayarBtn_Click;
        }

        private void _topupPage_Unloaded(object sender, RoutedEventArgs e)
        {
            value = 0;
            btnContainer.Children.Clear();
            btnPMContainer.Children.Clear();
            paidContainer.Visibility = Visibility.Collapsed;
            infoTapText.Text = "Silahkan Tap Kartu Anda";
            saldoTB.Text = $"Saldo Sekarang: ";
            valTopupTB.Text = $"Nilai Topup: {value:C}";
            valueContainer.Visibility = Visibility.Collapsed;
            pmContainer.Visibility = Visibility.Collapsed;
        }

        private void BayarBtn_Click(object sender, RoutedEventArgs e)
        {
            correctionBtn.Content = "Back";
            paidContainer.Visibility = Visibility.Collapsed;
            pmContainer.Visibility = Visibility.Visible;
        }

        private void CorrectionBtn_Click(object sender, RoutedEventArgs e)
        {
            value = 0;
            valTopupTB.Text = $"Nilai Topup: {value:C}";
            bayarBtn.IsEnabled = false;
        }

        private void _topupPage_Loaded(object sender, RoutedEventArgs e)
        {
            topContainer.MouseLeftButtonUp += TopContainer_MouseLeftButtonUp;
            bayarBtn.IsEnabled = false;
            foreach (Button btn in btnList)
            {
                btnContainer.Children.Add(btn);
                btn.Click += Btn_Click;
                btn.Style = FindResource("ButtonStyle") as Style;
                btn.Content = $"+ {int.Parse(btn.Tag.ToString()):C}";
            }
            foreach (Button pBtn in btnPMList)
            {
                btnPMContainer.Children.Add(pBtn);
                pBtn.Click += PBtn_Click;
                pBtn.Style = FindResource("ButtonStyle") as Style;
                pBtn.Content = $"{pBtn.Content}";
            }
        }
        private void PBtn_Click(object sender, RoutedEventArgs e)
        {
            Button cBtn = (Button)sender;
            MainWindow mWindow = (MainWindow)Application.Current.MainWindow;
            _ = cBtn.Name.ToString().Contains("cash")
                ? mWindow._mainFrame.NavigationService.Navigate(
                    new CashPage(
                        "Top Up",
                        cBtn.Content.ToString(),
                        "-",
                        "-",
                        value,
                        0))
                : mWindow._mainFrame.NavigationService.Navigate(
                    new OnlinePage(
                        "Top Up",
                        cBtn.Content.ToString(),
                        "-",
                        "-",
                        value));
        }

        int value = 0;
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            bayarBtn.IsEnabled = true;
            Button btn = (Button)sender;
            value += int.Parse(btn.Tag.ToString());
            valTopupTB.Text = $"Nilai Topup: {value:C}";
        }

        private void TopContainer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            topContainer.MouseLeftButtonUp -= TopContainer_MouseLeftButtonUp;
            paidContainer.Visibility = Visibility.Visible;
            infoTapText.Text = "Informasi Saldo Anda:";
            saldoTB.Text = $"Saldo Sekarang: {1000:C}";
            valTopupTB.Text = $"Nilai Topup: {value:C}";
            valueContainer.Visibility = Visibility.Visible;
        }
    }
}