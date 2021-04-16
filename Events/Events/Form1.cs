using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public Form1()
        {
            InitializeComponent();
            _demonstrator = new Demonstrator();
            _calculator = new Calculator(int.Parse(textBox1.Text));
            _demonstrator.DemonstrEv += new DemonstratorEventHandler(_calculator.Stop);
            _calculator.CalcEv += new CalculatorEventHandler(_demonstrator.Start);
            _demonstrator.DemEv += new DemonstrateEventHandler(this.Info);
            Thread threadCalc1 = new Thread(_calculator.Calculate);
            threadCalc1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _calculator = new Calculator(int.Parse(textBox1.Text));
            _demonstrator.DemonstrEv += new DemonstratorEventHandler(_calculator.Stop);
            _calculator.CalcEv += new CalculatorEventHandler(_demonstrator.Start);
            Thread threadCalc1 = new Thread(_calculator.Calculate);
            threadCalc1.Start();
            button1.Enabled = false;
            button2.Enabled = true;
            textBox1.Enabled = false;
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
            //Output.AppendText(e.Message + "\r\n");
            Output.Text += e.Message + "\r\n";
           
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
    }
}
