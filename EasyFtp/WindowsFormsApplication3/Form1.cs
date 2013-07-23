﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;               //for ftp
using System.IO;

namespace WindowsFormsApplication3
{
    
    public partial class Form1 : Form
    {

        //setting up default values
        public string ip1 = "192.168.125.7";

        public Form1()
        {


            InitializeComponent();

            string[] contents = { ip1, "", "", "",};

            try
            {
                contents = File.ReadAllText("config.txt").Split(',');
            }
            catch
            {
                MessageBox.Show("File read error");
                File.WriteAllText("config.txt",ip1+",,,");
            }
            textBox1.Text = (string.IsNullOrEmpty(contents[0]))?ip1:contents[0];
            textBox2.Text = contents[2];


            //converting password back to plain text
            textBox3.Text = Encoding.Unicode.GetString(Convert.FromBase64String(contents[3])); 
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //report any problem
            MessageBox.Show("Please send any problems, suggestions or bugs to manoj.adams@gmail.com\nThank you!");
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            //quit button
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //About box
            AboutBox1 form2 = new AboutBox1();
            form2.ShowDialog();

        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            //browser window
            string ip = textBox1.Text;
            string user = textBox2.Text;
            string pass = textBox3.Text;
            

            //navigating to the required ftp address
            webBrowser1.Navigate("ftp://"+user+":"+pass+"@"+ip+"/",null,null,"Authentication : Basic"+Convert.ToBase64String(Encoding.ASCII.GetBytes(user+":"+pass))+"\r\n");
            
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //documentation
            MessageBox.Show("This program is just made to make life for accessing ftp servers in our tezu university.  Please send any suggestions to manoj.adams@gmail.com\nDefault settings are stored in file config.txt in current directory");
        }
        
        
        private void proxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //calling form2(settings)
            Form2 form3 = new Form2(this);
            form3.ShowDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void iPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //calling form2(settings)
            Form2 form3 = new Form2(this);
            form3.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text;
            string usr = textBox2.Text;
            string pwd = textBox3.Text;

            //checking for correct ip address format
            Regex match1 = new Regex(@"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$", RegexOptions.IgnoreCase);
            if (!match1.IsMatch(ip))
            {
                MessageBox.Show("Ip format not recognized");
                return;
            }

            //converting password to base 64 string format
            string convert1 = Convert.ToBase64String(Encoding.Unicode.GetBytes(pwd));

            try
            {
                //saving file
                File.WriteAllText("config.txt", ip + ",," + usr + ',' + convert1);
                MessageBox.Show("Saved");
            }
            catch 
            {
                MessageBox.Show("Error in saving file");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     
    }
}
