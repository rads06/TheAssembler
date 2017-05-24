using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LMCTerminal
{
    class Register : IComparable
    {
        string labelName;
        int val;
        int programCounter;

        public int Value
        {
            get { return val; }
            set { val = (int)MathHelper.Clamp(value, -999, 999); }
        }
        public bool IsNegative { get { return val < 0; } }

        public Register(int pc)
        {
            //registerData = "000"; //null;
            labelName = String.Empty; // null;
            programCounter = pc;
        }

        public Register() : this(-1) { }

        #region Tom's Accessors
        public int getPosition()
        {
            return programCounter;
        }

        public string getData()
        {
            return this.ToString();
        }

        public void setData(string data)
        {
            //Value = Convert.ToInt32(data);
            int i;
            if (Int32.TryParse(data, out i))
            {
                Value = i;
            }
            
        }

        public string getLabelName()
        {
            return labelName;
        }

        public void setLabel(string str)
        {
            labelName = str;
        }

        public void removeLabel()
        {
            labelName = String.Empty;
        }
        public bool hasLabel(string label)
        {
            return this.labelName.Equals(label, StringComparison.InvariantCultureIgnoreCase);
        }
        public bool hasLabel()
        {
            return !this.hasLabel(String.Empty);
        }
        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int val = Math.Abs(this.Value);
            if (this.IsNegative) sb.Append('-');
            sb.Append(val/100);
            sb.Append((val/10)%10);
            sb.Append(val%10);
            return sb.ToString();
        }

        /*public static implicit operator Parser.CmdLine(Register r)
        {
            return (Parser.CmdLine)r.Value;
        }*/
        /*public static explicit operator Parser.CmdLine(Register r)
        {
            return (Parser.CmdLine)r.Value;
        }*/
        /*public static implicit operator Register(Parser.CmdLine cl)
        {
            Register r = new Register();
            r.Value = (int)cl;
            return r;
        }*/
        /*public static explicit operator Register(Parser.CmdLine cl)
        {
            Register r = new Register();
            r.Value = (int)cl;
            return r;
        }*/

        #region Operators
        public static explicit operator int(Register r)
        {
            return r.Value;
        }
        /*public void Add(Register reg)
        {
            this.Value += reg.Value;
        }
        public void Sub(Register reg)
        {
            this.Value -= reg.Value;
        }*/
        public int CompareTo(object obj) { return CompareTo((int)obj); }
        public int CompareTo(int i) { return this.Value.CompareTo(i); }
        int IComparable.CompareTo(object obj) { return this.CompareTo(obj); }
        #endregion
    }
}
