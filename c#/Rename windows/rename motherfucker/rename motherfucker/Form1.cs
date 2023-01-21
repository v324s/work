using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace rename_motherfucker
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern int SetWindowText(IntPtr hWnd, string text);

        public Form1()
        {
            InitializeComponent();
        }
        private static IntPtr handlewind;
        private void button1_Click(object sender, EventArgs e)
        {
            string nameprocess = textBox1.Text;
            Process[] processes = Process.GetProcessesByName(nameprocess);
            label2.Text = "Имя окна - " + processes[0].MainWindowTitle;
            handlewind = processes[0].MainWindowHandle;
           //  IntPtr ZagWindow = processes[0].MainWindowHandle;
           // HWND hHandle = FindWindow("Window", "Cheat Engine 6.4");
           // SetWindowText(hHandle, 'Новый заголовок окна');
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string newname = textBox2.Text;
            SetWindowText(handlewind, newname);
        }
    }
}
