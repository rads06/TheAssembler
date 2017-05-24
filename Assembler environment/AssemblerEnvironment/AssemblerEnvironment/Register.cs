using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblerEnvironment
{
    class Register
    {
        private string registerData, labelName;
        private bool label;
        private int programCounter;

        public Register(int pc)
        {
            registerData = "000"; //null;
            label = false;
            labelName = String.Empty; // null;
            programCounter = pc;
        }

        public int getPosition()
        {
            return programCounter;
        }

        public string getData()
        {
            return registerData;
        }

        public void setData(string data)
        {
            registerData = data;
        }

        public string getLabelName()
        {
            return labelName;
        }

        public void setLabel(string str)
        {
            labelName = str;
            label = true;
        }

        public void removeLabel()
        {
            labelName = null;
            label = false;
        }

        public bool hasLabel()
        {
            return label;
        }

    }
}
