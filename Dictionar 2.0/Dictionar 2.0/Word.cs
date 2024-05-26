using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dictionar_2._0
{
    [Serializable]
    public class Word
    {
        [XmlElement]
        public string word { get; set; }

        [XmlElement]
        public string category { get; set; }

        [XmlElement]
        public string description {  get; set; }

        public string image {  get; set; }
    }
}
