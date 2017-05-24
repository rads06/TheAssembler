using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace LogicGates
{
    public class VentBoard : CircuitBoard
    {
        public VentBoard(ContentManager Content)
            : base("Vent", Content)
        {
            Vector2 INV1Loc = new Vector2(408, 169);
            Vector2 XOR1Loc = new Vector2(233, 189);
            Vector2 INV2Loc = new Vector2(303, 188);
            Vector2 INV3Loc = new Vector2(567, 194);
            Vector2 XOR2Loc = new Vector2(418, 255);
            Vector2 MUX1Loc = new Vector2(600, 307);
            Vector2 INV4Loc = new Vector2(287, 311);
            Vector2 AND1Loc = new Vector2(201, 330);
            Vector2 AND2Loc = new Vector2(340, 397);

            ILogicInput powerOn = this.NewInput("PowerOn", Voltage.None);
            ILogicInput hotCold = this.NewInput("HotCold", Voltage.None);
            Slot INV1 = this.NewSlotAt(INV1Loc, "V0");
            Slot XOR1 = this.NewSlotAt(XOR1Loc, "V5");
            Slot INV2 = this.NewSlotAt(INV2Loc, "V4");
            Slot INV3 = this.NewSlotAt(INV3Loc, "V1");
            Slot XOR2 = this.NewSlotAt(XOR2Loc, "V3");
            Slot MUX1 = this.NewSlotAt(MUX1Loc, "V2");
            Slot INV4 = this.NewSlotAt(INV4Loc, "V6");
            Slot AND1 = this.NewSlotAt(AND1Loc, "V8");
            Slot AND2 = this.NewSlotAt(AND2Loc, "V7");

            //Set outputs
            this.NewOutput("Condenser", AND1);
            this.NewOutput("Heater", AND2);
            this.NewOutput("FansAndVents", powerOn);

            //Make connections
            INV3.AddInput(powerOn);
            
            INV1.AddInput(powerOn);
            
            MUX1.AddInput(hotCold);
            MUX1.AddInput(INV3);
            MUX1.AddInput(powerOn);
            
            XOR2.AddInput(INV1);
            XOR2.AddInput(MUX1);
            
            XOR1.AddInput(MUX1);
            XOR1.AddInput(powerOn);
            
            INV2.AddInput(XOR1);
            
            INV4.AddInput(XOR2);
            
            AND1.AddInput(MUX1);
            AND1.AddInput(INV2);

            AND2.AddInput(INV4);
            AND2.AddInput(MUX1);
        }
    }
}
