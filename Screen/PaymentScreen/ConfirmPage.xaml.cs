using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace VendingDisplay.Screen.PaymentScreen
{
    /// <summary>
    /// Interaction logic for ConfirmPage.xaml
    /// </summary>
    public partial class ConfirmPage : Page
    {
        private BitmapImage bmp;
        private MainWindow mWindow;
        private string tujuan, jumlah;
        private int totalTransaksi, price, countDown = 5;
        private DispatcherTimer timer;
        public ConfirmPage(string tujuan, string jumlah, int price, int totalTransaksi)
        {
            this.tujuan = tujuan;
            this.jumlah = jumlah;
            this.price = price;
            this.totalTransaksi = totalTransaksi;
            InitializeComponent();
            _confirmPage.Loaded += _confirmPage_Loaded;
            _confirmPage.Unloaded += _confirmPage_Unloaded;
            bmp = new BitmapImage(
                new Uri("D:\\K\\WPFApps\\VendingDisplay\\Image\\suksespayment.png")
            )
            {
                CacheOption = BitmapCacheOption.OnLoad
            };
            mWindow = (MainWindow)Application.Current.MainWindow;
            mWindow.backBtn.Visibility = Visibility.Collapsed;
            mWindow.topContainer.Visibility = Visibility.Collapsed;
            _confirmPage.MouseLeftButtonUp += _confirmPage_MouseLeftButtonUp;
            timer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 1),
            };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private void _confirmPage_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            timer.Tick -= Timer_Tick;
            mWindow.topContainer.Visibility = Visibility.Visible;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (countDown >= 0)
            {
                redirectingTB.Text = $"Redirecting in....{countDown}";
                countDown--;
            }
            else
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
                mWindow._mainFrame.NavigationService.Navigate(new MainPage());
            }
        }

        private void _confirmPage_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mWindow._mainFrame.NavigationService.Navigate(new MainPage());
        }

        private void _confirmPage_Loaded(object sender, RoutedEventArgs e)
        {
            successImg.Source = bmp;
            tujuanTB.Text = $": {tujuan}";
            jumlahTB.Text = $": {jumlah} tiket";
            hargaTB.Text = $": {price:C}";
            statusTB.Text = totalTransaksi > 0 ? $"Kembalian {totalTransaksi:C}" : "Terima Kasih!";
        }
    }
}
