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
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace autospaceau2
{
    public partial class Form1 : Form
    {
        private static bool rabotaet = false;
        private static int locpicx;
        private static Random rnd = new Random();
        private static IntPtr hwnd;
        private static Thread potok;
        private static int spaceX;
        private static int spaceY;
        private static int rezhim = 0;

        public Form1()
        {
            InitializeComponent();

            locpicx = pictureBox2.Location.X;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
            pictureBox2.Location = new Point(locpicx + trackBar1.Value, pictureBox2.Location.Y);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = (int)numericUpDown1.Value;
            pictureBox2.Location = new Point(locpicx + trackBar1.Value, pictureBox2.Location.Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rabotaet = false;
            button1.Enabled = true;
            button2.Enabled = false;
            if (potok != null)  //thrdStart - глобальная ссылка на тред
                if (potok.IsAlive)
                    potok.Abort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rabotaet = true;
            button1.Enabled = false;
            button2.Enabled = true;
            hwnd = FindWindow("UnrealWindow", null);
            var rectHW = GetWindowRectEx(hwnd);
            var rectXYXY = GetWindowRectExXY(hwnd);
            var rectCli = GetClientRectWiHe(hwnd);
            int windowWidth = rectHW.Width;
            int windowHeight = rectHW.Height;
            int windowX = rectXYXY.Left;
            int windowY = rectXYXY.Top;
            int window_X = rectXYXY.Right;
            int window_Y = rectXYXY.Bottom;
            int clientWidth = rectCli.Right;
            int clientHeight = rectCli.Bottom;
            int borderLRB = (windowWidth - clientWidth) / 2; //  (1296-1280)/2=8
            int borderT = windowHeight - clientHeight - borderLRB; // 758-720=38-8 =30
            spaceX = windowX + borderLRB + 759 - 125;
            spaceY = windowY + borderT + 545 - 10;
            potok = new Thread(delayscan);
            potok.Start();
            //func();
        }
        private static Bitmap screenPixel;
        private static Bitmap obrez;
        private static Bitmap bm;
        private static Graphics gdest;
        private static Graphics gsrc;
        private void mainfunc()
        {
            int _x = 174;
            int _y = 20;
            int x = spaceX + (int)numericUpDown1.Value;
            screenPixel=new Bitmap(_x,_y);
            Graphics gh = Graphics.FromImage(screenPixel as Image);
            gh.CopyFromScreen(x, spaceY, 0, 0, screenPixel.Size);
            screenPixel.Save(String.Format(@"D:\screens\barka.png"), ImageFormat.Jpeg);
        }
        private void func()
        {
            for (int i = 0; i < 15; i++)
            {
                int x = spaceX + (int)numericUpDown1.Value;
                var c = getColor(x, spaceY + 10, WindowHandle);
                string colid = c[0] + c[1] + c[2];
                textBox1.Text += "R=" + c[0] + " G=" + c[1] + " B=" + c[2] + '\r' + '\n';
            }
        }
        private void delayscan()
        {
            while (rabotaet)
            {
                rezhim = (int)numericUpDown2.Value;
                int x = spaceX + (int)numericUpDown1.Value;
                var c = getColor(x, spaceY+10, WindowHandle);
                string colid = c[0] + c[1] + c[2];
                //Int32.Parse(c[0]) > 151 && Int32.Parse(c[1]) > 126 && Int32.Parse(c[2]) < 35
                if (rezhim == 0)
                {
                    if (Int32.Parse(c[0]) > 210 && Int32.Parse(c[1]) > 130 && Int32.Parse(c[2]) < 70)
                    {
                        PressKey(Keys.Space, false);
                        Thread.Sleep(rnd.Next(23, 150));
                        PressKey(Keys.Space, true);
                    }
                }
                else if (rezhim == 1)
                {
                    if (Int32.Parse(c[0]) > 151 && Int32.Parse(c[1]) > 126 && Int32.Parse(c[2]) < 35)
                    {
                        PressKey(Keys.Space, false);
                        Thread.Sleep(rnd.Next(23, 150));
                        PressKey(Keys.Space, true);
                    }
                }
                Thread.Sleep(1);
            }
        }

        #region DLLImport
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hDC, int x, int y);
        #endregion


        public string[] getColor(int x, int y, IntPtr wndHandle)
        {
            byte r, g, b;
            IntPtr hDC = GetDC(wndHandle);
            uint pixel = GetPixel(hDC, x, y);
            ReleaseDC(IntPtr.Zero, hDC);

            r = (byte)(pixel & 0x000000FF);
            g = (byte)((pixel & 0x0000FF00) >> 8);
            b = (byte)((pixel & 0x00FF0000) >> 16);
            string[] rgbpx = new string[3];
            rgbpx[0] = r.ToString();
            rgbpx[1] = g.ToString();
            rgbpx[2] = b.ToString();
            //Color color = Color.FromArgb(1, r, g, b);
            return rgbpx;
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        public static void PressKey(Keys key, bool up)
        {
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            if (up)
            {
                keybd_event((byte)key, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            }
            else
            {
                keybd_event((byte)key, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
            }
        }
        public struct Rectangl
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;
            public readonly int Height;
            public readonly int Width;

            public Rectangl(int width, int height)
                : this()
            {
                this.Height = height;
                this.Width = width;
            }
            public Rectangl(int xx, int yy, int _xx, int _yy)
                : this()
            {
                this.Left = xx;
                this.Top = yy;
                this.Right = _xx;
                this.Bottom = _yy;
            }
        }
        public static Rectangl GetWindowRectExXY(IntPtr hWnd)
        {
            if (IntPtr.Zero == hWnd) return default(Rectangl);

            Rectangl lprect;
            GetWindowRect(hWnd, out lprect);
            return new Rectangl(lprect.Left, lprect.Top, lprect.Right, lprect.Bottom);
        }
        public static Rectangl GetWindowRectEx(IntPtr hWnd)
        {
            if (IntPtr.Zero == hWnd) return default(Rectangl);

            Rectangl lprect;
            GetWindowRect(hWnd, out lprect);
            return new Rectangl(lprect.Right - lprect.Left, lprect.Bottom - lprect.Top);
        }
        public static Rectangl GetClientRectWiHe(IntPtr hWnd)
        {
            if (IntPtr.Zero == hWnd) return default(Rectangl);

            Rectangl lprect;
            GetClientRect(hWnd, out lprect);
            return new Rectangl(lprect.Left, lprect.Top, lprect.Right, lprect.Bottom);
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out Rectangl lpRect);
        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangl lpRect);
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        static IntPtr WindowHandle;

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(
            [In()] System.IntPtr hdc, int x, int y, int cx, int cy,
            [In()] System.IntPtr hdcSrc, int x1, int y1, uint rop);
    }
}
