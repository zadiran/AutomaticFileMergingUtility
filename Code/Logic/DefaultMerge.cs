using System.Collections.Generic;
using Base.Logic;
using Base.Types;

namespace Code.Logic
{
    public abstract class DefaultMerge : IMerge
    {
        public abstract ITextFile MergeFiles(IDictionary<ITextFile, IFileAnalysisResult> files);
    }
}
