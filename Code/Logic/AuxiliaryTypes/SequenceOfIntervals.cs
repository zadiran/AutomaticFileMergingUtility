using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading.Tasks;
using Code.Logic.AuxiliaryTypes.Enums;


namespace Code.Logic.AuxiliaryTypes
{
    [Serializable]
    class SequenceOfIntervals : IComparable<SequenceOfIntervals>
    {

        public List<ProjectedInterval> Sequence { get; private set; }

        public ProjectedInterval Interval { get; private set; }

        public bool IsModified { get; set; }

        public SequenceType Type { get; set; }
        
        public SequenceOfIntervals Clone
        {
            get
            {
                var ms = new MemoryStream();
                var bf = new BinaryFormatter();
                bf.Serialize(ms, this);
                ms.Position = 0;
                return (SequenceOfIntervals)bf.Deserialize(ms);
            }
        }

        public int Length
        {
            get
            {
                return Sequence.Sum(x => x.Projected.Length);
            }
        }

        public SequenceOfIntervals() { }

        public SequenceOfIntervals(ProjectedInterval interval)
        {
            Sequence = new List<ProjectedInterval>();
            Sequence.Add(interval);
            Type = SequenceType.FullAtStage;
            Interval = interval;
            IsModified = false;
        }

        
        public void Add(ProjectedInterval interval)
        {
            Sequence.Add(interval);
            Interval.Projected.End = interval.Projected.End;
            Interval.ProjectedOn.End = interval.ProjectedOn.End;
            IsModified = true;
        }

        public int CompareTo(SequenceOfIntervals other)
        {
            if (other == null)
            {
                return 1;
            }
            if (Interval.Projected.Start.CompareTo(other.Interval.Projected.Start) == 0)
            {
                return Interval.ProjectedOn.Start.CompareTo(other.Interval.ProjectedOn.Start);
            }
            else
            {
                return Interval.Projected.Start.CompareTo(other.Interval.Projected.Start);
            }
        }

    }
}
