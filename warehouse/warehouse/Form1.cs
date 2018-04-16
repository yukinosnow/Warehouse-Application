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

namespace warehouse
{
    public partial class Form1 : Form
    {
        item_on_map[,] map;
        Label[,] labelset;
        int width;
        int height;
        itemset itemcollection;
        List<string[]> idset;
        string currentdirectory = System.Environment.CurrentDirectory;
        TextWriter tw;
        public Form1()
        {
            InitializeComponent();
           // MessageBox.Show(currentdirectory);
        }

        private void Select_File_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            itemcollection = new itemset();
            openFileDialog1.InitialDirectory = "C:\\Users\\Junchu\\Documents\\EECS221APP";
            openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MessageBox.Show(openFileDialog1.FileName.ToString());
                    string filedirectory = openFileDialog1.FileName.ToString();
                    using (var reader = new StreamReader(filedirectory))
                    {
                        
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            //MessageBox.Show(values[0].ToString() + " " + values[1].ToString() + " " + values[2].ToString());
                            itemcollection.additem(values[0].ToString(), values[1].ToString(), values[2].ToString());
                            
                        }
                        MessageBox.Show("Finish Reading"+" maxx="+itemcollection.maxx.ToString()+" maxy="+itemcollection.maxy.ToString());
                        

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
                
