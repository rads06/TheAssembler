using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LMCTerminal
{
    public enum LMCWord
    {
        ADD = 1,
        SUB = 2,
        STA = 3,
        LDA = 5,
        BRA = 6,
        BRZ = 7,
        BRP = 8,
        INP = 901,
        OUT = 902,
        HLT = -1,
        DAT = 0,
    };

    public class Parser
    {
        public class CmdLine
        {
            //public string label;
            public LMCWord commandWord;
            public int applyTo;

            public int ApplyTo
            {
                set { applyTo = value; }
            }

            public CmdLine(LMCWord Cmd, int ApplyTo)
            {
                //label = Label;
                commandWord = Cmd;
                applyTo = ApplyTo;
            }

            public CmdLine(int RegValue)
            {
                switch (RegValue)
                {
                    case 901:
                        commandWord = LMCWord.INP;
                        break;
                    case 902:
                        commandWord = LMCWord.OUT;
                        break;
                    case 000:
                        commandWord = LMCWord.HLT;
                        break;
                    default:
                        int i = RegValue / 100;
                        if (i < 1 || i > 9)
                            throw new ArgumentOutOfRangeException();
                        else
                            commandWord = (LMCWord)i;
                        break;
                }
                applyTo = RegValue % 100;
            }

            public static explicit operator int(CmdLine c)
            {
                int i = (int)c.commandWord;
                switch (i)
                {
                    case 901:
                        break;
                    case 902:
                        break;
                    case -1:
                        i = 0;
                        break;
                    default:
                        i = (i*100) + c.applyTo;
                        break;
                }
                return i;
            }

            public static explicit operator CmdLine(int i)
            {
                return new CmdLine(i);
            }
        }

        List<CmdLine> lines;
        Dictionary<string, int> labels;
        Dictionary<string, int> unresolvedLabels;
        
        public Parser()
        {
            lines = new List<CmdLine>();
            labels = new Dictionary<string, int>();
            unresolvedLabels = new Dictionary<string, int>();
            Reset();
        }

        //Resets the parser for a new program.
        public void Reset()
        {
            lines.Clear();
            labels.Clear();
            unresolvedLabels.Clear();
        }

        /// <summary>
        /// Parses the lines from the terminal and places each line into 
        /// the lines array as a string.
        /// </summary>
        public IEnumerable<CmdLine> seperateLines(IEnumerable<string> str)
        {            
            foreach (string value in str)
                    setFields(value);
            ResolveLabels();
            return lines;
        }

        private void ResolveLabels()
        {
            foreach (string s in unresolvedLabels.Keys)
            {
                int address = unresolvedLabels[s];
                if (labels.ContainsKey(s))
                    lines[address].ApplyTo = labels[s];
                else
                    throw new ArgumentException("Label " + s + " was not found.");
            }
        }

        /// <summary>
        /// sets the classes fields so that they can be used properly in other methods.
        /// </summary>
        private void setFields(string str)
        {
            string[] words = str.Split(new char[]{' '});
            string label = String.Empty;
            LMCWord cmd;
            string applyTo = String.Empty;
            int applyToInt;

            if (words.Length == 1 && words[0] == "HLT")
            {
                lines.Add(new CmdLine(000));
                return;
            }
            if (words.Length > 3 || words.Length < 2)
                throw new ArgumentOutOfRangeException("Illegal number of arguments in " + str);
            try
            { 
                cmd = (LMCWord)Enum.Parse(typeof(LMCWord), words[0]);
                applyTo = words[1];
                if (words.Length > 2)
                    throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentException e)
            { 
                cmd = (LMCWord)Enum.Parse(typeof(LMCWord), words[1]);
                label = words[0];
                if (cmd.Equals(LMCWord.DAT))
                    if (words.Length == 3)
                        applyTo = words[2];
            }

            //This part checks for either a label or a register address
            if (applyTo != String.Empty && !Int32.TryParse(applyTo, out applyToInt))
            {
                labels.Add(applyTo, labels.Count);
                unresolvedLabels.Add(applyTo, labels.Count);
                lines.Add(new CmdLine(cmd, 0));
            }
            else 
            {
                labels.Add(applyTo, labels.Count);
                applyToInt = Int32.Parse(applyTo.Equals(String.Empty) ? "000" : applyTo);
                lines.Add(new CmdLine(cmd, applyToInt));
            }                
        }

        //Old methods
        /*public string getApplyToLabel(int position)
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
        }*/



    }
}
