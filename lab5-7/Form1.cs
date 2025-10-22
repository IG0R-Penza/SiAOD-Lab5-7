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

            // Оптимальный поиск
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
                    while (RightBoder > LeftBoder)
                    {
                        i = (LeftBoder + RightBoder) / 2;
                        if (Key <= Array[i]) RightBoder = i;
                        else LeftBoder = i + 1;
                    }
                    if (Key == Array[RightBoder]) Index = RightBoder;
                }
                int ResultTime = Environment.TickCount - StartTime;

                if (Index != -1) OptimalIndex.Text = Index.ToString();
                else OptimalIndex.Text = "Не найдено";

                OptimalTime.Text = ResultTime.ToString();
            }
            
            //последовательный поиск
            {
                int Index = 0;
                int StartTime = Environment.TickCount;
                Array[N] = Key + 1;
                for (int cycle = 0; cycle < CYCLES_ORDERED; cycle++)
                {
                    Index = 0;
                    while (Key > Array[Index]) Index++;
                }
                int ResultTime = (Environment.TickCount - StartTime) * (CYCLES / CYCLES_ORDERED);

                if (Key == Array[Index]) SequentialInOrderedIndex.Text = Index.ToString();
                else SequentialInOrderedIndex.Text = "Не найдено";

                SequentialInOrderedTime.Text = ResultTime.ToString();
        }
    }
}
