using Base.Types;

namespace Base.Logic
{
    public interface IFileAnalyser
    {
        IFileAnalysisResult CompareFiles(ITextFile source, ITextFile modification);
    }
}
