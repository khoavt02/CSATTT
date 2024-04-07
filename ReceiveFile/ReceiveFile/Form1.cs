using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ReceiveFile
{
    public partial class Form1 : Form
    {
        private const int bufferSize = 1024;
        private TcpListener listener;
        private byte[] receivedFileData;
        private string decryptedFilePath;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartListening_Click(object sender, EventArgs e)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 5000);
                listener.Start();
                lblStatus.Text = "Listening...";
                btnStartListening.Enabled = false;

                // Chờ kết nối từ máy gửi
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                // Đọc kích thước của file
                byte[] fileSizeBytes = new byte[4];
                stream.Read(fileSizeBytes, 0, 4);
                int fileSize = BitConverter.ToInt32(fileSizeBytes, 0);

                // Đọc dữ liệu file
                byte[] buffer = new byte[bufferSize];
                int bytesRead;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, bytesRead);
                    }
                    receivedFileData = memoryStream.ToArray();

                    // Hiển thị đường dẫn của file
                    txtFilePath.Text = "File received. Click Download to save.";
                    btnDownload.Enabled = true;
                }

                client.Close();
                listener.Stop();
                lblStatus.Text = "Ready";
                btnStartListening.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    File.WriteAllBytes(filePath, receivedFileData);
                    MessageBox.Show("File downloaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUploadAndEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    byte[] fileData = File.ReadAllBytes(filePath);

                    // Perform decryption on the file data
                    string decryptedFilePath = Path.ChangeExtension(filePath, ".decrypted");
                    byte[] decryptedData = DecryptData(fileData, "AbcEFjkl");
                    File.WriteAllBytes(decryptedFilePath, decryptedData);

                    MessageBox.Show("File decrypted and saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Enable the "Download Decrypted" button
                    //btnDownloadDecrypted.Enabled = true;
                    this.decryptedFilePath = decryptedFilePath; // Save the path for later downloading
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện click cho nút "Download Decrypted"
        private void btnDownloadDecrypted_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Copy the decrypted file to the chosen location
                    File.Copy(decryptedFilePath, filePath, true);

                    MessageBox.Show("Decrypted file downloaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm giải mã DES
        private byte[] DecryptData(byte[] encryptedData, string key)
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
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(encryptedData, 0, encryptedData.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

    }
}
