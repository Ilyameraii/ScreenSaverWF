using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaverWF
{
    public partial class MainForm : Form
    {

        private Random rand = new Random(); // объявлен один раз
        private Image snowflakeImage;
        private List<Snowflake> snowflakes = new List<Snowflake>();
        public MainForm()
        {
            InitializeComponent();


            // Включаем двойную буферизацию — чтобы не было мерцания!
            this.DoubleBuffered = true;

            snowflakeImage = Properties.Resources.snowflake;

            CreateSnowflakes(20); // создаём 20 снежинок
            timer.Interval = 8;
            timer.Start();

        }
        private void CreateSnowflakes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                snowflakes.Add(new Snowflake
                {
                    X = rand.Next(-50, this.ClientSize.Width + 50),
                    Y = rand.Next(-this.ClientSize.Height, 0),
                    Size = this.ClientSize.Width / 20,  // от 2 до 10 пикселей
                    Speed = 3,
                });
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (var flake in snowflakes)
            {
                flake.Y += flake.Speed;
                // Если улетела вниз — телепортируем наверх
                if (flake.Y > this.ClientSize.Height + flake.Size)
                {
                    flake.Y = -flake.Size;
                    flake.X = rand.Next(-flake.Size, this.ClientSize.Width + flake.Size);
                }
            }

            // Перерисовываем форму
            this.Invalidate(); // вызывает Paint
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            foreach (var flake in snowflakes)
            {
                if (snowflakeImage != null)
                {
                    g.DrawImage(snowflakeImage,
                        new Rectangle((int)flake.X, (int)flake.Y, (int)flake.Size, (int)flake.Size));
                }
            }
        }
    }
}
