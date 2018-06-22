namespace uLable
{
    partial class userControlLabel
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
            this.labelControl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelControl
            // 
            this.labelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl.AutoSize = true;
            this.labelControl.Location = new System.Drawing.Point(3, 0);
            this.labelControl.Name = "labelControl";
            this.labelControl.Size = new System.Drawing.Size(50, 13);
            this.labelControl.TabIndex = 0;
            this.labelControl.Text = "X Length";
            this.labelControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelControl_MouseDown);
            this.labelControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelControl_MouseMove);
            this.labelControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelControl_MouseUp);
            // 
            // userControlLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl);
            this.Name = "userControlLabel";
            this.Size = new System.Drawing.Size(60, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelControl;
    }
}
