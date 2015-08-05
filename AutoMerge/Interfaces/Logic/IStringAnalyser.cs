using AutoMerge.Interfaces.Types;

namespace AutoMerge.Interfaces
{
    public interface IStringAnalyser
    {
        ISubstringSequence CompareStrings(string first, string second);
    }
}
