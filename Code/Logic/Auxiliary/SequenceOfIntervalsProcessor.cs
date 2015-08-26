using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Code.Logic.AuxiliaryTypes;
using Code.Logic.AuxiliaryTypes.Enums;

namespace Code.Logic.Auxiliary
{
    internal class SequenceOfIntervalsProcessor
    {
        private List<ProjectedInterval> Intervals { get; set; }

        private List<SequenceOfIntervals> Elements { get; set; }

        private SequenceOfIntervalsProcessor() {}

        public SequenceOfIntervalsProcessor(List<ProjectedInterval> intervals)
        {
            Intervals = intervals;
            Elements = new List<SequenceOfIntervals>();
            Initialize(intervals);
        }

        public IList<ProjectedInterval> Longest(int maxLength)
        {
            while (Elements.Any(x => x.Type == SequenceType.FullAtStage))
            {
                foreach (var element in Elements)
                {
                    if (element.Type == SequenceType.FullAtStage)
                    {
                        element.Type = SequenceType.Intermediate;
                    }
                    element.IsModified = false;
                }

                var longestAtStage = selectLongest(Elements).Length;
                var toDelete = new List<int>();
                for (int e = 0; e < Elements.Count; e++)
                {
                    if (Elements[e].Length + maxLength - Elements[e].Interval.Projected.End < longestAtStage) 
                    {
                        Elements.RemoveAt(e);
                        if (e > 0)
                        {
                            e--;
                        }
                    } 
                }
                Elements.Sort();
                // this code make algorythm twice slower, but give minimal profit
                //for (int e = 0; e < Elements.Count-1; e++)
                //{
                //    for (int f = e + 1; f < Elements.Count; f++)
                //    {
                //        if (Elements[e].Interval.Projected.End - Elements[f].Interval.Projected.Start == 1
                //            && Elements[e].Interval.ProjectedOn.End - Elements[f].Interval.ProjectedOn.Start == 1)
                //        {
                //            Elements[e].Interval.Projected.End = Elements[f].Interval.Projected.End;
                //            Elements[e].Interval.ProjectedOn.End = Elements[f].Interval.ProjectedOn.End;
                //            Elements.RemoveAt(f);
                //        }   
                //    }
                //}

                for (int i = 0; i < Elements.Count; i++)
                {
                    if (Elements[i].Type == SequenceType.Intermediate)
                    {
                        foreach (var interval in Intervals)
                        {
                            if (Elements[i].Interval.Projected.End < interval.Projected.Start)
                            {
                                if (Elements[i].Interval.ProjectedOn.End < interval.ProjectedOn.Start)
                                {
                                    Elements.Add(Elements[i].Clone);
                                    Elements[i].IsModified = true;

                                    Elements[Elements.Count - 1].Add(interval);
                                    Elements[Elements.Count - 1].Type = SequenceType.FullAtStage;
                                }
                            }
                        }
                    }
                }
                foreach (var element in Elements)
	            {
		            if (element.Type == SequenceType.Intermediate)
	                {
		                element.Type = element.IsModified ? SequenceType.Outdated : SequenceType.Full;
	                }
	            }

            }

            return selectLongest(Elements).Sequence;
        }

        private SequenceOfIntervals selectLongest(List<SequenceOfIntervals> intervals)
        {
            int LengthOfMax = 0;
            int IndexOfMax = 0;
            for (int i = 0; i < intervals.Count; i++)
			{
			    if (Elements[i].Length > LengthOfMax)
	            {
		            LengthOfMax = Elements[i].Length;
                    IndexOfMax = i;
	            }
			}
            return intervals[IndexOfMax];
        }

        private void Initialize(IList<ProjectedInterval> intervals)
        {
            foreach (var interval in intervals)
            {
                Elements.Add(new SequenceOfIntervals(interval));
            }
        }
    }
}
