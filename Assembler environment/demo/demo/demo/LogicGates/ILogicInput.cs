using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicGates
{
    //Represents the voltage of an input or output
    public enum Voltage
    {
        None=2,
        Low=0,
        High=1
    };

    //Interface represents an object that can act as an input to a logic gate
    public interface ILogicInput
    {
        //Name of this input
        string Name { get; }

        //This is called by a slot on a linked input to get it's usable output
        Voltage GetOutput(string namedOutput);
        Voltage GetOutput();

        //Update outputs
        void UpdateOutputs();
    }
}
