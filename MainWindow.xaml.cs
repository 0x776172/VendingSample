using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using VendingDisplay.Screen;

namespace VendingDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> path = new List<string>();
        public List<BitmapImage> list_image = new List<BitmapImage>();
        public MainWindow()
        {
            InitializeComponent();
            _mainWindow.Loaded += _mainWindow_Loaded;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
            backBtn.Click += BackBtn_Click;
            path.Add(@"D:\K\WPFApps\VendingDisplay\Image\display_picture\Stasiun Tangerang - Stasiun Kereta Api Indonesia.jpg");
            path.Add(@"D:\K\WPFApps\VendingDisplay\Image\display_picture\CC 206.jpg");
            path.Add(@"D:\K\WPFApps\VendingDisplay\Image\display_picture\Jakarta Tempo Doeloe.jpg");
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_mainFrame.NavigationService.CanGoBack)
            {
                _mainFrame.NavigationService.GoBack();
                _mainFrame.NavigationService.RemoveBackEntry();
            };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeTBlk.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }

        private void _mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string item in path)
            {
                BitmapImage bmp = new BitmapImage(new Uri(item))
                {
                    CacheOption = BitmapCacheOption.OnLoad,
                };
                list_image.Add(bmp);
            }
            _mainFrame.Navigate(new MainPage());
        }
    }
}
