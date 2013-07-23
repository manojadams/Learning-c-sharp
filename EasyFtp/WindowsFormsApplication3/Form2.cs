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
using System.IO;
using System.Security.Cryptography;         //class for encryption and decryption methods

namespace WindowsFormsApplication3
{
    public partial class Form2 : Form
    {
        //Form1 variable to be used to refer to Form1 object instance
        Form1 frm1;

        //Default values
        string ip = "192.168.125.7";

        public Form2(Form1 frm)
        {
            InitializeComponent();

            string[] contents = { ip, "", "", "" };
            try
            {
                //Reading file contents in an array
                contents = File.ReadAllText("config.txt").Split(',');
            }
            catch
            {
                MessageBox.Show("File read error");
            }
            //regex to check for correct ip address format in give file
            Regex rgx = new Regex(@"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$",RegexOptions.IgnoreCase);
            
            //comboBox1 to contain default value if ip address format not correct
            comboBox1.Text = (rgx.IsMatch(contents[0]))?contents[0]:ip;   

            textBox3.Text = contents[1];    //ip address of ftp
            textBox1.Text = contents[2];    //username

            //converting password back to plain text
            textBox2.Text = Encoding.Unicode.GetString(Convert.FromBase64String(contents[3])); 

            //saving passed object instance as a property value
            props = frm;    
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            //cancel button
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //save button

            string ip = comboBox1.Text;
            string usr = textBox1.Text;
            string pwd = textBox2.Text;

            //checking for correct ip address format in string ip
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
                //saving data to file
                File.WriteAllText("config.txt", ip + ',' + textBox3.Text + ',' + usr + ',' + convert1);
            }
            catch
            {
                MessageBox.Show("Error in saving file");
                
            }
            //updating values of Form1
            UpdateFrm(ip,usr,pwd);

            this.Close();

        }

        //Property to store Form1 object instance
        public Form1 props
        {
            get { return frm1; }
            set { frm1 = value; }
        }

        //Method to update Form1 
        public void UpdateFrm(string ip, string usr, string pwd)
        {
            Form1 frm = props;
            frm.textBox1.Text = ip;
            frm.textBox2.Text = usr;
            frm.textBox3.Text = pwd;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //not required by default but kept for future requirements
        }
        
    }
}
