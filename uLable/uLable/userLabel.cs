using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aveva.PDMS.PMLNet;
using Aveva.Pdms.Utilities.CommandLine;
using System.Collections;


[assembly: PMLNetCallable()]
namespace uLable
{
    [PMLNetCallable()]
    public partial class userControlLabel : UserControl
    {
        public int deltaX;
        public Int32 pdmsValue;
        private Point previousPosition;
        private bool pressed = false;

        private readonly static Random _random = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public string pdmsGadgetName = "";
        public string pdmsGadgetCallBack = "";
        public Int32 pdmsGadgetIncrement = 10;

        [PMLNetCallable()]
        public userControlLabel()
        {
            InitializeComponent();
        }
        [PMLNetCallable()]
        public void Ini(Hashtable _iniPara)
        {
            var list = _iniPara.Cast<DictionaryEntry>().OrderBy(k => k.Key).Select(s => s.Value.ToString()).ToList();

            labelControl.Text = list[0];
            pdmsGadgetName = list[1];
            pdmsGadgetCallBack = list[2];
            pdmsGadgetIncrement = Convert.ToInt32(list[3]);
        }
        [PMLNetCallable()]
        public void Assign(userControlLabel that) { }

        [PMLNetCallable()]
        public void labelControl_MouseDown(object sender, MouseEventArgs e)
        {
            labelControl.Cursor = Cursors.SizeWE;

            previousPosition = MousePosition;
            //Save value from pdms to C# variable
            pdmsValue = GetTextGadgetValueFromPdms(pdmsGadgetName);

            pressed = true;

        }
        [PMLNetCallable()]
        public void labelControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (pressed)
            {
                Point mousePos = MousePosition;
                if (mousePos == previousPosition)
                    return;
                //if (Control.ModifierKeys == Keys.Shift)
                //{
                //    int deltaX = 15;
                //}
                //else
                //{
                    int deltaX = (mousePos.X - previousPosition.X);
                //}

                previousPosition = MousePosition;
                try
                {
                    Int32 intValue = Convert.ToInt32(pdmsValue) + deltaX * pdmsGadgetIncrement;
                    SetTextGadgetValueToPdms(pdmsGadgetName, intValue);
                }
                catch (Exception)
                {
                    int intValue = deltaX;
                    SetTextGadgetValueToPdms(pdmsGadgetName, intValue);
                }
            }
        }
        [PMLNetCallable()]
        public void labelControl_MouseUp(object sender, MouseEventArgs e)
        {
            pressed = false;
        }
        private static string GenerateRandomPmlString(int size = 15)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_random.Next(_chars.Length)];
            }
            return new string(buffer);
        }
        public Int32 GetTextGadgetValueFromPdms(string _pdmsGadgetName)
        {
            var result = default(Int32);
            var variableName = GenerateRandomPmlString();
            try
            {

                var command = Command.CreateCommand(string.Format("!!{0} = {1}", variableName, _pdmsGadgetName + ".val"));
                if (command.Run())
                {
                    result = Convert.ToInt32(command.GetPMLVariableReal(variableName));
                }
            }
            catch (Exception) { }
            finally
            {
                Command.CreateCommand(string.Format("!!{0}.Delete()", variableName)).Run();
            }
            return result;
        }
        public void SetTextGadgetValueToPdms(string pdmsGadgetName, Int32 _value)
        {
            pdmsValue = _value;
            Command.CreateCommand(pdmsGadgetName + ".val" + " = " + _value).RunInPdms();
            Command.CreateCommand(pdmsGadgetCallBack).RunInPdms();
        }
    }
}
