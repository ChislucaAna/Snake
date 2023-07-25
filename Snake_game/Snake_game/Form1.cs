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

namespace Snake_game
{
    public partial class Form1 : Form
    {
        bool inceput = false;
        int xb, yb, xp, yp;
        int index = 0;
        int xnew, ynew;

        public Form1()
        {
            InitializeComponent();
        }

        public class Bulina
        {
            public int x;
            public int y;
            public string dir;

            public Bulina(int posx, int posy, string directie)
            {
                x = posx;
                y = posy;
                dir = directie;
            }
        }

        Bulina[] Sarpe = new Bulina[100];

        public class Papa
        {
            public int x;
            public int y;

            public Papa(int posx, int posy)
            {
                x = posx;
                y = posy;
            }
        }

        Papa food;

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //prostie
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //control
            timer1.Start();
            inceput = true;
            button1.Enabled = false;

            //snake head
            Random rnd = new Random();
            xb = rnd.Next(100, pictureBox1.Width - 100);
            yb = rnd.Next(100, pictureBox1.Height / 2);
            Bulina cap = new Bulina(xb, yb, "jos");
            Sarpe[index] = cap;
            index++;

            //food
            xp = rnd.Next(100, pictureBox1.Width - 100);
            yp = rnd.Next(pictureBox1.Height / 2, pictureBox1.Height - 100);
            food = new Papa(xp, yp);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i <= 99; i++)
            {
                if (Sarpe[i] != null)
                {
                    if (Sarpe[i].dir == "sus")
                    {
                        Sarpe[i].y = Sarpe[i].y - 30;
                    }
                    else if (Sarpe[i].dir == "jos")
                    {
                        Sarpe[i].y = Sarpe[i].y + 30;
                    }
                    else if (Sarpe[i].dir == "stanga")
                    {
                        Sarpe[i].x = Sarpe[i].x - 30;
                    }
                    else if (Sarpe[i].dir == "dreapta")
                    {
                        Sarpe[i].x = Sarpe[i].x + 30;
                    }
                }
            }

            for (int i = 99; i >= 1; i--)
            {
                if (Sarpe[i] != null)
                {
                    Sarpe[i].dir = Sarpe[i - 1].dir;
                }
            }

            //verifici daca se intersecteaza capul cu mancarea
            Rectangle cap = new Rectangle(Sarpe[0].x, Sarpe[0].y, 30, 30);
            Rectangle papa = new Rectangle(food.x, food.y, 30, 30);

            if (cap.IntersectsWith(papa))
            {
                //generezi mancarea in alta parte
                Random rnd = new Random();
                food.x = rnd.Next(100, pictureBox1.Width - 100);
                food.y = rnd.Next(100, pictureBox1.Height - 100);

                //adaugi bucatica noua la snake
                if (Sarpe[index - 1].dir == "stanga")
                {
                    xnew = Sarpe[index - 1].x + 30;
                    ynew = Sarpe[index - 1].y;
                }
                else if (Sarpe[index - 1].dir == "dreapta")
                {
                    xnew = Sarpe[index - 1].x - 30;
                    ynew = Sarpe[index - 1].y;
                }
                else if (Sarpe[index - 1].dir == "sus")
                {
                    xnew = Sarpe[index - 1].x;
                    ynew = Sarpe[index - 1].y + 30;
                }
                else if (Sarpe[index - 1].dir == "jos")
                {
                    xnew = Sarpe[index - 1].x;
                    ynew = Sarpe[index - 1].y - 30;
                }
                Bulina newbulina = new Bulina(xnew, ynew, Sarpe[index - 1].dir);
                Sarpe[index] = newbulina;
                index++;
            }

            //verifici daca capul se intersecteaza cu marginea pictureBoxului
            if (Sarpe[0].x < 10 || Sarpe[0].x > pictureBox1.Width-10 || Sarpe[0].y > pictureBox1.Height-10 || Sarpe[0].y < 10)
            {
                Application.Exit();
            }

            pictureBox1.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            if (e.KeyCode == Keys.A)
            {
                Sarpe[0].dir = "stanga";
            }
            else if (e.KeyCode == Keys.W)
            {
                Sarpe[0].dir = "sus";
            }
            else if (e.KeyCode == Keys.S)
            {
                Sarpe[0].dir = "jos";
            }
            else if (e.KeyCode == Keys.D)
            {
                Sarpe[0].dir = "dreapta";
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //desenezi capul cu alb
            if (inceput == true)
            {
                Rectangle base1 = new Rectangle(Sarpe[0].x, Sarpe[0].y, 30, 30);
                SolidBrush brush1 = new SolidBrush(Color.White);
                e.Graphics.FillEllipse(brush1, base1);

                //desenezi corpul cu verde
                for (int i = 1; i <= 99; i++)
                {
                    if (Sarpe[i] != null)
                    {
                        Rectangle base2 = new Rectangle(Sarpe[i].x, Sarpe[i].y, 30, 30);
                        SolidBrush brush2 = new SolidBrush(Color.Green);
                        e.Graphics.FillEllipse(brush2, base2);
                    }
                }

                //desenezi mancarea cu rosu
                Rectangle base3 = new Rectangle(food.x, food.y, 30, 30);
                SolidBrush brush3 = new SolidBrush(Color.Red);
                e.Graphics.FillEllipse(brush3, base3);
            }
        }

    }
}
