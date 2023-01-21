using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace A2RB
{
    public partial class Form1 : Form
    {
        private static bool viewpass = false;
        private static string userlogin;
        private static string userpass;
        private static string resultpost;
        private static string paramsforreq;
        private static string[] reqmass;
        public static string path;
        private static Thread potok;
        private static byte infaest = 0;

        private void checkfileauth()
        {
            
        }
        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
            potok = new Thread(checkfileauth);
            potok.Start();
        }
        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (viewpass == true)
            {
                textBox2.PasswordChar = '•';
                viewpass = false;
            }
            else
            {
                textBox2.PasswordChar = '\0';
                viewpass = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userlogin = textBox1.Text;
            userpass = textBox2.Text;
            if (userlogin != "" && userlogin != "+7" && userlogin.Length > 8)
            {
                if (userpass != "" && userpass.Length > 5)
                {
                    paramsforreq = "login=" + userlogin + "&pass=" + userpass;
                    resultpost = Program.POST(Program.urlserver, paramsforreq);
                    //label1.Text = resultpost;
                    if (resultpost[0] == '0')
                    {
                        label1.Text = "Неверный логин или пароль";
                    }else if (resultpost[0] == '3')
                    {
                        reqmass = resultpost.Split('|');
                        panel1.Visible = true;
                        pictureBox2.ImageLocation = reqmass[2];
                    }
                    else if (resultpost[0] == '1')
                    {
                       /* Form2 newChild = new Form2();
                        newChild.ShowDialog();*/
                        reqmass = resultpost.Split('|');
                        Program.vk_id=reqmass[1];
                        Program.vk_access_token = reqmass[2];
                        Program.vk_secret = reqmass[3];
                       /* if (Program.vk_secret.Contains(" "))
                        {
                            Program.vk_secret.Substring(0, Program.vk_secret.Length-1);
                        }*/
                        path = Program.basepath + "authorization";
                        StreamWriter str = new StreamWriter(string.Format(path));
                        str.WriteLine(Program.vk_id);
                        str.WriteLine(Program.vk_access_token);
                        str.WriteLine(Program.vk_secret);
                        str.Close();
                        Form2 f = new Form2();
                        f.Show();
                        this.Hide();
                    }
                    else if (resultpost[0] == '9')
                    {
                        DialogResult result = MessageBox.Show(resultpost+"test",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    label1.Text = "Введите логин и пароль";
                }
            }
            else
            {
                label1.Text = "Введите логин и пароль";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                paramsforreq = "login=" + userlogin + "&pass=" + userpass + "&captcha_sid=" + reqmass[1] + "&captcha_key=" + textBox3.Text;
                resultpost = Program.POST(Program.urlserver, paramsforreq);
            }
        }
    }
}
