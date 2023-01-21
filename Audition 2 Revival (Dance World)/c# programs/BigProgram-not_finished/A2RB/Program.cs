using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace A2RB
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!estliinet())
            {
                inetanet();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                string path = AppDomain.CurrentDomain.BaseDirectory + "authorization";
                if (File.Exists(path))
                {
                    string[] lines = File.ReadAllLines(path);
                    if (lines.Length > 0)
                    {
                        vk_id = lines[0];
                        vk_access_token = lines[1];
                        vk_secret = lines[2];
                        authmain = true;
                        Application.Run(new Form2());
                    }
                }
                else
                {
                    Application.Run(new Form1());
                }
            }
        }

        public static string vk_id;
        public static string vk_access_token;
        public static string vk_secret;
        public static bool authmain = false;
        public static bool internet = false;

        public static string POST(string Url, string Data)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sentData.Length;
            System.IO.Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            System.Net.WebResponse res = req.GetResponse();
            System.IO.Stream ReceiveStream = res.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8);
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Out;
        }
        public static bool estliinet()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                internet = false;
            }
            else
            {
                internet = true;
            }
            return internet;
        }
        public static void inetanet()
        {
            MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения", "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            Application.Exit();
        }
        public static string urlserver = "http://nmr.1gb.ru/soft/a2rb/server";
        public static string basepath = AppDomain.CurrentDomain.BaseDirectory;
    }
}
