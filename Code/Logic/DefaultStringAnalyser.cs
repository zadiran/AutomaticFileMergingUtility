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

            var intervals = new List<ProjectedInterval>();

            for (int i = 0; i < first.Length; i++)
            {
                for (int j = 0; j < second.Length; j++)
                {
                    if (first[i] == second[j])
                    {
                        if (j == 0)
                        {
                            intervals.Add(new ProjectedInterval(new Interval(j,j), new Interval(i,i)));
                        }
                        else
                        {
                            bool isIntervalFound = false;
                            for (int k = 0; k < intervals.Count; k++)
                            {
                                if (intervals[k].Projected.End == j-1)
                                {
                                    isIntervalFound = true;
                                    if (intervals[k].ProjectedOn.End != first.Length - 1 
                                        && intervals[k].Projected.End != second.Length -1
                                        && second[j - 1] == first[intervals[k].ProjectedOn.End]
                                        && second[j] == first[intervals[k].ProjectedOn.End + 1])
                                    {
                                        intervals[k].PushEnd();
                                    }
                                }
                                else
                                {
                                    intervals.Add(new ProjectedInterval(new Interval(j, j), new Interval(i, i)));
                                }
                            }
                            if (!isIntervalFound)
                            {
                                intervals.Add(new ProjectedInterval(new Interval(j,j), new Interval(i,i)));
                            }
                        }
                    }
                }
            }

            if (intervals.Count > 0)
            {
                result.IsEqual = false;
                result.Equality = calculateEquality(first, second, new SequenceOfIntervalsProcessor(intervals).Longest());
                return result;
            }
            else
            {
                result.Equality = 0;
                result.IsEqual = false;
                return result;
            }
            
        }

        private byte calculateEquality(string first, string second, IList<ProjectedInterval> intervals)
        {
            int intervalLength = 0;
            foreach (var interval in intervals)
            {
                intervalLength += interval.Length;
            }

            int percentOfSymbols = (int)(100 * intervalLength / first.Length);
            int percentOfAddedSymbols = (int)(100 * (second.Length - intervalLength) / second.Length);

            return (byte)(percentOfSymbols - percentOfAddedSymbols / 10);

        }
    }
}
