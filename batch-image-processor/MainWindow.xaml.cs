using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace batch_image_processor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if(!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "output")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "output"));
            }
        }

        private void bt_processfolder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bt_processfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                OpenPictureViewer(openFileDialog.FileName);
            }
        }

        private void OpenPictureViewer(string fileName)
        {
            try
            {
                int width = int.Parse(tb_resize_width.Text);
                int height = int.Parse(tb_resize_height.Text);

                ImageViewer imageViewer = new ImageViewer(fileName, Path.Combine(Directory.GetCurrentDirectory(), "output"), new System.Drawing.Size(width, height));
                imageViewer.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
