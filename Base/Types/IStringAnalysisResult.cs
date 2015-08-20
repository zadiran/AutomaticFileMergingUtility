using System;
using Base.Enums;

namespace Base.Types
{
    public interface IStringAnalysisResult
    {
        bool IsEqual { get; set; }

        byte Equality { get; set; }

        IStringAnalysisResult Clone { get; }
    }
}
