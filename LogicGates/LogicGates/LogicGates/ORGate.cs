using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LogicGates
{
    /// <summary>
    /// Gate is an OR.  Returns input[0] OR input[1].
    /// </summary>
    class ORGate : Gate
    {
        public ORGate(CircuitBoard Home, Vector2 Loc)
            : base(Home.GetTexture("A0393"), Loc)
        {
        }

        public override Voltage GetOutput(List<ILogicInput> inputs, string namedOutput)
        {
            base.GetOutput(inputs, namedOutput);

            if (inputs.Count < 2) return Voltage.None;
            Voltage a = inputs[0].GetOutput();
            Voltage b = inputs[1].GetOutput();
            if (a.Equals(Voltage.None) || b.Equals(Voltage.None)) return Voltage.None;
            if (a.Equals(Voltage.High) || b.Equals(Voltage.High)) return Voltage.High;
            else return Voltage.Low;
        }
    }
}
