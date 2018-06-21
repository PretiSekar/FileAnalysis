/*
 * Written By: Preethi Sekar
 * Submitted on : 02/16/2018
 * Project On:
 * Analysis on the text file created in the Assignment2
 * Steps:
 * 1.Select the old file which is in the same folder
 * 2.The program reads the old file
 * 3.Analyzes on the old file
 * 4.Creates a new file with the specified entries
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment3_pxs163930
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //initialize componenets
            InitializeComponent();
            toolStripStatusLabel1.Text = "Click on the Open to choose the text file";
        }

        private void OpenFolder(object sender, EventArgs e)
        {
            //open the file explorer to choose the file on click
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Select a text file";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            //customize to view only text files
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }

            String filename = textBox1.Text;

            toolStripStatusLabel1.Text = "";
            String line;
            int no_count = 0;
            int backspace_count = 0;
            ArrayList starttime = new ArrayList();
            ArrayList endtime = new ArrayList();
            //read the existing file to read the data
            toolStripStatusLabel1.Text = "File Created";
            if (filename.Contains("CS6326"))
            {
                if (File.Exists(filename))
                {
                    using (StreamReader sw = new StreamReader(filename))
                    {
                        while ((line = sw.ReadLine()) != null)
                        {
                            char[] delimiters = new char[] { '\t' };
                            string[] words = line.Split(delimiters);
                            starttime.Add(words[13]);
                            endtime.Add(words[14]);
                            no_count++;
                            backspace_count = backspace_count + Convert.ToInt32(words[15]);

                        }
                    }
                }
            


            //get the start time
            DateTime[] st = new DateTime[starttime.Count];
            for (int i = 0; i < st.Length; i++)
            {
                st[i] = Convert.ToDateTime(starttime[i]);
            }

            //get the end time
            DateTime[] et = new DateTime[endtime.Count];
            for (int i = 0; i < et.Length; i++)
            {
                et[i] = Convert.ToDateTime(endtime[i]);
            }

            //timediff
            TimeSpan[] entry = new TimeSpan[st.Length];
            for (int i = 0; i < st.Length; i++)
            {
                entry[i] = (st[i] - et[i]);
                //entry[i] = ts.ToString(@"hh\:mm\:ss");
            }
            //calculating minimum entry time

            TimeSpan min_entry = entry[0];
            for (int i = 0; i < entry.Length; i++)
            {
                if (TimeSpan.Compare(entry[i], min_entry) > 0)
                {
                    min_entry = entry[i];
                }
            }

            //calculating maximum entry time

            TimeSpan max_entry = entry[0];
            for (int i = 0; i < entry.Length; i++)
            {
                if (TimeSpan.Compare(entry[i], max_entry) < 0)
                {
                    max_entry = entry[i];
                }
            }

            //calculating average entry time

            double doubleAverage = entry.Average(timeSpan => timeSpan.Ticks);
            long longAverage = Convert.ToInt64(doubleAverage);
            TimeSpan avg_entry = new TimeSpan(Math.Abs(longAverage));



            TimeSpan[] inter = new TimeSpan[st.Length - 1];
            for (int i = 0; i < st.Length - 1; i++)
            {
                inter[i] = st[i + 1] - et[i];
                //entry[i] = ts.ToString(@"hh\:mm\:ss");
            }
            //calculating minimum entry time

            TimeSpan min_inter = inter[0];
            for (int i = 0; i < inter.Length; i++)
            {
                if (TimeSpan.Compare(inter[i], min_inter) > 0)
                {
                    min_inter = inter[i];
                }
            }

            //calculating maximum entry time

            TimeSpan max_inter = inter[0];
            for (int i = 0; i < inter.Length; i++)
            {
                if (TimeSpan.Compare(inter[i], max_inter) < 0)
                {
                    max_inter = inter[i];
                }
            }


            //calculating average entry time
            double doubleAve = inter.Average(timeSpan => timeSpan.Ticks);
            long longAve = Convert.ToInt64(doubleAve);

            TimeSpan avg_inter = new TimeSpan(longAve);
            //calculate total
            TimeSpan temp1 = new TimeSpan();
            TimeSpan temp2 = new TimeSpan();
            TimeSpan sum = new TimeSpan();
            for (int i = 0; i < inter.Length; i++)
            {
                temp1 = temp1.Add(inter[i]);
            }
            for (int i = 0; i < entry.Length; i++)
            {
                temp1 = temp1.Add(entry[i]);
            }
            sum = temp1.Add(temp2).Negate();
            //create a new file and input all the entries 
            String file = @"Asg3_Analysis.txt";
            using (StreamWriter tw = File.CreateText(file))
            {
                tw.Write("Number of records:" + "\t");
                tw.Write(no_count + "\n");
                tw.Write("Minimum entry time:" + "\t");
                tw.Write(min_entry.ToString(@"hh\:mm\:ss") + "\n");
                tw.Write("Maximum entry time:" + "\t");
                tw.Write(max_entry.ToString(@"hh\:mm\:ss") + "\n");
                tw.Write("Average entry time:" + "\t");
                tw.Write(avg_entry + "\n");
                tw.Write("Minimum inter-record time:" + "\t");
                tw.Write(min_inter + "\n");
                tw.Write("Maximum inter-record time:" + "\t");
                tw.Write(max_inter + "\n");
                tw.Write("Average inter-record time:" + "\t");
                tw.Write(avg_inter + "\n");
                tw.Write("Total time:" + "\t");
                tw.Write(sum + "\n");
                tw.Write("Backspace count:" + "\t");
                tw.Write(backspace_count + "\n");
                tw.Close();
                toolStripStatusLabel1.Text = "File Analyzed and available for view as Asg3_Analysis.txt";
            }
            }
           else
            {
                toolStripStatusLabel1.Text = "Select a valid file for analysis";
            }

        }
    }
}
