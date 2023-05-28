using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Diomond_squ
{
    public partial class Form1 : Form
    {
        int[,] array = new int[513, 513];
        public Form1()
        {
            InitializeComponent();
            Initialize(array);
            R_start();
            DoDiamondSquare(array,R);
        }
        private float R = 0.5f;
        private int _MAXINDEX = 512;
        private int step = 512;
        private Random rnd = new Random();    

        private void R_start()
        {
            AllocConsole();
            R = float.Parse(Console.ReadLine());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        private void Initialize(int[,] W)
        {
            W[0, 0] = 100;
            W[_MAXINDEX-1, 0] = 200;
            W[0, _MAXINDEX-1] = 300;
            W[_MAXINDEX - 1, _MAXINDEX - 1] = 100;
        }
        public int r(int range, float S)
        {
            return (int)((rnd.NextDouble() * 2 - 1) * range * S);
        }
        private void DoDiamondSquare(int[,] W, float S)
        {
            int hs, x, y;
            int A, B, C, D;


            while (step > 1)
            {
                hs = step / 2;
                for (y = hs; y < _MAXINDEX; y += step)
                    for (x = hs; x < _MAXINDEX; x += step)
                    {
                        A = W[x - hs, y - hs];
                        B = W[x - hs, y + hs];
                        C = W[x + hs, y - hs];
                        D = W[x + hs, y + hs];

                        W[x, y] = ((A + B + C + D) / 4) + r(hs, S);
                    }

                for (int i = 0; i < _MAXINDEX; i += hs)
                    for (int j = (i + hs) % step; j < _MAXINDEX; j += step)
                    {
                        int x1, x2,y1,y2;
                        x1 = (i - hs) % _MAXINDEX;
                        x2 = (i + hs) % _MAXINDEX;
                        y1 = (j - hs) % _MAXINDEX;
                        y2 = (j + hs) % _MAXINDEX;

                        if (x1< 0)
                        {
                            x1 = (_MAXINDEX + x1) % _MAXINDEX;
                        }
                        if (y1< 0)
                        {
                            y1 = (_MAXINDEX + y1) % _MAXINDEX;
                        }
                        A = W[x1, j];
                        B = W[x2, j];
                        C = W[i, y1];
                        D = W[i, y2];
                        W[i, j] = (A + B + C + D) / 4 + r(hs, S);
                    }
                step = hs;
            }

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            for (int i = 0; i < _MAXINDEX; i++)
            {
                for (int j = 0; j < _MAXINDEX; j++)
                {
                    e.Graphics.DrawLine(Check_color(array[i, j]), i, j, i + 1, j + 1);
                }
            }
        }
        private Pen Check_color(int a)
        {
            if (a < 10)
            {
                Pen pen = new Pen(Color.Blue);
                return pen; 
            }
            if (a < 25)
            {
                Pen pen = new Pen(Color.GreenYellow);
                return pen;
            }
            if (a < 50)
            {
                Pen pen = new Pen(Color.Green);
                return pen;
            }
            if (a < 60)
            {
                Pen pen = new Pen(Color.DarkGreen);
                return pen;
            }
            else
            {
                Pen pen = new Pen(Color.White);
                return pen;
            }
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }

}
