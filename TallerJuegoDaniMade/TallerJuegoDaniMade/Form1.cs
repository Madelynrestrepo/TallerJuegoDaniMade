using System.Windows.Forms.VisualStyles;

namespace TallerJuegoDaniMade
{
    public partial class Form1 : Form
    {
        bool goleft;
        bool goRight;
        bool isGamerOver;

        int puntaje;
        int ballx;
        int bally;
        int playerSpeed;

        Random rnd = new Random();

        PictureBox[] blockArray;

        public Form1()
        {
            InitializeComponent();
            blockArray = new PictureBox[15]; // Inicialización de blockArray
            PlaceBlocks();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Código que se ejecutará cuando el formulario se cargue
            setupGame();
        }

        private void setupGame()
        {
            isGamerOver = false;
            puntaje = 0;
            ballx = 5;
            bally = 5;
            playerSpeed = 12;
            txtPuntaje.Text = "Puntaje:" + puntaje;

            ball.Left = 375;
            ball.Top = 328;

            player.Left = 347;

            gameTIme.Start();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }
        }

        private void gameOver(string message)
        {
            isGamerOver = true;
            gameTIme.Stop();

            txtPuntaje.Text = "Puntaje: " + puntaje + " " + message;
        }

        private void PlaceBlocks()
        {
            blockArray = new PictureBox[15]; // Inicialización de blockArray en el constructor ya no es necesaria aquí
            int a = 0;

            int top = 50;
            int left = 100;

            for (int i = 0; i < blockArray.Length; i++)
            {
                blockArray[i] = new PictureBox();
                blockArray[i].Height = 32;
                blockArray[i].Width = 100;
                blockArray[i].Tag = "blocks";
                blockArray[i].BackColor = Color.White;

                if (a == 5)
                {
                    top += 50;
                    left = 100;
                    a = 0;
                }
                if (a < 5)
                {
                    a++;
                    blockArray[i].Left = left;
                    blockArray[i].Top = top;
                    this.Controls.Add(blockArray[i]);
                    left += 130;
                }
            }

            setupGame();
        }

        private void RemoveBlocks()
        {
            foreach (PictureBox x in blockArray)
            {
                this.Controls.Remove(x);
            }
        }

        private void mainGamerTimerEvent(object sender, EventArgs e)
        {
            txtPuntaje.Text = "Puntaje: " + puntaje;

            if (goleft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left < 688)
            {
                player.Left += playerSpeed;
            }

            ball.Left += ballx;
            ball.Top += bally;

            if (ball.Left < 0 || ball.Left > 775)
            {
                ballx = -ballx;
            }
            if (ball.Top < 0)
            {
                bally = -bally;
            }
            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                ball.Top = 469;
                bally = rnd.Next(5, 12) * -1;

                if (ballx < 0)
                {
                    ballx = rnd.Next(5, 12) * -1;
                }
                else
                {
                    ballx = rnd.Next(5, 12);
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        puntaje += 1;

                        bally = -bally;

                        this.Controls.Remove(x);
                    }
                }
            }

            if (puntaje == 15)
            {
                gameOver("¡GANASTE! Por favor, presiona Enter para volver a empezar");
            }

            if (ball.Top > 580)
            {
                gameOver("¡PERDISTE! Por favor, presiona Enter para volver a empezar");
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Enter && isGamerOver == true)
            {
                RemoveBlocks();
                PlaceBlocks();
            }
        }

        private void ball_Click(object sender, EventArgs e)
        {

        }
    }
}
