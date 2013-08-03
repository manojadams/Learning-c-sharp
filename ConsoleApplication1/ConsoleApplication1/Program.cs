using System;
using System.IO;                        //for file operations
using System.Text.RegularExpressions;   //for regex usage

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0,j = 0;
            int len = File.ReadAllLines("here.txt").Length;

            string dir = "0";
            string[] cpy = new String[len-4];
            string[] line = File.ReadAllLines("here.txt");
            foreach (string tmpline in line)
            {
                if (tmpline == "150 Here comes the directory listing.")
                {
                   break;
                }
                else
                j++;
            }
            
            Regex rgx = new Regex(@"[a-z0-9,.,_]*$", RegexOptions.IgnoreCase);
            Regex rgx2 = new Regex(@"^d", RegexOptions.IgnoreCase);

            for (int k = j + 1; k < len - 4; k++)
            {
                Match mch = rgx.Match(line[k]);
                
                if (rgx2.IsMatch(line[k])) 
                    dir = "1";
                else dir = "0";

                cpy[i++] = mch.Value+','+dir;
                
            }
            File.WriteAllLines("here2.txt", cpy);
            
        }
    }
}
