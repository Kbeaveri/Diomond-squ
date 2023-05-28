using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diomond_squ
{
    public partial class Form1 : Form
    {
        int[,] array = new int[513, 513];
        public Form1()
        {
            InitializeComponent();
            Initialize(array);
            DoDiamondSquare(array,0.5F);
        }

        private int _MAXINDEX = 512;
        private Random rnd = new Random();
        private const int _HIGHRANGE = 1000;  // max high difference will be this value * Entrophy    

        // prepare work array
        private void Initialize(int[,] W)
        {
            // Let's fill in corners
            W[0, 0] = rnd.Next() % _HIGHRANGE;
            W[_MAXINDEX, 0] = rnd.Next() % _HIGHRANGE;
            W[0, _MAXINDEX] = rnd.Next() % _HIGHRANGE;
            W[_MAXINDEX, _MAXINDEX] = rnd.Next() % _HIGHRANGE;
        }
        public int r(int range, float S)
        {
            return (int)((rnd.NextDouble() * 2 - 1) * range * S);
        }
        private void DoDiamondSquare(int[,] W, float S)
        {
            int hs, x, y;
            int A, B, C, D, M, n;

            // Lets iterate through side size until size is too small
            for (int it = _MAXINDEX; it > 1; it /= 2)
            {
                hs = it / 2;

                //Midpoints
                for (y = hs; y < _MAXINDEX; y += it)
                    for (x = hs; x < _MAXINDEX; x += it)
                    {
                        A = W[x - hs, y - hs];
                        B = W[x - hs, y + hs];
                        C = W[x + hs, y - hs];
                        D = W[x + hs, y + hs];

                        W[x, y] = ((A + B + C + D) / 4) + r(hs, S);
                    }

                // Going through each square point                
                for (y = 0; y < _MAXINDEX + 1; y += hs)
                    for (x = y % it == 0 ? hs : 0; x < _MAXINDEX + 1; x += it) // getting offset of x in function of y 
                    {
                        M = n = 0; // Sum and denominator

                        // this way we can calculate border points
                        try { M += W[x + hs, y]; n++; } catch (Exception) { }
                        try { M += W[x - hs, y]; n++; } catch (Exception) { }
                        try { M += W[x, y + hs]; n++; } catch (Exception) { }
                        try { M += W[x, y - hs]; n++; } catch (Exception) { }

                        // lets average sum plus random value
                        W[x, y] = M / n + r(hs, S) / 2;
                    }
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
            if (a < 100)
            {
                Pen pen = new Pen(Color.Blue);
                return pen; 
            }
            if (a < 300)
            {
                Pen pen = new Pen(Color.Blue);
                return pen;
            }
            if (a < 500)
            {
                Pen pen = new Pen(Color.Black);
                return pen;
            }
            if (a < 700)
            {
                Pen pen = new Pen(Color.Tomato);
                return pen;
            }
            else
            {
                Pen pen = new Pen(Color.White);
                return pen;
            }
        }
    }
}
