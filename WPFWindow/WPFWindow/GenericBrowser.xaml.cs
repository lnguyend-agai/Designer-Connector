using BrowserUserControl.Model;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WebBrowserNS;
using WpfAnimatedGif;

namespace WPFWindow
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class GenericBrowser : Window, IWebBrowser
    {
        private string enviroment_path;
        private readonly string historyFilePath;
        private readonly string HomeURL;
        private BoxDialog boxDialog;
        private readonly string attachScriptPath;
        private bool isReloading = false;
        BitmapImage _reloading, _reload;
        private bool _is_home_link = false;
        private bool _lock_append_history = false;
        public event EventHandler<NameValueArgs> FileDownloaded;

        public GenericBrowser(string home_url, string env_path, string custom_path, string history_path, string attach_script_path)
        {
            this.historyFilePath = history_path;
            this.enviroment_path = env_path;
            this.attachScriptPath = attach_script_path;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.HomeURL = home_url;
            InitReload();
            InitializeComponent();
            InitImageBehavior();
            PostInit(custom_path);
        }
        public void InitReload()
        {
            _reload = new BitmapImage();
            _reload.BeginInit();
            _reload.UriSource = new Uri("reload.png", UriKind.Relative);
            _reload.EndInit();
            _reloading = new BitmapImage();
            _reloading.BeginInit();
            _reloading.UriSource = new Uri("reload.png", UriKind.Relative);
            _reloading.EndInit();
        }

        public void InitImageBehavior()
        {

            ImageBehavior.SetRepeatBehavior(ReloadBtnImg, new System.Windows.Media.Animation.RepeatBehavior(0));
            ImageBehavior.SetRepeatBehavior(ReloadBtnImg, System.Windows.Media.Animation.RepeatBehavior.Forever);
        }
        public void AttachScript()
        {
            using (StreamReader r = new StreamReader(attachScriptPath))
            {
                var script = r.ReadToEnd();
                if (this.WebViewArea.IsInitialized)
                {
                    this.WebViewArea.ExecuteScriptAsync(script);
                }
            }
        }

        public void Browser_AddressChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            this.Title = "Designer Connector";
            this.IsReloading = true;

            BrowsingHistory current_history = new BrowsingHistory { Url = this.WebViewArea.Source, BrowsingTime = DateTime.Now };
            ApplyHistory(current_history);
        }

        private void ApplyHistory(BrowsingHistory entry)
        {
            if (!_lock_append_history && entry.Url.ToString().StartsWith(HomeURL))
            {
                var listBrowsing = GetListBrowsingHistory();
                if (listBrowsing.CurrentURL != entry.Url)
                    listBrowsing.Navigate(entry);
                SaveHistory(listBrowsing);
            }
        }

        public void Download_Completed(string filePath)
        {
            FileDownloaded?.Invoke(this, new NameValueArgs(filePath));
        }

        public ListBrowsingHistory GetListBrowsingHistory()
        {
            try
            {
                using (StreamReader r = new StreamReader(historyFilePath))
                {
                    string json = r.ReadToEnd();
                    var rs = JsonConvert.DeserializeObject<ListBrowsingHistory>(json);
                    return rs ?? new ListBrowsingHistory();
                }
            }
            catch (Exception e)
            {
                if (e is JsonException || e is FileNotFoundException || e is DirectoryNotFoundException)
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(historyFilePath));
                    File.Create(historyFilePath).Dispose();
                    return new ListBrowsingHistory();
                }
                throw e;
            }
        }

        public async void PostInit(string path)
        {
            var environ = await CoreWebView2Environment.CreateAsync(null, enviroment_path);
            await this.WebViewArea.EnsureCoreWebView2Async(environ);
            //this.WebViewArea.CoreWebView2.Profile.DefaultDownloadFolderPath = path;  // comment out this codeline if u just want to test in C# project
            var listBrowsing = GetListBrowsingHistory();
            this.WebViewArea.SourceChanged += Browser_AddressChanged;
            this.WebViewArea.CoreWebView2.DownloadStarting += WebView_DownloadStarting;
            this.WebViewArea.CoreWebView2.NavigationStarting += WebView_NavigationStarting;
            this.WebViewArea.CoreWebView2.WebMessageReceived += JavascriptMessageReceived;
            this.WebViewArea.CoreWebView2.NewWindowRequested += WebView_NewWindowRequested;
            this.WebViewArea.Source = listBrowsing.CurrentURL ?? new Uri(HomeURL);
            this.Title = "Designer Connector";
            this.IsReloading = true;
            this.WebViewArea.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            this.WebViewArea.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
            this.WebViewArea.CoreWebView2.NavigationCompleted += WebView_NavigationCompleted;
            this.WebViewArea.CoreWebView2.DOMContentLoaded += WebView_DOMContentLoaded;
            //Turn off context menu
            this.WebViewArea.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            this.WebViewArea.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
        }

        string EntryToString(BrowsingHistory entry)
        {
            if (entry.Url != null)
                return entry.Url.ToString();
            return String.Empty;
        }

        private void JavascriptMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string arg = e.WebMessageAsJson.Split('\"')[1];
            if (arg == "NAVIGATION:BACK")
            {
                _lock_append_history = true;
                var listBrowsing = GetListBrowsingHistory();
                string url;
                if (_is_home_link)
                {
                    var entry = listBrowsing.GoBack();
                    url = EntryToString(entry);
                }
                else
                {
                    url = listBrowsing.CurrentURL.ToString();
                }
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    this.WebViewArea.CoreWebView2.Navigate(url);
                    SaveHistory(listBrowsing);
                }
                _lock_append_history = false;
            }
            else if (arg == "NAVIGATION:FORWARD")
            {
                _lock_append_history = true;
                var listBrowsing = GetListBrowsingHistory();
                var entry = listBrowsing.GoForward();
                string url = EntryToString(entry);
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    this.WebViewArea.CoreWebView2.Navigate(url);
                    SaveHistory(listBrowsing);
                }
                _lock_append_history = false;
            }
            else if (Uri.IsWellFormedUriString(arg, UriKind.Absolute))
            {
                Process.Start(arg);
            }
        }
        private void RunUrlFromSystemBrowser(string url)
        {
            Process.Start(url);
        }

        public void SaveHistory(ListBrowsingHistory listHistory)
        {
            File.WriteAllText(historyFilePath, JsonConvert.SerializeObject(listHistory, Formatting.Indented));
        }

        public void WebView_DOMContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs arg)
        {
            AttachScript();
        }

        private void WebView_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            _is_home_link = e.Uri.ToString().StartsWith(HomeURL);
        }

        private void WebView_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true;
            Process.Start(e.Uri);
        }

        public void WebView_DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {
            e.Handled = true;
            boxDialog = new BoxDialog { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            boxDialog.Title = "Download Progress";
            boxDialog.Width = 300;
            boxDialog.Height = 100;
            boxDialog.Show();

            e.DownloadOperation.BytesReceivedChanged += (s, ev) =>
            {
                boxDialog.SetValue(e.DownloadOperation.BytesReceived, e.DownloadOperation.TotalBytesToReceive);
            };
            e.DownloadOperation.StateChanged += (s, ev) =>
            {
                if (e.DownloadOperation.State == CoreWebView2DownloadState.Completed)
                {
                    boxDialog.Close();
                    Download_Completed(e.ResultFilePath);
                }

            };
        }

        public void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            this.Title = "Designer Connector";
            this.IsReloading = false;
        }

        public partial class NameValueArgs
        {
            public string FilePath { get; set; }
            public NameValueArgs(string filePath)
            {
                FilePath = filePath;
            }
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WebViewArea.CoreWebView2.Navigate(this.HomeURL);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WebViewArea.CoreWebView2.CanGoBack)
            {
                this.WebViewArea.CoreWebView2.GoBack();
            }
        }

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WebViewArea.CoreWebView2.CanGoForward)
            {
                this.WebViewArea.CoreWebView2.GoForward();
            }
        }

        private void ReloadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WebViewArea.CoreWebView2.Reload();
            this.IsReloading=true;
        }

        public bool IsReloading
        {
            get => isReloading;
            set
            {
                this.isReloading = value;
                if (this.isReloading)
                {
                    ImageBehavior.SetAnimatedSource(ReloadBtnImg, _reloading);
                }
                else
                {
                    ImageBehavior.SetAnimatedSource(ReloadBtnImg, _reload);
                }
            }
        }

        private void WindowClosedFreeResource(object sender, EventArgs e)
        {
            this.WebViewArea?.Dispose();
        }
    }
}
