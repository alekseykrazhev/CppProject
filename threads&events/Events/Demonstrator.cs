using System;
using System.ComponentModel;
using System.Threading;

namespace Events
{
    public delegate void DemonstratorEventHandler(object sender, EventArgs e);

    public delegate void DemonstrateEventHandler(object sender, DemonstrateEventArgs e);

    public delegate void ShootingEventHandler(object sender, ShootingEventArgs e);

    public class Demonstrator
    {
        private uint shoot_radius = 150;
        public bool IsShooting;
        private int _radius = 150;
        private int _delay;
        private int maxX = 200, maxY = 200;
        public event DemonstratorEventHandler DemonstrEv;
        public event DemonstrateEventHandler DemEv;
        public event ShootingEventHandler ShotEvent;

        public Demonstrator(int delay, int rad)
        {
            _radius = rad;
            _delay = delay;
        }

        public void Start(object sender, CalculateEventArgs e)
        {
            DemEv?.Invoke(this,
                new DemonstrateEventArgs(
                    "Amount of Armstrong's numbers on interval [" + e.Begin + "," + e.End + "] equals " + e.Result,
                    true));
        }

        public void Stop()
        {
            DemonstrEv?.Invoke(this, new EventArgs());
        }

        public void StartShooting()
        {
            IsShooting = true;
            Random rnd = new Random();
            while (IsShooting)
            {
                int x = rnd.Next(0, 400);
                int y = rnd.Next(0, 400);
                ShotEvent?.Invoke(this, new ShootingEventArgs(x, -y, _radius));
                Thread.Sleep(_delay);
            }
        }

        public void StopShooting(object sender, EventArgs e)
        {
            IsShooting = false;
            DemEv?.Invoke(this, new DemonstrateEventArgs("shooting stopped", false));
        }
    }

    public class DemonstrateEventArgs : EventArgs
    {
        private string _mes;
        public string Message => _mes;

        private bool _numb;

        public bool Numb()
        {
            return _numb;
        }

        public DemonstrateEventArgs(string mes, bool numb)
        {
            _mes = mes;
            _numb = numb;
        }
    }

    public class ShootingEventArgs : EventArgs
    {
        private int _x;
        private int _y;
        private int _rad;

        public ShootingEventArgs(int X, int Y, int r)
        {
            _x = X;
            _y = Y;
            _rad = r;
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }

        public int Rad
        {
            get { return _rad; }
        }
    }
}