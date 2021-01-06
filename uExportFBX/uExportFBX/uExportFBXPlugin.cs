
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using Autodesk.Navisworks.Internal.ApiImplementation;
using System.Windows.Forms;
using System;

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
    [PluginAttribute("UMPNS.uExportFBXPlugin",
                         "NNAW",
                         ToolTip = "uExportFBXPlugin",
                         DisplayName = "uExportFBXPlugin")]
    public class uExportFBXPlugin : AddInPlugin
    {
        Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
        string exportPath = "";
        public override int Execute(params string[] parameters)
        {
            #region Plan
            //1. Find root element
            //2. Cicle thought all members
            //3. Form output file name 
            //4. Export 
            #endregion
            SelectDialog();

            ModelItemCollection newSelection = new ModelItemCollection();
            newSelection.AddRange(oDoc.CurrentSelection.SelectedItems);
            //newSelection.Add(oDoc.Models.First.RootItem);

            CicleThroughChildren(newSelection.First);
            return 0;
        }
        private void CicleThroughChildren(ModelItem modelItem)
        {


            Autodesk.Navisworks.Api.Application.ActiveDocument.Models.SetHidden(modelItem.Children, true);

            foreach (ModelItem modelItemChild in modelItem.Children)
            {
                //SelectionSet oSet = modelItemChild as SelectionSet;
                //LcOpSelectionSetsElement.MakeVisible(oDoc.State, oSet);
                ModelItemCollection hidden = new ModelItemCollection();
                hidden.Add(modelItemChild);
                oDoc.Models.SetHidden(hidden, false);
                ExportFbx(modelItemChild.DisplayName);
                oDoc.Models.SetHidden(hidden, true);
            }
        }
        private void ExportFbx(string displayName)
        {
            PluginRecord FBXPluginrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("NativeExportPluginAdaptor_LcFbxExporterPlugin_Export.Navisworks");
            if (FBXPluginrecord != null)
            {
                if (!FBXPluginrecord.IsLoaded)
                {
                    FBXPluginrecord.LoadPlugin();
                }
                string[] pa = { exportPath + "/" + displayName.Replace("=", "#").Replace("/", "#") + ".fbx" };

                #region  way 1: by base class of plugin

                //Plugin FBXplugin =
                //           FBXPluginrecord.LoadedPlugin as Plugin;


                //FBXplugin.GetType().InvokeMember("Execute",
                //    System.Reflection.BindingFlags.InvokeMethod,
                //    null, FBXplugin, pa);
                #endregion
                //way 2: by specific class of export plugin
                NativeExportPluginAdaptor FBXplugin = FBXPluginrecord.LoadedPlugin as NativeExportPluginAdaptor;
                FBXplugin.Execute(pa);
            }

        }
        private void SelectDialog()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                exportPath = dlg.SelectedPath;
            }
        }
    }
}
