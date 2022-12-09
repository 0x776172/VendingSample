using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VendingDisplay.Screen
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        MainWindow mWindow;
        public MainPage()
        {
            InitializeComponent();
            mWindow = (MainWindow)Application.Current.MainWindow;
            _mainPage.MouseLeftButtonUp += _mainPage_MouseLeftButtonUp;
            _mainPage.Loaded += _mainPage_Loaded;
            _mainPage.Unloaded += _mainPage_Unloaded;
        }

        private void _mainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            vidContainer.Stop();
            vidContainer.Close();
            vidContainer.MediaEnded -= VidContainer_MediaEnded;
        }

        private void _mainPage_Loaded(object sender, RoutedEventArgs e)
        {
            mWindow.backBtn.Visibility = Visibility.Collapsed;
            vidContainer.Source = new Uri("D:\\K\\WPFApps\\VendingDisplay\\Image\\Footage kereta api melintas.mp4");
            vidContainer.IsMuted = true;
            vidContainer.LoadedBehavior = MediaState.Manual;
            vidContainer.Stretch = Stretch.UniformToFill;
            vidContainer.MediaEnded += VidContainer_MediaEnded;
            vidContainer.Play();
        }

        private void VidContainer_MediaEnded(object sender, RoutedEventArgs e)
        {
            vidContainer.Position = TimeSpan.Zero;
            vidContainer.Play();
        }

        private void _mainPage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mWindow._mainFrame.NavigationService.Navigate(new HomePage());
            mWindow.backBtn.Visibility = Visibility.Visible;
        }
    }
}
