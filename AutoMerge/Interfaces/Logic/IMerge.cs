using System.Collections.Generic;
using AutoMerge.Interfaces.Types;

namespace AutoMerge.Interfaces.Logic
{
    public interface IMerge
    {
        ITextFile MergeFiles(ITextFile source, IEnumerable<ITextFile> modifications);
    }
}
