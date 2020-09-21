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
    class square: Shape
    {
        public Point p1;
        public Point p2;
        public Point p3;
        public Point p4;
        public int color;
        public int x;
        public int a = 100;
        public int y,selected =0;
        public int group = 0;

        public square(int dx, int dy)
        {
            group = 0;
            a = 100;
            x = dx;
            y = dy;
            this.color = 1;
            selected = 0;
        }

        public override void load(StreamReader stream,MyList l)
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
            stream.WriteLine("S");
            stream.WriteLine(x);
            stream.WriteLine(y);
            stream.WriteLine(a);
            stream.WriteLine(color);
            stream.WriteLine(selected);
            stream.WriteLine(group);
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

        public override bool CheckGroup()
        {
            if (group == 1)
            {
                return true;
            }
            else return false;
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

        public override bool mouseInShape(int dx, int dy)
        {

            if (Math.Abs((dx - x) + (dy - y)) + Math.Abs((dx - x) - (dy - y)) <= a)
            {
                return true;
            }
            else return false;
        }

        public override int GetColor()
        {
            return color;
        }

        public override void draw(Graphics g)
        {
            Pen pen;
            if (color == 1) pen = new Pen(Brushes.Blue);
            else if (color == 2)
                pen = new Pen(Brushes.Red);
            else if (color == 3)
                pen = new Pen(Brushes.Green);
            else if (color == 4)
                pen = new Pen(Brushes.Black);
            else pen = new Pen(Brushes.Yellow);
            p1 = new Point(x - a / 2, y - a / 2);
            p2 = new Point(x + a / 2, y - a / 2);
            p3 = new Point(x + a / 2, y + a / 2);
            p4 = new Point(x - a / 2, y + a / 2);
            Point[] Points = { p1, p2, p3 ,p4 };
            if (selected == 0)
                g.DrawPolygon(pen, Points);
            else
            {
                Brush back = Brushes.Black;
                g.FillPolygon(back, Points);
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
            if ((x - a / 2) <= 0 || (x + a / 2) >= w || (y - a / 2) <= 0 || (y + a / 2) >= h)
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
