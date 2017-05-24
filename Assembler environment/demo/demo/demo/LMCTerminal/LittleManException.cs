using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMCTerminal
{
    class LittleManException : Exception
    {
        public LittleManException(string e)
            : base(e)
        {
        }
    }
}
