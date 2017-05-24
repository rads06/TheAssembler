using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using LogicGates;

namespace demo
{
    public class ALU
    {
        CircuitBoard myALU;
        const int wordLength = 32;

        public ALU(CircuitBoard myALU)
        {
            this.myALU = myALU;
        }

        public int BinaryAdd(int a, int b)
        {
            bool Cin = false;
            BitArray A = new BitArray(new int[] {a});
            BitArray B = new BitArray(new int[] {b});
            BitArray C = new BitArray(0);
            A.Length = wordLength;
            B.Length = wordLength;
            C.Length = wordLength;

            //Console.WriteLine("Adding " + a + " and " + b);
            for (int i = 0; i < wordLength; i++)
            {
                myALU.SetInput("C", BoolToVolt(Cin));
                myALU.SetInput("A", BoolToVolt(A[i]));
                myALU.SetInput("B", BoolToVolt(B[i]));
                myALU.UpdateOutputs();
                C[i] = VoltToBool(myALU.GetOutput("AddResult"));
                //Console.WriteLine(C[i] + " = " + A[i] + " + " + B[i]);
                Cin = VoltToBool(myALU.GetOutput("AddCarry"));
            }

            int[] value = new int[1];
            C.CopyTo(value, 0);
            return value[0];
        }

        public int BinarySub(int a, int b)
        {
            bool Cin = false;
            BitArray A = new BitArray(new int[] { a });
            BitArray B = new BitArray(new int[] { b });
            BitArray C = new BitArray(0);
            A.Length = wordLength;
            B.Length = wordLength;
            C.Length = wordLength;

            //Console.WriteLine("Subtracting " + a + " and " + b);
            for (int i = 0; i < wordLength; i++)
            {
                myALU.SetInput("C", BoolToVolt(Cin));
                myALU.SetInput("A", BoolToVolt(A[i]));
                myALU.SetInput("B", BoolToVolt(B[i]));
                myALU.UpdateOutputs();
                C[i] = VoltToBool(myALU.GetOutput("SubResult"));
                //Console.WriteLine(C[i] + " = " + A[i] + " + " + B[i]);
                Cin = VoltToBool(myALU.GetOutput("SubCarry"));
            }

            int[] value = new int[1];
            C.CopyTo(value, 0);
            return value[0];
        }

        public Voltage BoolToVolt(bool b) 
        { 
            return b ? Voltage.High : Voltage.Low;
        }

        public bool VoltToBool(Voltage v)
        {
            if (v == Voltage.None)
                throw new LMCTerminal.LittleManException("Error: Missing voltage reading from ALU board.");
            return (v == Voltage.High) ? true : false;
        }
    }
}
