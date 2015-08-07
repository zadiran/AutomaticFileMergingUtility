using System;
using System.Collections.Generic;
using Base.Types;

namespace Base
{
    public interface IProcessing
    {
        ITextFile ProcessFiles(ITextFile source, IEnumerable<ITextFile> modifications); 
    }
}
