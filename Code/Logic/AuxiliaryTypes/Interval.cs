using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Logic.AuxiliaryTypes
{
    public class Interval  
    {
        public int Start { get; set; }

        public int End { get; set; }

        public int Length
        {
            get
            {
                return End - Start + 1;
            }
        }
        public Interval(int start, int end) 
        {
            Start = start;
            End = end;
        }
    }
}
