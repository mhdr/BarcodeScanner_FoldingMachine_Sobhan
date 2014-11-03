namespace BarcodeScanner
{
    partial class FormReadBarcode
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxBarcode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxBarcode
            // 
            this.textBoxBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxBarcode.Location = new System.Drawing.Point(0, 0);
            this.textBoxBarcode.Multiline = true;
            this.textBoxBarcode.Name = "textBoxBarcode";
            this.textBoxBarcode.ReadOnly = true;
            this.textBoxBarcode.Size = new System.Drawing.Size(459, 54);
            this.textBoxBarcode.TabIndex = 0;
            // 
            // FormReadBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 54);
            this.Controls.Add(this.textBoxBarcode);
            this.Name = "FormReadBarcode";
            this.Text = "FormReadBarcode";
            this.Load += new System.EventHandler(this.FormReadBarcode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxBarcode;
    }
}