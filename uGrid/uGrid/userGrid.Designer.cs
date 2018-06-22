namespace Aveva.Gadgets.uGrid
{
    partial class userGrid
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.attributeList = new System.Windows.Forms.DataGridView();
            this.Visibility = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.attributeList)).BeginInit();
            this.SuspendLayout();
            // 
            // attributeList
            // 
            this.attributeList.AllowUserToAddRows = false;
            this.attributeList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.attributeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Visibility,
            this.Type});
            this.attributeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attributeList.Location = new System.Drawing.Point(0, 0);
            this.attributeList.MultiSelect = false;
            this.attributeList.Name = "attributeList";
            this.attributeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.attributeList.Size = new System.Drawing.Size(305, 356);
            this.attributeList.TabIndex = 1;
            // 
            // Visibility
            // 
            this.Visibility.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Visibility.HeaderText = "Visibility";
            this.Visibility.Name = "Visibility";
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // userGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.attributeList);
            this.Name = "userGrid";
            this.Size = new System.Drawing.Size(305, 356);
            ((System.ComponentModel.ISupportInitialize)(this.attributeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView attributeList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Visibility;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
    }
}
