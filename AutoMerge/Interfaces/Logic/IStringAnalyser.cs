using AutoMerge.Interfaces.Types;

namespace AutoMerge.Interfaces.Logic
{
    public interface IStringAnalyser
    {
        ISubstringSequence CompareStrings(string first, string second);
    }
}
