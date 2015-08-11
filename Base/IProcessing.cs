using System;
using System.Collections.Generic;
using Base.Types;
using Base.Logic;

namespace Base
{
    public interface IProcessing
    {
        IFileAnalyser FileAnalyser { get; set; }

        IMerge MergeComponent { get; set; }

        ITextFile ProcessFiles(ITextFile source, IEnumerable<ITextFile> modifications);
    }
}
