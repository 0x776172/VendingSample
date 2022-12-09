using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace VendingDisplay.Screen
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        MainWindow mWindow;
        DispatcherTimer timer;
        List<BitmapImage> bmp = new List<BitmapImage>();
        public HomePage()
        {
            InitializeComponent();
            buyBtn.Click += BuyBtn_Click;
            topupBtn.Click += TopupBtn_Click;
            checkBtn.Click += CheckBtn_Click;
            _menuPage.Loaded += _menuPage_Loaded;

            mWindow = (MainWindow)Application.Current.MainWindow;
            bmp = mWindow.list_image;

            timer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 3),
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        int img = 0;


        private void Timer_Tick(object sender, EventArgs e)
        {
            img = img < bmp.Count - 1 ? img + 1 : 0;
            imgContainer.Source = bmp[img];
            
        }

        private void _menuPage_Loaded(object sender, RoutedEventArgs e)
        {
            imgContainer.Source = bmp[0];
        }

        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            mWindow._mainFrame.NavigationService.Navigate(new CheckSaldoPage());
        }

        private void TopupBtn_Click(object sender, RoutedEventArgs e)
        {
            mWindow._mainFrame.NavigationService.Navigate(new TopupPage());
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {
            mWindow._mainFrame.NavigationService.Navigate(new BuyPage());
        }
    }
}
