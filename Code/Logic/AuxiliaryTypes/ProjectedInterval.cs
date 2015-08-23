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
            Projected = projected;
            ProjectedOn = projectedOn;
        }

        public void PushEnd()
        {
            Projected.End++;
            ProjectedOn.End++;
        }
    }
}
