using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitVisualization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        { 
           Graphics g = e.Graphics;

            using (var repo = new Repository(@"C:\Users\h--sa\Desktop\new\draw-app-project"))
            {
                int count = 0;
                int x = 20;
                var branches = repo.Branches;// repo.Head.Tip
                foreach (var b in branches)
                {
                    if (b.FriendlyName.ToString() == "origin/main")
                    {
                        foreach (var commit in b.Commits.Reverse())
                        {
                            if (repo.Head.Tip.Id == commit.Id)
                            {
                                TextRenderer.DrawText(e.Graphics, "HEAD", new Font("Arial", 7),
                                new Rectangle(x, 20, 40, 20), SystemColors.ControlText);
                                g.DrawLine(new Pen(Brushes.Black, 3), x + 10, 40 , x +10, 100);

                            }
                            g.FillEllipse(new SolidBrush(Color.RosyBrown) ,x, 100, 20, 20);
                            TextRenderer.DrawText(e.Graphics, commit.Id.ToString().Substring(0,7), new Font("Arial",7),
                            new Rectangle(x, 120, 60, 20), SystemColors.ControlText);
                            if (count != b.Commits.Count() - 1)
                            {
                                g.DrawLine(new Pen(Brushes.Black, 3), x + 20, 110, x + 70, 110);
                            }
                            else
                            {
                                g.DrawRectangle(new Pen(Color.Black), x, 50, 40, 20);
                                g.DrawLine(new Pen(Brushes.Black, 3), x + 10, 70, x + 10, 100);

                            }
                            x += 70;
                            
                            count++;
                         
                        }
                    }
                }
            }

        }

        private void Form1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
