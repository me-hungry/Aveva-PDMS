using Aveva.PDMS.PMLNet;
using System;
using System.Collections;
using System.Windows.Forms;

[assembly: PMLNetCallable()]
namespace Aveva.Gadgets.uGrid
{
    [PMLNetCallable()]
    public partial class userGrid : UserControl
    {
        [PMLNetCallable()]
        public userGrid()
        {
            InitializeComponent();
        }
        [PMLNetCallable()]
        public void Assign(userGrid that) { }
        [PMLNetCallable()]
        public void AddType(string type)
        {
            attributeList.Rows.Add(true, type);
            //attributeList.FirstDisplayedScrollingRowIndex = attributeList.Rows.Count - 1;
            //attributeList.Update();
        }
        [PMLNetCallable()]
        public void Clear()
        {
            this.attributeList.Rows.Clear();
        }

        private void attributeList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        [PMLNetCallable()]
        public Hashtable GetSelectedType()
        {
            Hashtable Types = new Hashtable();

            for (int i = 0; i < attributeList.Rows.Count; i++)
            {
                if (Convert.ToBoolean(attributeList.Rows[i].Cells["Visibility"].Value) == true)
                {
                    Types.Add(i, attributeList.Rows[i].Cells["Type"].FormattedValue.ToString());
                }
            }
            return Types;
        }
    }
}
