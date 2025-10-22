using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5_7
{
    public partial class Form1 : Form
    {
        private const int N = 100000000;
        private int[] Array = new int[N+1];
        private const int CYCLES = 4000000;
        private const int CYCLES_ORDERED = 100;
        public Form1()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            
            //наполнение упорядоченного массива
            int OrderedElem = 0;
            for (int i = 0; i < N; i++)
            {
                OrderedElem += rnd.Next(0, 5);
                Array[i] = OrderedElem;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            int Key = Decimal.ToInt32(KeyUD.Value);


            // неоптимальный бинарный поиск
            {
                int Index = -1;

                int LeftBoder;
                int RightBoder;
                int i;

                int StartTime = Environment.TickCount;
                for (int cycle = 0; cycle < CYCLES; cycle++)
                {
                    LeftBoder = 0;
                    RightBoder = N - 1;
                    while (RightBoder >= LeftBoder)
                    {
                        i = (LeftBoder + RightBoder) / 2;
                        if (Key == Array[i])
                        {
                            Index = i;
                            break;
                        }
                        if (Key < Array[i]) RightBoder = i - 1;
                        else LeftBoder = i + 1;
                    }
                }
                int ResultTime = Environment.TickCount - StartTime;

                if (Index != -1) UnoptimalIndex.Text = Index.ToString();
                else UnoptimalIndex.Text = "Не найдено";

                UnoptimalTime.Text = ResultTime.ToString();
            }
        }
    }
}
