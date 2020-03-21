using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace musicOrganizer_V2
{

    public partial class Form1 : Form
    {
        private string name;
        public List<string> songList = new List<string>();
        public Form1()
        {
            InitializeComponent();
            this.listBox1.AllowDrop = true;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            name = DirectoryTools.Loc();



            int songC = Directory.GetFiles(name, "*", SearchOption.TopDirectoryOnly).Length;

            DirectoryInfo Files = new DirectoryInfo(name);

            int size = songC * 2;

            

            foreach (var hold in Files.GetFiles("*", SearchOption.TopDirectoryOnly))
            {
                songList.Add(hold.Name);
                listBox1.Items.Add(hold.Name);
            }

        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.listBox1.SelectedItem == null) return;
            this.listBox1.DoDragDrop(this.listBox1.SelectedItem, DragDropEffects.Move);
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            Point point = listBox1.PointToClient(new Point(e.X, e.Y));
            int index = this.listBox1.IndexFromPoint(point);
            if (index < 0) index = this.listBox1.Items.Count - 1;
            // what is this line doing \/
            object data = e.Data.GetData(typeof(String));
            this.listBox1.Items.Remove(data);
            this.listBox1.Items.Insert(index, data);
        }

        

        private void listBox1_MouseHover(object sender, EventArgs e)
        {
            //will implement at a later date.
        }

        private void save_Click(object sender, EventArgs e)
        {//start of save_click_button

            try
            {



                songList.Clear();

                foreach (string song in listBox1.Items)
                {
                    songList.Add(song);
                }

                string[] result = new string[songList.Count];

                string pat = @"\{\[[0-9]{1,}\]\}";
                string pats = @"\{\[[0-9]{1,}\]\}+";
                int matchnumb = 0;
                int songnumb = 1;



                for (int a = 0; a < songList.Count; a++)
                {
                    matchnumb = Regex.Matches(songList.ElementAt(a), pat).Count;

                    if (matchnumb >= 1)
                    {



                        string hold = Regex.Split(songList.ElementAt(a), pats).ElementAt(1);

                        result.SetValue(hold, a);



                    }
                else
                    {
                        result[a] = songList.ElementAt(a);
                    }
                }
                List<string> toconv = result.ToList<string>();

                int inc = 0;

                for (int i = 0; i < songList.Count; i++)


                    if (File.Exists(name + "\\" + songList[i]))
                    {

                        inc++;
                        System.IO.File.Move(name + "\\" + songList[i], name + "\\" + "{[" + songnumb + "]}" + toconv[i]);

                        songnumb++;

                    }
                label1.Text = "Changes were sucessful";
            }

            catch(DirectoryNotFoundException)
            {
                label1.Text = "Changes were Not sucessful. The directory could not be found.";
            }
        }//end of save_click_button
    }
}
