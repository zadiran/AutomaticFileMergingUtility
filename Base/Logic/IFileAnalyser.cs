using Base.Types;

namespace Base.Logic
{
    public interface IFileAnalyser
    {
        IStringAnalyser StringAnalyser { get; set; }

        ITextFile SourceFile { get; set; }

        IFileAnalysisResult CompareToSourceFile(ITextFile modification);

        IFileAnalysisResult CompareFiles(ITextFile first, ITextFile second);
    }
}
