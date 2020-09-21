using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace OOP_6
{
    class triangle: Shape
    {
        public Point p1;
        public Point p2;
        public Point p3;
        public int color;
        public int x,y,selected = 0;
        public int a = 100;
        public int k;
        public Point[] points;
        public int group = 0;

        public triangle(int dx,int dy)
        {
            group = 0;
            a = 100;
            x = dx;
            y = dy;
            this.color = 1;
            selected = 0;
        }

        public override bool CheckGroup()
        {
            if (group == 1)
            {
                return true;
            }
            else return false;
        }

        public override void unGroup(MyList l)
        {
            //for (l.First(); l.Eol(); l.Next())
            //{
            //    if (l.GetObj().CheckSelect() == true)
            //    {
            //        if (l.GetObj().CheckGroup() == true)
            //        {
            //            l.delete(l.GetObj());
            //            //Shape[] _shapes2 = new Shape[_count];
                        
            //        }
            //    }
            //}
        }

        public override void SwitchGroup(int i)
        {
            group = i;
        }

        public override void load(StreamReader stream, MyList l)
        {
            x = Convert.ToInt32(stream.ReadLine());
            y = Convert.ToInt32(stream.ReadLine());
            a = Convert.ToInt32(stream.ReadLine());
            color = Convert.ToInt32(stream.ReadLine());
            selected = Convert.ToInt32(stream.ReadLine());
            group = Convert.ToInt32(stream.ReadLine());
            
        }

        public override void save(StreamWriter stream)
        {
            stream.WriteLine("T");
            stream.WriteLine(x);
            stream.WriteLine(y);
            stream.WriteLine(a);
            stream.WriteLine(color);
            stream.WriteLine(selected);
            stream.WriteLine(group);
        }

        public override bool CheckSelect()
        {
            if (selected == 1)
                return true;
            else return false;
        }

        public override void SwitchColor(string c)
        {
            if (c == "Зелёный")
                color = 3;
            else if (c == "Синий")
                color = 1;
            else if (c == "Красный")
                color = 2;
            else if (c == "Чёрный")
                color = 4;
            else color = 5;
        }

        public override void Select()
        {
            if (selected == 1)
                selected = 0;
            else selected = 1;
        }

        override public bool mouseInShape(int dx, int dy)
        {

            Point p = new Point(dx, dy);
            k = Convert.ToInt32(a / Math.Sqrt(3));
            p1 = new Point(x, y - 2 * k);
            p2 = new Point(x - a / 2, y + k);
            p3 = new Point(x + a / 2, y + k);
            bool flag = false;
            if (func(p1, p2, p3) == func( p1, p2, p) + func( p1, p, p3) + func(p2,p,p3))
           // if(func(p,p1,p2,p3))1
            {
                flag = true;
            }
            return flag;
        }


        public int func(Point p1, Point p2, Point p3)
        {
            
            return Math.Abs((p1.X - p3.X) * (p2.Y - p3.Y) + (p2.X - p3.X) * (p3.Y - p1.Y));
        }

        public override int GetColor()
        {
            return color;
        }

        public override void draw(Graphics g)
        {
            k = Convert.ToInt32(a / Math.Sqrt(3));
            Pen pen;
            if (color == 1) pen = new Pen(Brushes.Blue);
            else if (color == 2)
                pen = new Pen(Brushes.Red);
            else if (color == 3)
                pen = new Pen(Brushes.Green);
            else if (color == 4)
                pen = new Pen(Brushes.Black);
            else pen = new Pen(Brushes.Yellow);
            p1 = new Point(x, y - 2 * k);
            p2 = new Point(x - a, y + k);
            p3 = new Point(x + a, y + k);
            points = new Point[3] { p1, p2, p3 };
            if (selected == 0)
                g.DrawPolygon(pen, points);
            else
            {
                Brush back = Brushes.Black;
                g.FillPolygon(back,points);
            }
        }

        public override void move(int dx, int dy, int w, int h)
        {
            if (selected == 1)
            {
                x = x + dx;
                y = y + dy;
                if (group == 0)
                {
                    if (check(w, h) == false)
                    {
                        x = x - dx;
                        y = y - dy;
                    }
                }
            }
        }

        public override bool check(int w, int h)
        {
            if ((x - a) <= 0 || (y - 2 * k) <= 0 || (x + a) >= w || (y + k) >= h)
                return false;
            else return true;
        }

        public override void sizeMove(int i, int w, int h)
        {
            a = a + 2 * i;
            if (check(w, h) == false)
            {
                a = a - 2 * i;
            }
            if (a < 0)
            {
                a = a - 2 * i;
            }
        }
    }
}
