using Base.Logic;
using Base.Types;

namespace Code.Logic
{
    public abstract class DefaultFileAnalyser : IFileAnalyser
    {
        public abstract IFileAnalysisResult CompareFiles(ITextFile first, ITextFile second);
    }
}
