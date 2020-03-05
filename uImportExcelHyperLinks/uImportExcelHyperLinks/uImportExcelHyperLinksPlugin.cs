using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Interop;
using Autodesk.Navisworks.Api.Plugins;
using Autodesk.Navisworks.Internal.ApiImplementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ComApi = Autodesk.Navisworks.Api.Interop.ComApi;
using ComApiBridge = Autodesk.Navisworks.Api.ComApi;

namespace UMPNS
{
    [AddInPlugin(AddInLocation.AddIn,
                CanToggle = true,
                LoadForCanExecute = true,
                CallCanExecute = CallCanExecute.Always,
                Icon = "Resources\\PluginIcon16x16.bmp",
                LargeIcon = "Resources\\PluginIcon32x32.bmp",
                Shortcut = "Ctrl+Shift+P",
                ShortcutWindowTypes = "")]
    [PluginAttribute("uImportExcelHyperLinksPlugin",
                         "IEHL",
                         ToolTip = "uImportExcelHyperLinksPlugin",
                         DisplayName = "uImportExcelHyperLinksPlugin")]
    public class uImportExcelHyperLinksPlugin : AddInPlugin
    {
        public Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
        public string importFileName = "";
        public List<ItemCSV> arrayFromCSVFie = new List<ItemCSV>();
        private const string m_caption = "Import CSV Attributes";
        private const string m_filter = "CSV Attributes (*.csv)|*.csv";

        public override int Execute(params string[] parameters)
        {
            #region Plan
            //Select CSV file
            //Loop thought elements
            //Set Hyperlink for CE
            #endregion

            if (parameters.Count() != 0)
            {
                oDoc.OpenFile(parameters[0]);
                importFileName = parameters[0].Replace(".nwd", ".csv");
            }
            else
                PromptForOpenFilename();

            FillArrayFromCSVFie();
            LoopThroughChildren();

            if (parameters.Count() != 0)
            {
                oDoc.SaveFile(parameters[0]);
                oDoc.Dispose();
            }
            return 0;
        }

        private void FillArrayFromCSVFie()
        {
            arrayFromCSVFie.Clear();

            var lines = File.ReadAllLines(importFileName).Select(a => a.Split(';'));

            var elementNames = (from line in lines select line[0]).ToArray();
            var elementUrls = (from line in lines
                               select (from col in line
                                       select col).Skip(1).ToArray()).ToList();
            var titleUrls = (from line in lines
                             select line.Skip(1).ToArray()).Take(1).FirstOrDefault().ToArray();

            for (int i = 0; i < elementNames.Count(); i++)
            {
                arrayFromCSVFie.Add(new ItemCSV(elementNames[i], elementUrls[i], titleUrls));
            }
        }

        private void LoopThroughChildren()
        {
            foreach (ModelItem modelItem in oDoc.Models.CreateCollectionFromRootItems().DescendantsAndSelf)
            {
                foreach (var arrayItem in arrayFromCSVFie)
                {
                    if (arrayItem.ElementName == modelItem.DisplayName)
                    {
                        AddURL(modelItem, arrayItem.ElementUrls, arrayItem.TitleUrls);
                    }
                }
            }
        }

        public bool PromptForOpenFilename()
        {
            //Dialog for selecting the Location of the file to open
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = m_caption;
            dlg.Filter = m_filter;

            //Ask user for file location
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                importFileName = dlg.FileName;
                return true;
            }
            else
            {
                importFileName = string.Empty;
                return false;
            }
        }

        private void AddURL(ModelItem modelItem, string[] urls, string[] titleUrls)
        {
            ComApi.InwOpState10 state;
            state = ComApiBridge.ComApiBridge.State;
            // create the hyperlink collection
            ComApi.InwURLOverride oMyURLOoverride = (ComApi.InwURLOverride)state.ObjectFactory(ComApi.nwEObjectType.eObjectType_nwURLOverride, null, null);
            // get current selected items
            ModelItemCollection modelItemCollectionIn = new ModelItemCollection();
            modelItemCollectionIn.Add(modelItem);
            // add the new hyperlink to the hyperlink collection
            ComApi.InwURLColl oURLColl = oMyURLOoverride.URLs();

            for (int i = 0; i < urls.Count(); i++)
            {
                // create one hyperlink
                ComApi.InwURL2 oMyURL = (ComApi.InwURL2)state.ObjectFactory(ComApi.nwEObjectType.eObjectType_nwURL, null, null);
                // create Hyperlink
                oMyURL.SetCategory("Hyperlink", "LcOaURLCategoryHyperlink");
                // Attachment Point of the hyperlink
                ComApi.InwLPos3f oNewP = (ComApi.InwLPos3f)state.ObjectFactory(ComApi.nwEObjectType.eObjectType_nwLPos3f, null, null);
                oNewP.data1 = modelItemCollectionIn.BoundingBox().Center.X;
                oNewP.data2 = modelItemCollectionIn.BoundingBox().Center.Y;
                oNewP.data3 = modelItemCollectionIn.BoundingBox().Center.Z;

                oMyURL.AttachmentPoints().Add(oNewP);
                // name of the hyperlink
                oMyURL.name = titleUrls[i];
                // site of the hyperlink
                oMyURL.URL = urls[i];

                oURLColl.Add(oMyURL);
            }

            //convert to InwOpSelection of COM API
            ComApi.InwOpSelection comSelectionOut = ComApiBridge.ComApiBridge.ToInwOpSelection(modelItemCollectionIn);
            // set the hyplerlink of the model items
            state.SetOverrideURL(comSelectionOut, oMyURLOoverride);
            // enable to the hyperlinks visible
            state.URLsEnabled = true;

            modelItemCollectionIn.Remove(modelItem);
        }
    }
}
