using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

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
        private List<Result> _results = new List<Result>();
        private List<Result> _restoredRes = new List<Result>();

        Label clock = new Label();
        Timer timer = new Timer();
        public Stopwatch executionTime = new Stopwatch();

        TimeSpan time;
        private string _name = null;

        public Game()
        {
            try
            {
                using (StreamReader sr = new StreamReader("result.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<Result>));
                    _results = (List<Result>) xs.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                
            }

            ClientSize = new Size(Side * 50 + 20, Side * 50 + 50);
            BackColor = Color.Azure;
            Buttons = new Button[Side, Side];
            Field = new int[Side, Side];

            MenuItem NewGame = new MenuItem("Start New Game", new EventHandler(OnStartGame), Shortcut.F1);
            MenuItem RepeatGame = new MenuItem("Repeat Game", new EventHandler(OnRepeat), Shortcut.F2);
            MenuItem Exit = new MenuItem("Exit", new EventHandler(OnMenuExit), Shortcut.F3);
            MenuItem mGame = new MenuItem("Fifteen", new MenuItem[] { NewGame, RepeatGame, Exit });
            MenuItem execut = new MenuItem("10 best based on time", new EventHandler(OnTime));
            MenuItem moves = new MenuItem("10 best based on moves", new EventHandler(OnMoves));
            MenuItem latest = new MenuItem("10 latest results", new EventHandler(OnLatest));
            MenuItem deleItem = new MenuItem("Delete results (insert date&time)", new EventHandler(OnDelete));
            MenuItem mMenu = new MenuItem("Tools", new MenuItem[] {execut, moves, latest, deleItem});

            Menu = new MainMenu(new MenuItem[] { mGame, mMenu });
            IsRunning = false;
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
                    Buttons[i, j].BackColor = Color.Tomato;
                }
                CenterToScreen();
            }

        }

        void FinallySerialize()
        {
            using (StreamWriter writer = new StreamWriter("result.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Result>));
                xs.Serialize(writer, _results);
            }
        }
        
        void PrintToXml()
        {
            executionTime.Stop();
            TimeSpan ts = executionTime.Elapsed;
            
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds);

            Result res = new Result();
            res.BuildingTime = elapsedTime;
            res.MovesAmount = Moves;
            res.BeginTime = DateTime.Now.ToString();
            res.Name = _name;
            _results.Add(res);
        }

        void RestoreFromXml()
        {
            Result resRestored;
            using (StreamReader sr = new StreamReader("result.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Result>));
                _restoredRes = (List<Result>) xs.Deserialize(sr);
            }
        }
        
        void OnDelete(object obj, EventArgs ea)
        {
            PrintToXml();
            InputBox inputBox = new InputBox("Enter date and time (press space after a date):");
            DateTime start = Convert.ToDateTime(inputBox.GetString());
            _results.RemoveAll(x => (Convert.ToDateTime(x.BeginTime) < start.Date));
            _restoredRes.RemoveAll(x => (Convert.ToDateTime(x.BeginTime) < start.Date));
            FinallySerialize();
        }

        void OnLatest(object obj, EventArgs ea)
        {
            RestoreFromXml();
            string mes = null;
            if (_restoredRes.Count <= 10)
            {
                foreach (var res in _restoredRes)
                {
                    mes += res.ToString() + "\n";
                }

                MessageBox.Show(mes, "10 latest results", MessageBoxButtons.OK);
            } 
            else 
            {
                foreach (var i in Enumerable.Range(0, 10))
                {
                    mes += _results.ElementAt(i).ToString() + "\n";
                }
                MessageBox.Show(mes, "10 latest results", MessageBoxButtons.OK);
            } 
        }
        
        void OnMoves(object obj, EventArgs ea)
        {
            RestoreFromXml();
            string mes = null;
            if (_restoredRes.Count <= 10)
            {
                foreach (var res in _restoredRes)
                {
                    mes += res.ToString() + "\n";
                }
                MessageBox.Show(mes, "10 best results based on moves", MessageBoxButtons.OK);
            }
            else
            {
                _restoredRes.Sort();
                foreach (var i in Enumerable.Range(0, 10))
                {
                    mes += _restoredRes.ElementAt(i).ToString() + "\n";
                }
                MessageBox.Show(mes, "10 best results based on moves", MessageBoxButtons.OK);
            }
        }
        
        void OnTime(object obj, EventArgs ea)
        {
            RestoreFromXml();
            string mes = null;
            if (_restoredRes.Count <= 10)
            {
                foreach (var res in _restoredRes)
                {
                    mes += res.ToString() + "\n";
                }
                MessageBox.Show(mes, "10 best results based on time", MessageBoxButtons.OK);
            }
            else
            {
                while (_restoredRes.Count > 10)
                {
                    var max = _restoredRes.ElementAt(0);
                    for (int i = 0; i < _restoredRes.Count; ++i)
                    {
                        if (_restoredRes.ElementAt(i) > max)
                        {
                            max = _restoredRes.ElementAt(i);
                        }
                    }
                    _restoredRes.Remove(max);
                }
            }
        }
        void OnMenuExit(object obj, EventArgs ea)
        {
            PrintToXml();
            FinallySerialize();
            Close();
        }

        void OnStartGame(object obj = null, EventArgs ea = null)
        {
            executionTime.Stop();
            executionTime.Reset();
            executionTime.Start();
            InputBox inputBox = new InputBox("Enter your name:", "Your name is ");
            _name = inputBox.GetString();
            int direction;
            const int max_max = 200;
            for (int i = 0; i < max_max; ++i)
            {
                direction = random.Next(4);
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
                Field[NumbX, NumbY] = Numb; 
                PrNumbX = NumbX; PrNumbY = NumbY;
            }
            ShowWindow(false);
            Moves = 0;
            IsRunning = true;
            label1.Text = "0";
            //label3.Text = "moves";
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
            PrintToXml();
            executionTime.Reset();
            executionTime.Start();
            TimerStart();
            IsRunning = true;
            Moves = 0;
            label1.Text = "0";
            //label3.Text = "moves";
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
                    PrintToXml();
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Game
            // 
            this.ClientSize = new System.Drawing.Size(775, 443);
            this.Name = "Game";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        protected System.Windows.Forms.Label label3;
    }
}
