using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BrowserUserControl.Model;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using WpfAnimatedGif;

namespace ButtonizedTitleBarWPFUserControl
{
    /// <summary>
    /// Please, assign this.HomeURL to the new homepage website address.
    /// Interaction logic for ButtonizedTitleBarWPFUserControl.xaml
    /// </summary>
    public partial class ButtonizedTitleBarWPFUserControl : UserControl
    {
        private string homeURL = "";
        private bool isReloading = false;

        public string HomeURL { get => homeURL; 
            set
            {
                homeURL = value;
                ReloadBtn_Click(null , null);
            }
        }

        BitmapImage _reloading, _reload;

        public bool IsReloading
        {
            get => isReloading;
            set
            {
                this.isReloading = value;
            }
        }

        public ListBrowsingHistory history = new ListBrowsingHistory();

        public ButtonizedTitleBarWPFUserControl()
        {
            _reload = new BitmapImage();
            _reload.BeginInit();
            _reload.UriSource = new Uri("reload.ico", UriKind.Relative);
            _reload.EndInit();
            _reloading = new BitmapImage();
            _reloading.BeginInit();
            _reloading.UriSource = new Uri("reload.ico", UriKind.Relative);
            _reloading.EndInit();
            InitializeComponent();
            this.DataContext = this;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
            }
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            foreach (WebView2 browser in FindVisualChildren<WebView2>(parentWindow))
            {
                browser.CoreWebView2.Navigate(this.HomeURL);
                //break;
            }
        }

        private void ReloadBtn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            foreach (WebView2 browser in FindVisualChildren<WebView2>(parentWindow))
            {
                browser.CoreWebView2.Reload();
                //break;
            }
        }

        public void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            foreach (WebView2 browser in FindVisualChildren<WebView2>(parentWindow))
            {
                browser.ExecuteScriptAsync("chrome.webview.postMessage('NAVIGATION:BACK')");
            }
        }

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            foreach (WebView2 browser in FindVisualChildren<WebView2>(parentWindow))
            {
                browser.ExecuteScriptAsync("chrome.webview.postMessage('NAVIGATION:FORWARD')");
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow.WindowState == WindowState.Maximized)
            {
                parentWindow.WindowState = WindowState.Normal;
            }
            else
            {
                parentWindow.WindowState = WindowState.Maximized;
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.DragMove();
        }
    }
}
