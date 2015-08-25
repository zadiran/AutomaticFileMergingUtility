using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Logic.AuxiliaryTypes
{
    [Serializable]
    public class ProjectedInterval
    {
        public Interval Projected { get; set; }

        public Interval ProjectedOn { get; set; }

        public int Length
        {
            get
            {
                return Projected.Length;
            }
        }
        public ProjectedInterval(Interval projected, Interval projectedOn)
        {
            Projected = new Interval(projected.Start, projected.End);
            ProjectedOn = new Interval(projectedOn.Start, projectedOn.End);
        }

        public void PushEnd()
        {
            Projected.End++;
            ProjectedOn.End++;
        }
    }
}
