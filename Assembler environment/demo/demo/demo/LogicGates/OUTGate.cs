using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LogicGates
{
    /// <summary>
    /// The OUT gate only reads the input at 0 and will throughput it.
    /// </summary>
    class OUTGate : Gate
    {
        public OUTGate(CircuitBoard Home, Vector2 Loc, string Victory) : base(Home.GetTexture("OUTGate"), Loc) 
        {

        }

        public override Voltage GetOutput(List<ILogicInput> inputs, string namedOutput)
        {
            base.GetOutput(inputs, namedOutput);

            if (inputs.Count == 0) return Voltage.None;
            else return inputs[0].GetOutput();
        }
    }
}
