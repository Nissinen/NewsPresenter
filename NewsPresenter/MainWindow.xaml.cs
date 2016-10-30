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
using System.Configuration;
using NewsPresenter.Classes;
using System.Net;

namespace NewsPresenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        feedReader feedR = new feedReader();
        ConfigReader cr = new ConfigReader();

        public MainWindow()
        {
            InitializeComponent();
            webBrowser.Navigate("https://www.google.fi/");
            webBrowser.LoadCompleted += new System.Windows.Navigation.LoadCompletedEventHandler(webBrowser_LoadCompleted);
            webBrowser.Navigated += new NavigatedEventHandler(wbMain_Navigated);
            cr.ReadWebSites(SelectWebsite);
            Headers.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight - 150;
        }


        void wbMain_Navigated(object sender, NavigationEventArgs e)
        {
            ScriptSuppress.SetSilent(webBrowser, true); // make it silent
        }

        private void SelectWebsite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            feedR.getRSS(SelectWebsite.SelectedValue.ToString(), Headers);
        }

        private void Headers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            webBrowser.Navigate(Headers.SelectedValue.ToString());
        }

        private void webBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string s = webBrowser.Source.AbsoluteUri;
            txtUrl.Text = s;
        }

        private void HideAndShow_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToString(HideAndShow.Content) == "Hide Sidebar")
            {
                SearchColumn.Width = new GridLength(0, GridUnitType.Star);
                MiddleColumn.Width = new GridLength(5, GridUnitType.Star);
                WebColumn.Width = new GridLength(95, GridUnitType.Star);
                HideAndShow.Content = "Show Sidebar";
            }
            else if(Convert.ToString(HideAndShow.Content) == "Show Sidebar")
            {
                SearchColumn.Width = new GridLength(3, GridUnitType.Star);
                MiddleColumn.Width = new GridLength(1, GridUnitType.Star);
                WebColumn.Width = new GridLength(6, GridUnitType.Star);
                HideAndShow.Content = "Hide Sidebar";
            }
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            // your event handler here
            e.Handled = true;
            string path = txtUrl.Text;
            bool url = feedR.RemoteFileExists(path);
            bool httpurl = feedR.RemoteFileExists("http://" + path);
            if (url == true)
            {
                try
                {
                    webBrowser.Navigate(txtUrl.Text);              
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }  
            }
            else if (httpurl == true)
            {
                try
                {
                    webBrowser.Navigate("http://" + txtUrl.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                webBrowser.Navigate("https://www.google.com/#q=" + txtUrl.Text);
            }
        }
    }

}
