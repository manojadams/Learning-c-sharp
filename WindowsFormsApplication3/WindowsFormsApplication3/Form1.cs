using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;   //for ftp
using System.IO;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made to make life easy with ftp !!");

        }

        private void button1_Click(object sender, EventArgs e)
        {   
            //setting up proxy
            WebProxy prxy1 = new WebProxy();
            prxy1.Address = new Uri("http://202.141.129.30:3128");
            prxy1.Credentials = new NetworkCredential("manoj_csm12", "hereiammanoj");
            
            //making ftp request for directory listing 
            FtpWebRequest request1 = (FtpWebRequest)WebRequest.Create("ftp://192.168.125.7");
            request1.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request1.Proxy = prxy1;
            request1.Credentials = new NetworkCredential("manoj_csm12", "hereiammanoj");

            //getting response
            FtpWebResponse response1 = (FtpWebResponse)request1.GetResponse();
           
            Stream rStream = response1.GetResponseStream();

            StreamReader reader1 = new StreamReader(rStream);
            
            //outputting result
            MessageBox.Show(response1.StatusDescription);
            MessageBox.Show(response1.ResponseUri.ToString());

            richTextBox1.Text = reader1.ReadToEnd();

            //using regex
            Regex rgx = new Regex(@"(<A)\b(.[^<]*)(</A>)", RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(richTextBox1.Text);
            if (matches.Count > 0)
                foreach (Match match in matches)
                {
                    Regex rgx2 = new Regex(@">(.*)<", RegexOptions.IgnoreCase);
                    Match mh2 = rgx2.Match(match.Value);
                    listBox1.Items.Add(mh2.Value);
                }
            reader1.Close();
            response1.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
