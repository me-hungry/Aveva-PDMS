using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using System.ComponentModel;
using System.Data;


using Aveva.PDMS.Database.Filters;
using System.Collections;
using Aveva.Pdms.Geometry;
using Aveva.Pdms.Utilities.CommandLine;
using Aveva.ApplicationFramework;
using Aveva.ApplicationFramework.Presentation;
using Aveva.Pdms.Shared;
using Aveva.Pdms.Database;
using Aveva.PDMS.PMLNet;

using System.Diagnostics;
namespace firstformonc
{
    //класс добавления аддина
    public class manageaddi : IAddin
    {
        //обьявляем переменные
        public static ServiceManager ServicManag;
        public static CommandManager CommadManag;
        public static CommandBarManager CommandBarManag;
        public static CommandBar MyToolBar;

        #region IAddin Members
        //свойства для менежера---имя \описание
        public string Description
        {
            get { return "New Panel"; }
        }
        public string Name
        {
            get { return "PanelForMacrosRun"; }
        }
        ///////////////////////////////////////

        public void Start(ServiceManager serviceManager)
        {

            //REGISTER NEW COMMAND

            ServicManag = serviceManager;
            CommadManag = (CommandManager)ServicManag.GetService(typeof(CommandManager));
            CommandBarManag = (CommandBarManager)ServicManag.GetService(typeof(CommandBarManager));
            //////////////////////
            //panel
            MyToolBar = CommandBarManag.CommandBars.AddCommandBar("Панелька");
            //buttons
            CommandBarManag.RootTools.AddButtonTool("key1", "Show", null, new CommandFilter("ShowForm"));
            MyToolBar.Tools.AddTool("key1");
            CommandBarManag.RootTools.AddButtonTool("key2", "Form", null, new CommandFilter("ShowForm2"));
            MyToolBar.Tools.AddTool("key2");

        }
        public void Stop()
        {

        }
        #endregion

    }

    //panel button check
    [PMLNetCallable()]
    public class CommandFilter : Aveva.ApplicationFramework.Presentation.Command
    {

        public CommandFilter()
        {
        }

        public CommandFilter(string key)
        {
            this.Value = key;
        }
        [PMLNetCallable()]
        public override void Execute()
        {
            string tester = (string)this.Value;
            try
            {
                switch (tester)
                {

                    case "ShowForm":
                        {
                            List af = new List();
                            topForm.top(af.Handle);
                            af.Show();
                        }
                        break;
                    case "ShowForm2":
                        {
                            List af = new List();
                            topForm.top(af.Handle);
                            af.Show();
                            MessageBox.Show("hi");
                        }
                        break;
                }
            }
            catch { }
        }
    }
    //класс для отображения формы поверх других
    class topForm
    {
        const string PROCESS_NAME = "des";
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int attr, IntPtr mapp);
        public static readonly int GWL_HWNDPARENT = (-8);

        public static void top(IntPtr fhWnd)
        {
            foreach (Process process in Process.GetProcessesByName(PROCESS_NAME))
            {
                IntPtr hWnd = process.MainWindowHandle;
                SetWindowLong(fhWnd, GWL_HWNDPARENT, hWnd);
                break;
            }
        }
    }
    //
    [PMLNetCallable]
    class CallableCallBack
    {
        [PMLNetCallable]
        public CallableCallBack()
        {
        }

        [PMLNetCallable]
        public void Assign(CallableCallBack that)
        {
        }

        [PMLNetCallable]
        public void ReceiveFromPMLHashtable(Hashtable hashTable)
        {
            DesignProxy.ReceiveFromPMLHashtable(hashTable);
        }
    }

    public static class DesignProxy
    {
        public static Hashtable hashTableExchange;

        static DesignProxy()
        {
            Aveva.Pdms.Utilities.CommandLine.Command.CreateCommand("import 'firstformonc'").RunInPdms();
        }

        public static Hashtable GetHashtable(string expression)
        {
            string cmdText = String.Format("using namespace 'firstformonc'\n" +
                                                                "!a = object CallableCallBack()\n" +
                                                                "!b = {0}\n" +
                "!b = !b.Evaluate(object BLOCK('!b[!evalIndex].name.String()'))\n" +
                                                                "!a.ReceiveFromPMLHashtable(!b)\n" +
                                                                "!a.Delete()\n" +
                                                                "!b.Delete()\n",
                                                            expression
                );
            Aveva.Pdms.Utilities.CommandLine.Command.CreateCommand(cmdText).RunInPdms();
            if (hashTableExchange != null)
            {
                var temp = hashTableExchange;
                hashTableExchange = null;
                return temp;
            }
            return null;
        }

        public static void ReceiveFromPMLHashtable(Hashtable hashTable)
        {
            hashTableExchange = new Hashtable(hashTable);
        }
    }
}

