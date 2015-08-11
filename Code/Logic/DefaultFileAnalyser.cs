using Base.Logic;
using Base.Types;

namespace Code.Logic
{
    public abstract class DefaultFileAnalyser : IFileAnalyser
    {
        public IStringAnalyser StringAnalyser { get; set; }

        public ITextFile SourceFile { get; set; }

        public abstract IFileAnalysisResult CompareToSourceFile(ITextFile modification);

        public abstract IFileAnalysisResult CompareFiles(ITextFile first, ITextFile second);
    }
}
