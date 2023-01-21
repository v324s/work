using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace A2RB
{
    public partial class Form2 : Form
    {
        private static Thread potok;
        private static string require;
        private static string alf = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private static string response;
        private static string[] resmass;
        private static bool defaultmenubotopen = false;

        private string getHWID()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string HWID = "";
            foreach (ManagementObject mo in mbsList)
            {
                HWID = mo["ProcessorId"].ToString();
                break;
            }
            return HWID;
        }
        private string getMAC()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String MacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (MacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    MacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return MacAddress;
        }
        private string getBSSID()
        {
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd";
            proc.StartInfo.Arguments = @"/C ""arp -a """;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            string bssid = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            return bssid;
        }
        private string shifr(string shifr)
        {
            int m = alf.Length;
            int n = 1;
            int k = 20;
            string res = "";
            for (int i = 0; i < shifr.Length; i++)
            {
                for (int j = 0; j < alf.Length; j++)
                {
                    if (shifr[i] == alf[j])
                    {
                        int temp = j * n + k;
                        while (temp >= m)
                        {
                            temp -= m;
                        }
                        res += alf[temp].ToString();
                    }
                }
            }
            return res;
        }
        private string rasshifr(string rasshifr)
        {
            int m = alf.Length;
            int n = 1;
            int k = 15;
            string res = "";
            for (int i = 0; i < rasshifr.Length; i++)
            {
                for (int j = 0; j < alf.Length; j++)
                {
                    if (rasshifr[i] == alf[j])
                    {
                        int temp = (j - k) * n;
                        while (temp < 0)
                        {
                            temp += m;
                        }
                        while (temp >= m)
                        {
                            temp -= m;
                        }
                        res += alf[temp];
                    }
                }
            }
            return res;
        }
        private void fungetinfo()
        {
            string HWID = shifr(getHWID());         // HWID
            string MacAddress = shifr(getMAC());    // MacAddress
            string BSSID = getBSSID();       // BSSID
            if (Program.vk_id != "" && Program.vk_access_token != "" && Program.vk_secret != "" && HWID != "" && MacAddress != "" && BSSID != "")
            {
                if (Program.vk_id != "")
                    require = Program.vk_id + "|" + Program.vk_access_token + "|" + Program.vk_secret + "|" + HWID + "|" + MacAddress + "|" + BSSID;
                string paramsforreq = "action=getuserinfo&params=" + require;
                response = Program.POST(Program.urlserver, paramsforreq);
               /* MessageBox.Show(response,
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);*/
                if (response[0] == '1')
                {
                    resmass = response.Split('|');
                    label1.Text = resmass[1];
                    pictureBox1.ImageLocation = resmass[2];
                    if (resmass.Length > 3)
                    {
                        string skokdney = rasshifr(resmass[3]);
                        label3.Visible = true;
                        label4.Visible = true;
                        string stroka="";
                        for (int i = 5; i < skokdney.Length; i++)
                        {
                            stroka += skokdney[i].ToString();
                        }
                        label4.Text = stroka+" д.";
                        if (skokdney[5] > 0 && skokdney[0] == 'A' && skokdney[2] == 'B')
                        {
                            defaultmenubotopen=true;
                            pictureBox5.Enabled = true;
                            pictureBox5.Visible = true;
                        }
                    }
                    else
                    {
                        label3.Visible = false;
                        label4.Visible = false;
                    }
                }
                else if (response[0] == '0')
                {
                    DialogResult result = MessageBox.Show(
                        "Не удалось получить информацию о пользователе - " + Program.vk_id + '\r' + '\n' + "Возможно, Вы сменили пароль. Нажмите \"ОК\" и запустите программу снова, чтобы пройти авторизацию еще раз" + '\r' + '\n' + "Если данный метод не помогает, обратитесь в личные сообщения группы vk.com/ofnmr за помощью.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    if (result == DialogResult.OK)
                    {
                        File.Delete(string.Format(Program.basepath + "authorization"));
                        Application.Exit();

                        //Application.Exit();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else if (response[0] == '9')
                {
                    resmass = response.Split('|');
                    DialogResult result = MessageBox.Show(resmass[1],
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
        }
        public Form2()
        {
           // potok = new Thread(fungetinfo);
           // potok.Start();
            InitializeComponent();
            fungetinfo();
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            string paramsreq = "action=usercloseprogram&who=" + Program.vk_id;
            Program.POST(Program.urlserver, paramsreq);
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel1.Visible = false;
            label6.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            panel1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string vvedeniykey = textBox1.Text;
            Program.internet=Program.estliinet();
            if (Program.internet == false)
            {
                Program.inetanet();
            }
            else if (vvedeniykey!="")
            {
                string paramsreq = "action=goactivekey&key=" + vvedeniykey + "&user=" + Program.vk_id + "|" + Program.vk_access_token + "|" + Program.vk_secret;
                response=Program.POST(Program.urlserver, paramsreq);
                 /*  MessageBox.Show(response,
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);*/
                if (response[0] == '1')
                {
                    label6.Text = "Ключ успешно активирован!";
                    label6.Visible = true;
                    textBox1.Text = "";
                    fungetinfo();
                }
                else if (response[0] == '0')
                {
                    resmass = response.Split('|');
                    textBox1.Text = "";
                    MessageBox.Show(resmass[1],
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //ботопен
            Program.internet=Program.estliinet();
            if (Program.internet == false)
            {
                Program.inetanet();
            }
            else
            {
                string paramsreq = "action=userlogs&act=openbot&user=" + Program.vk_id + "|" + Program.vk_access_token + "|" + Program.vk_secret + "|" + defaultmenubotopen.ToString();
                response = Program.POST(Program.urlserver, paramsreq);
                if (response[0] == '1')
                {
                    panel2.Visible = true;
                    panel2.Enabled = true;
                }
                else if (response[0] == '0')
                {
                    
                }
            }
        }
    }
}
