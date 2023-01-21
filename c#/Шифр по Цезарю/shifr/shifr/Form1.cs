using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shifr
{
    public partial class Шифратор : Form
    {
        private static string alf = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public Шифратор()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string shifr = textBox1.Text;
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
            textBox2.Text = res;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string rasshifr = textBox3.Text;
            int m = alf.Length;
            int n = 1;
            int k = 20;
            string res = "";
            for (int i = 0; i < rasshifr.Length; i++)
            {
                /* поиск соответствующего символа в алфавите */
                for (int j = 0; j < alf.Length; j++)
                {
                    /* если символ найден */
                    if (rasshifr[i] == alf[j])
                    {
                        /* осуществляем "обратный" сдвиг позиции символа в алфавите */
                        int temp =(j - k) * n;
 
                        /* берем остаток от деления сдвига на длину алфавита    */
                        /* ( чтобы индекс temp не выходил за пределы алфавита ) */
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
            textBox4.Text = res;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
        }
    }
}
