using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using VendingDisplay.Resource;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace VendingDisplay.Screen.PaymentScreen
{
    /// <summary>
    /// Interaction logic for OnlinePage.xaml
    /// </summary>
    public partial class OnlinePage : Page
    {
        private string title, tujuan, jumlah;
        private int price;
        AssetQuery aq = new AssetQuery();
        MainWindow mWindow = (MainWindow)Application.Current.MainWindow;
        TransactionLog log = new TransactionLog();
        public OnlinePage(string method, string tujuan, string jumlah, int price)
        {
            InitializeComponent();
            title = method;
            this.price = price;
            this.tujuan = tujuan;
            this.jumlah = jumlah;
            _onlinePage.Loaded += _onlinePage_Loaded;
            _onlinePage.Unloaded += _onlinePage_Unloaded;
            bgImg.MouseLeftButtonUp += BgImg_MouseLeftButtonUp;
        }

        private void BgImg_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            log.WriteLog(title, tujuan, jumlah, price, price, 0);
            mWindow._mainFrame.NavigationService.Navigate(new ConfirmPage(tujuan, jumlah, price, 0));
        }

        private void _onlinePage_Unloaded(object sender, RoutedEventArgs e)
        {
            mWindow.topContainer.Visibility = Visibility.Visible;
        }

        private void _onlinePage_Loaded(object sender, RoutedEventArgs e)
        {
            mWindow.topContainer.Visibility = Visibility.Collapsed;
            float width = 120;
            float height = 90;

            BarcodeWriter writer = new BarcodeWriter();
            EncodingOptions options = new EncodingOptions()
            {
                Width = 300,
                Height = 300,
                Margin = 0,
                PureBarcode = false
            };
            options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            writer.Renderer = new BitmapRenderer();
            writer.Options = options;
            writer.Format = BarcodeFormat.QR_CODE;
            using (Bitmap bmp = writer.Write(title + price))
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Seek(0, SeekOrigin.Begin);
                Bitmap logo = new Bitmap(aq.GetPath(title));
                float scale = Math.Min(width / logo.Width, height / logo.Height);
                Console.WriteLine(scale);
                int sWidth = (int)(logo.Width * scale);
                int sHeight = (int)(logo.Height * scale);
                Bitmap rLogo = new Bitmap(logo, new System.Drawing.Size(sWidth, sHeight));
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(rLogo, new System.Drawing.Point((bmp.Width - rLogo.Width) / 2, (bmp.Height - rLogo.Height) / 2));
                g.Flush();
                bmp.Save(stream, ImageFormat.Png);
                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.StreamSource = stream;
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.EndInit();
                bgImg.Source = bmi;
                stream.Dispose();
            }
        }
    }
}
