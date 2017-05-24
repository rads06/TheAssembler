using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LogicGates
{
    public class Inventory
    {
        public bool IsVisible { get; set; }
        List<HolderSlot> allSlots;
        Texture2D myTexture;

        Vector2 ANDLoc = new Vector2(200+100, 150+100);
        Vector2 ORLoc = new Vector2(200+300+25, 150+100);
        Vector2 INVLoc = new Vector2(200+200, 150+100);
        Vector2 XORLoc = new Vector2(200+133, 150+225);
        Vector2 MUXLoc = new Vector2((200+133*2+25), 150+225);

        public Inventory(Microsoft.Xna.Framework.Content.ContentManager Content, CircuitBoard Home)
        {
            allSlots = new List<HolderSlot>();
            Texture2D texture = Content.Load<Texture2D>("SlotBG");
            this.IsVisible = false;
            myTexture = Content.Load<Texture2D>("Toolbox");

            allSlots.Add(new HolderSlot(texture, ANDLoc, "AND", typeof(ANDGate), Home));
            allSlots.Add(new HolderSlot(texture, INVLoc, "INV", typeof(INVGate), Home));
            allSlots.Add(new HolderSlot(texture, ORLoc, "OR", typeof(ORGate), Home));
            allSlots.Add(new HolderSlot(texture, XORLoc, "XOR", typeof(XORGate), Home));
            allSlots.Add(new HolderSlot(texture, MUXLoc, "MUX", typeof(MUXGate), Home));
        }

        public Slot CheckGateCollisions(Hand h)
        {
            Slot t = null;
            allSlots.ForEach(delegate(HolderSlot s)
            {
                if (!s.IsEmpty)
                {
                    if (s.Holding.Area.Contains(h.X, h.Y))
                        t = s;
                }
            });
            if (t != null) this.IsVisible = false;
            return t;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(myTexture, new Rectangle(200, 150, 400, 300), Color.White);
            foreach (HolderSlot h in allSlots)
            {
                //h.Draw(sb);
                h.DrawGate(sb);
            }
        }
    }
}
