using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;

namespace MyScreenshoter
{

    public partial class Form1 : Form
    {
        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(
            [In()] System.IntPtr hdc, int x, int y, int cx, int cy,
            [In()] System.IntPtr hdcSrc, int x1, int y1, uint rop);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        static IntPtr WindowHandle;
        static int countcicle=0;
        static bool botrobit = false;
        static bool testscan = false;
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out Rectangl lpRect);
        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangl lpRect);

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

        private static bool gloscan = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, System.EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "DanceGameClient-Win32-Shipping";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "DanceGameClient-Win64-Shipping";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var title = textBox1.Text;
            Process[] processes = Process.GetProcessesByName(title);
            // processes.Length 1 - нашел, 0 - не нашел
            if (processes.Length == 0)
            {
                label1.Text = "Процесс не найден";
                label2.Text = "";
                label3.Text = "";
                label1.ForeColor = Color.FromArgb(255, 240, 0, 0);
            }
            else
            {
                label1.Text = "Процесс обнаружен";
                label4.Text = processes.Length.ToString();
                var kolvoproc = Convert.ToInt32(label4.Text);
                //numericUpDown1.MaximumSize = kolvoproc;
                label1.ForeColor = Color.FromArgb(255, 0, 240, 0);
                label2.Text = "      ID процесса: " + processes[0].Id;
                label3.Text = "Название процесса: " + processes[0].MainWindowTitle;
                WindowHandle = processes[0].MainWindowHandle;
                IntPtr hWnd = processes[0].MainWindowHandle;
                // переключается к процессу
                //SetForegroundWindow(WindowHandle);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //SetForegroundWindow(WindowHandle);
            botrobit = true;
            delayscreen(false);
        }
        private void checkprocess()
        {
          /*  var title = textBox1.Text;
            Process[] processes = Process.GetProcessesByName(title);
            int smesh = Convert.ToInt32(numericUpDown1.Value);
            label2.Text = "      ID процесса: " + processes[smesh].Id;
            label3.Text = "Название процесса: " + processes[smesh].MainWindowTitle;
            IntPtr hWnd = processes[smesh].MainWindowHandle;
           // RECT rct = new RECT();
            //GetWindowRect(hWnd, ref rct);

            label5.Text = Top.ToString();
            label6.Text = Left.ToString();
            label7.Text = Bottom.ToString();
            label8.Text = Right.ToString();*/

        }
        static void arrowdown(string colorarrow,string iarrow)
        {
            if (colorarrow=="B") 
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
            else if (colorarrow=="R") 
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
        static void delayscreen(bool chet)
        {
            while (botrobit)
            {
                // 1600x900
                int x = 523; //+22 +2
                int y = 703;

                int _x = 554;
                int _y = 41; //50


                Bitmap screenPixel = new Bitmap(_x, _y);
                Graphics gdest = Graphics.FromImage(screenPixel);
                Graphics gsrc = Graphics.FromHwnd(WindowHandle);
                IntPtr hsrcdc = gsrc.GetHdc();
                IntPtr hdc = gdest.GetHdc();
                BitBlt(hdc, 0, 0, _x, _y, hsrcdc, x, y, (int)CopyPixelOperation.SourceCopy);
                gdest.ReleaseHdc();
                gsrc.ReleaseHdc();
                int widtharrow = 41;
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
                    /* if (i!=0 || i!=3 || i!=6 || i!=9){
                         marginarrow=marginarrow+widtharrow+5;
                     }else{
                         marginarrow=marginarrow+widtharrow+6;
                     }*/
                    int plusmarg = 0;
                    if (vsegoshagov == 11)
                    {
                        plusmarg = 24;
                        if (i == 0)
                        {
                            marginarrow = 24;
                        }
                    }
                    switch (i)
                    {
                        case 1: marginarrow = 46 + plusmarg;
                            break;
                        case 2: marginarrow = 93 + plusmarg;
                            break;
                        case 3: marginarrow = 140 + plusmarg;
                            break;
                        case 4: marginarrow = 186 + plusmarg;
                            break;
                        case 5: marginarrow = 233 + plusmarg;
                            break;
                        case 6: marginarrow = 279 + plusmarg;
                            break;
                        case 7: marginarrow = 326 + plusmarg;
                            break;
                        case 8: marginarrow = 373 + plusmarg;
                            break;
                        case 9: marginarrow = 419 + plusmarg;
                            break;
                        case 10: marginarrow = 466 + plusmarg;
                            break;
                        case 11: marginarrow = 513 + plusmarg;
                            break;
                    }

                    Bitmap obrez = new Bitmap(screenPixel).Clone(new Rectangle(marginarrow, 0, widtharrow, widtharrow), PixelFormat.Undefined);
                    // obrez.Save(String.Format(@"D:\screens\{0}.png", i + 1), ImageFormat.Jpeg);
                    Bitmap bm = new Bitmap(obrez, obrez.Width, obrez.Height);

                    
                    string[] myarr = new string[41];
                    for (int ii = 0; ii < bm.Width; ii++)
                    {
                        for (int j = 0; j < bm.Height; j++)
                        {
                            int jj = j;

                            if (bm.GetPixel(ii, j).Name.Equals("ffffffff"))
                            {
                                //альфа-канал = 255. R = 0, G = 0, B = 0
                                myarr[j] += "█";
                            }
                            else if (bm.GetPixel(ii, j).Name.Equals("ffc10c0c") || bm.GetPixel(ii, j).Name.Equals("ffd21210") || bm.GetPixel(ii, j).Name.Equals("ffc20000") || bm.GetPixel(ii, j).Name.Equals("ffc60000"))
                            {
                                //R ffcb0505
                                myarr[j] += "R";
                            }
                            else if (bm.GetPixel(ii, j).Name.Equals("ff008310") || bm.GetPixel(ii, j).Name.Equals("ff077a10"))
                            {
                                //g ff00c710
                                myarr[j] += "G";
                            }
                            else if (bm.GetPixel(ii, j).Name.Equals("ff4a45ce") || bm.GetPixel(ii, j).Name.Equals("ff4a18c3"))
                            {//b ff4a3cd6 ff4a22c7
                                myarr[j] += "B";
                            }
                            else
                            {
                                myarr[j] += "0";
                            }
                        }

                    }

                    obrez.Dispose();
                    obrez = null;
                    bm.Dispose();
                    bm = null;
                    int z = 0;
                    bool itsblue = false;
                    bool itsgreen = false;
                    bool itsred = false;
                    string strelement = "";
                    for (int opi = 0; opi < myarr.Length; opi++)
                    {
                        for (int iii = 0; iii < myarr[opi].Length; iii++)
                        {
                            if (myarr[opi][iii] == 'B')
                            {
                                itsblue = true;
                            }
                            else if (myarr[opi][iii] == 'G')
                            {
                                itsgreen = true;
                            }
                            else if (myarr[opi][iii] == 'R')
                            {
                                itsred = true;
                            }
                            if (myarr[opi][iii] == '█')
                            {
                                z++;
                            }
                        }
                        strelement += z;
                        z = 0;
                    }
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
                        case "000000000134666656729303029665555542100000000": kudastrelka = "Right";
                            break;
                        case "000000000244665556728292928655554431100000000": kudastrelka = "Right";
                            break;
                        case "000000000134666556728303028665665542100000000": kudastrelka = "Right";
                            break;
                        case "000000000245666555727292927555554541100000000": kudastrelka = "Right";
                            break;
                        case "00000333333333333381212131310864200000": kudastrelka = "Right";
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
                        case "000000002343545232322545444310000000": kudastrelka = "Down";
                            break;
                        case "00000002354555923226434444200000000": kudastrelka = "Down";
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
                    }
                    if (kudastrelka == "")
                    {
                        countcicle++;
                    }
                    else
                    {
                        countcicle = 0;
                    }
                    if (testscan)
                    {
                         StreamWriter str = new StreamWriter(string.Format(@"D:\screens\arrow{0}.txt", i));
                         for (int ji = 0; ji < myarr.Length; ji++)
                         {
                             str.WriteLine(myarr[ji]);
                         }

                         str.WriteLine("шагов - " + vsegoshagov + " чет? - " + chet + " не обнаружено - " + countcicle);
                         str.WriteLine();
                         if (itsblue)
                         {
                             str.WriteLine("Синяя");
                         }
                         else if (itsred)
                         {
                             str.WriteLine("Красная");
                         }
                         else if (itsgreen)
                         {
                             str.WriteLine("Зеленая");
                         }
                         str.WriteLine(strelement);
                         if (kudastrelka != "")
                         {
                             str.WriteLine(kudastrelka);
                         }
                         str.Close();
                         testscan = false;
                    }
                    if (itsblue)
                    {
                        //str.WriteLine("Синяя");
                        string col = "B";
                        arrowdown(col, kudastrelka);
                    }
                    else if (itsred)
                    {
                        //str.WriteLine("Красная");
                        string col = "R";
                        arrowdown(col, kudastrelka);
                    }
                    else if (itsgreen)
                    {
                        //str.WriteLine("Зеленая");
                        string col = "G";
                        arrowdown(col, kudastrelka);
                    }
                    itsblue = false;
                    itsgreen = false;
                    itsred = false;
                    /*str.WriteLine(strelement);
                    if (kudastrelka != "")
                    {
                        str.WriteLine(kudastrelka);
                    }
                    str.Close();*/
                    strelement = "";

                }
                if (countcicle > 7)
                {
                    countcicle = 0;
                    if (chet == false)
                    {
                        delayscreen(true);
                    }
                    else
                    {
                        delayscreen(false);
                    }
                }
            screenPixel.Dispose();
            screenPixel = null;
            }
           // screenPixel.Save(@"D:\screens\screen.jpg", ImageFormat.Jpeg);
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            checkprocess();
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            botrobit = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
             // 1600x900
                int x = 0; //+22 +2
                int y = 0;

                int _x = 1280;
                int _y = 720; //50
                int widtharrow = 33; //41;

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
                // _x = windowX + borderLRB;
                // _y = windowY + borderT;
                x = windowX + borderLRB + 418;
                y = windowY + borderT + 562;
                _x = 444;
                _y = 33;

                Bitmap screenPixel = new Bitmap(_x, _y);
                Graphics gdest = Graphics.FromImage(screenPixel);
                Graphics gsrc = Graphics.FromHwnd(WindowHandle);
                IntPtr hsrcdc = gsrc.GetHdc();
                IntPtr hdc = gdest.GetHdc();
                BitBlt(hdc, 0, 0, _x, _y, hsrcdc, x, y, (int)CopyPixelOperation.SourceCopy);
                gdest.ReleaseHdc();
                gsrc.ReleaseHdc();
                int marginarrow = 0;
                int vsegoshagov = 0;
                    vsegoshagov = 11;
                    screenPixel.Save(@"D:\screens\screen.jpg", ImageFormat.Jpeg);
                for (int i = 0; i < vsegoshagov; i++)
                {
                    /* if (i!=0 || i!=3 || i!=6 || i!=9){
                         marginarrow=marginarrow+widtharrow+5;
                     }else{
                         marginarrow=marginarrow+widtharrow+6;
                     }*/
                    int plusmarg = 4;
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
                    

                    Bitmap obrez = new Bitmap(screenPixel).Clone(new Rectangle(marginarrow, 0, widtharrow, widtharrow), PixelFormat.Undefined);
                     obrez.Save(String.Format(@"D:\screens\{0}.png", i + 1), ImageFormat.Jpeg);
                    Bitmap bm = new Bitmap(obrez, obrez.Width, obrez.Height);

                    int z = 0;
                    bool itsblue = false;
                    bool itsgreen = false;
                    bool itsred = false;
                    string strelement = "";

                    string[] myarr = new string[widtharrow];

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
                    }

                    for (int ii = 0; ii < bm.Width; ii++)
                    {
                        for (int j = 0; j < bm.Height; j++)
                        {
                            int jj = j;

                            if (bm.GetPixel(ii, j).Name.Equals("ffffffff"))
                            {
                                //альфа-канал = 255. R = 0, G = 0, B = 0
                                myarr[j] += "█";
                                z++;
                            }
                          /*  else if (bm.GetPixel(ii, j).Name.Equals("ffd61212") || bm.GetPixel(ii, j).Name.Equals("ffc90f0d") || bm.GetPixel(ii, j).Name.Equals("ffce1010") || bm.GetPixel(ii, j).Name.Equals("ffd31110") || bm.GetPixel(ii, j).Name.Equals("ffd91918"))
                            {
                                myarr[j] += "R";
                                itsred = true;
                            }
                            else if (bm.GetPixel(ii, j).Name.Equals("ff4a54d6") || bm.GetPixel(ii, j).Name.Equals("ff4a5bd6") || bm.GetPixel(ii, j).Name.Equals("ff4a56d6") || bm.GetPixel(ii, j).Name.Equals("ff4a5ad5") || bm.GetPixel(ii, j).Name.Equals("ff454cdb"))
                            {
                                myarr[j] += "B";
                                itsblue = true;
                            }*/
                            /*else if (bm.GetPixel(ii, j).Name.Equals("ff4a45ce") || bm.GetPixel(ii, j).Name.Equals("ff4a18c3"))
                            {//b ff4a3cd6 ff4a22c7
                                myarr[j] += "B";
                            }*/
                            else
                            {
                                myarr[j] += bm.GetPixel(ii, j).Name; // bm.GetPixel(ii, j).Name;//"0";
                            }
                        }
                        strelement += z;
                        z = 0;
                    }
                    /*
                    for (int opi = 0; opi < myarr.Length; opi++)
                    {
                        for (int iii = 0; iii < myarr[opi].Length; iii++)
                        {
                            if (myarr[opi][iii] == 'B')
                            {
                                itsblue = true;
                            }
                            else if (myarr[opi][iii] == 'G')
                            {
                                itsgreen = true;
                            }
                            else if (myarr[opi][iii] == 'R')
                            {
                                itsred = true;
                            }
                            if (myarr[opi][iii] == '█')
                            {
                                z++;
                            }
                        }
                        strelement += z;
                        z = 0;
                    }*/
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
                        case "00000001234468777787666131200000000": kudastrelka = "RiDo";
                            break;
                        case "000000002354368888887661313120000000": kudastrelka = "RiDo";
                            break;
                        case "000000013455777887787661313130000000": kudastrelka = "RiDo";
                            break;
                        case "000000024444776777876613131300000000": kudastrelka = "RiDo";
                            break;
                        case "000000134557778877876613131300000000": kudastrelka = "RiDo";
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
                         StreamWriter str = new StreamWriter(string.Format(@"D:\screens\arrow{0}.txt", i));
                         for (int ji = 0; ji < myarr.Length; ji++)
                         {
                             str.WriteLine(myarr[ji]);
                         }

                         str.WriteLine(strelement);
                         str.WriteLine("шагов - " + vsegoshagov + " чет? - *" + " не обнаружено - " + countcicle);
                         str.WriteLine();
                    if (itsblue)
                    {
                        str.WriteLine("Синяя");
                        string col = "B";
                        arrowdown(col, kudastrelka);
                    }
                    else if (itsred)
                    {
                        str.WriteLine("Красная");
                        string col = "R";
                        arrowdown(col, kudastrelka);
                    }
                    else if (itsgreen)
                    {
                        str.WriteLine("Зеленая");
                        string col = "G";
                        arrowdown(col, kudastrelka);
                    }
                    if (strelement[5]<4 && strelement[6]<4 && strelement[7]<4 && strelement[9]<4 && strelement[12]<4 && strelement[15]<4 && strelement[30]>4)
                    {
                        kudastrelka="Right";
                    }
                    else if (strelement[12] > 4 && (strelement[20] > 4 || strelement[21] > 4))
                    {
                        kudastrelka = "Down";
                    }
                    else if (strelement[9] >= 4 && strelement[10] >= 4 && strelement[11] >= 4)
                    {
                        kudastrelka = "Up";
                    }
                    else if ((strelement[7]>6 || strelement[8]>6 || strelement[9]>6) && (strelement[17]>5 || strelement[18]>5 || strelement[19]>5))
                    {
                        kudastrelka = "Left";
                    }
                    itsblue = false;
                    itsgreen = false;
                    itsred = false;
                    str.WriteLine(strelement); 
                    str.WriteLine(bm.GetPixel(1, 15).Name);
                    if (kudastrelka != "")
                    {
                        str.WriteLine(kudastrelka);
                    }
                    str.Close();
                    strelement = "";

                }
                
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int x = 950; //793 //950 //938
            int y = 670; //669
            int _x = 1; //217 //1 //25
            int _y = 1; //25 //23
            Bitmap spacepix = new Bitmap(_x, _y);
            Graphics gdest = Graphics.FromImage(spacepix);
            Graphics gsrc = Graphics.FromHwnd(WindowHandle);
            IntPtr hsrcdc = gsrc.GetHdc();
            IntPtr hdc = gdest.GetHdc();
            BitBlt(hdc, 0, 0, _x, _y, hsrcdc, x, y, (int)CopyPixelOperation.SourceCopy);
            gdest.ReleaseHdc();
            gsrc.ReleaseHdc();
            spacepix.Save(@"D:\screens\spacebar.jpg", ImageFormat.Jpeg);
            string[] myarr = new string[25];
            for (int ii = 0; ii < spacepix.Width; ii++)
            {
                for (int j = 0; j < spacepix.Height; j++)
                {
                    int jj = j;

                    if (spacepix.GetPixel(ii, j).Name.Equals("ffebeef5") || spacepix.GetPixel(ii, j).Name.Equals("ffeeedf4") || spacepix.GetPixel(ii, j).Name.Equals("ff7f835b"))
                    {
                        //альфа-канал = 255. R = 0, G = 0, B = 0
                        myarr[j] += "█";
                    }
                    else if (spacepix.GetPixel(ii, j).Name.Equals("ff877854"))
                    {
                        //R ffcb0505
                        myarr[j] += "S";
                    }
                    else
                    {
                        myarr[j] += spacepix.GetPixel(ii, j).Name;
                    }
                }

            }
            StreamWriter str = new StreamWriter(string.Format(@"D:\screens\spacebar.txt"));
            for (int ji = 0; ji < myarr.Length; ji++)
            {
                str.WriteLine(myarr[ji]);
            }
            str.Close();
        }
        private static bool autospace = false;
        private void button8_Click(object sender, EventArgs e)
        {
            autospace = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            autospace = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            while (autospace)
            {
                int sx = 938; //793
                int sy = 670;
                int _sx = 25; //217
                int _sy = 25;
                Bitmap spacepix = new Bitmap(_sx, _sy);
                Graphics sgdest = Graphics.FromImage(spacepix);
                Graphics sgsrc = Graphics.FromHwnd(WindowHandle);
                IntPtr shsrcdc = sgsrc.GetHdc();
                IntPtr shdc = sgdest.GetHdc();
                BitBlt(shdc, 0, 0, _sx, _sy, shsrcdc, sx, sy, (int)CopyPixelOperation.SourceCopy);
                sgdest.ReleaseHdc();
                sgsrc.ReleaseHdc();
                if (spacepix.GetPixel(12, 0).Name.Equals("ff028fba"))
                {
                    if (spacepix.GetPixel(12, 12).Name.Equals("fff59532"))
                    {
                        if (spacepix.GetPixel(12, 12).Name.Equals("ff018fbb"))
                        {
                            SendKeys.SendWait(" ");
                        }
                    }
                }
                if (spacepix.GetPixel(12, 12).Name.Equals("fff59532"))
                {
                    
                        SendKeys.SendWait(" ");
                    
                }
                
                spacepix.Dispose();
                spacepix = null;
                //string[] smyarr = new string[41];
                /*if (spacepix.GetPixel(157, 1).Name.Equals("a69766"))
                {
                    SendKeys.SendWait("{SPACE}");
                }*/
                //int shokrug = 0;
                /*for (int j = 0; j < spacepix.Height; j++)
                {

                    if (j == 0 && spacepix.GetPixel(0, j).Name.Equals("ff120a2d"))
                    {
                        shokrug += 1;
                    }
                    if (j == 1 && spacepix.GetPixel(0, j).Name.Equals("ffbbc2c7"))
                    {
                        shokrug += 1;
                    }
                    if (j == 2 && spacepix.GetPixel(0, j).Name.Equals("ffe5cb6e"))
                    {
                        shokrug += 1;
                    }
                    if (j == 3 && spacepix.GetPixel(0, j).Name.Equals("fff7d679"))
                    {
                        shokrug += 1;
                    }
                    if (j == 4 && spacepix.GetPixel(0, j).Name.Equals("fffdd97d"))
                    {
                        shokrug += 1;
                    }
                    if (j == 5 && spacepix.GetPixel(0, j).Name.Equals("fffdd575"))
                    {
                        shokrug += 1;
                    }
                    if (j == 6 && spacepix.GetPixel(0, j).Name.Equals("fffdcd6f"))
                    {
                        shokrug += 1;
                    }
                    if (j == 7 && spacepix.GetPixel(0, j).Name.Equals("fffdc869"))
                    {
                        shokrug += 1;
                    }
                    if (j == 8 && spacepix.GetPixel(0, j).Name.Equals("fffdc265"))
                    {
                        shokrug += 1;
                    }
                    if (j == 9 && spacepix.GetPixel(0, j).Name.Equals("fff9bb62"))
                    {
                        shokrug += 1;
                    }
                    if (j == 10 && spacepix.GetPixel(0, j).Name.Equals("fff6b65e"))
                    {
                        shokrug += 1;
                    }
                    if (j == 11 && spacepix.GetPixel(0, j).Name.Equals("fff8af5a"))
                    {
                        shokrug += 1;
                    }
                    if (j == 12 && spacepix.GetPixel(0, j).Name.Equals("fff7ab58"))
                    {
                        shokrug += 1;
                    }
                    if (j == 13 && spacepix.GetPixel(0, j).Name.Equals("fff59f4e"))
                    {
                        shokrug += 1;
                    }
                    if (j == 14 && spacepix.GetPixel(0, j).Name.Equals("fff1913a"))
                    {
                        shokrug += 1;
                    }
                    if (j == 15 && spacepix.GetPixel(0, j).Name.Equals("ffed8428"))
                    {
                        shokrug += 1;
                    }
                    if (j == 16 && spacepix.GetPixel(0, j).Name.Equals("ffed7618"))
                    {
                        shokrug += 1;
                    }
                    if (j == 17 && spacepix.GetPixel(0, j).Name.Equals("ffed7617"))
                    {
                        shokrug += 1;
                    }
                    if (j == 18 && spacepix.GetPixel(0, j).Name.Equals("ffec6c17"))
                    {
                        shokrug += 1;
                    }
                    if (j == 19 && spacepix.GetPixel(0, j).Name.Equals("ffeb6917"))
                    {
                        shokrug += 1;
                    }
                    if (j == 20 && spacepix.GetPixel(0, j).Name.Equals("ffe75f17"))
                    {
                        shokrug += 1;
                    }
                    if (j == 21 && spacepix.GetPixel(0, j).Name.Equals("ffe65c17"))
                    {
                        shokrug += 1;
                    }
                    if (j == 22 && spacepix.GetPixel(0, j).Name.Equals("ffdb5e21"))
                    {
                        shokrug += 1;
                    }
                    if (j == 23 && spacepix.GetPixel(0, j).Name.Equals("ffadb8bd"))
                    {
                        shokrug += 1;
                    }
                    if (j == 24 && spacepix.GetPixel(0, j).Name.Equals("ff130a2d"))
                    {
                        shokrug += 1;
                    }
                }*/
                /*for (int j = 0; j < spacepix.Width; j++)
                {
                    for (int jj = 0; jj < spacepix.Height; jj++)
                    {
                        if (spacepix.GetPixel(j, jj).Name.Equals("ffa9a49b") || spacepix.GetPixel(j, jj).Name.Equals("ff9c9170") || spacepix.GetPixel(j, jj).Name.Equals("ff9a9170") || spacepix.GetPixel(j, jj).Name.Equals("ffc4c2c7") || spacepix.GetPixel(j, jj).Name.Equals("ffaa9665") || spacepix.GetPixel(j, jj).Name.Equals("ffb1a272") || spacepix.GetPixel(j, jj).Name.Equals("ffa9a49b") || spacepix.GetPixel(j, jj).Name.Equals("ffacaaae") || spacepix.GetPixel(j, jj).Name.Equals("ff9c9170"))
                        {
                            SendKeys.SendWait(" ");
                        }
                    }
                }*/
                /* if (shokrug == 25)
                 {ffac9a6c
                     SendKeys.SendWait(" ");
                 }
                 else if (shokrug > 2)//8
                 {
                     SendKeys.SendWait(" ");
                 }
                 textBox1.Text = shokrug.ToString();*/
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // 1600x900
            int x = 0; //+22 +2
            int y = 0;

            int _x = 1280;
            int _y = 720; //50
            int widtharrow = 33; //41;

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
            // _x = windowX + borderLRB;
            // _y = windowY + borderT;
            x = windowX + borderLRB + 418;
            y = windowY + borderT + 562;
            _x = 444;
            _y = 33;

            Bitmap screenPixel = new Bitmap(_x, _y);
            Graphics gdest = Graphics.FromImage(screenPixel);
            Graphics gsrc = Graphics.FromHwnd(WindowHandle);
            IntPtr hsrcdc = gsrc.GetHdc();
            IntPtr hdc = gdest.GetHdc();
            BitBlt(hdc, 0, 0, _x, _y, hsrcdc, x, y, (int)CopyPixelOperation.SourceCopy);
            gdest.ReleaseHdc();
            gsrc.ReleaseHdc();
            int marginarrow = 0;
            int vsegoshagov = 0;
            vsegoshagov = 12;
            screenPixel.Save(@"D:\screens\screen.jpg", ImageFormat.Jpeg);
            for (int i = 0; i < vsegoshagov; i++)
            {
                /* if (i!=0 || i!=3 || i!=6 || i!=9){
                     marginarrow=marginarrow+widtharrow+5;
                 }else{
                     marginarrow=marginarrow+widtharrow+6;
                 }*/
                int plusmarg = 4;
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


                Bitmap obrez = new Bitmap(screenPixel).Clone(new Rectangle(marginarrow, 0, widtharrow, widtharrow), PixelFormat.Undefined);
                obrez.Save(String.Format(@"D:\screens\{0}.png", i + 1), ImageFormat.Jpeg);
                Bitmap bm = new Bitmap(obrez, obrez.Width, obrez.Height);

                int z = 0;
                bool itsblue = false;
                bool itsgreen = false;
                bool itsred = false;
                string strelement = "";

                string[] myarr = new string[widtharrow];

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
                }

                for (int ii = 0; ii < bm.Width; ii++)
                {
                    for (int j = 0; j < bm.Height; j++)
                    {
                        int jj = j;

                        if (bm.GetPixel(ii, j).Name.Equals("ffffffff"))
                        {
                            //альфа-канал = 255. R = 0, G = 0, B = 0
                            myarr[j] += "█";
                            z++;
                        }
                        /*  else if (bm.GetPixel(ii, j).Name.Equals("ffd61212") || bm.GetPixel(ii, j).Name.Equals("ffc90f0d") || bm.GetPixel(ii, j).Name.Equals("ffce1010") || bm.GetPixel(ii, j).Name.Equals("ffd31110") || bm.GetPixel(ii, j).Name.Equals("ffd91918"))
                          {
                              myarr[j] += "R";
                              itsred = true;
                          }
                          else if (bm.GetPixel(ii, j).Name.Equals("ff4a54d6") || bm.GetPixel(ii, j).Name.Equals("ff4a5bd6") || bm.GetPixel(ii, j).Name.Equals("ff4a56d6") || bm.GetPixel(ii, j).Name.Equals("ff4a5ad5") || bm.GetPixel(ii, j).Name.Equals("ff454cdb"))
                          {
                              myarr[j] += "B";
                              itsblue = true;
                          }*/
                        /*else if (bm.GetPixel(ii, j).Name.Equals("ff4a45ce") || bm.GetPixel(ii, j).Name.Equals("ff4a18c3"))
                        {//b ff4a3cd6 ff4a22c7
                            myarr[j] += "B";
                        }*/
                        else
                        {
                            myarr[j] += bm.GetPixel(ii, j).Name; // bm.GetPixel(ii, j).Name;//"0";
                        }
                    }
                    strelement += z;
                    z = 0;
                }
                /*
                for (int opi = 0; opi < myarr.Length; opi++)
                {
                    for (int iii = 0; iii < myarr[opi].Length; iii++)
                    {
                        if (myarr[opi][iii] == 'B')
                        {
                            itsblue = true;
                        }
                        else if (myarr[opi][iii] == 'G')
                        {
                            itsgreen = true;
                        }
                        else if (myarr[opi][iii] == 'R')
                        {
                            itsred = true;
                        }
                        if (myarr[opi][iii] == '█')
                        {
                            z++;
                        }
                    }
                    strelement += z;
                    z = 0;
                }*/
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
                    case "00000001234468777787666131200000000": kudastrelka = "RiDo";
                        break;
                    case "000000002354368888887661313120000000": kudastrelka = "RiDo";
                        break;
                    case "000000013455777887787661313130000000": kudastrelka = "RiDo";
                        break;
                    case "000000024444776777876613131300000000": kudastrelka = "RiDo";
                        break;
                    case "000000134557778877876613131300000000": kudastrelka = "RiDo";
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
                StreamWriter str = new StreamWriter(string.Format(@"D:\screens\arrow{0}.txt", i));
                for (int ji = 0; ji < myarr.Length; ji++)
                {
                    str.WriteLine(myarr[ji]);
                }

                str.WriteLine(strelement);
                str.WriteLine("шагов - " + vsegoshagov + " чет? - *" + " не обнаружено - " + countcicle);
                str.WriteLine();
                if (strelement[5] < 4 && strelement[6] < 4 && strelement[7] < 4 && strelement[9] < 4 && strelement[12] < 4 && strelement[15] < 4 && strelement[30] > 4)
                {
                    kudastrelka = "Right";
                }
                else if (strelement[12] > 4 && (strelement[20] > 4 || strelement[21] > 4))
                {
                    kudastrelka = "Down";
                }
                else if (strelement[9] >= 4 && strelement[10] >= 4 && strelement[11] >= 4)
                {
                    kudastrelka = "Up";
                }
                else if ((strelement[7] > 6 || strelement[8] > 6 || strelement[9] > 6) && (strelement[17] > 5 || strelement[18] > 5 || strelement[19] > 5))
                {
                    kudastrelka = "Left";
                }
                if (itsblue)
                {
                    str.WriteLine("Синяя");
                    string col = "B";
                    arrowdown(col, kudastrelka);
                }
                else if (itsred)
                {
                    str.WriteLine("Красная");
                    string col = "R";
                    arrowdown(col, kudastrelka);
                }
                else if (itsgreen)
                {
                    str.WriteLine("Зеленая");
                    string col = "G";
                    arrowdown(col, kudastrelka);
                }
                itsblue = false;
                itsgreen = false;
                itsred = false;
                str.WriteLine(strelement);
                str.WriteLine(bm.GetPixel(1, 15).Name);
                if (kudastrelka != "")
                {
                    str.WriteLine(kudastrelka);
                }
                str.Close();
                strelement = "";

            }
                
        }



















        

        
        private static int countinmass=500;
        private static int nachcountms=0;
        private static string[] glomass = new string[countinmass];

        private void button15_Click(object sender, EventArgs e)
        {
            glomass = null;
            glomass = new string[countinmass];
            nachcountms=0;
        }
        private void button13_Click(object sender, EventArgs e)
        {
            // 1600x900
            int x = 0; //+22 +2
            int y = 0;

            int _x = 1280;
            int _y = 720; //50
            int widtharrow = 33; //41;

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
            // _x = windowX + borderLRB;
            // _y = windowY + borderT;
            x = windowX + borderLRB + 418;
            y = windowY + borderT + 562;
            _x = 444;
            _y = 33;

            Bitmap screenPixel = new Bitmap(_x, _y);
            Graphics gdest = Graphics.FromImage(screenPixel);
            Graphics gsrc = Graphics.FromHwnd(WindowHandle);
            IntPtr hsrcdc = gsrc.GetHdc();
            IntPtr hdc = gdest.GetHdc();
            BitBlt(hdc, 0, 0, _x, _y, hsrcdc, x, y, (int)CopyPixelOperation.SourceCopy);

            //screenPixel.Save(@"D:\screens\screen.jpg", ImageFormat.Jpeg);
            gdest.ReleaseHdc();
            gsrc.ReleaseHdc();
            int marginarrow = 0;
            int vsegoshagov = 0;
            vsegoshagov = 11;
            for (int i = 0; i < vsegoshagov; i++)
            {
                int plusmarg = 4;
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


                Bitmap obrez = new Bitmap(screenPixel).Clone(new Rectangle(marginarrow, 0, widtharrow, widtharrow), PixelFormat.Undefined);
                Bitmap bm = new Bitmap(obrez, obrez.Width, obrez.Height);
                obrez.Save(String.Format(@"D:\screens\{0}.png", nachcountms), ImageFormat.Jpeg);
                int z = 0;
                string strelement = "";
                int whip = 0;
                for (int ii = 0; ii < bm.Width; ii++)
                {
                    for (int j = 0; j < bm.Height; j++)
                    {
                        int jj = j;

                        if (bm.GetPixel(ii, j).Name.Equals("ffffffff"))
                        {
                            z++;
                            whip++;
                        }
                    }
                    strelement += z;
                    z = 0;
                }
                string inglomass = "["+nachcountms+"] "+bm.GetPixel(1, 15).Name + " - " + strelement;
                glomass[nachcountms] = inglomass;
                nachcountms++;
                strelement = "";
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            // 1600x900
            int x = 0; //+22 +2
            int y = 0;

            int _x = 1280;
            int _y = 720; //50
            int widtharrow = 33; //41;

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
            // _x = windowX + borderLRB;
            // _y = windowY + borderT;
            x = windowX + borderLRB + 418;
            y = windowY + borderT + 562;
            _x = 444;
            _y = 33;

            Bitmap screenPixel = new Bitmap(_x, _y);
            Graphics gdest = Graphics.FromImage(screenPixel);
            Graphics gsrc = Graphics.FromHwnd(WindowHandle);
            IntPtr hsrcdc = gsrc.GetHdc();
            IntPtr hdc = gdest.GetHdc();
            BitBlt(hdc, 0, 0, _x, _y, hsrcdc, x, y, (int)CopyPixelOperation.SourceCopy);
            gdest.ReleaseHdc();
            gsrc.ReleaseHdc();

           // screenPixel.Save(@"D:\screens\screen.jpg", ImageFormat.Jpeg);
            int marginarrow = 0;
            int vsegoshagov = 0;
            vsegoshagov = 12;
            for (int i = 0; i < vsegoshagov; i++)
            {
                int plusmarg = 4;
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
                Bitmap obrez = new Bitmap(screenPixel).Clone(new Rectangle(marginarrow, 0, widtharrow, widtharrow), PixelFormat.Undefined);
                Bitmap bm = new Bitmap(obrez, obrez.Width, obrez.Height);
                obrez.Save(String.Format(@"D:\screens\{0}.png", nachcountms), ImageFormat.Jpeg);
                int z = 0;
                string strelement = "";
                int whip = 0;
                for (int ii = 0; ii < bm.Width; ii++)
                {
                    for (int j = 0; j < bm.Height; j++)
                    {
                        int jj = j;

                        if (bm.GetPixel(ii, j).Name.Equals("ffffffff"))
                        {
                            z++;
                            whip++;
                        }
                    }
                    strelement += z;
                    z = 0;
                }
                string inglomass = "[" + nachcountms + "] " + bm.GetPixel(1, 15).Name + " - " + strelement;
                glomass[nachcountms] = inglomass;
                nachcountms++;
                strelement = "";
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            StreamWriter str = new StreamWriter(string.Format(@"D:\screens\gloarr.txt"));
            for (int ji = 0; ji < glomass.Length; ji++)
            {
                if (glomass[ji] == "")
                {
                    break;
                }
                else
                {
                    str.WriteLine(glomass[ji]);
                }
            }
            str.WriteLine();
            str.Close();
        }

        
    }
}
