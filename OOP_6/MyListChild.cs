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
    class MyListChild: MyList
    {
        public  Shape createShape(string code)
        {
            Shape shape;
            switch (code)
            {
                case "K":
                    shape = new Krug(0, 0);
                    return shape;
                case "T":
                    shape = new triangle(0, 0);
                    return shape;
                case "S":
                    shape = new square(0, 0);
                    return shape;
                case "G":
                    shape = new Group(0);
                    return shape;
                default:
                    return null;
            }

        }
    }
}
