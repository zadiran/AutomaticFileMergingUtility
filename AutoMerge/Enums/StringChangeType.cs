using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMerge.Enums
{
    public enum StringChangeType
    {
        Unknown = -1,
        Deleted = 0, 
        Added = 1,
        Changed = 2,
        NotChanged = 3
    }
}
