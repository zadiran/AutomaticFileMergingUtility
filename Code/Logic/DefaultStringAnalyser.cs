using System;
using System.Collections.Generic;
using Base.Logic;
using Base.Types;
using Base.Enums;
using Code.Logic.Auxiliary;
using Code.Logic.AuxiliaryTypes;

namespace Code.Logic
{
    public class DefaultStringAnalyser : IStringAnalyser
    {
        public IStringAnalysisResult ResultPrototype { get; set; }
        
        public IStringAnalysisResult CompareStrings(string source, string mod)
        {
            var result = ResultPrototype.Clone;

            source = source.Trim(' ', '\t');
            mod = mod.Trim(' ', '\t');
            if (source == mod)
            {
                result.IsEqual = true;
                result.Equality = 100;
                return result;
            }

            var intervals = new List<ProjectedInterval>();

            for (int i = 0; i < source.Length; i++)
            {
                for (int j = 0; j < mod.Length; j++)
                {
                    if (source[i] == mod[j])
                    {
                        int k = 0;
                        while (i + k < source.Length && j + k < mod.Length && source[i + k] == mod[j + k])
                        {
                            k++;
                        }
                        intervals.Add(new ProjectedInterval(new Interval(j, j + k - 1), new Interval(i, i + k - 1)));
                    }
                }
            }

            if (intervals.Count > 0)
            {
                result.IsEqual = false;
                result.Equality = calculateEquality(source, mod, new SequenceOfIntervalsProcessor(intervals).Longest());
                return result;
            }
            else
            {
                result.Equality = 0;
                result.IsEqual = false;
                return result;
            }
            
        }

        private byte calculateEquality(string source, string mod, IList<ProjectedInterval> intervals)
        {
            int intervalLength = 0;
            foreach (var interval in intervals)
            {
                intervalLength += interval.Length;
            }

           
            int percentOfSymbols = (int)(100 * intervalLength / source.Length);
            int percentOfAddedSymbols = (int)(100 * (mod.Length - intervalLength) / source.Length);
            int percentOfDeletedSymbols = (int)(100 * (source.Length - intervalLength)/ source.Length);

            return (byte)(percentOfSymbols - percentOfAddedSymbols / 10 - percentOfDeletedSymbols / 10 );

        }
    }
}
