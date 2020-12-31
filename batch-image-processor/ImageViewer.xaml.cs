using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace batch_image_processor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ImageViewer : Window
    {
        Image img = null;
        string _fileName = "";
        string _destPath = "";
        System.Drawing.Size _windowSize;

        public ImageViewer(string filename, string destpath, System.Drawing.Size windowsize)
        {
            InitializeComponent();

            _fileName = filename;
            _destPath = destpath;
            _windowSize = windowsize;

            img = Image.FromFile(_fileName);
            this.Width = img.Width;
            this.Height = img.Height;
            
            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                image_control.Source = bitmapImage;
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(image_control);

            System.Drawing.Point startPoint = new System.Drawing.Point(Math.Max(0, (int)p.X - (_windowSize.Width/2)), Math.Max(0, (int)p.Y - (_windowSize.Height/2)));

            if(startPoint.X + _windowSize.Width > img.Width)
            {
                _windowSize.Width = img.Width - startPoint.X;
            }
            if (startPoint.Y + _windowSize.Height > img.Height)
            {
                _windowSize.Height = img.Height - startPoint.Y;
            }


            Image image = CropImage(img, new Rectangle(startPoint, _windowSize));
            FileInfo fileInfo = new FileInfo(_fileName);
            image.Save(Path.Combine(_destPath, fileInfo.Name), ImageFormat.Jpeg);

            this.Close();
        }

        private static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }
    }
}
