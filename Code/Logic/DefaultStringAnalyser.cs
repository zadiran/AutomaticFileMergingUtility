using System.Collections.Generic;
using Base.Logic;
using Base.Types;
using Base.Enums;

namespace Code.Logic
{
    public abstract class DefaultStringAnalyser : IStringAnalyser
    {
        public IDictionary<StringChangeType, byte> ComparisonPolicy { get; set; }

        public abstract IStringAnalysisResult CompareStrings(string first, string second);
    }
}
