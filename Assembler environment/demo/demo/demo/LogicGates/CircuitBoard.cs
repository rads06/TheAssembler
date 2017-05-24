using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Xml;
using demo;

namespace LogicGates
{
    //This class contains all objects of the circuit board module of the game
    public class CircuitBoard : IPage
    {

        //Private properties
        string name;
        Texture2D Texture;
        ContentManager Content;
        public Inventory Inven;

        public Hand playerHand { get; private set; }
        List<Slot> allSlots;
        BoardOutputs allOutputs;
        BoardInputs allInputs;

        public Slot[] Slots { get { return allSlots.ToArray(); } }

        public CircuitBoard(string TextureName, ContentManager Content, string Name)
        {
            this.name = Name;
            this.Content = Content;
            this.Texture = Content.Load<Texture2D>(TextureName);
            this.Inven = new Inventory(Content, this);

            //Instantiate Hand
            playerHand = new Hand(this.Content.Load<Texture2D>("Crosshair"), this);
            allSlots = new List<Slot>();
            allOutputs = new BoardOutputs();
            allInputs = new BoardInputs();

            //Load XML level file below
            allOutputs.UpdateAllOutputs();
        }

        public void Update(GameTime gt)
        {
            //Updates
            bool updateOutputs = false;
            playerHand.Update(gt, ref updateOutputs);
            if (updateOutputs)
                UpdateOutputs();
        }

        public void UpdateOutputs() { allOutputs.UpdateAllOutputs(); }

        public void Draw(SpriteBatch sb)
        {
            allSlots.ForEach(delegate(Slot s) { s.Draw(sb); });
            sb.Draw(Texture, Vector2.Zero, Color.White);
            allSlots.ForEach(delegate(Slot s) { s.DrawGate(sb); });
            playerHand.Draw(sb);
            if (Inven.IsVisible) Inven.Draw(sb);
        }

        #region Get and Set Methods
        public void SetInput(string Name, Voltage Volt)
        {
            allInputs.SetVoltage(Name, Volt);
            allOutputs.UpdateAllOutputs();
        }
        public Voltage GetOutput(string Name)
        {
            allOutputs.UpdateAllOutputs();
            return allOutputs.GetOutputFrom(Name);
        }
        #endregion

        #region Populating Methods
        public Texture2D GetTexture(string Name)
        {
            return Content.Load<Texture2D>(Name);
        }
        public SpriteFont GetFont(string Name)
        {
            return Content.Load<SpriteFont>(Name);
        }
        public ILogicInput NewInput(string Name, Voltage Volt)
        {
            allInputs.AddInput(Name, Volt);
            return allInputs.GetLogicInput(Name);
        }
        public string NewOutput(string Alias, ILogicInput Output)
        {
            allOutputs.AddOutputAlias(Alias, Output);
            return Alias;
        }
        public Gate NewGateAt(Vector2 Loc, Type GateType)
        {
            Object[] p = { this, Loc };
            return (Gate)Activator.CreateInstance(GateType, p);
        }
        /*public Slot NewSlotAt(Vector2 Loc)
        {
            return NewSlotAt(Loc, "Default");
        }*/
        public Slot NewSlotAt(Vector2 Loc, String Name)
        {
            Slot s = new Slot(Content.Load<Texture2D>("SlotBG"), Loc, Name);
            allSlots.Add(s);
            return s;
        }
        /*private void LoadFromXML(string filename)
        {
            using (XmlReader reader = XmlReader.Create(filename))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Slot":
                                AddXMLSlot(reader["Name"], reader["Type"], reader["X"], reader["Y"], reader["Inputs"]);                                
                                break;
                        }
                    }
                }
            }
        }*/
        /*private void AddXMLSlot(string name, string type, string x, string y, string inputs)
        {
            Vector2 loc = StringsToVector(x, y);
            Slot s = NewSlotAt(loc);

            switch (type)
            {
                case "INHI":
                    s.SlotGate(new INPGate(this, loc, Voltage.High));
                    break;

                case "INLO":
                    s.SlotGate(new INPGate(this, loc, Voltage.Low));
                    break;

                case "AND":
                    s.SlotGate(new ANDGate(this, loc));
                    break;

                case "OUT":
                    s.SlotGate(new OUTGate(this, loc, "high"));
                    break;
            }

            if (!inputs.Equals(String.Empty))
            {
                string[] i = inputs.Split(',');
                foreach (string j in i)
                    s.AddInput(allSlots.Find(t => t.Name.Equals(j)));
            }
        }*/
        private Vector2 StringsToVector(string x, string y)
        {
            return new Vector2(Convert.ToInt32(x), Convert.ToInt32(y));
        }
        #endregion

        #region Collision Methods
        public Slot CheckGateCollisions(Hand h)
        {
            Slot t = null;
            if (Inven.IsVisible == true)
                t = Inven.CheckGateCollisions(h);
            else
            {
                allSlots.ForEach(delegate(Slot s)
                {
                    if (!s.IsEmpty)
                    {
                        if (s.Holding.Area.Contains(h.X, h.Y))
                            t = s;
                    }
                });
            }
            return t;
        }
        #endregion

        #region Explicit Implementation
        string IPage.Name
        {
            get { return name; }
        }

        void IPage.Update(GameTime gt)
        {
            this.Update(gt);
        }

        void IPage.Draw(SpriteBatch sb)
        {
            this.Draw(sb);
        }
        #endregion
    }
}
