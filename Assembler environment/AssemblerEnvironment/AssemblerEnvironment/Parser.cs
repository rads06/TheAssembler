using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblerEnvironment
{
    class Parser
    {
        private string[] lines;
        private bool isDAT;
        private string datInt;
        private string[] commandWords;
        private string[] labels;
        private string[] applyToLabels;
        private int fieldCounter;
        //private ITerminal terminal;

        public Parser()
        {
            lines = new string[100];
            isDAT = false;
            datInt = null;
            commandWords = new string[100];
            labels = new string[100];
            applyToLabels = new string[100];
            fieldCounter = 0;
            
        }

        /// <summary>
        /// Imports the .txt file and seperates each line into its own string.
        /// Stores the strings in the lines[] field.
        /// </summary>
        /// <param name="counter">integer to be used for the lines array</param>
        /*public void seperateLines()
        {
            int counter = 0;
            string line;

            System.IO.StreamReader file =
            new System.IO.StreamReader(@"C:\Users\Thomas\Dropbox\visual studio\AssemblerEnvironment\AssemblerEnvironment\TextFile.txt");
            
            while ((line = file.ReadLine()) !=null)//file.ReadLine()) != null)
            {
                lines[counter] = line;                
                setFields(counter);
                counter++;
                fieldCounter++;
            }

            file.Close();
            int counter = 0;

            IEnumerable<string> str = terminal.Buffer;

            foreach (string value in str)
            {
                lines[counter] = value;
                setFields(counter);
                counter++;
                fieldCounter++;
            }
        }*/

        /// <summary>
        /// Parses the lines from the terminal and places each line into 
        /// the lines array as a string.
        /// </summary>
        public void seperateLines()
        {
            int counter = 0;
            string line;

            System.IO.StreamReader file =
            new System.IO.StreamReader(@"C:\Users\Thomas\Dropbox\visual studio\AssemblerEnvironment1\AssemblerEnvironment\TextFile.txt");

            while ((line = file.ReadLine()) != null)
            {
                lines[counter] = line;
                setFields(counter);
                counter++;
                fieldCounter++;
            }

            file.Close();
        }

        /// <summary>
        /// sets the classes fields so that they can be used properly in other methods.
        /// </summary>
        private void setFields(int counter)
        {
            char[] delimiterChars = {' '};
            string str = lines[counter];

            string[] words = str.Split(delimiterChars);
            int length = words.Length;

            if (length > 3)
            {
                Console.WriteLine("You cannot enter more then 3 instructions per line.");
                //Also need to reset
                return;
            }

            switch (length)
            {
                case 1:
                    labels[counter] = null;
                    commandWords[counter] = words[0];
                    applyToLabels[counter] = null;
                    break;
                case 2:
                    labels[counter] = null;
                    commandWords[counter] = words[0];
                    applyToLabels[counter] = words[1];
                    break;
                case 3:
                    labels[counter] = words[0];
                    commandWords[counter] = words[1];
                    applyToLabels[counter] = words[2];
                    break;
            }
        }

        public string getApplyToLabel(int position)
        {
            return applyToLabels[position];
        }

        public string getCommandWord(int position)
        {
            return commandWords[position];
        }

        public string getLabel(int position)
        {
            return labels[position];
        }

        public int getFieldCounter()
        {
            return fieldCounter;
        }

        public bool isDat()
        {
            return isDAT;
        }

        public string getDatInt()
        {
            return datInt;
        }

        public void setDatInt(string data)
        {
            datInt = data;
        }

        public void setLabel(int position, string str)//TESTING METHOD
        {
            labels[position] = str;
        }

        public void setCommand(int p, string str)//TEST METHOD
        {
            commandWords[p] = str;
        }

        public void setApply(int p, string str)//TEST METHOD
        {
            applyToLabels[p] = str;
        }



    }
}
