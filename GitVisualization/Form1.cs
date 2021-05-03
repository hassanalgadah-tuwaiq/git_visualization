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
        private List<string> commitId;

        public Form1()
        {
            InitializeComponent();
            commitId = new List<string>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (var repo = new Repository(@"C:\Users\Hassan\Desktop\playinggit"))
            {
                int count = 0;
                int counter = 0;
                int x = 0;
                int y = 100;
                var branches = repo.Branches; // repo.Head.Tip
                foreach (var b in branches.Reverse())
                {

                    // MessageBox.Show(b.FriendlyName);
                    foreach (var commit in b.Commits.Reverse())
                    {
                        if (commitId.Contains(commit.Id.ToString())) {counter++;count++; continue;}
                        if(counter != 0 && x==0){x = 70 * (counter);}

                        if (repo.Head.Tip.Id == commit.Id)
                        {
                            TextRenderer.DrawText(e.Graphics, "HEAD", new Font("Arial", 7),
                                new Rectangle(x, y-80, 40, 20), SystemColors.ControlText);
                            g.DrawLine(new Pen(Brushes.Black, 3), x + 10, y-60, x + 10, y);
                        }
                        g.FillEllipse(new SolidBrush(Color.RosyBrown), x, y, 20, 20);
                        TextRenderer.DrawText(e.Graphics, commit.Id.ToString().Substring(0, 7),
                            new Font("Arial", 7),
                            new Rectangle(x, y+20, 60, 20), SystemColors.ControlText);
                        
                        if (count == b.Commits.Reverse().Count() - 1 )
                        {
                            g.DrawRectangle(new Pen(Color.Black), x, y-50, 40, 20);
                            g.DrawLine(new Pen(Brushes.Black, 3), x + 10, y-30, x + 10, y);
                        }
                        else
                        {
                            g.DrawLine(new Pen(Brushes.Black, 3), x + 20, y+10, x + 70, y+10);
                        }

                        commitId.Add(commit.Id.ToString());
                        x += 70;

                        count++;
                    }

                    counter = 0;

                    count = 0;

                    y += 150;
                    x = 0;
                    //
                    // int branchPlace = 0;
                    //     int counter = 0;
                    //
                    //     foreach (var commit in b.Commits.Reverse())
                    //     {
                    //         if (commitId.Contains(commit.Id.ToString())) {counter++; continue;}
                    //         if(branchPlace == 0 ){branchPlace = 55 * counter;}
                    //
                    //         g.FillEllipse(new SolidBrush(Color.RosyBrown), branchPlace, 150, 20, 20);
                    //         
                    //         
                    //         TextRenderer.DrawText(e.Graphics, commit.Id.ToString().Substring(0, 7),
                    //             new Font("Arial", 7),
                    //             new Rectangle(branchPlace, 170, 60, 20), SystemColors.ControlText);
                    //         
                    //         
                    //         if (count != b.Commits.Count() - 1) g.DrawLine(new Pen(Brushes.Black, 3), branchPlace + 20, 160, branchPlace + 70, 160);
                    //         else g.DrawLine(new Pen(Brushes.Black, 3), branchPlace + 10, 160, branchPlace + 10, 160);
                    //         
                    //         branchPlace += 70;
                    //     }
                    
                }
            }
        }

        private void Form1_Scroll(object sender, ScrollEventArgs e)
        {
        }
    }
}