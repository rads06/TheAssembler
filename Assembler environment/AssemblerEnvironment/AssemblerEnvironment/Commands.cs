using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblerEnvironment
{
    /// <summary>
    /// This class sets up all of the command that the assembly computer can run.
    /// </summary>
    class Commands
    {

        public Commands()
        {
        }

        public int add(int accumulator, int registerValue)
        {
            return accumulator + registerValue;
        }

        public int sub(int accumulator, int registerValue)
        {
            return accumulator - registerValue;
        }

        public string sta(int accumulator)
        {
            return accumulator.ToString();
        }



    }
}
