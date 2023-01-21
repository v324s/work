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
//using System.Windows;

namespace botinok
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
        GlobalKeyboardHook gHook;
        private void delayscan(bool chet)
        {
            while (botrobit)
            {
                /*while (autospace && spacenazhat==false)
                {
                    var c = getColor(spaceX, spaceY, WindowHandle);
                    if (Int32.Parse(c[0])>200 && Int32.Parse(c[1])>100 && Int32.Parse(c[2])<100)
                    {
                        SendKeys.SendWait(" ");
                    }
                }*/
                // auto cool - Int32.Parse(c[0]) > 240 && Int32.Parse(c[1]) > 160 && Int32.Parse(c[2]) < 100
                // auto great - Int32.Parse(c[0]) > 240 && Int32.Parse(c[1]) > 100 && Int32.Parse(c[2]) < 100
                while (autospace && spacenazhat == false)
                {
                    var c = getColor(spaceX, spaceY, WindowHandle);
                    // R=169 G=127 B=49   Int32.Parse(c[0]) > 155 && Int32.Parse(c[1]) > 126 && Int32.Parse(c[2]) < 35
                    //R=229 G=148 B=29 Int32.Parse(c[0]) > 225 && Int32.Parse(c[1]) < 180 && Int32.Parse(c[2]) < 35
                    //Int32.Parse(c[0]) > 150 && Int32.Parse(c[1]) > 126 && Int32.Parse(c[2]) < 35
                    //(colid == "24215957" || colid == "24515130" || colid == "18513046" || colid == "23916471" || colid == "24515957" || colid == "24515443" || colid == "24515551" || colid == "24315442" || colid == "24215957" || colid == "24416160")
                    string colid = c[0] + c[1] + c[2];
                    if (Int32.Parse(c[0]) > 151 && Int32.Parse(c[1]) > 126 && Int32.Parse(c[2]) < 35)
                    {
                        //Thread.Sleep(5);
                        //SendKeys.SendWait(" ");
                        PressKey(Keys.Space, false);
                        Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                        PressKey(Keys.Space, true);
                        if (arrowdaunnadospace)
                        {
                            if (mnogookonnrezh)
                            {
                                if (countactshagov < (countshagov-1))
                                {
                                    countactshagov++;
                                    //    MessageBox.Show("[" + countactokna.ToString() + " / " + countwindowgame.ToString()+ "]" + countactshagov.ToString() + " / " + countshagov.ToString(), "ERROR");
                                }
                                else
                                {
                                    countactshagov = 0;
                                    hwidnewwindowget = false;
                                }
                            }
                            if (mnogookonnrezh)
                            {
                                if (countactokna == countwindowgame && countactshagov == countshagov)
                                {
                                    botrobit = false;
                                    button3_Click(this, null);
                                }
                            }
                            arrowdaunnadospace = false;
                        }
                    }
                }
                while (spacenazhat)
                {

                    System.Threading.Thread.Sleep(zaderzhdvizha);
                    if (mnogookonnrezh)
                    {
                        // nameprocess - название процесса
                        // countwindowgame
                        // countshagov 
                        // countactokna
                        // countactshagov
                        Process[] processes = Process.GetProcessesByName(nameprocess);
                        if (hwidnewwindowget == false) // countactokna == -1
                        {
                            if (countactokna < countwindowgame)
                            {
                                countactokna++;
                                IntPtr ZagWindow = processes[countactokna].MainWindowHandle;
                                var rectHW = GetWindowRectEx(ZagWindow);
                                var rectXYXY = GetWindowRectExXY(ZagWindow);
                                var rectCli = GetClientRectWiHe(ZagWindow);
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
                                x = windowX + borderLRB + 418;
                                y = windowY + borderT + 562;
                                spaceX = windowX + borderLRB + 759;
                                spaceY = windowY + borderT + 545;
                                hwidnewwindowget = true;
                                SetForegroundWindow(ZagWindow);
                            }
                        }
                      /*  if (countactshagov < countshagov+1)
                        {
                            countactshagov++;
                        }
                        else
                        {
                            countactshagov = 0;
                            hwidnewwindowget = false;
                        }
                        if (countactokna == countwindowgame)
                        {
                            botrobit = false;
                        }*/
                    }
                //spacenazhat = false;
                    if (forclean ==10 && screenPixel != null && gdest != null && gsrc != null)
                {
                        screenPixel.Dispose();
                        gdest.Dispose();
                        gsrc.Dispose();
                        screenPixel = null;
                        gdest = null;
                        gsrc = null;
                }
                screenPixel = new Bitmap(_x, _y);
                gdest = Graphics.FromImage(screenPixel);
                gsrc = Graphics.FromHwnd(WindowHandle);
                IntPtr hsrcdc = gsrc.GetHdc();
                IntPtr hdc = gdest.GetHdc();
                BitBlt(hdc, 0, 0, _x, _y, hsrcdc, x, y, (int)CopyPixelOperation.SourceCopy);
                if (gdest != null)
                {
                    gdest.ReleaseHdc();
                }
                if (gsrc != null)
                {
                    gsrc.ReleaseHdc();
                }
                int marginarrow = 0;
                int vsegoshagov = 0;
                if (chet)
                {
                    vsegoshagov = 12;
                }
                else
                {
                    vsegoshagov = 11;
                }

                for (int i = 0; i < vsegoshagov; i++)
                {
                    int plusmarg = 4; // 0;
                    if (vsegoshagov == 11)
                    {
                        plusmarg = 4;
                        if (i == 0)
                        {
                            marginarrow = 19;
                        }
                        switch (i)
                        {
                            case 1: marginarrow += widtharrow + plusmarg;
                                break;
                            case 2: marginarrow += widtharrow + plusmarg;
                                break;
                            case 3: marginarrow += widtharrow + plusmarg + 1;
                                break;
                            case 4: marginarrow += widtharrow + plusmarg;
                                break;
                            case 5: marginarrow += widtharrow + plusmarg;
                                break;
                            case 6: marginarrow += widtharrow + plusmarg + 1;
                                break;
                            case 7: marginarrow += widtharrow + plusmarg;
                                break;
                            case 8: marginarrow += widtharrow + plusmarg;
                                break;
                            case 9: marginarrow += widtharrow + plusmarg + 1;
                                break;
                            case 10: marginarrow += widtharrow + plusmarg;
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 1: marginarrow += widtharrow + plusmarg + 1;
                                break;
                            case 2: marginarrow += widtharrow + plusmarg;
                                break;
                            case 3: marginarrow += widtharrow + plusmarg;
                                break;
                            case 4: marginarrow += widtharrow + plusmarg + 1;
                                break;
                            case 5: marginarrow += widtharrow + plusmarg;
                                break;
                            case 6: marginarrow += widtharrow + plusmarg;
                                break;
                            case 7: marginarrow += widtharrow + plusmarg + 1;
                                break;
                            case 8: marginarrow += widtharrow + plusmarg;
                                break;
                            case 9: marginarrow += widtharrow + plusmarg;
                                break;
                            case 10: marginarrow += widtharrow + plusmarg + 1;
                                break;
                            case 11: marginarrow += widtharrow + plusmarg;
                                break;
                        }
                    }

                    obrez = new Bitmap(screenPixel).Clone(new Rectangle(marginarrow, 0, widtharrow, widtharrow), PixelFormat.Undefined);
                    bm = new Bitmap(obrez, obrez.Width, obrez.Height);
                    int z = 0;
                    bool itsblue = false;
                    bool itsgreen = false;
                    bool itsred = false;
                    string strelement = "";
                    switch (bm.GetPixel(1, 15).Name)
                    {
                        case "ffd61212": itsred = true;
                            break;
                        case "ffc90f0d": itsred = true;
                            break;
                        case "ffce1010": itsred = true;
                            break;
                        case "ffd31110": itsred = true;
                            break;
                        case "ffd91918": itsred = true;
                            break;
                        case "ffd41211": itsred = true;
                            break;
                        case "ffd41313": itsred = true;
                            break;
                        case "ff4a54d6": itsblue = true;
                            break;
                        case "ff4a5bd6": itsblue = true;
                            break;
                        case "ff4a56d6": itsblue = true;
                            break;
                        case "ff4a5ad5": itsblue = true;
                            break;
                        case "ff454cdb": itsblue = true;
                            break;
                        case "ff4a58d5": itsblue = true;
                            break;
                        case "ff4551db": itsblue = true;
                            break;
                        case "ff4a5ad6": itsblue = true;
                            break;
                        case "ff4a59d6": itsblue = true;
                            break;
                        case "ff4f00bd": itsblue = true;
                            break;
                        case "ff5200bd": itsblue = true;
                            break;
                        case "ff4d00bd": itsblue = true;
                            break;
                        case "ff4a55d6": itsblue = true;
                            break;
                        case "ff4a5bd5": itsblue = true;
                            break;
                    }
                    for (int ii = 0; ii < bm.Width; ii++)
                    {
                        for (int j = 0; j < bm.Height; j++)
                        {
                            if (bm.GetPixel(ii, j).Name.Equals("ffffffff"))
                            {
                                z++;
                            }
                        }
                        strelement += z;
                        z = 0;
                    }
                    obrez.Dispose();
                    obrez = null;
                    bm.Dispose();
                    bm = null;
                    string kudastrelka = "";

                    switch (strelement)
                    {
                        case "000000137811131517171514963333333333333333000000": kudastrelka = "Up";
                            break;
                        case "0000003578111315171716151064444444444444444000000": kudastrelka = "Up";
                            break;
                        case "0000002259111314171614141083333333333333334000000": kudastrelka = "Up";
                            break;
                        case "0000003358111315171715151084444444444444444000000": kudastrelka = "Up";
                            break;
                        case "0000001477101315171615161284444444444444444000000": kudastrelka = "Up";
                            break;
                        case "000000013555554222323644444310000000": kudastrelka = "Up";
                            break;
                        case "00000002554444522236554444310000000": kudastrelka = "Up";
                            break;
                        case "00000000255444452223655444431000000": kudastrelka = "Up";
                            break;
                        case "00000001355554452222534444310000000": kudastrelka = "Up";
                            break;
                        case "000000002554444522232244554421000000": kudastrelka = "Up";
                            break;
                        case "000000025544445222322445544210000000": kudastrelka = "Up";
                            break;
                        case "000000000134666656729303029665555542100000000": kudastrelka = "Right";
                            break;
                        case "000000000244665556728292928655554431100000000": kudastrelka = "Right";
                            break;
                        case "000000000134666556728303028665665542100000000": kudastrelka = "Right";
                            break;
                        case "000000000245666555727292927555554541100000000": kudastrelka = "Right";
                            break;
                        case "00003333333333333781111141311864200000": kudastrelka = "Right";
                            break;
                        case "00000333333333333338111114131086420000": kudastrelka = "Right";
                            break;
                        case "00003333333333333381111141310864200000": kudastrelka = "Right";
                            break;
                        case "00000333333333333381212131310864200000": kudastrelka = "Right";
                            break;
                        case "00000333333333333378111114131186420000": kudastrelka = "Right";
                            break;
                        case "00000333333333333381212131311864200000": kudastrelka = "Right";
                            break;
                        case "0000004443333333333333510141411171411119752000000": kudastrelka = "Down";
                            break;
                        case "0000004444444444444444512151413161512119742000000": kudastrelka = "Down";
                            break;
                        case "0000014444444444444444713151413171412119752000000": kudastrelka = "Down";
                            break;
                        case "0000034444444444444444812141616151512119752000000": kudastrelka = "Down";
                            break;
                        case "0000013333333333333333610141313171412119741000000": kudastrelka = "Down";
                            break;
                        case "000000023445552223235434345200000000": kudastrelka = "Down";
                            break;
                        case "000000002343545232322545444310000000": kudastrelka = "Down";
                            break;
                        case "00000002354555923226434444200000000": kudastrelka = "Down";
                            break;
                        case "00000000235455592322643444420000000": kudastrelka = "Down";
                            break;
                        case "000000002333445232322545545310000000": kudastrelka = "Down";
                            break;
                        case "000000002344555222323543434520000000": kudastrelka = "Down";
                            break;
                        case "000000001245566566283030287656666531000000000": kudastrelka = "Left";
                            break;
                        case "000000001245566566283030287655666431000000000": kudastrelka = "Left";
                            break;
                        case "000000001145455555272929277556666542000000000": kudastrelka = "Left";
                            break;
                        case "000000001145455555272929277555666542000000000": kudastrelka = "Left";
                            break;
                        case "00000246811131312118333333333333300000": kudastrelka = "Left";
                            break;
                        case "00002468111314111187333333333333300000": kudastrelka = "Left";
                            break;
                        case "00000246811131411118733333333333330000": kudastrelka = "Left";
                            break;
                        case "00000246811131312128333333333333300000": kudastrelka = "Left";
                            break;
                        case "000000001013136678777678644420000000": kudastrelka = "LeUp";
                            break;
                        case "0000000010131313678877667645531000000": kudastrelka = "LeUp";
                            break;
                        case "000000001313136678776688544310000000": kudastrelka = "LeUp";
                            break;
                        case "0000000013131311678765578855531000000": kudastrelka = "LeUp";
                            break;
                        case "0000000131313116787655788555310000000": kudastrelka = "LeUp";
                            break;
                        case "0000000101313136788776676455310000000": kudastrelka = "LeUp";
                            break;
                        case "000000002444777777787710141420000000": kudastrelka = "RiUp";
                            break;
                        case "000000002444777776788771314140000000": kudastrelka = "RiUp";
                            break;
                        case "000000002333777777787713141460000000": kudastrelka = "RiUp";
                            break;
                        case "000000002444777777778771314140000000": kudastrelka = "RiUp";
                            break;
                        case "000000024447777777787713141400000000": kudastrelka = "RiUp";
                            break;
                        case "000000024447777767887713141400000000": kudastrelka = "RiUp";
                            break;
                        case "000000001313116788777667444310000000": kudastrelka = "LeDo";
                            break;
                        case "000000001313137789777766533310000000": kudastrelka = "LeDo";
                            break;
                        case "000000001313126786666768555420000000": kudastrelka = "LeDo";
                            break;
                        case "000000001313137789666767544420000000": kudastrelka = "LeDo";
                            break;
                        case "000000013131377896667675444200000000": kudastrelka = "LeDo";
                            break;
                        case "000000013131377897777665333100000000": kudastrelka = "LeDo";
                            break;
                        case "00000001234468777787666131200000000": kudastrelka = "RiDo";
                            break;
                        case "000000023543688888876613131200000000": kudastrelka = "RiDo";
                            break;
                        case "000000002354368888887661313120000000": kudastrelka = "RiDo";
                            break;
                        case "000000013455777887787661313130000000": kudastrelka = "RiDo";
                            break;
                        case "000000024444776777876613131300000000": kudastrelka = "RiDo";
                            break;
                        case "000000134557778877876613131300000000": kudastrelka = "RiDo";
                            break;
                        case "000000000256644101310689972000000000": counthzarrow++;
                            break;
                        case "000000000045542612131077987100000000": counthzarrow++;
                            break;
                    }
                    if (kudastrelka == "")
                    {
                        countcicle++;
                    }
                    else
                    {
                        countcicle = 0;
                    }
                    if (itsblue)
                    {
                        col = "B";
                        arrowdown(col, kudastrelka);
                        countineng++;
                    }
                    else if (itsred)
                    {
                        col = "R";
                        arrowdown(col, kudastrelka);
                        countineng++;
                    }
                    else if (itsgreen)
                    {
                        col = "G";
                        arrowdown(col, kudastrelka);
                        countineng++;
                    }
                    myarr = null;
                    itsblue = false;
                    itsgreen = false;
                    itsred = false;
                    strelement = "";
                }
                if (countcicle > 7)
                {
                    countcicle = 0;
                    if (chet == false)
                    {
                        delayscan(true);
                    }
                    else
                    {
                        delayscan(false);
                    }
                }
                if (screenPixel != null && gdest != null && gsrc != null)
                {
                    screenPixel.Dispose();
                    gdest.Dispose();
                    gsrc.Dispose();
                    screenPixel = null;
                    gdest = null;
                    gsrc = null;
                }
                
                if (countineng > 0 && counthzarrow==0)
                {
                    spacenazhat = false;
                    arrowdaunnadospace = true;
                }
                else
                {
                    spacenazhat = true;
                }
                
                countineng = 0;
                counthzarrow = 0;
                forclean++;
                }
            System.Threading.Thread.Sleep(0);
         }
        }
        
        static void arrowdown(string colorarrow,string iarrow)
        {
            if (skokkey == 4)
            {
                if (speedofarrows == 1)
                {
                    // getzaderzhmezharrow = rnd.Next(msekot, msekdo);
                    // System.Threading.Thread.Sleep(getzaderzhmezharrow);
                    if (colorarrow == "B")
                    {
                        if (iarrow == "Left")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.Left, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.Left, true);
                        }
                        else if (iarrow == "Up")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.Up, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.Up, true);
                        }
                        else if (iarrow == "Right")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.Right, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.Right, true);
                        }
                        else if (iarrow == "Down")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.Down, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.Down, true);
                        }
                    }
                    else if (colorarrow == "R")
                    {
                        if (iarrow == "Left")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.Right, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.Right, true);
                        }
                        else if (iarrow == "Up")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.Down, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.Down, true);
                        }
                        else if (iarrow == "Right")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.Left, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.Left, true);
                        }
                        else if (iarrow == "Down")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.Up, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.Up, true);
                        }
                    }
                }
                else
                {
                    if (colorarrow == "B")
                    {
                        if (iarrow == "Left")
                        {
                            SendKeys.SendWait("{LEFT}");
                        }
                        else if (iarrow == "Up")
                        {
                            SendKeys.SendWait("{UP}");
                        }
                        else if (iarrow == "Right")
                        {
                            SendKeys.SendWait("{RIGHT}");
                        }
                        else if (iarrow == "Down")
                        {
                            SendKeys.SendWait("{DOWN}");
                        }
                    }
                    else if (colorarrow == "R")
                    {
                        if (iarrow == "Left")
                        {
                            SendKeys.SendWait("{RIGHT}");
                        }
                        else if (iarrow == "Up")
                        {
                            SendKeys.SendWait("{DOWN}");
                        }
                        else if (iarrow == "Right")
                        {
                            SendKeys.SendWait("{LEFT}");
                        }
                        else if (iarrow == "Down")
                        {
                            SendKeys.SendWait("{UP}");
                        }
                    }
                }
                colorarrow = "";
                iarrow = "";
            }
            else if (skokkey == 8)
            {
                if (speedofarrows == 1)
                {
                    // getzaderzhmezharrow = rnd.Next(msekot, msekdo);
                    // System.Threading.Thread.Sleep(getzaderzhmezharrow);
                    if (colorarrow == "B")
                    {
                        if (iarrow == "Left")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad4, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad4, true);
                        }
                        else if (iarrow == "Up")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad8, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad8, true);
                        }
                        else if (iarrow == "Right")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad6, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad6, true);
                        }
                        else if (iarrow == "Down")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad2, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad2, true);
                        }
                        else if (iarrow == "LeUp")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad7, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad7, true);
                        }
                        else if (iarrow == "RiUp")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad9, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad9, true);
                        }
                        else if (iarrow == "LeDo")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad1, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad1, true);
                        }
                        else if (iarrow == "RiDo")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad3, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad3, true);
                        }
                    }
                    else if (colorarrow == "R")
                    {
                        if (iarrow == "Left")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad6, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad6, true);
                        }
                        else if (iarrow == "Up")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad2, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad2, true);
                        }
                        else if (iarrow == "Right")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad4, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad4, true);
                        }
                        else if (iarrow == "Down")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad8, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad8, true);
                        }
                        else if (iarrow == "LeUp")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad3, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad3, true);
                        }
                        else if (iarrow == "RiUp")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad1, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad1, true);
                        }
                        else if (iarrow == "LeDo")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad9, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad9, true);
                        }
                        else if (iarrow == "RiDo")
                        {
                            Thread.Sleep(rnd.Next(msekot, msekdo));
                            PressKey(Keys.NumPad7, false);
                            Thread.Sleep(rnd.Next(upmsekot, upmsekdo));
                            PressKey(Keys.NumPad7, true);
                        }
                    }
                }
                else
                {
                    if (colorarrow == "B")
                    {
                        if (iarrow == "Left")
                        {
                            SendKeys.SendWait("{LEFT}");
                        }
                        else if (iarrow == "Up")
                        {
                            SendKeys.SendWait("{UP}");
                        }
                        else if (iarrow == "Right")
                        {
                            SendKeys.SendWait("{RIGHT}");
                        }
                        else if (iarrow == "Down")
                        {
                            SendKeys.SendWait("{DOWN}");
                        }
                        else if (iarrow == "LeUp")
                        {
                            PressKey(Keys.NumPad7, false);
                            PressKey(Keys.NumPad7, true);
                        }
                        else if (iarrow == "RiUp")
                        {
                            PressKey(Keys.NumPad9, false);
                            PressKey(Keys.NumPad9, true);
                        }
                        else if (iarrow == "LeDo")
                        {
                            PressKey(Keys.NumPad1, false);
                            PressKey(Keys.NumPad1, true);
                        }
                        else if (iarrow == "RiDo")
                        {
                            PressKey(Keys.NumPad3, false);
                            PressKey(Keys.NumPad3, true);
                        }
                    }
                    else if (colorarrow == "R")
                    {
                        if (iarrow == "Left")
                        {
                            SendKeys.SendWait("{RIGHT}");
                        }
                        else if (iarrow == "Up")
                        {
                            SendKeys.SendWait("{DOWN}");
                        }
                        else if (iarrow == "Right")
                        {
                            SendKeys.SendWait("{LEFT}");
                        }
                        else if (iarrow == "Down")
                        {
                            SendKeys.SendWait("{UP}");
                        }
                        else if (iarrow == "LeUp")
                        {
                            PressKey(Keys.NumPad3, false);
                            PressKey(Keys.NumPad3, true);
                        }
                        else if (iarrow == "RiUp")
                        {
                            PressKey(Keys.NumPad1, false);
                            PressKey(Keys.NumPad1, true);
                        }
                        else if (iarrow == "LeDo")
                        {
                            PressKey(Keys.NumPad9, false);
                            PressKey(Keys.NumPad9, true);
                        }
                        else if (iarrow == "RiDo")
                        {
                            PressKey(Keys.NumPad7, false);
                            PressKey(Keys.NumPad7, true);
                        }
                    }
                }
                colorarrow = "";
                iarrow = "";
            }
        }
        public static bool botbudetrabotat=false;
        private static string alf = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private static int zaderzhdvizha;

        private string shifruem(string shifr)
        {
            int m = alf.Length;
            int n = 1;
            int k = 20;
            string res = "";
            for (int i = 0; i < shifr.Length; i++)
            {
                /* поиск соответствующего символа в алфавите */
                for (int j = 0; j < alf.Length; j++)
                {
                    /* если символ найден */
                    if (shifr[i] == alf[j])
                    {
                        /* осуществляем сдвиг позиции символа в алфавите */
                        int temp = j * n + k;

                        /* берем остаток от деления сдвига на длину алфавита    */
                        /* ( чтобы индекс temp не выходил за пределы алфавита ) */
                        while (temp >= m)
                        {
                            temp -= m;
                        }
                        /* в закодированной строке - символ заменяется на "сдвинутый" */
                        res += alf[temp].ToString();
                    }
                }
            }
            return res;
        }
        public Form1()
        {
            InitializeComponent();
            groupBox5.Enabled = false;
            groupBox6.Enabled = false;

       /*     NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            textBox1.Text = sMacAddress;
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string hwid = "";
            foreach (ManagementObject mo in mbsList)
            {
                hwid = mo["ProcessorId"].ToString();
                break;
            }
            if (sMacAddress == "E0D55E4098B1" && hwid == "BFEBFBFF000906EA")
            {
                string shifmac = shifruem(sMacAddress);
                string shifhwid = shifruem(hwid);
                if (shifmac == "YTXOOYNTSRVK" && shifhwid == "VZYVZVZZTTTSTPYU")
                {
                    //ok
                    label3.Text = "user: °•Satan•°";
                }
            }
            else
            {
                Environment.Exit(0);
            }
            */
            
            
            /*
            string userscreenwidth = System.Windows.SystemParameters.PrimaryScreenWidth.ToString();
            string userscreenheight = System.Windows.SystemParameters.PrimaryScreenHeight.ToString();
            if (userscreenwidth == "1600" && userscreenheight == "900")
            {
                x = 523;
                y = 703;
                _x = 554;
                _y = 41;
                widtharrow = 41;
                myarr = new string[widtharrow];
            }
            else
            {
                MessageBox.Show("Код ошибки - 1001", "ERROR");
            }*/
        }
    
        private void mainfuncbbot()
        {

            if (x != 0 && y != 0 && _y != 0 && _x != 0)
            {
                delayscan(false);
            }
            else 
            {
                botrobit = false;
                MessageBox.Show("Код ошибки - 1003", "ERROR");
            }
        }
        private void elementsenable(bool thisenabledelmet)
        {
            if (thisenabledelmet)
            {
                groupBox1.Enabled = true;
                button3.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;
                button3.Enabled = true;
                button2.Enabled = false;
            }
        }
        private void changespeedofarrows(int speedarr)
        {
            speedofarrows = speedarr;
            if (speedarr == 1)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
            }
        }
    /*    public void MyKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                spacenazhat = true;
            }
            MessageBox.Show("Код ошибки - 1003", "ERROR");
        }*/
        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                spacenazhat = true;
                if (arrowdaunnadospace)
                {
                    if (mnogookonnrezh)
                    {
                        if (countactshagov < countshagov)
                        {
                            countactshagov++;
                            //    MessageBox.Show("[" + countactokna.ToString() + " / " + countwindowgame.ToString()+ "]" + countactshagov.ToString() + " / " + countshagov.ToString(), "ERROR");
                        }
                        else
                        {
                            countactshagov = 0;
                            hwidnewwindowget = false;
                        }
                    }
                    if (mnogookonnrezh)
                    {
                        if (countactokna == countwindowgame && countactshagov == countshagov)
                        {
                            botrobit = false;
                            button3_Click(this, null);
                        }
                    }
                    arrowdaunnadospace = false;
                }

                //gHook.unhook();
                //delayscan(false);
                //MessageBox.Show("Код ошибки - 1003", "space");
            }// MessageBox.Show(e.KeyCode.ToString(), "ERROR");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zaderzhdvizha = Int32.Parse(textBox3.Text);
            spacenazhat = true;
            gHook.hook();
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
            x = windowX + borderLRB + 418;
            y = windowY + borderT + 562;
            spaceX = windowX + borderLRB + 759;
            spaceY = windowY + borderT + 545;
            _x = 444;
            _y = 33;
            widtharrow = 33;
            if (windowWidth > 0 && windowHeight > 0 && windowX > 0 && windowY > 0 && window_X > 0 && window_Y > 0 && clientWidth > 0 && clientHeight > 0)
            {
                botbudetrabotat = true;
            }
            elementsenable(false);
            potok = new Thread(mainfuncbbot);
            botrobit = true;
            msekot = Int32.Parse(textBox1.Text);
            msekdo = Int32.Parse(textBox2.Text);
            upmsekot = Int32.Parse(textBox4.Text);
            upmsekdo = Int32.Parse(textBox5.Text);
            countwindowgame = ((int)numericUpDown1.Value)-1;
            countshagov = (int)numericUpDown2.Value;
            potok.Start();   
        }

        private void button3_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("bot off", "ERROR");
            gHook.unhook();
            botrobit = false;
            if (screenPixel != null)
            {
                screenPixel.Dispose();
            }
            if (obrez != null)
            {
                obrez.Dispose();
            }
            if (bm != null)
            {
                bm.Dispose();
            }
                screenPixel = null;
                gdest = null;
                gsrc = null;
                obrez = null;
                bm = null;
                countcicle = 0;
                    myarr = null;
                itsblue = false;
                itsgreen = false;
                itsred = false;
                hwidnewwindowget = false;

            countactokna=-1;
            countwindowgame=0;
            countactshagov=0;
            countshagov=0;
            if (potok != null)  //thrdStart - глобальная ссылка на тред
                    if (potok.IsAlive)
                        potok.Abort();
            elementsenable(true);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            changespeedofarrows(0);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            changespeedofarrows(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "80";
            textBox2.Text = "120";
            textBox1.Text = "23";
            textBox2.Text = "160";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook();
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
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

       

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            autospace = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            autospace = true;
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
        
        void TestProc(string kudastrelka)
        { 
            if (kudastrelka == "Left")
            {
                Thread.Sleep(rnd.Next(msekot, msekdo));
                PressKey(Keys.Left, false);
                Thread.Sleep(rnd.Next(msekot, msekdo));
                PressKey(Keys.Left, true);
            }
            if (kudastrelka == "Up")
            {
                Thread.Sleep(rnd.Next(msekot, msekdo));
                PressKey(Keys.Up, false);
                Thread.Sleep(rnd.Next(msekot, msekdo));
                PressKey(Keys.Up, true);
            }
            if (kudastrelka == "Right")
            {
                Thread.Sleep(rnd.Next(msekot, msekdo));
                PressKey(Keys.Right, false);
                Thread.Sleep(rnd.Next(msekot, msekdo));
                PressKey(Keys.Right, true);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://vk.com/qq324");
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            mnogookonnrezh = false;
            groupBox5.Enabled = false;
            groupBox6.Enabled = false;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            mnogookonnrezh = true;
            groupBox5.Enabled = true;
            groupBox6.Enabled = true;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            nameprocess = "DanceGameClient-Win32-Shipping";
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            nameprocess = "DanceGameClient-Win64-Shipping";
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            skokkey = 4;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            skokkey = 8;
        }

    }
}
