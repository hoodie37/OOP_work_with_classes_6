
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
    public class Krug: Shape
    {
        
        public int a = 0;
        public int x = 0;
        public int y = 0;
        public int color = 0;
        public int selected = 0;
        public int group = 0;

        public Krug(int dx, int dy)
        {
            group = 0;
            x = dx;
            y = dy;
            a = 200;
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
                        
                        
            //        }
            //    }
            //}
        }

        public override int GetColor()
        {
            return color;
        }

        public override bool CheckSelect()
        {
            if (selected == 1)
                return true;
            else return false;
        }

        public override bool mouseInShape(int dx, int dy)
        {
            return (((dx - x) * (dx - x) + (dy - y) * (dy - y)) <= ((a / 2) * (a / 2)));
        }

        public override void draw(Graphics g)
        {
            Pen pen;
            if (color == 1) pen = new Pen(Brushes.Blue);
            else if(color == 2)
                pen = new Pen(Brushes.Red);
            else if(color == 3)
                pen = new Pen(Brushes.Green);
            else  if(color == 4)
                pen = new Pen(Brushes.Black);
            else pen = new Pen(Brushes.Yellow);
            if (selected == 0)
                g.DrawEllipse(pen, x - a / 2, y - a / 2, a, a);
            else {
                Brush brush = Brushes.Black;
                g.FillEllipse(brush, x - a / 2, y - a / 2, a, a);
            }
        }

        public override void load(StreamReader stream, MyList l)
        {
            x = Convert.ToInt32(stream.ReadLine());
            y = Convert.ToInt32(stream.ReadLine());
            color = Convert.ToInt32(stream.ReadLine());
            selected = Convert.ToInt32(stream.ReadLine());
            group = Convert.ToInt32(stream.ReadLine());
            
        }

        public override void save(StreamWriter stream)
        {
            stream.WriteLine("K");
            stream.WriteLine(x);
            stream.WriteLine(y);
            stream.WriteLine(color);
            stream.WriteLine(selected);
            stream.WriteLine(group);
        }

        public override void move(int dx, int dy,int w,int h)
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

        public override void SwitchGroup(int i)
        {
            group = i;
        }

        public override bool check(int w,int h)
        {
            if((x - a/2) <= 0 || (y - a/2) <= 0 || (y + a/2) >= h  || (x + a/2) >= w)
            return false;
            else return true;
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
            {
                selected = 0;
            }
            else
            {
                selected = 1;
            }
        }

        public override void sizeMove(int i,int w,int h)
        {
            a = a + 2 * i;
            if (group == 0)
            {
                if (check(w, h) == false)
                {
                    a = a - 2 * i;
                }
            }
            if (a < 0)
            {
                a = a - 2 * i;
            }
        }
    }
}
