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
   public class Group: Shape
    {
        public int _count;
        public Shape[] _shapes;
        public int selected = 0;
        public int color = 0;
        public int group = 1;

        public Group(int count)
        {
            group = 1;
            color = 1;
            selected = 1;
            _count = count;
            _shapes = new Shape[_count];
            for (int i = 0; i < _count; i++)
                _shapes[i] = null;
        }

       public void addShapes(MyList l)
        {
            int counter=0;
            for (l.First(); l.Eol(); l.Next())
            {
                if (l.GetObj().CheckSelect() == true)
                {
                    _shapes[counter] = l.GetObj();
                    _shapes[counter].SwitchGroup(1);
                    counter += 1;
                }
            }
            for (l.First(); l.Eol(); l.Next())
            {
                if (l.GetObj().CheckSelect() == true)
                {
                    l.delete(l.GetObj());
                }
                if (l.nalichie() == true && l.GetNextNode() == null)
                {
                    if (l.GetHeadObj().CheckSelect())
                    {
                        l.First();
                        l.delete(l.GetObj());
                    }
                }
            }
            // l.addShape();
        }

        public override void load(StreamReader stream, MyList l)
        {
            _count = Convert.ToInt32(stream.ReadLine());
           this._shapes = new Shape[_count];
            for (int i = 0; i < _count; i++)
                _shapes[i] = null;
            color = Convert.ToInt32(stream.ReadLine());
            selected = Convert.ToInt32(stream.ReadLine());
            group = Convert.ToInt32(stream.ReadLine());
            for (int i = 0; i < _count; ++i)
            {
                string code;
                code = stream.ReadLine();
                _shapes[i] = l.createShape(code);
                if (_shapes[i] != null)
                {
                    _shapes[i].load(stream, l);
                    
                     
                }
            }
        }

        public override void save(StreamWriter stream)
        {
            stream.WriteLine("G");
            stream.WriteLine(_count);
            stream.WriteLine(color);
            stream.WriteLine(selected);
            stream.WriteLine(group);
            for(int i=0; i < _count; ++i)
            {
                _shapes[i].save(stream);
            }
        }

        public override bool CheckGroup()
        {
            if (group == 1)
            {
                return true;
            }
            else return false;
        }

        public override void SwitchGroup(int i)
        {
           
        }

        public override void unGroup(MyList l)
        {
            for (l.First(); l.Eol(); l.Next())
            {
                if (l.GetObj().CheckSelect() == true)
                {
                   if(l.GetObj().CheckGroup() == true)
                    {
                        l.delete(l.GetObj());
                        for(int i = 0; i < _count; i++)
                        {
                            _shapes[i].SwitchGroup(0);
                            l.addShape(_shapes[i]);
                        }
                    }
                }
            }
        }

        public override bool mouseInShape(int dx, int dy)
        {
            for(int i = 0;i < _count; i++)
            {
                if (_shapes[i].mouseInShape(dx, dy))
                    return true;
            }
            return false;
        }
        public override void draw(Graphics g)
        {
            for (int i = 0; i < _count; i++)
            {
                _shapes[i].draw(g);
            }
        }
        public override void Select()
        {
            if (selected == 1)
            {
                selected = 0;
                for (int i = 0; i < _count; i++)
                {
                    _shapes[i].Select();
                }
            }
            else
            {
                selected = 1;
                for (int i = 0; i < _count; i++)
                {
                    _shapes[i].Select();
                }
            }
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

            for (int i = 0; i < _count; i++)
            {
                _shapes[i].SwitchColor(c);
            }
        }
        public override int GetColor()
        {
            return color;
        }

        public override void move(int dx, int dy, int w, int h)
        {
            for (int i = 0; i < _count; i++)
            {
                _shapes[i].move(dx, dy, w, h);
            }
            //if (check(w, h) == false)
            //{
            //    for (int i = 0; i < _count; i++)
            //    {
            //        _shapes[i].move(-dx, -dy, w, h);
            //    }
            //}
        }

        public override bool check(int w, int h)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_shapes[i].check(w, h) == false)
                    return false;
            }
            return true;
        }

        public override bool CheckSelect()
        {
            if (selected == 1)
                return true;
            else return false;
        }

        public override void sizeMove(int i, int w, int h)
        {
            for (int t = 0; t < _count; t++)
            {
                _shapes[t].sizeMove(i, w, h);
            }
            //if (check(w, h) == false)
            //{
            //    for (int r = 0; r < _count; r++)
            //    {
            //        _shapes[r].sizeMove(-i, w, h);
            //    }
            //}
        }
    }
}
