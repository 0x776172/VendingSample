using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace VendingDisplay.Screen
{
    /// <summary>
    /// Interaction logic for CheckSaldo.xaml
    /// </summary>
    public partial class CheckSaldoPage : Page
    {
        MainWindow mWindow;
        DispatcherTimer timer;
        BitmapImage bmp;
        public CheckSaldoPage()
        {
            InitializeComponent();
            mWindow = (MainWindow)Application.Current.MainWindow;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            _checkSaldoPage.MouseLeftButtonUp += _checkSaldoPage_MouseLeftButtonUp;
            _checkSaldoPage.Loaded += _checkSaldoPage_Loaded;
            _checkSaldoPage.Unloaded += _checkSaldoPage_Unloaded;
            mWindow.backBtn.Visibility = Visibility.Hidden;
            bmp = new BitmapImage(new Uri("D:/K/WPFApps/VendingDisplay/Image/tap.jpg"))
            {
                CacheOption = BitmapCacheOption.OnLoad
            };
        }

        private void _checkSaldoPage_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            timer.Tick -= Timer_Tick;
        }

        private void _checkSaldoPage_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
            timer.Tick += Timer_Tick;
            _tapImg.Source = bmp;
            _tapImg.Stretch = Stretch.Uniform;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            MainPage mPage = new MainPage();
            mWindow._mainFrame.NavigationService.Navigate(mPage);
        }

        private void _checkSaldoPage_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
