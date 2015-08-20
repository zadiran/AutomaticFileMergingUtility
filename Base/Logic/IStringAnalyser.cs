using System.Collections.Generic;
using Base.Types;
using Base.Enums;

namespace Base.Logic
{
    public interface IStringAnalyser
    {
        IStringAnalysisResult ResultPrototype { get; set; }

        IStringAnalysisResult CompareStrings(string first, string second);
    }
}
