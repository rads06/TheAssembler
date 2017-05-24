using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LogicGates
{
    public class HolderSlot : Slot
    {
        Type gateClass;
        CircuitBoard home;

        public HolderSlot(Texture2D Texture, Vector2 Loc, string Name, Type Gate, CircuitBoard Home) : base(Texture, Loc, Name)
        {
            this.gateClass = Gate;
            this.home = Home;
            this.SlotGate(Home.NewGateAt(Vector2.Zero, Gate));
        }

        public override void SlotGate(Gate g)
        {
            if (this.Holding == null)
                base.SlotGate(g);
        }

        public override Gate UnSlotGate()
        {
            return home.NewGateAt(Vector2.Zero, gateClass);
        }
    }
}
