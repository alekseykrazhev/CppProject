using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Events
{
    public delegate void StopShootingEventHandler(object sender, EventArgs e);
    public delegate void CloseShootingEventHandler(object sender, EventArgs e);
    public partial class shooting : Form
    {
        public shooting()
        {
            InitializeComponent();
        }

        private bool _isRunning = false;
        private int _radius = 150;
        private int _hit = 0, _total = 0;
        
        private Bitmap _btm;
        private Graphics _grf;

        private PointF[] _points = new PointF[1000];
        private List<Point> _hits = new List<Point>();
        
        public event StopShootingEventHandler StopShootEvent;
        public event CloseShootingEventHandler CloseShootEvent;

        protected override void OnPaint(PaintEventArgs e)
        {
            _btm = new Bitmap(Target.Width, Target.Height);
            _grf = Graphics.FromImage(_btm);
            base.OnPaint(e);
            DrawTarget(_grf);
            DrawHit(_grf);
            Target.Image = _btm;
            Target.DrawToBitmap(_btm, Target.ClientRectangle);
        }

        private void DrawTarget(Graphics g)
        {
            g.Clear(Color.Wheat);
            if (_radius == 1)
            {
                return;
            }

            if (_radius < 50)
            {
                var final = "Radius is way too small";
                var message = "ERROR";
                MessageBox.Show(final, message, MessageBoxButtons.OK);
                _radius = 150;
                return;
            }

            if (_radius > 235)
            {
                var final = "Radius is way too big";
                var message = "ERROR";
                MessageBox.Show(final, message, MessageBoxButtons.OK);
                _radius = 150;
                return;
            }

            SolidBrush brush1 = new SolidBrush(Color.Aqua);

            Pen pen = new Pen(Color.Black, 1.5f);

            Point three = new Point(0, Target.Height / 2 - 15);
            Point four = new Point(Target.Height, Target.Height / 2 - 15);
            g.DrawLine(pen, three, four);

            float x = -30f;

            g.TranslateTransform(Target.Width / 2 + 20, Target.Height / 2 - 15);
            foreach (var i in Enumerable.Range(0, 1000))
            {
                float y = -(x - 1) * (x - 1);
                PointF first = new PointF(x, y);
                _points[i] = first;

                x += 0.1f;
            }

            g.FillPolygon(brush1, _points);
            Rectangle numb = new Rectangle(-Target.Height / 2, -Target.Width / 2, Target.Height / 2 - 8, Target.Width / 2);
            g.FillRectangle(new SolidBrush(Color.Wheat), numb);
            Rectangle sheesh =
                new Rectangle(-19, -Target.Height / 2, Target.Width / 2, Target.Height / 2 - _radius + 4);
            g.FillRectangle(new SolidBrush(Color.Wheat), sheesh);
            Point gg = new Point(-9, Target.Height / 2 + 50);
            Point wp = new Point(-9, -Target.Height / 2);
            g.DrawLine(pen, gg, wp);

            Rectangle rect = new Rectangle(-_radius, -_radius + 2, 2 * _radius - 20, 2 * _radius);
            g.FillPie(brush1, rect, 180, -90);
            
            g.DrawEllipse(pen, rect);
            g.TranslateTransform(-(Target.Width - 20), -(Target.Height-20));
        }
        
        private void Stop_button_Click_1(object sender, EventArgs e)
        {
            if (Calculator.working)
            {
                var message = "Stop calculation first!";
                MessageBox.Show(message);
            }
            else
            {
                //Stop_button.Enabled = false;
                _hit = _total = 0;
                _hits.Clear();
                StopShootEvent?.Invoke(this, e);
            }
        }

        public void DrawHit(Graphics g)
        {
            int H = Target.Height;
            int W = Target.Width;
            Pen pen = new Pen(new SolidBrush(Color.Red));
            foreach (var p in _hits)
            {
                g.DrawEllipse(pen, p.X - 3 + W / 2, H / 2 - p.Y - 3, 6, 6);
                g.FillEllipse(new SolidBrush(Color.Red), p.X - 3 + W / 2, H / 2 - p.Y - 3, 6, 6);
                g.DrawEllipse(pen, p.X - 6 + W / 2, H / 2 - p.Y - 6, 12, 12);
            }
        }
        
        public void DrawShot(object sender, ShootingEventArgs e)
        {
            var radius = e.Rad;
            ++_total;
            Pen p = new Pen(Color.Red, 3);
            var b = e.Y;
            if (e.Y < 0)
            {
                b = -e.Y;
            }
            Color shootColor = _btm.GetPixel(e.X, b);
            
            if (shootColor.A == 255 && shootColor.B == 255 && shootColor.R == 0 && shootColor.G == 255)
            {
                ++_hit;
            }
            
            label1.Invoke(new Action(() => label1.Text = (_hit).ToString()));
            label2.Invoke(new Action(() => label2.Text = (_total - _hit).ToString()));
            
            //label1.Text = e.X.ToString();
            //label2.Text = e.Y.ToString();
            _hits.Add(new Point(e.X, e.Y));
            Invalidate();
        }
    }
}