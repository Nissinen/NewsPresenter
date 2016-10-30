using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPresenter.Classes
{
    class SelectionPairs
    {
        public string _Key { get; set; }
        public string _Value { get; set; }

        public SelectionPairs(string _key, string _value)
        {
            _Key = _key;
            _Value = _value;
        }
    }
}
