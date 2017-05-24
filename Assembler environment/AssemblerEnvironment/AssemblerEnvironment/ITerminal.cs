using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblerEnvironment
{
    //Interface to use the terminal
    interface ITerminal
    {
        //An ienumerable containing a string for each line in the buffer
        IEnumerable<string> Buffer { get; }

        //Clears the buffer of all text.
        void Clear();

        //Appends a string to the end of the buffer.
        void Write(string text);

        //Appends a string to the end of the buffer, then terminates the line.
        void WriteLine(string text);
    }
}
