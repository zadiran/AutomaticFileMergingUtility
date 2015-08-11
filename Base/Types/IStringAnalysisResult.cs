using Base.Enums;

namespace Base.Types
{
    public interface IStringAnalysisResult
    {
        bool IsEqual { get; }

        StringChangeType Equality { get; }

        double GetEqualityInPercents { get; }        
    }
}
