namespace barcode_scanner_00
{
    partial class BarcodeScannerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxBC = new System.Windows.Forms.PictureBox();
            this.pictureBoxQR = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQR)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxBC
            // 
            this.pictureBoxBC.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pictureBoxBC.Location = new System.Drawing.Point(63, 35);
            this.pictureBoxBC.Name = "pictureBoxBC";
            this.pictureBoxBC.Size = new System.Drawing.Size(360, 75);
            this.pictureBoxBC.TabIndex = 0;
            this.pictureBoxBC.TabStop = false;
            // 
            // pictureBoxQR
            // 
            this.pictureBoxQR.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pictureBoxQR.Location = new System.Drawing.Point(63, 158);
            this.pictureBoxQR.Name = "pictureBoxQR";
            this.pictureBoxQR.Size = new System.Drawing.Size(360, 360);
            this.pictureBoxQR.TabIndex = 0;
            this.pictureBoxQR.TabStop = false;
            // 
            // BarcodeScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 550);
            this.Controls.Add(this.pictureBoxQR);
            this.Controls.Add(this.pictureBoxBC);
            this.Name = "BarcodeScannerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan codes to test...";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBoxBC;
        private PictureBox pictureBoxQR;
    }
}