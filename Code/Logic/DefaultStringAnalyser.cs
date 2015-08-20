using System;
using System.Collections.Generic;
using Base.Logic;
using Base.Types;
using Base.Enums;

namespace Code.Logic
{
    public class DefaultStringAnalyser : IStringAnalyser
    {
        public IStringAnalysisResult ResultPrototype { get; set; }
        
        public IStringAnalysisResult CompareStrings(string first, string second)
        {
            var result = ResultPrototype.Clone;

            first = first.Trim(' ', '\t');
            second = second.Trim(' ', '\t');
            if (first == second)
            {
                result.IsEqual = true;
                result.Equality = 100;
                return result;
            }

            byte equality = 0;

            //for (int i = 0; i < first.Length; i++)
            //{
            //    for (int j = 0; j < second.Length; j++)
            //    {
                           
            //    }
            //}

            return result;
            
        }
    }
}
