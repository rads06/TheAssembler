using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LMCTerminal
{
    public enum ShellCommand
    {
        List,
        Clear,
        Edit,
        Promote,
        Demote,
        Delete,
        Registers,
        Help,
        None,
    };

    public struct CommandString
    {
        public ShellCommand command;
        public Queue<string> parameters;

        public CommandString(ShellCommand Cmd, IEnumerable<string> Prms)
        {
            command = Cmd;
            parameters = new Queue<string>();
            foreach (string s in Prms)
                parameters.Enqueue(s);
        }
    }

    public class Shell
    {
        //Nested private class
        class Program
        {
            string name;
            IEnumerable<string> buffer;

            public string Name { get { return name; } }
            public IEnumerable<string> Buffer 
            { 
                get { return buffer; } 
                set { buffer = value; } 
            }

            public Program(string Name, IEnumerable<string> Buffer)
            {
                name = Name;
                buffer = Buffer;
            }
        }

        ITerminal term;
        List<Program> allPrograms;
        bool editMode;
        int timer;
        const int timerReset = 100;

        Program EditProgram { get; set; }
        public bool IsInEditMode
        {
            get
            {
                return editMode;
            }
        }

        public Shell(ITerminal Term)
        {
            timer = timerReset;
            term = Term;
            allPrograms = new List<Program>();
            editMode = false;
        }

        public void SaveProgram(string Name, IEnumerable<string> Buffer)
        {
            Program p = new Program(Name, Buffer);
            allPrograms.Add(p);
        }

        public void ParseCommand(string cmdStr)
        {
            string[] fields = cmdStr.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
            CommandString cs = new CommandString(ShellCommand.None, new List<string>());
            term.Clear();
            if (fields.Length == 0)
            {
                term.ReportWrite("No command specified.  Please enter a valid command!");
                return;
            }
            else
                try
                {
                    switch (fields[0].ToUpperInvariant())
                    {
                        case "LIST":
                            cs = new CommandString(ShellCommand.List, fields);
                            break;
                        case "CLEAR":
                            cs = new CommandString(ShellCommand.Clear, fields);
                            break;
                        case "EDIT":
                            if (fields.Length < 2) throw new ArgumentNullException();
                            else cs = new CommandString(ShellCommand.Edit, fields.Skip(1));
                            break;
                        case "PROMOTE":
                            if (fields.Length < 2) throw new ArgumentNullException();
                            cs = new CommandString(ShellCommand.Promote, fields.Skip(1));
                            break;
                        case "DEMOTE":
                            if (fields.Length < 2) throw new ArgumentNullException();
                            cs = new CommandString(ShellCommand.Demote, fields.Skip(1));
                            break;
                        case "DELETE":
                            if (fields.Length < 2) throw new ArgumentNullException();
                            cs = new CommandString(ShellCommand.Delete, fields.Skip(1));
                            break;
                        case "REGISTERS":
                            cs = new CommandString(ShellCommand.Registers, fields);
                            break;
                        case "HELP":
                            cs = new CommandString(ShellCommand.Help, fields);
                            break;
                        default:
                            term.ReportLine("Error: " + fields[0] + " is not a valid command.");
                            return;
                    }
                }
                catch (ArgumentNullException e)
                {
                    term.ReportLine("Error: Invalid parameters or command syntax.");
                    return;
                }
               
                ExecuteCommand(cs);
        }



        public void ExecuteCommand(CommandString cmdStr)
        {
            Program prog;
            string param = cmdStr.parameters.Dequeue();
            int index;
            switch (cmdStr.command)
            {
                case ShellCommand.List:
                    allPrograms.ForEach(delegate(Program p) { term.ReportLine(p.Name); });
                    term.ReportLine("...found " + allPrograms.Count + " programs.");
                    break;

                case ShellCommand.Clear:
                    term.Clear();
                    break;

                case ShellCommand.Promote:
                    prog = allPrograms.Find(m => m.Name.Equals(param));
                    if (prog == null)
                    {
                        term.ReportLine("Error: Program " + param + " not found.");
                        break;
                    }
                    index = allPrograms.IndexOf(prog);
                    if (index > 0)
                    {
                        allPrograms.RemoveAt(index);
                        allPrograms.Insert(index - 1, prog);
                    }
                    term.ReportLine("Program " + param + " has been promoted in the run order.");
                    break;

                case ShellCommand.Demote:
                    prog = allPrograms.Find(m => m.Name.Equals(param));
                    if (prog == null)
                    {
                        term.ReportLine("Error: Program " + param + " not found.");
                        break;
                    }
                    int index1 = allPrograms.IndexOf(prog);
                    if (index1 < allPrograms.Count-1)
                    {
                        allPrograms.RemoveAt(index1);
                        allPrograms.Insert(index1+1, prog);
                    }
                    term.ReportLine("Program " + param + " has been demoted in the run order.");
                    break;

                case ShellCommand.Edit:
                    prog = allPrograms.Find(m => m.Name.Equals(param));
                    if (prog == null)
                    {
                        prog = new Program(param, new List<string>());
                        allPrograms.Add(prog);
                    }
                    this.EditProgram = prog;
                    this.editMode = true;
                    term.ReportLine("Editting Program: " + param + " (Press F1 to Save and Quit)");
                    term.ReportLine("-----------------------------------------------------------");
                    foreach (string s in EditProgram.Buffer)
                    {
                        term.WriteLine(s);
                    }
                    break;

                case ShellCommand.Registers:
                    string[] regs = term.Registers.ToArray();
                    for (int i=0; i<regs.Length; i+=5)
                    {
                        string s = String.Format("|{0,10}{1,10}{2,10}{3,10}{4,10}|",
                            (i) + ":" + regs[i].ToString(), 
                            (i+1) + ":" + regs[i + 1].ToString(), 
                            (i+2) + ":" + regs[i + 2].ToString(), 
                            (i+3) + ":" + regs[i + 3].ToString(), 
                            (i+4) + ":" + regs[i + 4].ToString());
                        term.ReportLine(s);
                    }                       
                    break;

                case ShellCommand.Help:
                    ShellCommand sc = ShellCommand.None;
                    try { param = cmdStr.parameters.Dequeue(); }
                    catch { param = String.Empty; }
                    try { sc = (ShellCommand) Enum.Parse(typeof(ShellCommand), param, true); }
                    catch { }
                    //term.ReportLine(param);
                    term.ReportLine(GetHelpText(sc));
                    break;

                case ShellCommand.Delete:
                    prog = allPrograms.Find(pr => pr.Name.Equals(param));
                    if (prog == null)
                        term.ReportLine("Error: Program " + param + " does not exist.");
                    else
                    {
                        allPrograms.Remove(prog);
                        term.ReportLine("Program " + prog.Name + " successfully deleted.");
                    }
                    break;
            }
        }

        public void SaveProgram()
        {
            if (this.IsInEditMode)
            {
                this.EditProgram.Buffer = term.Buffer;
                this.editMode = false;
                term.Clear();
                term.ReportLine("Program " + this.EditProgram.Name + " saved.");
                this.EditProgram = null;
            }
            return;
        }

        public string GetHelpText(ShellCommand cmd)
        {
            string helpText = String.Empty;
            switch (cmd)
            {
                case ShellCommand.None:
                    helpText =
@"This system recognizes the following commands:
LIST, CLEAR, EDIT, PROMOTE, DEMOTE, DELETE, REGISTERS
Type HELP [COMMAND] for more information on a system command.";
                    break;
                case ShellCommand.Clear:
                    helpText =
@"Clear Command
Syntax: CLEAR
This command will flush the terminal display buffers.";
                    break;
                case ShellCommand.Delete:
                    helpText =
@"Delete Command
Syntax: DELETE [PROGRAM]
This command will remove a program from system memory.";
                    break;
                case ShellCommand.Demote:
                    helpText =
@"Demote Command
Syntax: DEMOTE [PROGRAM]
This command will lower a program's run priority.";
                    break;
                case ShellCommand.Edit:
                    helpText =
@"Edit Command
Syntax: EDIT [PROGRAM]
This command will edit the program provided as a parameter.
If the program does not exist it will be created.";
                    break;
                case ShellCommand.Help:
                    helpText =
@"Help Command
Syntax: HELP *[PROGRAM]
This command will display general help information on this system.
The optional program parameter will provide more specific information.";
                    break;
                case ShellCommand.List:
                    helpText =
@"List Command
Syntax: LIST
This command lists all programs in resident memory in 
descending order of run priority.";
                    break;
                case ShellCommand.Promote:
                    helpText =
@"Promote Command
Syntax: PROMOTE [PROGRAM]
This command will raise the run priority of the program
provided as a parameter.";
                    break;
                case ShellCommand.Registers:
                    helpText =
@"Registers Command
Syntax: REGISTERS
This command will display the current status of all system
memory registers.";
                    break;
            }
            return helpText;
        }

        //Use update to run all programs in resident memory
        public void Update()
        {
            if (timer == 0)
            {
                foreach (Program p in allPrograms)
                {
                    term.RunProgram(p.Buffer);
                }
                timer = timerReset;
            }
            timer--;
        }
    }
}
