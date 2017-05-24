using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LogicGates
{
    /// <summary>
    /// This is an XOR gate.  It returns input[0] XOR input[1].
    /// </summary>
    class XORGate : Gate
    {
        public XORGate(CircuitBoard Home, Vector2 Loc)
            : base(Home.GetTexture("C3-302"), Loc)
        {
        }

        public override Voltage GetOutput(List<ILogicInput> inputs, string namedOutput)
        {
            base.GetOutput(inputs, namedOutput);

            if (inputs.Count < 2) return Voltage.None;
            Voltage a = inputs[0].GetOutput();
            Voltage b = inputs[1].GetOutput();
            if (a.Equals(Voltage.None) || b.Equals(Voltage.None)) return Voltage.None;
            if (a.Equals(Voltage.Low) && b.Equals(Voltage.High)) return Voltage.High;
            if (a.Equals(Voltage.High) && b.Equals(Voltage.Low)) return Voltage.High;
            else return Voltage.Low;
        }
    }
}
