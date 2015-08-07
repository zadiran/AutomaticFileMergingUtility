using Base.Types;

using System.Collections.Generic;

namespace Base.Logic
{
    public interface IMerge
    {
        ITextFile MergeFiles(ITextFile source, IEnumerable<ITextFile> modifications);
    }
}
