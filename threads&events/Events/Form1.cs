using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Events;

namespace Events
{
    public partial class Form1 : Form
    {
        private Calculator _calculator;
        private Demonstrator _demonstrator;
        private uint _delay;
        private uint _radius;
        private shooting shoot;

        public Form1()
        {
            InitializeComponent();
            _delay = uint.Parse(textBox1.Text);
            _radius = uint.Parse(textBox2.Text);
            _demonstrator = new Demonstrator(int.Parse(textBox1.Text), (int) _radius);
            _calculator = new Calculator(int.Parse(textBox1.Text));
            _demonstrator.DemonstrEv += _calculator.Stop;
            _calculator.CalcEv += _demonstrator.Start;
            _demonstrator.DemEv += Info;
            Thread threadCalc1 = new Thread(_calculator.Calculate);
            threadCalc1.Start();
            
            Thread threadShoot = new Thread(_demonstrator.StartShooting);
            threadShoot.Start();


            shoot = new shooting();
            _demonstrator.ShotEvent += shoot.DrawShot;
            shoot.StopShootEvent += _demonstrator.StopShooting;
            shoot.CloseShootEvent += shooting_Close;
            shoot.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _calculator = new Calculator(int.Parse(textBox1.Text));
            if (_demonstrator == null | shoot == null)
            {
                _demonstrator = new Demonstrator((int) _delay, (int) _radius);
                _demonstrator.DemEv += Info;
            }

            _demonstrator.DemonstrEv += _calculator.Stop;
            _calculator.CalcEv += _demonstrator.Start;

            if (shoot == null)
            {
                shoot = new shooting();
                _demonstrator.ShotEvent += shoot.DrawShot;
                shoot.StopShootEvent += _demonstrator.StopShooting;
                shoot.CloseShootEvent += shooting_Close;
                shoot.Show();
            }

            if (!_demonstrator.IsShooting)
            {
                Thread threadShoot = new Thread(_demonstrator.StartShooting);
                threadShoot.Start();
            }

            Thread threadCalc = new Thread(_calculator.Calculate);
            threadCalc.Start();

            button1.Enabled = false;
            button2.Enabled = true;
            textBox1.Enabled = false;
        }

        private void shooting_Close(object sender, EventArgs e)
        {
            shoot = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _demonstrator.Stop();

            button2.Enabled = false;
            button1.Enabled = true;
            textBox1.Enabled = true;
        }

        
        public void Info(object sender, DemonstrateEventArgs e)
        {
            String message = e.Message + "\r\n";
            if (IsHandleCreated)
            {
                Output.Invoke(new Action<string>((s) => Output.Text += s), 
                    e.Message + Environment.NewLine);
            }
            else
            {
                Output.Text += e.Message + "\r\n";
            }

            Invalidate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _delay = uint.Parse(textBox1.Text);
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            Сheck(sender, e, ref _delay);
        }

        private void Сheck(object sender, CancelEventArgs e, ref uint num)
        {
            try
            {
                num = uint.Parse((sender as TextBox).Text);
                errorProvider1.Clear();
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(sender as TextBox, ex.Message);
                e.Cancel = true;
                (sender as TextBox).Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBox1.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
    }
}