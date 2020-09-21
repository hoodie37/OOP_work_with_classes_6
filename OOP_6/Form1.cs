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
    public partial class Form1 : Form
    {
        MyList l = new MyList();
        MyListChild l2 = new MyListChild();
        public Form1()
        {
            InitializeComponent();
            
           
        }

        /////////////////////////// ВАЖНЫЕ СОБЫТИЯ \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        private void pBox_MouseClick(object sender, MouseEventArgs e)
        {
            int flag = 0;
            if (radioBSelect.Checked == true)
            { 
                for (l.First(); l.Eol(); l.Next())
                {
                    if (l.GetObj().mouseInShape(e.X, e.Y))
                    {
                        l.GetObj().Select();
                        flag = 1;
                        pBox.Select();
                        break;
                    }
                }

                if (flag != 1)
                {
                    l.addNew(cBoxShape.Text, e.X, e.Y, pBox.Size.Width, pBox.Size.Height);
                }
               
            }
            else for (l.First(); l.Eol(); l.Next())
                {
                    if (l.GetObj().mouseInShape(e.X, e.Y))
                    {
                        l.GetObj().SwitchColor(cBoxColor.Text);
                    }
                    
                }
            this.Refresh();
            pBox.Select();
        }

        
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            for (l.First(); l.Eol(); l.Next())
            {
                if (l.GetObj().CheckSelect())
                {
                    l.delete(l.GetObj());
                }
                if (l.nalichie() == true && l.GetNextNode() == null)
                {

                    if (l.GetHeadObj().CheckSelect())
                    {
                        l.delete(l.GetHeadObj());
                    }
                }

            }
            this.Refresh();
            pBox.Select();
        }

        private void pBox_KeyDown(object sender, KeyEventArgs e)
        {
            int dx=0, dy=0;
            //////////////ДВИЖЕНИЕ\\\\\\\\\\\\\\\\\
            if (e.KeyCode == Keys.W)
            {
                dy = -1;
            }

            if (e.KeyCode == Keys.S)
            {
                dy = 1;
            }
            if (e.KeyCode == Keys.A)
            {
                dx = -1;
            }
            if (e.KeyCode == Keys.D)
            {
                dx = 1;
            }
            for (l.First(); l.Eol(); l.Next())
            {
                l.GetObj().move(dx, dy, pBox.Size.Width, pBox.Size.Height);
                if(l.GetObj().check(pBox.Size.Width, pBox.Size.Height) == false)
                    l.GetObj().move(-dx, -dy, pBox.Size.Width, pBox.Size.Height);
            }

            //////////////УДАЛЕНИЕ ЧЕРЕЗ DEL\\\\\\\\\\\\\\\\\
            if (e.KeyCode == Keys.Delete)
            {
                for (l.First(); l.Eol(); l.Next())
                {
                    if (l.GetObj().CheckSelect())
                    {
                        l.delete(l.GetObj());
                    }
                    if (l.nalichie() == true && l.GetNextNode() == null)
                    {

                        if (l.GetHeadObj().CheckSelect())
                        {
                            l.delete(l.GetHeadObj());
                        }
                    }
                }
            }


            //////////////УВЕЛИЧЕНИЕ И УМЕНЬШЕНИЕ\\\\\\\\\\\\\\\\\
            if (e.KeyCode == Keys.Oemplus)
            {
                for (l.First(); l.Eol(); l.Next())
                {
                    if (l.GetObj().CheckSelect() == true )
                    {
                        l.GetObj().sizeMove(1, pBox.Size.Width, pBox.Size.Height);
                        if (l.GetObj().check(pBox.Size.Width, pBox.Size.Height) == false)
                            l.GetObj().sizeMove(-1, pBox.Size.Width, pBox.Size.Height);
                    }
                }
            }
            if (e.KeyCode == Keys.OemMinus)
            {
                for (l.First(); l.Eol(); l.Next())
                {
                    if (l.GetObj().CheckSelect() == true)
                    {
                        l.GetObj().sizeMove(-1, pBox.Size.Width, pBox.Size.Height);
                        if (l.GetObj().check(pBox.Size.Width, pBox.Size.Height) == false)
                            l.GetObj().sizeMove(1, pBox.Size.Width, pBox.Size.Height);
                    }
                }
            }


            this.Refresh();
        }

        /////////////////////////// НЕ ОЧЕНЬ ВАЖНЫЕ СОБЫТИЯ \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        private void pBox_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.X.ToString();
            label2.Text = e.Y.ToString();
            pBox.Select();
        }

        private void pBox_Paint(object sender, PaintEventArgs e)
        {
            for (l.First(); l.Eol(); l.Next())
            {
                l.GetObj().draw(e.Graphics);
            }
            pBox.Select();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cBoxShape.SelectedItem = "Круг";
            cBoxColor.SelectedItem = "Синий";
            radioBSelect.Select();
            pBox.KeyDown += new KeyEventHandler(pBox_KeyDown); 
            pBox.Select();
        }

        public int SelectedCount()
        {
            int count = 0;
            for (l.First(); l.Eol(); l.Next())
            {
                if (l.GetObj().CheckSelect() == true)
                    count += 1;
            }
            return count;
        }

        private void bGroup_Click(object sender, EventArgs e)
        {
            int count = SelectedCount();
            Group g = new Group(count);
            g.addShapes(l);
            l.addShape(g);
            pBox.Select();
        }

        private void bUnGroup_Click(object sender, EventArgs e)
        {
            for (l.First(); l.Eol(); l.Next())
            {
                l.GetObj().unGroup(l);
            }
            pBox.Select();
        }

        private void SaveBt_Click(object sender, EventArgs e)
        {
            FileStream file = new FileStream("C:\\Users\\User\\Desktop\\6.3\\OOP_6\\First.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(file);
            l.Save(writer);
            writer.Close();
        }

        private void LoadBt_Click(object sender, EventArgs e)
        {
            FileStream file = new FileStream("C:\\Users\\User\\Desktop\\6.3\\OOP_6\\First.txt", FileMode.Open);
            StreamReader reader = new StreamReader(file);
            l.Load(reader,l);
            reader.Close();
            this.Refresh();
        }
    }
}

