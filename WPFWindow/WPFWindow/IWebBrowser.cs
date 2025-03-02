using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserUserControl.Model;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WebBrowserNS
{

    public interface IWebBrowser
    {
        void PostInit(string path);
        void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e);
        void Browser_AddressChanged(object sender, CoreWebView2SourceChangedEventArgs e);
        void AttachScript();
        void WebView_DOMContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs arg);
        ListBrowsingHistory GetListBrowsingHistory();
        void SaveHistory(ListBrowsingHistory listHistory);
        void WebView_DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e);
        void Download_Completed(string filePath);
    }

}
