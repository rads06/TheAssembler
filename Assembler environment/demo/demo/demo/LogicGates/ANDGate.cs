using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LogicGates
{
    /// <summary>
    /// This gate behaves like a normal AND gate.  It outputs the result of the operation Input[0] AND Input[1].
    /// </summary>
    class ANDGate : Gate
    {
        public ANDGate(CircuitBoard Home, Vector2 Loc)
            : base(Home.GetTexture("A0-B3"), Loc)
        {
        }

        public override Voltage GetOutput(List<ILogicInput> inputs, string namedOutput)
        {
            base.GetOutput(inputs, namedOutput);

            if (inputs.Count < 2) return Voltage.None;
            Voltage a = inputs[0].GetOutput();
            Voltage b = inputs[1].GetOutput();
            if (a.Equals(Voltage.None) || b.Equals(Voltage.None)) return Voltage.None;
            if (a.Equals(Voltage.High) && b.Equals(Voltage.High)) return Voltage.High;
            else return Voltage.Low;
        }
    }
}
