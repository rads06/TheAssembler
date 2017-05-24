using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicGates
{
    public class BoardOutputs
    {
        Dictionary<string, ILogicInput> allOutputs;

        public BoardOutputs()
        {
            allOutputs = new Dictionary<string,ILogicInput>();
        }

        public void AddOutput(ILogicInput Input)
        {
            allOutputs.Add(Input.Name, Input);
        }

        public void AddOutputAlias(string Alias, ILogicInput Input)
        {
            allOutputs.Add(Alias, Input);
        }

        public Voltage GetOutputFrom(string Name)
        {
            try
            {
                return allOutputs[Name].GetOutput();
            }
            catch
            {
                throw new KeyNotFoundException();
            }
        }

        public void UpdateAllOutputs()
        {
            foreach (string s in allOutputs.Keys)
            {
                allOutputs[s].UpdateOutputs();
            }
        }
    }
}
