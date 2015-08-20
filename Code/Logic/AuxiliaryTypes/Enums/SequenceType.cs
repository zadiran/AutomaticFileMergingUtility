using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Logic.AuxiliaryTypes.Enums
{
    public enum SequenceType
    {
        // Used as a template in previous iterations
        Outdated = 1,

        // Used as a template in current iteration
        Intermediate = 2, 

        // With added in current iteration elements 
        FullAtStage = 3, 

        // Final version of sequence. Immutable
        Full = 4
    }
}
