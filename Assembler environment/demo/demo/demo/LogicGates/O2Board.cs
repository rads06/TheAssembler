using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace LogicGates
{

/* Receiver Locations on O2 Regulator:
Adder Part:
NAME:	INPUT:		LOCATION:	FUNCTION:
V1	|A,B|		(246,136)	XOR
V2	|V1,C|		(361,150)	XOR
V3	|C,V1|		(448,190)	AND
V4	|V3,V5|		(655,182)	AND
V5	|B,A|		(533,224)	OR

Subtractor Part:
NAME:	INPUT:		LOCATION:	FUNCTION:
V6	|B|		(157,286)	INV
V7	|B,A|		(246,286)	XOR
V8	|V7,C|		(361,300)	XOR
V9	|V7|		(311,393)	INV
V10	|C,V9|		(448,340)	AND
V11	|V6,A|		(532,374)	AND
V12	|V11,V10|	(655,332)	OR
V13	|V8|		(691,257)	INV*/
    public class O2Board : CircuitBoard
    {
        Slot V1, V2, V3, V4, V5, V6, V7, V8, V9, V10, V11, V12, V13;

        public O2Board(ContentManager Content)
            : base("O2 Reg", Content, "O2Reg")
        {
            Vector2 V1Loc = new Vector2(246, 136);
            Vector2 V2Loc = new Vector2(361, 150);
            Vector2 V3Loc = new Vector2(448, 190);
            Vector2 V4Loc = new Vector2(655, 182);
            Vector2 V5Loc = new Vector2(533, 224);
            Vector2 V6Loc = new Vector2(157, 286);
            Vector2 V7Loc = new Vector2(246, 286);
            Vector2 V8Loc = new Vector2(361, 300);
            Vector2 V9Loc = new Vector2(311, 393);
            Vector2 V10Loc = new Vector2(448, 340);
            Vector2 V11Loc = new Vector2(533, 374);
            Vector2 V12Loc = new Vector2(655, 332);
            Vector2 V13Loc = new Vector2(691, 257);

            //ILogicInput powerOn = this.NewInput("PowerOn", Voltage.None);
            //ILogicInput hotCold = this.NewInput("HotCold", Voltage.None);
            V1 = this.NewSlotAt(V1Loc, "V1");
            V2 = this.NewSlotAt(V2Loc, "V2");
            V3 = this.NewSlotAt(V3Loc, "V3");
            V4 = this.NewSlotAt(V4Loc, "V4");
            V5 = this.NewSlotAt(V5Loc, "V5");
            V6 = this.NewSlotAt(V6Loc, "V6");
            V7 = this.NewSlotAt(V7Loc, "V7");
            V8 = this.NewSlotAt(V8Loc, "V8");
            V9 = this.NewSlotAt(V9Loc, "V9");
            V10 = this.NewSlotAt(V10Loc, "V10");
            V11 = this.NewSlotAt(V11Loc, "V11");
            V12 = this.NewSlotAt(V12Loc, "V12");
            V13 = this.NewSlotAt(V13Loc, "V13");

            //Set outputs
            this.NewOutput("AddResult", V2);
            this.NewOutput("AddCarry", V5);
            this.NewOutput("SubCarry", V12);
            this.NewOutput("SubResult", V13);

            //Make connections
            ILogicInput A = this.NewInput("A", Voltage.None);
            ILogicInput B = this.NewInput("B", Voltage.None);
            ILogicInput C = this.NewInput("C", Voltage.None);

            V1.AddInput(A);
            V1.AddInput(B);

            V2.AddInput(V1);
            V2.AddInput(C);

            V3.AddInput(C);
            V3.AddInput(V1);

            V4.AddInput(V3);
            V4.AddInput(V5);

            V5.AddInput(B);
            V5.AddInput(A);

            V6.AddInput(B);
            V7.AddInput(B);
            V7.AddInput(A);
            V8.AddInput(V7);
            V8.AddInput(C);
            V9.AddInput(V7);
            V10.AddInput(C);
            V10.AddInput(V9);
            V11.AddInput(V6);
            V11.AddInput(A);
            V12.AddInput(V11);
            V12.AddInput(V10);
            V13.AddInput(V8);
        }

/* Receiver Locations on O2 Regulator:
Adder Part:
NAME:	INPUT:		LOCATION:	FUNCTION:
V1	|A,B|		(246,136)	XOR
V2	|V1,C|		(361,150)	XOR
V3	|C,V1|		(448,190)	AND
V4	|V3,V5|		(655,182)	AND
V5	|B,A|		(533,224)	OR

Subtractor Part:
NAME:	INPUT:		LOCATION:	FUNCTION:
V6	|B|		(157,286)	INV
V7	|B,A|		(246,286)	XOR
V8	|V7,C|		(361,300)	XOR
V9	|V7|		(311,393)	INV
V10	|C,V9|		(448,340)	AND
V11	|V6,A|		(532,374)	AND
V12	|V11,V10|	(655,332)	OR
V13	|V8|		(691,257)	INV*/

        public void DebugAdder()
        {
            V1.UnSlotGate();
            V2.UnSlotGate();
            V3.UnSlotGate();
            V4.UnSlotGate();
            V5.UnSlotGate();

            V1.SlotGate(new XORGate(this, Vector2.Zero));
            V2.SlotGate(new XORGate(this, Vector2.Zero));
            V3.SlotGate(new ANDGate(this, Vector2.Zero));
            V4.SlotGate(new ANDGate(this, Vector2.Zero));
            V5.SlotGate(new ORGate(this, Vector2.Zero));
        }

        public void DebugSubtractor()
        {
            V6.UnSlotGate();
            V7.UnSlotGate();
            V8.UnSlotGate();
            V9.UnSlotGate();
            V10.UnSlotGate();
            V11.UnSlotGate();
            V12.UnSlotGate();
            V13.UnSlotGate();

            V6.SlotGate(new INVGate(this, Vector2.Zero));
            V7.SlotGate(new XORGate(this, Vector2.Zero));
            V8.SlotGate(new XORGate(this, Vector2.Zero));
            V9.SlotGate(new INVGate(this, Vector2.Zero));
            V10.SlotGate(new ANDGate(this, Vector2.Zero));
            V11.SlotGate(new ANDGate(this, Vector2.Zero));
            V12.SlotGate(new ORGate(this, Vector2.Zero));
            V13.SlotGate(new INVGate(this, Vector2.Zero));
        }
    }
}
