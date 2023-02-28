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
            Rectangle rectangle = new Rectangle(10, 10, 150, 150);
            graphics.DrawEllipse(pen, rectangle);

            // Nastavení středu hodin
            int centerX = rectangle.X + rectangle.Width / 2;
            int centerY = rectangle.Y + rectangle.Height / 2;

            // Vykreslení ciferníku
            for (int i = 1; i <= 12; i++)
            {
                double angle = (i * 30) * Math.PI / 180;
                int x = (int)(centerX + (rectangle.Width / 2 - 25) * Math.Sin(angle));
                int y = (int)(centerY - (rectangle.Height / 2 - 25) * Math.Cos(angle));
                graphics.DrawString(i.ToString(), Font, Brushes.Black, new PointF(x, y));
            }

            // Nastavení délky ruček hodin
            int secondHandLength = 70;
            int minuteHandLength = 50;
            int hourHandLength = 30;

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
    }
}
