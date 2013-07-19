using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        

        class Form1 : Form
        {
            public Form1() : base()     //constructor
            {
                //Form
                this.Text = "ShootDown Installer";
                this.Size = new Size(600, 600);
                
                //Label
                Label l1 = new Label();
                l1.Size = new Size(50, 50);
                l1.Text = "Hello world";
                l1.Location = new Point(250, 250);

                //Button
                Button b1 = new Button();
                b1.Text = "Next";
                b1.Location = new Point(400, 500);

                //Button2
                Button b2 = new Button();
                b2.Text = "Exit";
                b2.Location = new Point(500, 500);
                b2.Click += new EventHandler(termination);
           

                //TextBox
                TextBox t1 = new TextBox();
                t1.Size = new Size(200, 200);

                this.Controls.Add(l1);
                this.Controls.Add(b1);
                this.Controls.Add(t1);
                this.Controls.Add(b2);
            }
            public void termination(object sender, EventArgs e)
            {
                Application.Exit();
            }
        }
    }
}
