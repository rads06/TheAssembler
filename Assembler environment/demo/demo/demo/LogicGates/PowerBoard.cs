using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace LogicGates
{
    public class PowerBoard : CircuitBoard
    {
        public PowerBoard(ContentManager Content) : base("Power", Content, "Power")
        {
            Vector2 INVLoc = new Vector2(645,214);

            //Create the inverter slot
            Slot s = this.NewSlotAt(INVLoc, "INV");
            s.AddInput(this.NewInput("POWER", Voltage.High));
            //s.SlotGate(new INVGate(this, INVLoc));
            this.NewOutput("POUT", s);
        }
    }
}
