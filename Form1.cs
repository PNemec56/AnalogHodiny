using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalogHodiny
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Vytvoření Timeru a nastavení intervalu na 1 sekundu
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            // Přiřazení obslužné metody události Paint PictureBoxu
            pictureBox1.Paint += new PaintEventHandler(DrawClock);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate(); // Překreslení PictureBoxu
        }

        private void DrawClock(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black, 3);

            // Získání velikosti PictureBox
            int pictureBoxWidth = pictureBox1.Width;
            int pictureBoxHeight = pictureBox1.Height;

            // Nastavení velikosti a pozice obdélníku
            int rectSize = Math.Min(pictureBoxWidth, pictureBoxHeight) - 40;
            int rectX = (pictureBoxWidth - rectSize) / 2;
            int rectY = (pictureBoxHeight - rectSize) / 2;
            Rectangle rectangle = new Rectangle(rectX, rectY, rectSize, rectSize);

            // Vykreslení ciferníku
            graphics.DrawEllipse(pen, rectangle);

            // Nastavení středu hodin
            int centerX = rectX + rectSize / 2;
            int centerY = rectY + rectSize / 2;

            for (int i = 1; i <= 60; i++)
            {
                double angle = (i * 6) * Math.PI / 180;
                int x1 = (int)(centerX + (rectSize / 2 - 5) * Math.Sin(angle));
                int y1 = (int)(centerY - (rectSize / 2 - 5) * Math.Cos(angle));
                int x2 = (int)(centerX + (rectSize / 2 - (i % 5 == 0 ? 20 : 15)) * Math.Sin(angle));
                int y2 = (int)(centerY - (rectSize / 2 - (i % 5 == 0 ? 20 : 15)) * Math.Cos(angle));

                if (i % 5 == 0)
                {
                    // Každá pátá čárka bude delší a zvýrazněna
                    graphics.DrawLine(new Pen(Brushes.Black, 3), new Point(x1, y1), new Point(x2, y2));
                }
                else
                {
                    // Ostatní čárky budou mít normální styl
                    graphics.DrawLine(new Pen(Brushes.Black,0), new Point(x1, y1), new Point(x2, y2));
                }

                if (i % 5 == 0)
                {
                    int number = i / 5;
                    double numberAngle = (i * 6) * Math.PI / 180;
                    int numberX = (int)(centerX + (rectSize / 2 - 30) * Math.Sin(numberAngle));
                    int numberY = (int)(centerY - (rectSize / 2 - 30) * Math.Cos(numberAngle));
                    graphics.DrawString(number.ToString(), Font, Brushes.Black, new PointF(numberX, numberY));
                }
            }

            // Nastavení délky ruček hodin
            int secondHandLength = rectSize / 2;
            int minuteHandLength = rectSize / 3;
            int hourHandLength = rectSize / 4;

            // Získání aktuálního času
            DateTime time = DateTime.Now;

            // Vykreslení vteřinové ručičky
            int secondAngle = time.Second * 6;
            int secondHandX = (int)(centerX + secondHandLength * Math.Sin(secondAngle * (Math.PI / 180)));
            int secondHandY = (int)(centerY - secondHandLength * Math.Cos(secondAngle * (Math.PI / 180)));
            graphics.DrawLine(pen, centerX, centerY, secondHandX, secondHandY);

            // Vykreslení minutové ručičky
            int minuteAngle = time.Minute * 6;
            int minuteHandX = (int)(centerX + minuteHandLength * Math.Sin(minuteAngle * (Math.PI / 180)));
            int minuteHandY = (int)(centerY - minuteHandLength * Math.Cos(minuteAngle * (Math.PI / 180)));
            graphics.DrawLine(pen, centerX, centerY, minuteHandX, minuteHandY);

            // Vykreslení hodinové ručičky
            int hourAngle = (time.Hour % 12) * 30 + time.Minute / 2;
            int hourHandX = (int)(centerX + hourHandLength * Math.Sin(hourAngle * (Math.PI / 180)));
            int hourHandY = (int)(centerY - hourHandLength * Math.Cos(hourAngle * (Math.PI / 180)));
            graphics.DrawLine(pen, centerX, centerY, hourHandX, hourHandY);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
