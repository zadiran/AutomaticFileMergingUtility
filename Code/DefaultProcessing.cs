using System.Collections.Generic;
using Base;
using Base.Types;

namespace Code
{
    public abstract class DefaultProcessing : IProcessing
    {
        public abstract ITextFile ProcessFiles(ITextFile source, IEnumerable<ITextFile> modifications);
    }
}
