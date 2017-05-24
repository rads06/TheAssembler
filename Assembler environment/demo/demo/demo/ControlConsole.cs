using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LogicGates;
using LMCTerminal;

namespace demo
{
    public class ControlConsole : IPage
    {
        class CriticalStat
        {
            Vector2 loc;
            SpriteFont font;
            string name;
            float startValue;
            float currentValue;
            float decayRate;
            public int Value { get { return (int)currentValue; } }
            public float Percent { get { return currentValue / startValue; } }
            public float Remaining { get { return (1 - Percent); } }
            public float DecayRate { set { decayRate = value; } }

            public CriticalStat(string Name, float StartingValue, float DecayRate, Vector2 Loc, SpriteFont Font)
            {
                name = Name;
                startValue = StartingValue;
                currentValue = StartingValue;
                decayRate = DecayRate;
                loc = Loc;
                font = Font;
            }
            public void Add(float val) { currentValue = val; }
            public void Update(GameTime gt) { currentValue -= decayRate; }
            public void Draw(SpriteBatch sb)
            {
                string s = currentValue.ToString("###.#");///*name + ": " +*/ currentValue.ToString();
                sb.DrawString(font, s, loc, Color.Red);
            }
        }

        string name;
        public Texture2D Texture { get; private set; }
        Texture2D overlay;
        IPage activePage;
        Dictionary<Rectangle, IPage> allPages;
        Rectangle powerRect = new Rectangle(521, 132, (675-521), (236-132));
        Rectangle ventRect = new Rectangle(102, 136, (265-102), (240-136));
        Rectangle lmcRect = new Rectangle(270, 100, (500-270), (400-100));
        Rectangle o2Rect = new Rectangle(100, 15, (260-100), (90-15));
        Rectangle manRect = new Rectangle(140, 300, (330 - 140), (400 - 300));
        PowerBoard power;
        VentBoard vent;
        O2Board o2;
        Manual man;
        Terminal term;
        CriticalStat temp, dist;

        public ControlConsole(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>("Main");
            this.overlay = Content.Load<Texture2D>("SlotBG");
            name = "Mainframe";
            activePage = this;

            temp = new CriticalStat("Temp", 60, -0.001F, new Vector2(178, 246), Content.Load<SpriteFont>("MS Quartz"));
            dist = new CriticalStat("Distance", 1000000, 1, Vector2.Zero, Content.Load<SpriteFont>("MS Quartz"));

            power = new PowerBoard(Content);
            vent = new VentBoard(Content);
                DebugMode.vBoard = vent;
            o2 = new O2Board(Content);
                DebugMode.oBoard = o2;
            man = new Manual(Content);
            term = new Terminal(Content.Load<SpriteFont>("TerminalFont"),
                Content.Load<Texture2D>("CompCons"), Content.Load<Texture2D>("Darkness"), o2, ">>");

            allPages = new Dictionary<Rectangle, IPage>();
            allPages.Add(powerRect, power);
            allPages.Add(ventRect, vent);
            allPages.Add(lmcRect, term);
            allPages.Add(o2Rect, o2);
            allPages.Add(manRect, man);
        }

        public void Update(GameTime gt)
        {
            //Critical stat updates
            temp.Update(gt);
            dist.Update(gt);
            float f = 2 - dist.Percent;
            temp.DecayRate = ((f * f) * (-0.001F));
            if (vent.GetOutput("FansAndVents") == Voltage.High)
            {
                if (vent.GetOutput("Condenser") == Voltage.High)
                    temp.Add(-.01F);
                if (vent.GetOutput("Heater") == Voltage.High)
                    temp.Add(-.02F);
            }

            //Check for system powerup.  If system is powered, terminal is on.
            term.IsActive = (power.GetOutput("POUT").Equals(Voltage.Low)) ? true : false;
            //If terminal is active check for outputs to panels
            if (term.IsActive)
            {
                //Update registers
                term.SetRegister(99, temp.Value);

                //Get PowerOn voltage value from LMC
                Voltage volt = Voltage.None;
                int i = term.GetRegister(98);
                if (i != 0) volt = (i > 0) ? Voltage.High : Voltage.Low;
                vent.SetInput("PowerOn", volt);
                //Get CoolHot voltage value from LMC
                volt = Voltage.None;
                i = term.GetRegister(97);
                if (i != 0) volt = (i > 0) ? Voltage.High : Voltage.Low;
                vent.SetInput("HotCold", volt);
            }
            term.Update(gt);
            //Mouse controls
            if (!activePage.Equals(this))
            {
                if (KeyboardExt.IsKeyJustPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                    activePage = this;
                else activePage.Update(gt);
            }
            else if (MouseExt.IsLeftButtonJustPressed())
            {
                foreach (Rectangle r in allPages.Keys)
                {
                    if (r.Contains(MouseExt.State.X, MouseExt.State.Y))
                    {
                        activePage = allPages[r];
                        break;
                    }
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (!activePage.Equals(this))
                activePage.Draw(sb);
            else
            {
                sb.Draw(Texture, Vector2.Zero, Color.White);
                temp.Draw(sb);
            }
            sb.Draw(overlay, new Rectangle(0, 0, 800, 600), Color.Red*((temp.Value-60)/300F));
        }

        #region Explicit Interface Implementation
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
