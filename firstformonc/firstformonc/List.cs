using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Aveva.Pdms.Shared;
using Aveva.PDMS.Database.Filters;
using Aveva.Pdms.Database;
using Aveva.ApplicationFramework.Presentation;
using Aveva.ApplicationFramework;
using System.Collections;


namespace firstformonc
{
    public partial class List : Form
    {
        private bool _toogleValue;
        public List()
        {
            InitializeComponent();
            _toogleValue = false;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void List_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_toogleValue)
            {
                var elements = DesignProxy.GetHashtable("MEMBERS");
                //
                foreach (DictionaryEntry i in elements)
                {
                    //string tt = "$p" + i.Key.ToString() + i.Value.ToString();
                    string tt = "$p" + i.Value.ToString();
                    Aveva.Pdms.Utilities.CommandLine.Command.CreateCommand(tt).RunInPdms();
                }
            }
            textBox1.Text = CurrentElement.Element.ToString();
            //заполнить лист
            listBox1.Items.Clear();
            //если се бран
            if (CurrentElement.Element.GetElementType().Equals(DbElementTypeInstance.BRANCH))
            {
                //AttributeRefFilter ownerfilter = new AttributeRefFilter(DbAttributeInstance.OWNER, DbElement.GetElement("/" + branmembers.ToString()));
                AttributeRefFilter ownerfilter = new AttributeRefFilter(DbAttributeInstance.OWNER, DbElement.GetElement(CurrentElement.Element.RefNo()));
                var collection = new DBElementCollection(CurrentElement.Element, ownerfilter);
                foreach (DbElement element in collection)
                {
                    //if (element.GetElementType().Equals(DbElementTypeInstance.TUBING))
                    //{
                    //    listBox1.Items.Add(element);
                    //}
                    listBox1.Items.Add(element);
                }
            }
            else
            {
              //филтр по имени владельца
              TypeFilter BranFilt = new TypeFilter(DbElementTypeInstance.BRANCH);
              var BraColl = new DBElementCollection(CurrentElement.Element, BranFilt);
              foreach (DbElement branmembers in BraColl)
              {
                  listBox1.Items.Add("BRAN: "+ branmembers);
                  //AttributeRefFilter ownerfilter = new AttributeRefFilter(DbAttributeInstance.OWNER, DbElement.GetElement("/" + branmembers.ToString()));
                  AttributeRefFilter ownerfilter = new AttributeRefFilter(DbAttributeInstance.OWNER, DbElement.GetElement(branmembers.RefNo()));
                  var collection = new DBElementCollection(branmembers, ownerfilter);
                     foreach (DbElement element in collection)
                     {
                         //if (element.GetElementType().Equals(DbElementTypeInstance.TUBING))
                         //{
                         //    listBox1.Items.Add(element);
                         //}
                         listBox1.Items.Add(element);  
                     }
               }
            }
           //////////////////
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _toogleValue = !_toogleValue;
        }
    }
}