                map = new item_on_map[2*(itemcollection.maxy+1)+1, 2*(itemcollection.maxx+1)+1];
                width = 2 * (itemcollection.maxx+1) + 1;
                height = 2 * (itemcollection.maxy+1) + 1;
                for (int row=0;row< 2 * (itemcollection.maxy+1)+1; row++)
                {
                    for(int column=0;column< 2 * (itemcollection.maxx+1)+1; column++)
                    {
                        map[row, column] = new item_on_map();
                    }
                }
                for(int count=0;count<itemcollection.storelist.Count;count++)
                {
                    map[2*(itemcollection.storelist[count].y)+1, 2*(itemcollection.storelist[count].x)+1].itemnumber++;
                }
                int maprow = groupBox1.Height / (2 * (itemcollection.maxy+1)+1);
                int mapcolumn = groupBox1.Width/(2 * (itemcollection.maxx+1)+1);
                MessageBox.Show("x=" + mapcolumn.ToString() + " y=" + maprow.ToString());
                labelset = new Label[2 * (itemcollection.maxy+1)+1, 2 * (itemcollection.maxx+1)+1];
                for (int i=0;i< 2 * (itemcollection.maxy+1)+1; i++)
                {
                    for (int j=0;j< 2 * (itemcollection.maxx+1)+1; j++)
                    {

                        labelset[i, j] = new Label();
                        labelset[i,j].Text = map[(2 * (itemcollection.maxy+1))-i, j].itemnumber.ToString() + "\n" + "[" + (j).ToString() + "," + ((2 * (itemcollection.maxy+1))-i).ToString() + "]";
                        if(map[(2 * (itemcollection.maxy+1)) - i, j].itemnumber!=0)
                        {
                            labelset[i, j].BackColor = Color.Black;
                            labelset[i, j].ForeColor = Color.White;
                        }
                        labelset[i, j].Font = new Font("Verdana", 6, FontStyle.Bold | FontStyle.Regular);
                        labelset[i, j].Size = new Size(mapcolumn, maprow);
                        labelset[i, j].Location = new Point(j*mapcolumn+20, i * maprow+20);
                        groupBox1.Controls.Add(labelset[i, j]);
                        //MessageBox.Show("column=" + (j * mapcolumn).ToString() + " row=" + (i * maprow).ToString());
                    }
                }
                MessageBox.Show("Complete Showing Map");
                Starting_Position.Enabled = true;
                Destionation.Enabled = true;
                Find_Path.Enabled = true;
                Start_location.Enabled = true;
                End_location.Enabled = true;
                Multi_item_set.Enabled = true;
                Find_multi_item.Enabled = true;
                LoadOrder.Enabled = true;
                Clear_Path.Enabled = true;
                Clear_Path_Simply.Enabled = true;
                Find_Path_Multi_No_Change_Order.Enabled = true;

            }
        }

        private void Find_Path_Click(object sender, EventArgs e)
        {
            try
            {
                String[] start = Starting_Position.Text.Split(',');
                string[] end = Destionation.Text.Split(',');
                int startx = Convert.ToInt32(start[0]);
                int starty = Convert.ToInt32(start[1]);
                int endx = Convert.ToInt32(end[0]);
                int endy = Convert.ToInt32(end[1]);
                if (startx > map.GetLength(1) || starty > map.GetLength(0) || map[starty, startx].itemnumber != 0)
                {
                    MessageBox.Show("invalid starting point");
                    //MessageBox.Show(map.GetLength(0).ToString() + " " + map.GetLength(1).ToString());
                    return;
                }
                if (endx > map.GetLength(1) || endy > map.GetLength(0) || map[endy, endx].itemnumber == 0)
                {
                    MessageBox.Show("invalid end point");
                    return;
                }
                findpath(startx, starty, endx, endy);
                
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            

        }

        void findpath(int sx, int sy,int dx, int dy)
        {
            
            Queue<string> q = new Queue<string>();
            Queue<string> nextround = new Queue<string>();
            q.Enqueue(sx.ToString() + "," + sy.ToString());
            int distance = 0;
            map[sy, sx].setnode(distance, "start");
            bool find = false;
            while ((q.Count > 0 || nextround.Count > 0)&&!find)
            {

                while (q.Count > 0)
                {
                    string location = q.Dequeue();
                    string[] cordination = location.Split(',');
                    int y = Convert.ToInt32(cordination[1]);
                    int x = Convert.ToInt32(cordination[0]);
                    //MessageBox.Show("start position=" + x.ToString() + "," + y.ToString() + " end position="+dx.ToString()+","+dy.ToString());
                    int nexty = y - 1;
                    int nextx = x;
                    if(nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                    {
                        if(!map[nexty,nextx].visited)
                        {
                            
                            if (map[nexty, nextx].itemnumber == 0)
                            {
                                map[nexty, nextx].setnode(distance, location);
                                nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                            }
                        }
                    }
                    nexty = y + 1;
                    nextx = x;
                    if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                    { 
                        if (!map[nexty, nextx].visited)
                        {
                            
                            if (map[nexty, nextx].itemnumber == 0)
                            {
                                map[nexty, nextx].setnode(distance, location);
                                nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                            }
                        }
                    }
                    nexty = y;
                    nextx = x-1;
                    if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                    {
                        if (!map[nexty, nextx].visited)
                        {
                            map[nexty, nextx].setnode(distance, location);
                            if (map[nexty, nextx].itemnumber == 0)
                            {
                                nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                            }
                        }
                    }
                    if ((nextx == dx) && (nexty == dy))
                    {
                        find = true;
                        break;
                    }
                    nexty = y;
                    nextx = x + 1;
                    if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                    {
                        if (!map[nexty, nextx].visited)
                        {
                            map[nexty, nextx].setnode(distance, location);
                            if (map[nexty, nextx].itemnumber == 0)
                            {
                                nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                            }
                        }
                    }
                    if ((nextx == dx) && (nexty == dy))
                    {
                        find = true;
                        break;
                    }

                }
                distance++;
                while(nextround.Count>0)
                {
                    q.Enqueue(nextround.Dequeue());
                }
                
            }
            MessageBox.Show("starting position=" + sx.ToString() + "," + sy.ToString() + " end point=" + dx.ToString() + "," + dy.ToString() + " distance=" + map[dy, dx].distance.ToString());
            paint(dx, dy);


        }

        int findpath_exit(int sx,int sy, int dx, int dy,bool write)
        {
            Queue<string> q = new Queue<string>();
            Queue<string> nextround = new Queue<string>();
            q.Enqueue(sx.ToString() + "," + sy.ToString());
            int distance = 0;
            map[sy, sx].setnode(distance, "start");
            bool find = false;
            while ((q.Count > 0 || nextround.Count > 0) && !find)
            {

                while (q.Count > 0)
                {
                    string location = q.Dequeue();
                    string[] cordination = location.Split(',');
                    int y = Convert.ToInt32(cordination[1]);
                    int x = Convert.ToInt32(cordination[0]);
                    //MessageBox.Show("start position=" + x.ToString() + "," + y.ToString() + " end position="+dx.ToString()+","+dy.ToString());
                    int nexty = y - 1;
                    int nextx = x;
                    if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                    {
                        if (!map[nexty, nextx].visited)
                        {

                            if (map[nexty, nextx].itemnumber == 0)
                            {
                                map[nexty, nextx].setnode(distance, location);
                                nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                            }
                        }
                    }
                    if ((nextx == dx) && (nexty == dy))
                    {
                        find = true;
                        break;
                    }
                    nexty = y + 1;
                    nextx = x;
                    if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                    {
                        if (!map[nexty, nextx].visited)
                        {

                            if (map[nexty, nextx].itemnumber == 0)
                            {
                                map[nexty, nextx].setnode(distance, location);
                                nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                            }
                        }
                    }
                    if ((nextx == dx) && (nexty == dy))
                    {
                        find = true;
                        break;
                    }
                    nexty = y;
                    nextx = x - 1;
                    if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                    {
                        if (!map[nexty, nextx].visited)
                        {
                            map[nexty, nextx].setnode(distance, location);
                            if (map[nexty, nextx].itemnumber == 0)
                            {
                                nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                            }
                        }
                    }
                    if ((nextx == dx) && (nexty == dy))
                    {
                        find = true;
                        break;
                    }
                    nexty = y;
                    nextx = x + 1;
                    if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                    {
                        if (!map[nexty, nextx].visited)
                        {
                            map[nexty, nextx].setnode(distance, location);
                            if (map[nexty, nextx].itemnumber == 0)
                            {
                                nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                            }
                        }
                    }
                    if ((nextx == dx) && (nexty == dy))
                    {
                        find = true;
                        break;
                    }

                }
                distance++;
                while (nextround.Count > 0)
                {
                    q.Enqueue(nextround.Dequeue());
                }

            }
            
            if (!write)
            {
                MessageBox.Show("starting position=" + sx.ToString() + "," + sy.ToString() + " end point=" + dx.ToString() + "," + dy.ToString() + " distance=" + map[dy, dx].distance.ToString());
                paint(dx, dy);
            }
            else
            {
                tw.WriteLine("starting position=" + sx.ToString() + "," + sy.ToString() + " end point=" + dx.ToString() + "," + dy.ToString() + " distance=" + map[dy, dx].distance.ToString());
            }
            return map[dy, dx].distance;
        }

        List<string> findpath_multi(int startx, int starty, List<string>destination,bool write)
        {
            bool firststart = true;
            int sx = startx;
            int sy = starty;
            string resultcor = "";
            List<int> dx = new List<int>();
            List<int> dy = new List<int>();
            int direction = 0;
            int totaldistance = 0;
            List<string> coordinateset = new List<string>();
            foreach (string cor in destination)
            {
                string[] temp = cor.Split(',');
                dx.Add(Convert.ToInt32(temp[0]));
                dy.Add(Convert.ToInt32(temp[1]));
            }
            while (dx.Count > 0)
            {
                if(firststart)
                {
                    firststart = false;
                }
                else
                {
                    string[] temp = resultcor.Split(',');
                    sx = Convert.ToInt32(temp[0])+direction;
                    sy = Convert.ToInt32(temp[1]);
                    direction = 0;
                }
                Queue<string> q = new Queue<string>();
                Queue<string> nextround = new Queue<string>();
                q.Enqueue(sx.ToString() + "," + sy.ToString());
                int distance = 0;
                map[sy, sx].setnode(distance, "start");
                bool find = false;
                
                while ((q.Count > 0 || nextround.Count > 0) && !find)
                {

                    while (q.Count > 0)
                    {
                        string location = q.Dequeue();
                        string[] cordination = location.Split(',');
                        int y = Convert.ToInt32(cordination[1]);
                        int x = Convert.ToInt32(cordination[0]);
                        //MessageBox.Show("start position=" + x.ToString() + "," + y.ToString() + " end position=" + dx.ToString() + "," + dy.ToString());
                        int nexty = y - 1;
                        int nextx = x;
                        if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                        {
                            if (!map[nexty, nextx].visited)
                            {
                                
                                if (map[nexty, nextx].itemnumber == 0)
                                {
                                    map[nexty, nextx].setnode(distance, location);
                                    nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                                }
                            }
                        }
                        nexty = y + 1;
                        nextx = x;
                        if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                        {
                            if (!map[nexty, nextx].visited)
                            {
                                
                                if (map[nexty, nextx].itemnumber == 0)
                                {
                                    map[nexty, nextx].setnode(distance, location);
                                    nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                                }
                            }
                        }
                        nexty = y;
                        nextx = x - 1;
                        if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                        {
                            if (!map[nexty, nextx].visited)
                            {
                                map[nexty, nextx].setnode(distance, location);
                                if (map[nexty, nextx].itemnumber == 0)
                                {
                                    nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                                }
                            }
                        }
                        for (int i = 0; i < dx.Count; i++)
                        {
                            if ((nextx == dx[i]) && (nexty == dy[i]))
                            {
                                find = true;
                                resultcor = dx[i].ToString() + "," + dy[i].ToString();
                                dx.RemoveAt(i);
                                dy.RemoveAt(i);
                                direction = 1;
                                if (!write)
                                {
                                    MessageBox.Show(resultcor);
                                }
                                break;
                            }
                        }
                        if(find)
                        {
                            break;
                        }
                        nexty = y;
                        nextx = x + 1;
                        if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                        {
                            if (!map[nexty, nextx].visited)
                            {
                                map[nexty, nextx].setnode(distance, location);
                                if (map[nexty, nextx].itemnumber == 0)
                                {
                                    nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                                }
                            }
                        }
                        for (int i = 0; i < dx.Count; i++)
                        {
                            if ((nextx == dx[i]) && (nexty == dy[i]))
                            {
                                find = true;
                                resultcor = dx[i].ToString() + "," + dy[i].ToString();
                                dx.RemoveAt(i);
                                dy.RemoveAt(i);
                                direction = -1;
                                if (!write)
                                {
                                    MessageBox.Show(resultcor);
                                }
                                break;
                            }
                        }
                        if (find)
                        {
                            break;
                        }
                    }
                    distance++;
                    while (nextround.Count > 0)
                    {
                        q.Enqueue(nextround.Dequeue());
                    }

                }
                string[] temp1 = resultcor.Split(',');
                int rx = Convert.ToInt32(temp1[0]);
                int ry = Convert.ToInt32(temp1[1]);
                totaldistance = totaldistance + map[ry, rx].distance;
                if (!write)
                {
                    MessageBox.Show("starting position=" + sx.ToString() + "," + sy.ToString() + " end point=" + rx.ToString() + "," + ry.ToString() + " distance=" + map[ry, rx].distance.ToString());
                }
                else
                {
                    tw.WriteLine("starting position=" + sx.ToString() + "," + sy.ToString() + " end point=" + rx.ToString() + "," + ry.ToString() + " distance=" + map[ry, rx].distance.ToString());
                }
                coordinateset.Add(rx.ToString() + "," + ry.ToString());
                if (!write)
                {
                    paint(rx, ry);
                }
                foreach (item_on_map cordination in map)
                {
                    cordination.cleardata();
                }
            }
            string[]lasttemp = resultcor.Split(',');
            sx = Convert.ToInt32(lasttemp[0]) + direction;
            sy = Convert.ToInt32(lasttemp[1]);
            direction = 0;
            string[] lastend = End_location.Text.Split(',');
            int ex = Convert.ToInt32(lastend[0]);
            int ey = Convert.ToInt32(lastend[1]);
            totaldistance=totaldistance+findpath_exit(sx, sy, ex, ey,write);
            string path = "";
            foreach (string temp in coordinateset)
            {
                path = path + " " + temp;
            }
            if (!write)
            {
                MessageBox.Show("the path is " + path + " the total distance is " + totaldistance.ToString());
            }
            else
            {
                tw.WriteLine("the optimal path is " + path + " the total distance is " + totaldistance.ToString());
            }
            return coordinateset;

        }

        List<string> findpath_multi_no_change_order(int startx, int starty, List<string> destination,bool write)
        {
            bool firststart = true;
            int sx = startx;
            int sy = starty;
            string resultcor = "";
            List<int> dx = new List<int>();
            List<int> dy = new List<int>();
            int direction = 0;
            int totaldistance = 0;
            List<string> coordinateset = new List<string>();
            foreach (string cor in destination)
            {
                string[] temp = cor.Split(',');
                dx.Add(Convert.ToInt32(temp[0]));
                dy.Add(Convert.ToInt32(temp[1]));
            }
            while (dx.Count > 0)
            {
                if (firststart)
                {
                    firststart = false;
                }
                else
                {
                    string[] temp = resultcor.Split(',');
                    sx = Convert.ToInt32(temp[0]) + direction;
                    sy = Convert.ToInt32(temp[1]);
                    direction = 0;
                }
                Queue<string> q = new Queue<string>();
                Queue<string> nextround = new Queue<string>();
                q.Enqueue(sx.ToString() + "," + sy.ToString());
                int distance = 0;
                map[sy, sx].setnode(distance, "start");
                bool find = false;

                while ((q.Count > 0 || nextround.Count > 0) && !find)
                {

                    while (q.Count > 0)
                    {
                        string location = q.Dequeue();
                        string[] cordination = location.Split(',');
                        int y = Convert.ToInt32(cordination[1]);
                        int x = Convert.ToInt32(cordination[0]);
                        //MessageBox.Show("start position=" + x.ToString() + "," + y.ToString() + " end position=" + dx.ToString() + "," + dy.ToString());
                        int nexty = y - 1;
                        int nextx = x;
                        if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                        {
                            if (!map[nexty, nextx].visited)
                            {

                                if (map[nexty, nextx].itemnumber == 0)
                                {
                                    map[nexty, nextx].setnode(distance, location);
                                    nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                                }
                            }
                        }
                        nexty = y + 1;
                        nextx = x;
                        if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                        {
                            if (!map[nexty, nextx].visited)
                            {

                                if (map[nexty, nextx].itemnumber == 0)
                                {
                                    map[nexty, nextx].setnode(distance, location);
                                    nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                                }
                            }
                        }
                        nexty = y;
                        nextx = x - 1;
                        if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                        {
                            if (!map[nexty, nextx].visited)
                            {
                                map[nexty, nextx].setnode(distance, location);
                                if (map[nexty, nextx].itemnumber == 0)
                                {
                                    nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                                }
                            }
                        }

                        if ((nextx == dx[0]) && (nexty == dy[0]))
                        {
                            find = true;
                            resultcor = dx[0].ToString() + "," + dy[0].ToString();
                            dx.RemoveAt(0);
                            dy.RemoveAt(0);
                            direction = 1;
                            if (!write)
                            {
                                MessageBox.Show(resultcor);
                            }
                            break;
                        }

                        nexty = y;
                        nextx = x + 1;
                        if (nexty >= 0 && nexty < height && nextx >= 0 && nextx < width)
                        {
                            if (!map[nexty, nextx].visited)
                            {
                                map[nexty, nextx].setnode(distance, location);
                                if (map[nexty, nextx].itemnumber == 0)
                                {
                                    nextround.Enqueue(nextx.ToString() + "," + nexty.ToString());
                                }
                            }
                        }

                        if ((nextx == dx[0]) && (nexty == dy[0]))
                        {
                            find = true;
                            resultcor = dx[0].ToString() + "," + dy[0].ToString();
                            dx.RemoveAt(0);
                            dy.RemoveAt(0);
                            direction = -1;
                            if (!write)
                            {
                                MessageBox.Show(resultcor);
                            }
                            break;
                        }


                    }
                    distance++;
                    while (nextround.Count > 0)
                    {
                        q.Enqueue(nextround.Dequeue());
                    }

                }
                string[] temp1 = resultcor.Split(',');
                int rx = Convert.ToInt32(temp1[0]);
                int ry = Convert.ToInt32(temp1[1]);
                totaldistance = totaldistance + map[ry, rx].distance;
                if (!write)
                { 
                    MessageBox.Show("starting position=" + sx.ToString() + "," + sy.ToString() + " end point=" + rx.ToString() + "," + ry.ToString() + " distance=" + map[ry, rx].distance.ToString());
                }
                else
                {
                    tw.WriteLine("starting position=" + sx.ToString() + "," + sy.ToString() + " end point=" + rx.ToString() + "," + ry.ToString() + " distance=" + map[ry, rx].distance.ToString());
                }
                coordinateset.Add(rx.ToString() + "," + ry.ToString());
                if (!write)
                {
                    paint(rx, ry);
                }
                foreach (item_on_map cordination in map)
                {
                    cordination.cleardata();
                }
            }
            string[] lasttemp = resultcor.Split(',');
            sx = Convert.ToInt32(lasttemp[0]) + direction;
            sy = Convert.ToInt32(lasttemp[1]);
            direction = 0;
            string[] lastend = End_location.Text.Split(',');
            int ex = Convert.ToInt32(lastend[0]);
            int ey = Convert.ToInt32(lastend[1]);
            totaldistance = totaldistance + findpath_exit(sx, sy, ex, ey,write);
            string path = "";
            foreach (string temp in coordinateset)
            {
                path = path + " " + temp;
            }
            if (!write)
            {
                MessageBox.Show("the path is " + path + " the total distance is " + totaldistance.ToString());
            }
            else
            {
                tw.WriteLine("the original path is " + path + " the total distance is " + totaldistance.ToString());
            }
            return coordinateset;

        }

        private void paint(int dx, int dy)
        {
            int y = dy;
            int x = dx;
            while(map[y,x].previousnode!="start")
            {
                string[] cor = map[y, x].previousnode.Split(',');
                x = Convert.ToInt32(cor[0]);
                y = Convert.ToInt32(cor[1]);
                labelset[height-y-1, x].BackColor = Color.Red;
            }
        }

        private void Clear_Path_Click(object sender, EventArgs e)
        {
            Find_multi_item.Enabled = true;
            Find_Path_Multi_No_Change_Order.Enabled = true;
            clear();
        }

        private void Clear_Path_Simply_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            foreach (Label i in labelset)
            {
                string[] text = i.Text.Split('\n');
                if (Convert.ToInt32(text[0]) == 0)
                {
                    i.BackColor = Color.White;
                }
                else
                {
                    i.BackColor = Color.Black;
                }
            }
            foreach (item_on_map cordination in map)
            {
                cordination.cleardata();
            }
        }

        private void Find_multi_item_Click(object sender, EventArgs e)
        {
            Find_Path_Multi_No_Change_Order.Enabled = false;
            Find_multi_item.Enabled = false;
            string[] itemid = Multi_item_set.Text.Split(',');
            List<item> collection = new List<item>();
            List<string> cordination = new List<string>();
            String[] start = Start_location.Text.Split(',');
            string[] end = End_location.Text.Split(',');
            int startx = Convert.ToInt32(start[0]);
            int starty = Convert.ToInt32(start[1]);
            List<string> result = new List<string>();
            string order="";
            foreach (string item in itemid)
            {
                bool finded = false;
                foreach (item storeitem in itemcollection.storelist)
                {
                    
                    if(storeitem.id==Convert.ToInt32(item))
                    {
                        collection.Add(storeitem);
                        finded = true;
                    }
                }
                if(!finded)
                {
                    MessageBox.Show("item " + item + " does not find");
                }

            }
            foreach(item individualitem in collection)
            {
                //MessageBox.Show(individualitem.id.ToString() + " [" + individualitem.x.ToString() + "," + individualitem.y.ToString()+"]");
                cordination.Add((2*individualitem.x+1).ToString() + "," + (2*individualitem.y+1).ToString());
            }
            result=findpath_multi(startx, starty, cordination,false);
            foreach (string lookfor in result)
            {
                string[] cor = lookfor.Split(',');
                int x = Convert.ToInt32(cor[0]);
                int y = Convert.ToInt32(cor[1]);
                for(int i=0;i<collection.Count;i++)
                {
                    if(collection[i].x==(x-1)/2 && collection[i].y == (y - 1) / 2)
                    {
                        order = order + collection[i].id.ToString() + ",";
                        collection.RemoveAt(i);
                    }
                }
                
            }
            MessageBox.Show(order);

        }

        private void Find_Path_Multi_No_Change_Order_Click(object sender, EventArgs e)
        {
            Find_multi_item.Enabled = false;
            Find_Path_Multi_No_Change_Order.Enabled = false;
            string[] itemid = Multi_item_set.Text.Split(',');
            List<item> collection = new List<item>();
            List<string> cordination = new List<string>();
            String[] start = Start_location.Text.Split(',');
            string[] end = End_location.Text.Split(',');
            int startx = Convert.ToInt32(start[0]);
            int starty = Convert.ToInt32(start[1]);
            foreach (string item in itemid)
            {
                bool finded = false;
                foreach (item storeitem in itemcollection.storelist)
                {

                    if (storeitem.id == Convert.ToInt32(item))
                    {
                        collection.Add(storeitem);
                        finded = true;
                    }
                }
                if (!finded)
                {
                    MessageBox.Show("item " + item + " does not find");
                }

            }
            foreach (item individualitem in collection)
            {
                //MessageBox.Show(individualitem.id.ToString() + " [" + individualitem.x.ToString() + "," + individualitem.y.ToString() + "]");
                cordination.Add((2 * individualitem.x + 1).ToString() + "," + (2 * individualitem.y + 1).ToString());
            }
            findpath_multi_no_change_order(startx, starty, cordination,false);
        }

        private void LoadOrder_Click(object sender, EventArgs e)
        {
            tw = new StreamWriter(System.Environment.CurrentDirectory + "\\file.txt");
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            idset = new List<string[]>();
            List<string> intput = new List<string>();
            List<item> collection = new List<item>();
            List<string> cordination = new List<string>();
            List<string> result = new List<string>();
            openFileDialog1.InitialDirectory = "C:\\Users\\Junchu\\Documents\\EECS221APP";
            openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            int count = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MessageBox.Show(openFileDialog1.FileName.ToString());
                    string filedirectory = openFileDialog1.FileName.ToString();
                    using (var reader = new StreamReader(filedirectory))
                    {

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split('\t');
                            //MessageBox.Show(line+"/"+values[0]+"/");
                            //MessageBox.Show(values[0].ToString() + " " + values[1].ToString() + " " + values[2].ToString());
                            idset.Add(values);

                        }
                        //MessageBox.Show("Finish Reading" + " maxx=" + itemcollection.maxx.ToString() + " maxy=" + itemcollection.maxy.ToString());


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            String[] start = Start_location.Text.Split(',');
            string[] end = End_location.Text.Split(',');
            string oldorder = "";
            string optimalorder = "";
            int startx = Convert.ToInt32(start[0]);
            int starty = Convert.ToInt32(start[1]);
            foreach (string[] order in idset)
            {
                collection = new List<item>();
                cordination = new List<string>();
                intput = new List<string>();
                oldorder = "";
                optimalorder = "";
                result = new List<string>();
                foreach (string test in order)
                {
                    if (test != "")
                    {
                        intput.Add(test);
                    }
                }
                
                foreach (string item in intput)
                {
                    bool finded = false;
                    foreach (item storeitem in itemcollection.storelist)
                    {

                        if (storeitem.id == Convert.ToInt32(item))
                        {
                            collection.Add(storeitem);
                            finded = true;
                        }
                    }
                    if (!finded)
                    {
                        //MessageBox.Show("item " + item + " does not find");
                        tw.WriteLine("item " + item + " does not find");
                    }

                }
                tw.WriteLine("Order:" + count.ToString());
                tw.WriteLine("Start position=" + Start_location.Text);
                tw.WriteLine("End position=" + End_location.Text);
                tw.WriteLine("");
                foreach (item individualitem in collection)
                {
                    //MessageBox.Show(individualitem.id.ToString() + " [" + individualitem.x.ToString() + "," + individualitem.y.ToString() + "]");
                    cordination.Add((2 * individualitem.x + 1).ToString() + "," + (2 * individualitem.y + 1).ToString());
                    oldorder = oldorder + individualitem.id.ToString() + ",";
                }
                findpath_multi_no_change_order(startx, starty, cordination, true);
                tw.WriteLine("The original order is:" + oldorder);
                clear();
                tw.WriteLine("");
                result = findpath_multi(startx, starty, cordination, true);
                foreach (string lookfor in result)
                {
                    string[] cor = lookfor.Split(',');
                    int x = Convert.ToInt32(cor[0]);
                    int y = Convert.ToInt32(cor[1]);
                    for (int i = 0; i < collection.Count; i++)
                    {
                        if (collection[i].x == (x - 1) / 2 && collection[i].y == (y - 1) / 2)
                        {
                            optimalorder = optimalorder + collection[i].id.ToString() + ",";
                            collection.RemoveAt(i);
                        }
                    }

                }
                tw.WriteLine("The optimal order is:" + optimalorder);
                tw.WriteLine("");
                tw.WriteLine("----------------------------------------------------------------------------------------------------------");
                count++;
                clear();
            }
            
            tw.Close();
            MessageBox.Show("complete");
        }
        
    }
    public class item
    {
        public int id;
        public int x;
        public int y;
        public int dec;
        public item(int id, int x, int y,int dec){
            this.id = Convert.ToInt32(id);
            this.x = x;
            this.y = Convert.ToInt32(y);
            this.dec = dec;
            
            
            //MessageBox.Show("id="+this.id.ToString()+" x="+this.x.ToString()+" y="+ this.y.ToString()+" dec="+this.dec.ToString());

        }
    }
    public class itemset
    {
        public int maxx=0;
        public int maxy=0;
        public List<item> storelist;
        public itemset()
        {
            storelist = new List<item>();
        }
        public void additem(string id,string xdir,string ydir)
        {
            IEnumerable<int> temp = xdir.Split('.').Select(int.Parse);
            List<int> sample = temp.ToList<int>();
            int idnumber = Convert.ToInt32(id);
            int x = Convert.ToInt32(sample[0]);
            int y = Convert.ToInt32(ydir);
            if (x > maxx)
            {
                maxx = x;
            }
            if(y>maxy)
            {
                maxy = y;
            }
            int dec=0;
            if (sample.Count > 1)
            {
                dec = Convert.ToInt32(sample[1]);
            }
            storelist.Add(new item(idnumber, x, y,dec));
        }
    }
    public class item_on_map
    {
        public int itemnumber;
        public int distance;
        public string previousnode;
        public bool visited;
        public item_on_map()
        {
            itemnumber = 0;
            distance = 0;
            previousnode = "";
            visited = false;
        }
        public void cleardata()
        {
            distance = 0;
            previousnode = "";
            visited = false;
        }
        public void setnode(int distance, string previousnode)
        {
            this.visited = true;
            this.distance = distance;
            this.previousnode = previousnode;
        }
    }
}
