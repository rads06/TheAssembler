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

        public Commands()
        {
        }

        public int add(int accumulator, int registerValue)
        {
            return accumulator + registerValue;
        }
        public void add(ref int accumulator, int registerValue)
        {
            accumulator = this.add(accumulator, registerValue);
        }
        public int sub(int accumulator, int registerValue)
        {
            return accumulator - registerValue;
        }
        public void sub(ref int accumulator, int registerValue)
        {
            accumulator = this.sub(accumulator, registerValue);
        }
        public void sta(Register[] regs, int address, int accumulator)
        {
            regs[address].Value = accumulator;
        }
        public void lda(Register[] regs, int address, ref int accumulator)
        {
            accumulator = regs[address].Value;
        }
        public void bra(int addressTo, ref int programCounter)
        {
            programCounter = addressTo;
        }
        public void brz(int accumulator, int addressTo, ref int programCounter)
        {
            programCounter = (accumulator == 0) ? addressTo : programCounter;
        }
        public void brp(int accumulator, int addressTo, ref int programCounter)
        {
            programCounter = (accumulator >= 0) ? addressTo : programCounter;
        }
    }
}
