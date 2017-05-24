using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LogicGates
{
    //Class defines an object which holds a Gate Item
    public class Slot : ILogicInput
    {
        public string Name { get; private set; }
        public Texture2D Texture { get; private set; }
        public bool IsEmpty { get { return (Holding == null); } }
        public Gate Holding { get; private set; }
        Vector2 loc;
        public Vector2 Loc 
        { 
            get { return loc; }
            private set
            {
                loc = value;
                X = (int)value.X;
                Y = (int)value.Y;
            }
        }
        int x, y;
        public int X { get { return x - (int)offset.X; } private set { x = value; } }
        public int Y { get { return y - (int)offset.Y; } private set { y = value; } }
        Color color;
        Vector2 offset;
        Voltage output;

        #region Inputs Logic
        /// <summary>
        /// Stores the inputs for this slot.  Note that inputs are ordered, so if the gate demands them in a certain order
        /// make sure to insert rather than just add.
        /// </summary>
        private List<ILogicInput> allInputs;
        public ILogicInput[] Inputs { get { return allInputs.ToArray(); } }
        public void AddInput(ILogicInput i) { allInputs.Add(i); }
        public void InsertInput(ILogicInput i, int n) { allInputs.Insert(n, i); }
        public void RemoveInput(ILogicInput i) { allInputs.Remove(i); }

        /*public bool IsOutSlot
        {
            get
            {
                if (!this.IsEmpty)
                    if (this.Holding is OUTGate)
                        return true;
                return false;
            }
        }

        public bool IsInSlot
        {
            get
            {
                if (!this.IsEmpty)
                    return true;
                return false;
            }
        }*/

        #endregion

        public Slot(Texture2D Texture, Vector2 Loc, String Name)
        {
            this.Name = Name;
            this.Texture = Texture;
            this.Loc = Loc;
            this.Holding = null;
            this.color = Color.Gray;
            this.allInputs = new List<ILogicInput>();

            //Calculate offset
            //offset = new Vector2(Texture.Width / 2, Texture.Height / 2);
            offset = (new Vector2(88, 85))/2;
            
        }

        public void SlotGate(Gate g)
        {
            this.Holding = g;
            g.Loc = this.Loc;
            this.GetOutput("default");
        }

        public Gate UnSlotGate()
        {
            Gate g = this.Holding;
            this.Holding = null;
            SetColor(Voltage.None);
            return g;
        }

        public void Update(GameTime gt)
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.Texture, this.Loc, null, color, 0f, offset, 1f, SpriteEffects.None, 0f);         
        }
        public void DrawGate(SpriteBatch sb)
        {
            if (this.Holding != null) this.Holding.Draw(sb);
        }

        //This will calculate the output of the slot as well as set the color
        private Voltage GetOutput(string namedOutput)
        {
            Voltage v = this.IsEmpty ? Voltage.None : this.Holding.GetOutput(this.allInputs, namedOutput);
            SetColor(v);
            return v;
        }
        private void SetColor(Voltage v)
        {
            switch (v)
            {
                case Voltage.None:
                    this.color = Color.Gray;
                    break;
                case Voltage.Low:
                    this.color = Color.Red;
                    break;
                case Voltage.High:
                    this.color = Color.Green;
                    break;
            }
        }

        //ILogicInput implementation
        Voltage ILogicInput.GetOutput(string namedOutput)
        {
            return this.GetOutput(namedOutput);
        }

        Voltage ILogicInput.GetOutput()
        {
            return this.GetOutput("default");
        }

        void ILogicInput.UpdateOutputs()
        {
            this.GetOutput("default");
        }
        string ILogicInput.Name
        {
            get
            {
                return this.Name;
            }
        }
    }
}
