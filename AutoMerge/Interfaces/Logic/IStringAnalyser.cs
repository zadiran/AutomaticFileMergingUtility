using AutoMerge.Interfaces.Types;

namespace AutoMerge.Interfaces.Logic
{
    public interface IStringAnalyser
    {
        IStringAnalysisResult CompareStrings(string first, string second);
    }
}
