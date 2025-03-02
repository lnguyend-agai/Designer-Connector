using BrowserUserControl.Model;
using System;
using System.Windows;
using WPFWindow;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path = @"Env_Test";
        private string download_path = @"Env_Test";
        private string history_path = @"Env_Test\History\";
        private string attach_script_path = @"Env_Test\listener.js";

        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var win = new GenericBrowser("https://unsplash.com/fr", path, download_path, history_path + "unsplash.json", attach_script_path);
            win.Show();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var win = new GenericBrowser("https://pixabay.com/", path, download_path, history_path + "pixabay.json", attach_script_path);
            win.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var win = new GenericBrowser("https://www.pexels.com/vi-vn/", path, download_path, history_path + "pexels.json", attach_script_path);
            win.Show();
        }
    }
}
