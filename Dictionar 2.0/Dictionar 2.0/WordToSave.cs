using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dictionar_2._0
{
    [Serializable]
    public class WordToSave
    {
        [XmlArray]
        public ObservableCollection<Word> Words { get; set; }
        public WordToSave()
        {
            Words=new ObservableCollection<Word>();
        }
    }
}
