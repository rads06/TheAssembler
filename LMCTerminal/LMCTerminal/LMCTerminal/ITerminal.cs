using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMCTerminal
{
    //Interface to use the terminal
    public interface ITerminal
    {
        //An ienumerable containing a string for each line in the buffer
        IEnumerable<string> Buffer { get; }

        //Clears the buffer of all text.
        void Clear();

        //Appends a string to the end of the buffer.
        void Write(string text);

        //Appends a string to the end of the buffer, then terminates the line.
        void WriteLine(string text);

        //The below commands are identical to Write commands but add to the report buffer instead.
        void ReportWrite(string text);
        void ReportLine(string text);

        //Gets the registers of the LMC attached to this terminal
        IEnumerable<string> Registers { get; }

        //Sends a buffer to the LMC for running
        void RunProgram(IEnumerable<string> buffer);
    }
}
