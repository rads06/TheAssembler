using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicGates
{
    internal class InputConnection : ILogicInput
    {
        internal string name;
        internal Voltage voltage;

        public InputConnection(string Name, Voltage Volt)
        {
            name = Name;
            voltage = Volt;
        }

        string ILogicInput.Name { get { return this.name; } }
        Voltage ILogicInput.GetOutput(string namedOutput) { return voltage; }
        Voltage ILogicInput.GetOutput() { return voltage; }
        void ILogicInput.UpdateOutputs() { return; }
    }

    public class BoardInputs
    {
        List<InputConnection> allInputs;

        public BoardInputs()
        {
            allInputs = new List<InputConnection>();
        }

        public void AddInput(string InputName)
        {
            this.AddInput(InputName, Voltage.None);
        }

        public void AddInput(string InputName, Voltage Voltage)
        {
            allInputs.Add(new InputConnection(InputName, Voltage));
        }

        public void SetVoltage(string InputName, Voltage Voltage)
        {
            try {
                allInputs.Find(i => i.name.Equals(InputName)).voltage = Voltage;
            } catch {
                throw new KeyNotFoundException();
            }
        }

        public ILogicInput GetLogicInput(string InputName)
        {
            try {
                return allInputs.Find(i => i.name.Equals(InputName));
            } catch {
                throw new KeyNotFoundException();
            }
        }
    }
}
