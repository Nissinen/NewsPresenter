using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NewsPresenter.Classes
{
    class ConfigReader
    {
        public void ReadWebSites(ComboBox cb)
        {
            List<SelectionPairs> cbp = new List<SelectionPairs>();
            foreach (var item in ConfigurationSettings.AppSettings.Keys)
            {
                cbp.Add(new SelectionPairs(item.ToString(), ConfigurationSettings.AppSettings[item.ToString()].ToString()));
            }
            cb.ItemsSource = cbp;
            cb.DisplayMemberPath = "_Key";
            cb.SelectedValuePath = "_Value";
        }
    }
}
