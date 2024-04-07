
namespace ReceiveFile
{
    partial class Form1
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
            this.btnStartListening = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnUploadAndEncrypt = new System.Windows.Forms.Button();
            //this.btnDownloadDecrypted = new System.Windows.Forms.Button(); // Thêm nút Download Encrypted

            this.SuspendLayout();
            // 
            // btnStartListening
            // 
            this.btnStartListening.Location = new System.Drawing.Point(12, 12);
            this.btnStartListening.Name = "btnStartListening";
            this.btnStartListening.Size = new System.Drawing.Size(120, 23);
            this.btnStartListening.TabIndex = 0;
            this.btnStartListening.Text = "Start Listening";
            this.btnStartListening.UseVisualStyleBackColor = true;
            this.btnStartListening.Click += new System.EventHandler(this.btnStartListening_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(138, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(38, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Ready";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(12, 41);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(250, 20);
            this.txtFilePath.TabIndex = 2;
            // 
            // btnDownload
            // 
            //this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(268, 39);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnUploadAndEncrypt
            // 
           // this.btnUploadAndEncrypt.Enabled = false;
            this.btnUploadAndEncrypt.Location = new System.Drawing.Point(12, 67);
            this.btnUploadAndEncrypt.Name = "btnUploadAndEncrypt";
            this.btnUploadAndEncrypt.Size = new System.Drawing.Size(160, 23);
            this.btnUploadAndEncrypt.TabIndex = 4;
            this.btnUploadAndEncrypt.Text = "Upload and Encrypt";
            this.btnUploadAndEncrypt.UseVisualStyleBackColor = true;
            this.btnUploadAndEncrypt.Click += new System.EventHandler(this.btnUploadAndEncrypt_Click);
            // 
            // btnDownloadEncrypted
            // 
           // this.btnDownloadEncrypted.Enabled = false;
            //this.btnDownloadDecrypted.Location = new System.Drawing.Point(188, 67);
            //this.btnDownloadDecrypted.Name = "btnDownloadEncrypted";
            //this.btnDownloadDecrypted.Size = new System.Drawing.Size(160, 23);
            //this.btnDownloadDecrypted.TabIndex = 5;
            //this.btnDownloadDecrypted.Text = "Download Encrypted"; // Đặt tên cho nút mới
            //this.btnDownloadDecrypted.UseVisualStyleBackColor = true;
            //this.btnDownloadDecrypted.Click += new System.EventHandler(this.btnDownloadDecrypted_Click); // Gắn sự kiện click
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 102);
            //this.Controls.Add(this.btnDownloadDecrypted); // Thêm nút Download Encrypted vào form
            this.Controls.Add(this.btnUploadAndEncrypt);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStartListening);
            this.Name = "Form1";
            this.Text = "Receive File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartListening;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnUploadAndEncrypt;
        //private System.Windows.Forms.Button btnDownloadDecrypted;

    }
}

