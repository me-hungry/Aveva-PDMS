using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMPNS
{
    public class ItemCSV
    {
        public string ElementName;
        public string[] ElementUrls;
        public string[] TitleUrls;

        public ItemCSV(string elementName, string[] elementUrls, string[] titleUrls)
        {
            this.ElementName = elementName;
            this.ElementUrls = elementUrls;
            this.TitleUrls = titleUrls;
        }
    }
}
