using System.Collections.Generic;
using AutoMerge.Interfaces.Types;

namespace AutoMerge.Interfaces
{
    public interface IMerge
    {
        ITextFile MergeFiles(ITextFile source, IEnumerable<ITextFile> modifications);
    }
}
