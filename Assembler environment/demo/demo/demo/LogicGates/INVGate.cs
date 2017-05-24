using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LogicGates
{
    /// <summary>
    /// This is an Inverter.  It returns input[0].
    /// </summary>
    class INVGate : Gate
    {
        public INVGate(CircuitBoard Home, Vector2 Loc)
            : base(Home.GetTexture("DII"), Loc)
        {
        }

        public override Voltage GetOutput(List<ILogicInput> inputs, string namedOutput)
        {
            base.GetOutput(inputs, namedOutput);

            if (inputs.Count < 1) return Voltage.None;
            Voltage a = inputs[0].GetOutput();
            if (a.Equals(Voltage.None)) return a;
            if (a.Equals(Voltage.Low)) return Voltage.High;
            return Voltage.Low;
        }
    }
}
