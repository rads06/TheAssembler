using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblerEnvironment
{

    class Program//final code
    {
        private int outBox, accumulator; //programCounter;//, inBox memAddress memData
        private Register[] registers;
        private Parser parser;
        private Commands commands;
        //private TxtFile txtFile;
        //private ITerminal terminal;

        public Program()
        {
            outBox = 0;
            accumulator = 0;
            parser = new Parser();
            commands = new Commands();
            //txtFile = new TxtFile();
            

            registers = new Register[100];
            for (int i = 0; i < 100; i++)
            {
                registers[i] = new Register(i);
            }
        }

        /// <summary>
        /// returns the results for running the little man program
        /// </summary>
        /* public void runResults()
         {
             Terminal.WriteLine("The register values are:\n");

             for (int i = 0; i < parser.getFieldCounter(); i++)
             {
                 terminal.WriteLine(getRegister(i));
             }

             terminal.WriteLine("\nOut Box Value = " + getOutBox());
         }*/

        /// <summary>
        /// takes a label and gets the register address of the register.
        /// </summary>
        /// <param name="label">label to be found</param>
        /// <returns> int of register address of register with given label</returns>
        public int getRegisterPosition(string label)
        {
            for (int i = 0; i < registers.Length; i++)
            {
                if (registers[i].hasLabel())
                {
                    if (registers[i].getLabelName().Equals(label))
                    {
                        return registers[i].getPosition();
                    }
                }
            }
            return 0;//programCounter; //THIS IS A STUB TO AVOID AN ERROR MESSAGE AND THIS NEEDS TO BE FIXED.
        }

        /// <summary>
        /// Takes the user inputed text file and places commands into registers
        /// </summary>
        public void compile()
        {
            int position = 0;
            parser.seperateLines();
            setLabels();

            while (position < parser.getFieldCounter())
            {
                string commandWord = parser.getCommandWord(position);
                if (commandWord.Equals("dat", StringComparison.InvariantCultureIgnoreCase))//THIS CONDITION MAY NOT BE NEEDED!
                {
                    parser.setDatInt(commandWord);
                }
                setRegisterData(position);
                position++;

            }
        }

        /// <summary>
        /// IDK RIGHT NOW
        /// </summary>
        /// <param name="position"></param>
        private void setRegisterData(int position)
        {
            string label = parser.getApplyToLabel(position);
            string commandWord = parser.getCommandWord(position);
            registers[position].setData(convert(commandWord, label, position));            
        }

        /// <summary>
        /// sets all of the registers' labels if they have one.
        /// </summary>
        private void setLabels()
        {
            string label = null;

            for (int i = 0; i < parser.getFieldCounter(); i++)
            {
                label = parser.getLabel(i);

                if (label != null)
                {
                    registers[i].setLabel(label);
                }
            }
        }

        public void run()
        {
            bool stop = false;
            int programCounter = 0;
            //txtFile.run();
            compile();
            
            while (!stop)
            {

                string word = registers[programCounter].getData();
                char[] letters = word.ToCharArray();
                int num = Convert.ToInt32(letters[0].ToString());
                int regAddress = ((Convert.ToInt32(letters[1].ToString()) * 10  ) + (Convert.ToInt32(letters[2].ToString())));
                int regValue = Convert.ToInt32(registers[regAddress].getData());

                switch (num)
                {
                    case 0://hlt
                        stop = true;
                        break;
                    case 1://add
                        accumulator = commands.add(accumulator, regValue);
                        programCounter++;
                        break;
                    case 2://sub
                        accumulator = commands.sub(accumulator, regValue);
                        programCounter++;
                        break;
                    case 3://sta
                        registers[regAddress].setData(commands.sta(accumulator));
                        programCounter++;
                        break;
                    case 5://lda
                        accumulator = regValue;
                        programCounter++;
                        break;
                    case 6://bra
                        programCounter = regAddress;//getRegisterPosition(parser.getApplyToLabel(programCounter));
                        break;
                    case 7://brz
                        if (accumulator == 0)
                        {
                            programCounter = regAddress; // getRegisterPosition(parser.getApplyToLabel(programCounter));
                        }
                        else
                        {
                            programCounter++;
                        }
                        break;
                    case 8://brp
                        if (accumulator >= 0)
                        {
                            programCounter = regAddress;
                        }
                        else
                        {
                            programCounter++;
                        }
                        break;
                    case 9:
                        if (regAddress == 01)//inp
                        {
                            accumulator = getUserInput();
                        }
                        if (regAddress == 2)//out
                        {
                            outBox = accumulator;
                        }
                        programCounter++;
                        break;
                }
            }
        }

        private int getUserInput()
        {
            string str = Console.ReadLine();
            return Convert.ToInt32(str);
            //terminal.WriteLine("Enter an integer.");
            //IEnumerable<string> str = terminal.Buffer;
            //return Convert.ToInt32(str);
        }

        /*private void getTxtFile()// for test purposes only
        {
            txtFile.readLine();
        }*/

        public int getAccumulator()// for test purposes only
        {
            return accumulator;
        }

        public int getOutBox()// for test purposes only
        {
            return outBox;
        }

        /// <summary>
        /// Converts the command word and apply to label into the correct code to be stored in a register
        /// </summary>
        /// <param name="commandWord"></param>
        /// <param name="applyToLabel"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public string convert(string commandWord, string applyToLabel, int position)
        {
            bool check = true;
            string command = "000";

            int registerValue = getRegisterPosition(applyToLabel); 

            if (commandWord.Equals("add", StringComparison.InvariantCultureIgnoreCase))
            {
                command = (100 + registerValue).ToString();
                check = false;
            }

            if (commandWord.Equals("sub", StringComparison.InvariantCultureIgnoreCase))
            {
                command = (200 + registerValue).ToString();
                check = false;
            }

            if (commandWord.Equals("sta", StringComparison.InvariantCultureIgnoreCase))
            {
                command = (300 + registerValue).ToString();
                check = false;
            }

            if (commandWord.Equals("lda", StringComparison.InvariantCultureIgnoreCase))
            {
                command = (500 + registerValue).ToString();
                check = false;
            }

            if (commandWord.Equals("bra", StringComparison.InvariantCultureIgnoreCase))
            {
                command = (600 + registerValue).ToString();
                check = false;
            }

            if (commandWord.Equals("brz", StringComparison.InvariantCultureIgnoreCase))
            {
                command = (700 + registerValue).ToString();
                check = false;
            }

            if (commandWord.Equals("brp", StringComparison.InvariantCultureIgnoreCase))
            {
                command = (800 + registerValue).ToString();
                check = false;
            }

            if (commandWord.Equals("inp", StringComparison.InvariantCultureIgnoreCase))
            {
                command = "901";
                check = false;
            }

            if (commandWord.Equals("out", StringComparison.InvariantCultureIgnoreCase))
            {
                command = "902";
                check = false;
            }

            if (commandWord.Equals("hlt", StringComparison.InvariantCultureIgnoreCase))
            {
                command = "000";
                check = false;
            }

            if (commandWord.Equals("dat", StringComparison.InvariantCultureIgnoreCase))
            {
                command = parser.getApplyToLabel(position); //STUB NEED TO RETURN SOME SORT OF DAT INTEGER VALUE ToInt32(string);
                check = false;
            }

            if (commandWord.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
            {
                check = false;
            }

            if (check)
            {
                //Console.WriteLine("Not a proper command word");
                //run();
                //THROW AN ERROR IF THIS HAPPENS STUB!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //Something like, you did not enter a valid command. reset the txtField
            }

            return command;
        }


        public string getRegister(int i)
        {
            return registers[i].getData();
        }


        static void Main(string[] args)
        {
            Program program = new Program();
            TxtFile text = new TxtFile();

            //text.run();
            program.run();
            //program.compile();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(program.getRegister(i));
                
            }

            //Console.WriteLine("\n\n" + program.getAccumulator());
            //Console.WriteLine("\n\n" + program.getOutBox().ToString());
          
            //Console.WriteLine(program.getOutBox().ToString());
            Console.ReadLine();

        }        
    }
}
