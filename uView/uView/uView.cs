using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aveva.PDMS.PMLNet;
using System.IO;

[assembly: PMLNetCallable()]
namespace Aveva.Gadgets.uView
{
    [PMLNetCallable()]
    public partial class uView : UserControl
    {
        private string docContetnt = "<html></html>";
        private string docContetnt1 = "<meta http-equiv='X-UA-Compatible' content='IE=10' /><html>";
        private string docContetnt2 = "</html>";
        [PMLNetCallable()]
        public uView()
        {
            InitializeComponent();
            IniView();
        }

        private void IniView()
        {
            webBrowser1.AllowNavigation = true;
            webBrowser1.Navigate("about:blank");
            webBrowser1.DocumentText = docContetnt;
            webBrowser1.Dock = DockStyle.Fill;

            if (webBrowser1.Document != null)
                webBrowser1.Document.Write(string.Empty);
        }

        [PMLNetCallable()]
        public void Assign(uView that) { }

        [PMLNetCallable()]
        public void Preview(string fileName, string pageNumber)
        {
            int r = 0;

            if (int.TryParse(pageNumber, out r))
                webBrowser1.Url = new Uri(fileName + "#page=" + pageNumber);
            else
                webBrowser1.Url = new Uri(fileName);
        }

        [PMLNetCallable()]
        public void PreviewSVG(string fileName)
        {
            string fileContent = File.ReadAllText(fileName);
            webBrowser1.DocumentText = docContetnt1 + fileContent + docContetnt2;
        }
    }
}
