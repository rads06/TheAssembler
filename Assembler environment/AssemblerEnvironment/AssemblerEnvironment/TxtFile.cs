using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AssemblerEnvironment
{
    class TxtFile
    {

        public TxtFile()
        {
        }

        /// <summary>
        /// Creates a text file from the string array parameter.
        /// </summary>
        /// <param name="str"></param>
        public void createFile(string[] str)
        {
            //First we delete the file.
            string sourceDir = @"c:\Users\Thomas\Dropbox\visual studio\AssemblerEnvironment\AssemblerEnvironment";
            
            string[] txtList = Directory.GetFiles(sourceDir, "*.txt");

            foreach (string f in txtList)
            {
                File.Delete(f);
            }

            //Then we create the new file.
            string[] lines = str;
            System.IO.File.WriteAllLines(@"C:\Users\Thomas\Dropbox\visual studio\AssemblerEnvironment\AssemblerEnvironment\TextFile.txt", lines);//(@"C:\Users\Thomas\Desktop\Txt\file.txt", lines);
        }

        /// <summary>
        /// Takes user inputed strings and creates a string array.
        /// Stops taking user input once the user types "exit"
        /// </summary>
        /// <returns>a string array of the user input</returns>
        public string[] readLine()
        {
            bool check = true;
            int counter = 0;
            string[] str = new string[100];

            while (check)
            {
                string s = Console.ReadLine();

                if (s.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
                {
                    check = false;
                }
                else
                {
                    str[counter] = s;
                    counter++;
                }
            }

            return str;
        }

        public void run()
        {
            createFile(readLine());
        }
    }
}
