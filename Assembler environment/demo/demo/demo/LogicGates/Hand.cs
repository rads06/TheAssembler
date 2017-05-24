using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using demo;

namespace LogicGates
{
    public class Hand
    {
        #region Properties
        Texture2D Texture { get; set; }
        SpriteFont font;
        public CircuitBoard Board { get; private set; }
        public int X { get { return (int)(Loc.X); } }
        public int Y { get { return (int)(Loc.Y); } }
        public Vector2 Loc 
        { 
            get { return new Vector2(this.State.X, this.State.Y); }
        }
        public IGamePiece Holding { get; private set; }
        public MouseState State 
        { 
            get { return Mouse.GetState(); }
        }
        Vector2 offset;
        
        int gateChoice;
        Dictionary<int, Type> gateTypes; 

        #endregion

        public Hand(Texture2D Texture, CircuitBoard Board)
        {
            this.Texture = Texture;
            this.Board = Board;
            this.Holding = null;

            offset = new Vector2(Texture.Width / 2, Texture.Height / 2);
            gateChoice = 0;
            font = Board.GetFont("SpriteFont1");
            gateTypes = new Dictionary<int, Type>();
            gateTypes.Add(0, typeof(INVGate));
            gateTypes.Add(1, typeof(ANDGate));
            gateTypes.Add(2, typeof(ORGate));
            gateTypes.Add(3, typeof(XORGate));
            gateTypes.Add(4, typeof(MUXGate));
        }

        public void Grab(IGamePiece o)
        {
            this.Holding = o;
        }

        public void Drop(ref bool DroppedInSlot)
        {
            if (Holding is Gate)
            {
                try
                {
                    Slot s = Board.Slots.Single(t => this.Holding.Area.Contains(t.X, t.Y));
                    if (s.IsEmpty)
                    {
                        s.SlotGate(this.Holding as Gate);
                        DroppedInSlot = true;
                    }
                }
                catch (InvalidOperationException e)
                {
                }
                finally
                {
                    this.Holding = null;
                }
            }
        }

        public void Update(GameTime gt, ref bool UpdateOutputs)
        {
            if (MouseExt.IsLeftButtonJustPressed())
            {
                Slot s = Board.CheckGateCollisions(this);
                if (s != null)
                {
                    this.Grab(s.UnSlotGate());
                    UpdateOutputs = true;
                }
                else
                {
                    //this.Grab(this.Board.NewGateAt(this.Loc, gateTypes[gateChoice]));
                }
            }

            if (KeyboardExt.IsKeyJustPressed(Keys.I))
                Board.Inven.IsVisible = true;

            if (MouseExt.IsLeftButtonJustReleased())
            {
                bool slotted = false;
                Drop(ref slotted);
                if (slotted) UpdateOutputs = true;
            }

            /*if (MouseExt.IsRightButtonJustPressed())
            {
                gateChoice++;
                if (gateChoice > 4)
                    gateChoice = 0;
            }*/

            //Update any held items now
            try
            {
                this.Holding.Update(this);
            }
            catch { }
        }

        public void Draw(SpriteBatch sb)
        {
            if (this.Holding != null) this.Holding.Draw(sb);
            sb.Draw(this.Texture, this.Loc, null, Color.White, 0f, offset, 1f, SpriteEffects.None, 0f);
            //sb.DrawString(font, gateTypes[gateChoice].ToString(), new Vector2(50, 50), Color.GreenYellow);
            sb.DrawString(font, "Press 'I' to access your inventory...", new Vector2(80, 53), Color.GreenYellow);
        }
    }
}
