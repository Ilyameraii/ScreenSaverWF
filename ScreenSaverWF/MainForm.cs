namespace ScreenSaverWF
{
    public partial class MainForm : Form
    {
        private Random rand = new Random();
        private Image snowflakeImage;
        private List<Snowflake> snowflakes = new List<Snowflake>();
        public MainForm()
        {
            InitializeComponent();

            KeyPreview = true; // перехватывает нажатие клавиш
                               // Включаем двойную буферизацию — чтобы не было мерцания!
            DoubleBuffered = true;

            snowflakeImage = Properties.Resources.flake;

            CreateSnowflakes(150); // создаём 150 снежинок
            timer.Interval = 1000 / 120;
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
                    Size = this.ClientSize.Width / 100,
                    Speed = 10,
                });
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            foreach (var flake in snowflakes)
            {
                flake.Y += flake.Speed;
                // Если улетела вниз - телепортируем наверх
                if (flake.Y > this.ClientSize.Height + flake.Size)
                {
                    flake.Y = -flake.Size;
                    flake.X = rand.Next(-flake.Size, this.ClientSize.Width + flake.Size);
                }
            }

            // Перерисовываем форму
            Invalidate(); // вызывает Paint

            timer.Start();
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
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Close();
        }
    }
}
