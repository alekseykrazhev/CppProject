using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pyatnashki
{
    class Game : Form1
    {
        const int Side = 4;
        const int Numb = Side * Side;
        int NumbX = Side - 1, NumbY = Side - 1;
        int PrNumbX, PrNumbY;
        Button[,] Buttons;
        int[,] Field;
        int[,] PrField = new int[Side, Side];
        int Moves;
        bool IsRunning;
        Random random = new Random();
        int[] Directions = new int[200];

        Label clock = new Label();
        Timer timer = new Timer();

        TimeSpan time;

        public Game()
        {
            ClientSize = new Size(Side * 50 + 20, Side * 50 + 50);
            BackColor = Color.Azure;
            Buttons = new Button[Side, Side];
            Field = new int[Side, Side];

            MenuItem NewGame = new MenuItem("Start New Game", new EventHandler(OnStartGame), Shortcut.F1);
            MenuItem RepeatGame = new MenuItem("Repeat Game", new EventHandler(OnRepeat), Shortcut.F2);
            MenuItem Exit = new MenuItem("Exit", new EventHandler(OnMenuExit), Shortcut.F3);
            MenuItem mGame = new MenuItem("Fifteen", new MenuItem[] { NewGame, RepeatGame, Exit });

            Menu = new MainMenu(new MenuItem[] { mGame });
            IsRunning = false;
            //label1.Text = "0";
            timer.Interval = 1000;
            timer.Tick += new EventHandler(OnTimer);

            void OnTimer(object obj, EventArgs ea)
            {
                time += new TimeSpan(0, 0, 1);
                clock.Text = time.ToString();
            }

            clock.Location = new Point(10, 10);
            clock.Parent = this;

            for (int i = 0; i < Side; ++i)
            {
                for (int j = 0; j < Side; ++j)
                {
                    Buttons[i, j] = new Button();
                    Buttons[i, j].Parent = this;
                    Field[i, j] = i * Side + j + 1;
                    if (Field[i, j] != Numb)
                    {
                        Buttons[i, j].Text = Convert.ToString(Field[i, j]);
                    }
                    Buttons[i, j].Left = 10 + j * 50;
                    Buttons[i, j].Top = 40 + i * 50;
                    Buttons[i, j].Width = 50;
                    Buttons[i, j].Height = 50;
                    Buttons[i, j].Tag = new Point(i, j);
                    Buttons[i, j].Click += new EventHandler(OnClick);
                    Buttons[i, j].ForeColor = Color.Black;
                    Buttons[i, j].BackColor = Color.Coral;
                }
                CenterToScreen();
            }

        }
        void OnMenuExit(object obj, EventArgs ea)
        {
            Close();
        }

        void OnStartGame(object obj = null, EventArgs ea = null)
        {
            int direction;
            const int max_max = 200;
            for (int i = 0; i < max_max; ++i)
            {
                direction = random.Next(4);
                //Directions[i] = direction;
                switch(direction)
                {
                    case 0: //up
                       if (NumbX - 1 >= 0)
                        {
                            Field[NumbX, NumbY] = Field[NumbX - 1, NumbY];
                            --NumbX;
                        } 
                        else
                        {
                            for (int k = 0; k < Side - 1; ++k)
                            {
                                Field[k, NumbY] = Field[k + 1, NumbY];
                            }
                            NumbX = Side - 1;

                        }
                        break;
                    case 1: //down
                        if (NumbX + 1 < Side)
                        {
                            Field[NumbX, NumbY] = Field[NumbX + 1, NumbY];
                            ++NumbX;
                        }
                        else
                        {
                            for (int k = Side - 1; k > 0; --k)
                            {
                                Field[k, NumbY] = Field[k - 1, NumbY];
                            }
                            NumbX = 0;
                        }
                        break;
                    case 2: //left
                        if (NumbY - 1 >= 0)
                        {
                            Field[NumbX, NumbY] = Field[NumbX, NumbY - 1];
                            --NumbY;
                        }
                        else
                        {
                            for (int k = 0; k < Side - 1; ++k)
                            {
                                Field[NumbX, k] = Field[NumbX, k + 1];
                            }
                            NumbY = Side - 1;
                        }
                        break;
                    case 3: //right
                        if (NumbY + 1 < Side)
                        {
                            Field[NumbX, NumbY] = Field[NumbX, NumbY + 1];
                            ++NumbY;
                        }
                        else
                        {
                            for (int k = Side - 1; k > 0; --k)
                            {
                                Field[NumbX, k] = Field[NumbX, k - 1];
                            }
                            NumbY = 0;
                        }
                        break;
                }
                Field[NumbX, NumbY] = Numb; //new position
                PrNumbX = NumbX; PrNumbY = NumbY;
            }
            ShowWindow(false);
            Moves = 0;
            IsRunning = true;
            label1.Text = "0";
            label3.Text = "moves";
            TimerStart();
        }

        void ShowWindow(bool IsRepeating)
        {
            if (IsRepeating)
            {
                for (int i = 0; i < Side; ++i)
                {
                    for (int j = 0; j < Side; ++j)
                    {
                        Field[i, j] = PrField[i, j];
                    }
                }
            }
            for (int i = 0; i < Side; ++i)
            {
                for (int j = 0; j < Side; ++j)
                {
                    if (!IsRepeating)
                    {
                        PrField[i, j] = Field[i, j];
                    }
                    if (Field[i, j] != Numb)
                    {
                        Buttons[i, j].Text = Convert.ToString(Field[i, j]);
                    }
                    else
                    {
                        Buttons[i, j].Text = null;
                    }
                }
            }
        }
        void TimerStart ()
        {
            time = new TimeSpan(0, 0, 0);
            clock.Text = "00.00.00";
            timer.Start();
        }

        void OnRepeat(object obj, EventArgs ea)
        {
            TimerStart();
            IsRunning = true;
            Moves = 0;
            label1.Text = "0";
            label3.Text = "moves";
            NumbX = PrNumbX; NumbY = PrNumbY;
            ShowWindow(true);
        }

        void OnClick(object obj, EventArgs ea)
        {
            if (IsRunning == false)
            {
                return;
            }

            Button pressed = (Button)obj;
            int x = ((Point)pressed.Tag).X;
            int y = ((Point)pressed.Tag).Y;

            if (Math.Abs(x - NumbX) + Math.Abs(y - NumbY) == 1) 
            {
                Field[NumbX, NumbY] = Field[x, y];
                Buttons[NumbX, NumbY].Text = Buttons[x, y].Text;

                NumbX = x;
                NumbY = y;
                Field[NumbX, NumbY] = Numb;
                Buttons[NumbX, NumbY].Text = null;
                ++Moves;
                label1.Text = Moves.ToString();
            }
            if (NumbX == Side - 1 && NumbY == Side - 1)
            {
                if (IsVictory())
                {
                    timer.Stop();
                    IsRunning = false;
                    string victory = "You won within ";
                    victory += Moves;
                    victory += " steps!";
                    MessageBox.Show(victory, "Congrats!", MessageBoxButtons.OK);
                }
            }
        }
        bool IsVictory ()
        {
            int order = 1;
            for (int i = 0; i < Side; ++i)
            {
                for (int j = 0; j < Side; ++j)
                {
                    if (Field[i, j] != order)
                    {
                        return false;
                    }
                    ++order;
                }
            }
            return true;
        }

    }
}
