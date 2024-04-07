using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFilePath.Text))
                {
                    MessageBox.Show("Please choose a file to send.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string filePath = txtFilePath.Text;

                // Kết nối đến địa chỉ IP và cổng của máy nhận
                TcpClient client = new TcpClient();
                client.Connect("192.168.1.210", 5000);

                // Đọc file cần truyền và mã hóa dữ liệu
                byte[] data = File.ReadAllBytes(filePath);
                string key = "AbcEFjkl"; // Thay thế bằng key mã hóa của bạn, chỉ sử dụng 8 ký tự
                byte[] encryptedData = EncryptData(data, key);

                // Gửi kích thước của file
                NetworkStream stream = client.GetStream();
                byte[] fileSizeBytes = BitConverter.GetBytes(encryptedData.Length);
                stream.Write(fileSizeBytes, 0, fileSizeBytes.Length);

                // Gửi dữ liệu file đã được mã hóa
                stream.Write(encryptedData, 0, encryptedData.Length);

                MessageBox.Show("File sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm mã hóa DES
        private byte[] EncryptData(byte[] data, string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                // Adjust the key size to 8 bytes (64 bits)
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] adjustedKey = new byte[8];
                Array.Copy(keyBytes, adjustedKey, Math.Min(keyBytes.Length, 8));

                des.Key = adjustedKey;
                des.IV = new byte[8]; // IV phải có đúng 8 bytes

                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
