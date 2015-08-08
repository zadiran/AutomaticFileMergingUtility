using Base.Logic;
using Base.Types;

namespace Code.Logic
{
    public abstract class DefaultStringAnalyser : IStringAnalyser
    {
        public abstract IStringAnalysisResult CompareStrings(string first, string second);
    }
}
