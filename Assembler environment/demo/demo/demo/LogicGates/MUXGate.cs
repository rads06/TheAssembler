using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LogicGates
{
    class MUXGate : Gate
    {
        ushort ControlBits;

        public MUXGate(CircuitBoard Home, Vector2 Loc)
            : this(Home, Loc, 1)
        {
        }

        public MUXGate(CircuitBoard Home, Vector2 Loc, ushort Bits)
            : base(Home.GetTexture("MK-00"), Loc)
        {
            ControlBits = (ushort)MathHelper.Clamp(Bits, 1f, 2f);

            if (this.ControlBits == 2)
                this.Texture = Home.GetTexture("MX-01");
        }

        public override Voltage GetOutput(List<ILogicInput> inputs, string namedOutput)
        {
            if (inputs.Count < ControlBits) return Voltage.None;
            foreach (ILogicInput l in inputs)
                if (l.GetOutput().Equals(Voltage.None))
                    return Voltage.None;
            
            //Calculate index to use from control bits
            int cindex = ControlBits;
            for (int i = 0; i < ControlBits; i++)
            {
                cindex += (int)inputs[i].GetOutput() * (int)Math.Pow(2d, (double)i);
            }
            
            //Get the output at the index specified
            if (inputs.Count <= cindex)
                return Voltage.None;
            return inputs[cindex].GetOutput();
        }
    }
}
