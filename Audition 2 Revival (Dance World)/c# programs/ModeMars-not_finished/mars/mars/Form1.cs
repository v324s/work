using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Input;
using System.IO;
using System.Net.NetworkInformation;
using System.Management;

namespace mars
{
    public partial class Form1 : Form
    {
        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(
            [In()] System.IntPtr hdc, int x, int y, int cx, int cy,
            [In()] System.IntPtr hdcSrc, int x1, int y1, uint rop);
        //[StructLayout(LayoutKind.Sequential)]

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        static IntPtr WindowHandle;

        static int countcicle = 0;

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out Rectangl lpRect);
        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangl lpRect);

        private static int speedofarrows = 0; // 0-max, 1 -random 
        private static bool botrobit = false;
        private static int x;
        private static int y;
        private static int _x;
        private static int _y;
        private static int widtharrow;
        private static bool itsblue = false;
        private static bool itsgreen = false;
        private static bool itsred = false;
        private static string col = "";
        private static string nameprocess = "";
        private static string[] myarr;
        private static Random rnd = new Random();
        private static int getzaderzhmezharrow;
        private static int msekot;
        private static int msekdo;
        private static int upmsekot;
        private static int upmsekdo;
        private static int countineng = 0;
        private static int counthzarrow = 0;
        private static bool autospace = false;
        private static bool mnogookonnrezh = false;
        private static bool hwidnewwindowget = false;
        private static int spaceX;
        private static int spaceY;
        private static int countshagov;
        private static int countwindowgame;

        private static Thread potok;

        private static Bitmap screenPixel;
        private static Bitmap obrez;
        private static Bitmap bm;
        private static Graphics gdest;
        private static Graphics gsrc;
        private static bool spacenazhat = false;
        private static bool arrowdaunnadospace = false;
        private static int forclean = 0;
        private static int countactokna = -1;
        private static int countactshagov = 0;
        private static byte skokkey = 4;
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
        public static Rectangl GetWindowRectEx(IntPtr hWnd)
        {
            if (IntPtr.Zero == hWnd) return default(Rectangl);

            Rectangl lprect;
            GetWindowRect(hWnd, out lprect);
            return new Rectangl(lprect.Right - lprect.Left, lprect.Bottom - lprect.Top);
        }
        public static Rectangl GetWindowRectExXY(IntPtr hWnd)
        {
            if (IntPtr.Zero == hWnd) return default(Rectangl);

            Rectangl lprect;
            GetWindowRect(hWnd, out lprect);
            return new Rectangl(lprect.Left, lprect.Top, lprect.Right, lprect.Bottom);
        }
        public static Rectangl GetClientRectWiHe(IntPtr hWnd)
        {
            if (IntPtr.Zero == hWnd) return default(Rectangl);

            Rectangl lprect;
            GetClientRect(hWnd, out lprect);
            return new Rectangl(lprect.Left, lprect.Top, lprect.Right, lprect.Bottom);
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

        private static int countlines = 0;
        private static int needalllines = 0;
        private static int[] positionlines = new int[8];
        private static int kschetchik=0;
        private static int ix;
        private static int scix;
        private static bool reversmars=false;
        private void delayscan()
        {
            while (botrobit)
            {
                if (needalllines == 0)
                {
                    for (int i = 1; i <= 8; i++)
                    {
                        switch (i)
                        {
                            case 1: ix = x + 42;
                                break;
                            case 2: ix = x + 76;
                                break;
                            case 3: ix = x + 109;
                                break;
                            case 4: ix = x + 142;
                                break;
                            case 5: ix = x + 176;
                                break;
                            case 6: ix = x + 209;
                                break;
                            case 7: ix = x + 242;
                                break;
                            case 8: ix = x + 276;
                                break;
                        }
                        //for (int i = x; i <= x+_x; i++)
                        // 1 - 42,43   (2px)
                        // 2 - 76      (1px)
                        // 3 - 109     (1px)
                        // 4 - 142,143 (2px)
                        // 5 - 176     (1px)
                        // 6 - 209     (1px)
                        // 7 - 242,243 (2px)
                        // 8 - 276     (1px)
                        var c = getColor(ix, y + 12+3, WindowHandle);
                        if (Int32.Parse(c[0]) > 220 && Int32.Parse(c[1]) > 210 && Int32.Parse(c[2]) > 210)
                        {
                            countlines++;
                            positionlines[kschetchik] = ix;
                            kschetchik++;
                        }
                        //textBox1.Text += c[0].ToString() + " " + c[1].ToString() + " " + c[2].ToString() + '\r' + '\n';
                    }
                    needalllines = countlines * 2 - 1;
                    scix=0;
                  /*  textBox1.Text += "needalllines = " + needalllines + '\r' + '\n';
                    textBox1.Text += "countlines = " + countlines + '\r' + '\n';

                    Graphics graph = null;
                    var bmp = new Bitmap(_x, _y);
                    graph = Graphics.FromImage(bmp);
                    graph.CopyFromScreen(x, y, 0, 0, bmp.Size);
                    bmp.Save(String.Format(@"D:\screens\mars.png"), ImageFormat.Jpeg);*/
                }
                else
                {
                    if (kschetchik > 0)
                    {
                        var c = getColor(positionlines[scix], y + 12 + 3, WindowHandle);
                        if (Int32.Parse(c[0]) > 210 && Int32.Parse(c[1]) > 140 && Int32.Parse(c[2]) > 150)
                        {
                            if (reversmars == false && scix < positionlines.Length)
                            {
                                scix++;
                                needalllines--;
                                if (scix + 1 == positionlines.Length)
                                {
                                    reversmars = true;
                                }
                            }
                            else
                            {
                                scix--;
                                kschetchik--;
                                needalllines--;
                                if (needalllines == 0)
                                {
                                    kschetchik = 0;
                                    countlines = 0;
                                }
                            }

                            PressKey(Keys.Space, false);
                            Thread.Sleep(rnd.Next(40, 100));
                            PressKey(Keys.Space, true);
                        }
                    }
                }
            }
        }
        private void mainfuncbbot()
        {

            if (x != 0 && y != 0 && _y != 0 && _x != 0)
            {
                delayscan();
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IntPtr hwnd = FindWindow("UnrealWindow", null);
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
            x = windowX + borderLRB + 497;
            y = windowY + borderT + 561;
            _x = 286;
            _y = 33;
            
            potok = new Thread(mainfuncbbot);
            botrobit = true;
            potok.Start(); 
            delayscan();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IntPtr hwnd = FindWindow("UnrealWindow", null);
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
            x = windowX + borderLRB + 497;
            y = windowY + borderT + 561;
            _x = 286;
            _y = 33;
           /* x = 0;
            y = 0;
            _x = 1600;
            _y = 900;*/
            Graphics graph = null;
            var bmp = new Bitmap(_x, _y);
            graph = Graphics.FromImage(bmp);
            graph.CopyFromScreen(x, y, 0, 0, bmp.Size);
            /*Bitmap screenPixel = new Bitmap(_x, _y);
            Graphics gdest = Graphics.FromImage(screenPixel);
            Graphics gsrc = Graphics.FromHwnd(hwnd);WindowHandle
            IntPtr hsrcdc = gsrc.GetHdc();
            IntPtr hdc = gdest.GetHdc();
            BitBlt(hdc, 0, 0, _x, _y, hsrcdc, x, y, (int)CopyPixelOperation.SourceCopy);*/
            bmp.Save(String.Format(@"D:\screens\mars.png"), ImageFormat.Jpeg);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            botrobit = false;
            if (potok != null)  //thrdStart - глобальная ссылка на тред
                if (potok.IsAlive)
                    potok.Abort();
        }
    }
}
