using System;

namespace Events
{
    public delegate void DemonstratorEventHandler(object sender, EventArgs e);

    public delegate void DemonstrateEventHandler(object sender, DemonstrateEventArgs e);
    
    public class Demonstrator
    {
        public event DemonstratorEventHandler DemonstrEv;
        public event DemonstrateEventHandler DemEv;

        public Demonstrator()
        {
        }

        public void Start(object sender, CalculateEventArgs e)
        {
            if (e.Begin == -1 && e.End == -1 && e.Result == -1)
            {
                DemEv?.Invoke(this,new DemonstrateEventArgs("Calculation canceled"));
            }
            else
            {
                DemEv?.Invoke(this,
                    new DemonstrateEventArgs(
                        "Amount of Armstrong's numbers on interval [" + e.Begin + "," + e.End + "] equals " + e.Result));
            }
        }
        
        public void Stop()
        {
            DemonstrEv?.Invoke(this, new EventArgs());
        }
    }

    public class DemonstrateEventArgs : EventArgs
    {
        private string _mes;
        public string Message => _mes;
        
        public DemonstrateEventArgs(string mes)
        {
            _mes = mes;
        }
    }
}