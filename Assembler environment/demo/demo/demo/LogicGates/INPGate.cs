using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LogicGates
{
    /// <summary>
    /// This class covers gates used as starting points for circuits.  The output voltage must be set when instantiating the object.
    /// </summary>
    class INPGate : Gate
    {
        Voltage volt;

        public INPGate(CircuitBoard Home, Vector2 Loc, Voltage Volt) : base(Home.GetTexture("INPGate"), Loc)
        {
            volt = Volt;
        }

        public override Voltage GetOutput(List<ILogicInput> inputs, string namedOutput)
        {
            base.GetOutput(inputs, namedOutput);

            return volt;
        }
    }
}
