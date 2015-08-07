using Base.Types;

namespace Base.Logic
{
    public interface IStringAnalyser
    {
        IStringAnalysisResult CompareStrings(string first, string second);
    }
}
