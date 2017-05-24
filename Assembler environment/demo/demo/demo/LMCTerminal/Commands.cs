using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMCTerminal
{
    /// <summary>
    /// This class sets up all of the command that the assembly computer can run.
    /// </summary>
    class Commands
    {
        demo.ALU myALU;

        public Commands(demo.ALU myALU)
        {
            this.myALU = myALU;
        }

        public int add(int accumulator, int registerValue)
        {
            //return accumulator + registerValue;
            return myALU.BinaryAdd(accumulator, registerValue);
        }

        public int sub(int accumulator, int registerValue)
        {
            //return accumulator - registerValue;
            return myALU.BinarySub(accumulator, registerValue);
        }

        public string sta(int accumulator)
        {
            return accumulator.ToString();
        }



    }
}
