using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string FileName;

        Form3 frm3 = new Form3();
        Form2 frm2 = new Form2();
        Pen mypen = new Pen(Color.Black);
        Font myfont = new Font("Microsoft Sans Serif", 14.0f);
        char[,] matris = new char[9,9];
        int[,] sudoku = new int[9,9];
        int[,] sudoku2 = new int[9, 9];
        int[,] sudoku3= new int[9, 9];
        int sayac = 0;
        Thread t1, t2, t3;
        private void textYaz(int [,] s,int t)
        {
            if (t == 1)
            {
                string dosya_yolu = @"C:\Users\BuğraSoysal\sudoku.txt";
                FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                int a, b;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        a = i + 1; b = j + 1;
                        String sudoku = "" + a + "," + "" + b + " " + "Noktası=" + "" +(s[i, j]);
                        sw.WriteLine(sudoku);
                    }

                }

                sw.Flush();
                sw.Close();
                fs.Close();
            }
            else if(t==2)
            {
                string dosya_yolu = @"C:\Users\BuğraSoysal\sudoku2.txt";
                FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                int a, b;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        a = i + 1; b = j + 1;
                        String sudoku = "" + a + "," + "" + b + " " + "Noktası=" + "" + (s[i, j]);
                        sw.WriteLine(sudoku);
                    }

                }

                sw.Flush();
                sw.Close();
                fs.Close();
            }
            else if(t==3)
            {
                string dosya_yolu = @"C:\Users\BuğraSoysal\sudoku3.txt";
                FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                int a, b;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        a = i + 1; b = j + 1;
                        String sudoku = "" + a + "," + "" + b + " " + "Noktası=" + "" + (s[i, j]);
                        sw.WriteLine(sudoku);
                    }

                }

                sw.Flush();
                sw.Close();
                fs.Close();
            }
           
           
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            frm2.Hide();frm3.Hide();
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open Text File";
            open.Filter = "TXT files|*.txt";

            if (open.ShowDialog() == DialogResult.OK)
            {
                FileName = open.FileName;

            }
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);


            String s = "";
            char[] r;
            
            for (int k = 0; (s = sw.ReadLine()) != null; k++)
            {
                r = s.ToCharArray();
                for (int l = 0; l < r.Length; l++)
                {
                    matris[k,l] = r[l];
                }
            }
           
            sudoku = intYap(matris);
            sudoku2 = intYap(matris);
            sudoku3 = intYap(matris);
            sayiYaz(sudoku,panel1);
         
            sw.Close();
            fs.Close();
        }
     
        private void sayiYaz(int[,] m,Panel pnl)
        {
            panelTemizle(pnl);
            Graphics g = pnl.CreateGraphics();
            int k = 25, l = 25;
            String s = "";

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Point point = new Point(k, l);
                    s = String.Concat(m[i, j]);
                    if (s == "0")
                    {
                        s=" ";
                        g.DrawString(s, myfont, Brushes.Black, point);

                    }
                    else
                    {
                        g.DrawString(s, myfont, Brushes.Black, point);

                    }
                    k = k + 70;
                }
                k = 25;
                l = l + 70;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            for (int i = 70; i <= 560; i = i + 70)
            {
                Point point = new Point(0, i);
                Point point2 = new Point(630, i);
                Point point3 = new Point(i, 0);
                Point point4 = new Point(i, 630);
                if (i == 210 || i == 420)
                {
                    mypen.Width = 3;
                }
                g.DrawLine(mypen, point, point2);
                g.DrawLine(mypen, point3, point4);
                mypen.Width = 1;
            }
        }
        private void ekranaYaz(int[,] s,Panel pnl)
        {
            sayiYaz(s,pnl);
            Thread.Sleep(10);
        }
        private void panelTemizle(Panel pnl)
        {
            Graphics g = pnl.CreateGraphics();
           
            SolidBrush whiteBrush = new SolidBrush(Color.White);
           
            g.FillRectangle(whiteBrush,0, 0, 630, 630);
            this.Invalidate();
        }
        private void onCompleted(int a)
        {
            if(a==1)
            {
               
                t2.Abort();
                t3.Abort();
                sayac = 1;
            }
            else if (a==2)
            {
               
                t1.Abort();
                t3.Abort();
                sayac = 2;
            }
            else if (a==3)
            {
                t2.Abort();
                t1.Abort();
                sayac = 3;
               
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch sira = new Stopwatch();
            Stopwatch sira2= new Stopwatch();
            Stopwatch sira3= new Stopwatch();
            long hesap,hesap2,hesap3;
           
            t1 = new Thread(() =>
            {
                sira.Start();
                sudokuCoz(sudoku,3, 7);
                sira.Stop();
                onCompleted(1); 
            });
            t1.Start();
          
           
            t2 = new Thread(() => {

                sira2.Start();
                sudokuCoz(sudoku2, 0, 0);
                sira2.Stop();
                onCompleted(2);              
            });
            t2.Start();
            
            
            t3 = new Thread(() => {
                sira3.Start();
                sudokuCoz(sudoku3, 8,2);
                sira3.Stop();
                onCompleted(3);            
            });
            t3.Start();

            Thread.Sleep(140);
            if (sayac!=0)
            {
                frm2.Show(); frm3.Show();
                ekranaYaz(sudoku, panel1);
                ekranaYaz(sudoku2, frm2.panel1);
                ekranaYaz(sudoku3, frm3.panel1);
                textYaz(sudoku, 1);
                textYaz(sudoku2, 2);
                textYaz(sudoku3, 3);
                sayac = 0;
            }
           
            
            hesap = sira.ElapsedTicks;          
            textBox1.Text = ""+hesap+"ms";
            hesap2 = sira2.ElapsedTicks; 
            textBox2.Text = ""+hesap2 + "ms";
            hesap3 = sira3.ElapsedTicks;   
            textBox3.Text = ""+hesap3+"ms";

            

        }
        private Boolean ayniSutun(int x,int y,int num, int[,]s)
        {
          for(int i=0;i<9;i++)
            {
                if(s[x,i]==num)
                {
                    return true;
                }
            }
            return false;
        }
        private Boolean ayniSira(int x, int y, int num, int[,]s)
        {
            for (int i = 0; i < 9; i++)
            {
                if (s[i,y] == num)
                {
                    return true;
                }
            }
            return false;
        }
        private Boolean ayniKare(int x, int y, int num,int[,]s)
        {
            if (x < 3)
            {
                x = 0;
            }
            else if(x < 6)
            {
                x = 3;
            }
            else
            {
                x = 6;
            }

            if (y < 3)
            {
                y = 0;
            }else if (y < 6)
            {
                y = 3;
            }else
            {
                y = 6;
            }

            for (int i = x; i < x+3; i++)
            {
                for (int j = y; j < y+3; j++)
                {
                    if (s[i, j] == num)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

      

        private Boolean sudokuCoz(int [,] s,int x, int y)
        {
            int num = 1;
            int tx = 0;
            int ty = 0;
            if(s[x,y]!=0)
            {

                if (x==8 && y==8){
                    return true;
                }
                if (x < 8)
                {
                    x++;
                }
                else
                {
                    x = 0;
                    y++;
                }
                if (sudokuCoz(s,x, y))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (s[x, y] == 0)
            {
                while (num < 10)
                {
                    if(!ayniSutun(x,y,num,s) && !ayniSira(x,y,num,s) && !ayniKare(x,y,num,s))
                    {
                        s[x, y] = num;
                        /* if (a == 1)
                        {
                            ekranaYaz(s, panel1);
                        }
                        else if (a == 2)
                        {
                            ekranaYaz(s, frm2.panel1);
                        }
                        else if (a == 3)
                        {
                            ekranaYaz(s, frm3.panel1);
                        }
                        */
                        if (x == 8 && y == 8)
                        {
                            x = 0;
                            y = 0;
                            if(sudokuCoz(s,x,y))
                                return true;
                        }
                        if (x < 8)
                        {
                            tx=x+1;
                        }
                        else
                        {
                            tx = 0;
                            ty=ty+1;
                        }

                        if (sudokuCoz(s,tx, ty))
                        {
                            return true;
                        }
                        
                    }
                    num++;
                }
                s[x, y] = 0;
               
                return false;
            }
            return true;
        }
        
        private int[,] intYap(char [,] m)
        {
            int[,] dizi = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if((Convert.ToInt16(m[i, j]) - 48)==-6)
                    {
                        dizi[i, j] = 0;
                    }
                    else
                    {
                        dizi[i, j] = (Convert.ToInt16(m[i, j]) - 48);

                    }
                }
                
            }

            return dizi;
        }
    }
}
