using NewsPresenter.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml;

namespace NewsPresenter.Classes
{
    class feedReader
    {
        public void getRSS(string site, ListBox lb)
        {
            try
            {
                List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();
                XmlDocument doc1 = new XmlDocument();
                doc1.Load(site);
                XmlElement root = doc1.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("/rss/channel/item");
                foreach (XmlNode node in nodes)
                {
                 ComboBoxPairs cb = new ComboBoxPairs(node["title"].InnerText, node["link"].InnerText);
                 cb.Content = new TextBlock {Text = cb._Key, TextWrapping = TextWrapping.Wrap };
                 cbp.Add(cb);
                }
                lb.ItemsSource = cbp;
                lb.SelectedValuePath = "_Value";
            }
            catch (Exception ex)
            {
                
            }
        }
        public void getXMLfeeds(string site, ComboBox cb)
        {
            try
            {
                XmlDocument doc1 = new XmlDocument();
                doc1.Load(site);
                XmlElement root = doc1.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("/TheatreAreas/TheatreArea");
                foreach (XmlNode node in nodes)
                {
                    cb.Items.Add(new KeyValuePair<string, string>(node["Name"].InnerText, node["ID"].InnerText));                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        public bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }
    }
}
