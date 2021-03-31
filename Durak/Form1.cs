using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Durak
{
    public partial class Form1 : Form
    {
        string cheatcode;
        public Form1()
        {
            InitializeComponent();
            MenuItem exit = new MenuItem("Exit", new EventHandler(OnExit), Shortcut.CtrlC);
            Menu = new MainMenu(new MenuItem[] { exit });
        }

        private void OnExit(object obj, EventArgs ea)
        {
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Ты уверен? Ну ладно:(", "Кого ответ?", MessageBoxButtons.OK);
            Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Окей, ты победил, но какой ценой?", "Victory", MessageBoxButtons.OK);
            Close();
        }

        private void button2_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (cheatcode == "cheatcode")
            {
                return;
            }
            /*Random r = new Random();
            button2.Left = r.Next(0, this.ClientSize.Width - button1.Width);
            button2.Top = r.Next(0, this.ClientSize.Height - button1.Height);*/
            int stepX = (button2.Width / 3); //X
            int stepY = (button2.Height / 3); //Y

            if (((button2.Left + button2.Width + stepX) < (this.ClientSize.Width)) && (e.X <= button2.Width / 2))
            {
                button2.Location = new Point(button2.Location.X + stepX, button2.Location.Y);
            }
            if ((button2.Left > stepX) && (e.X > button2.Width / 2))
            {
                button2.Location = new Point(button2.Location.X - (button2.Width / 3), button2.Location.Y);
            }

            if (((button2.Top + button2.Height + stepY) < (this.ClientSize.Height)) && (e.Y <= button2.Height / 2))
            {
                button2.Location = new Point(button2.Location.X, button2.Location.Y + stepY);
            }
            if ((button2.Top > stepY) && (e.Y > button2.Height / 2))
            {
                button2.Location = new Point(button2.Location.X, button2.Location.Y - stepY);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cheatcode = textBox1.Text;
        }
    }
}
