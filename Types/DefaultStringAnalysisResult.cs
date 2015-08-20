using System.Collections.Generic;
using Base.Types;
using Base.Enums;

namespace Types
{
    public struct DefaultStringAnalysisResult : IStringAnalysisResult
    {

        #region properties

        public bool IsEqual { get; set; }

        public byte Equality { get; set; }

        public IStringAnalysisResult Clone 
        {
            get 
            {
                var copy = this;
                copy.IsEqual = false;
                copy.Equality = 0;
                return copy;
            } 
        }
        
        #endregion
    }
}
