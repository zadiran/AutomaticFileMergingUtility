using Base.Types;
using Base.Enums;

namespace Types
{
    public abstract class DefaultStringAnalysisResult : IStringAnalysisResult
    {
        public abstract bool IsEqual { get; }

        public abstract StringChangeType Equality { get; }

        public abstract double GetEqualityInPercents { get; }
    }
}
