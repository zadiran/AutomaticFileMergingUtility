using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMerge
{
    class TwoIntFields : IComparable<TwoIntFields>
    {
        public int CompareTo(TwoIntFields other)
        {
            if (other == null)
            {
                return 1;
            }

            return mod.CompareTo(other.mod);
        }
        internal int src { get; set; }
        internal int mod { get; set; }
        internal int type { get; set; }
        internal TwoIntFields(int Src, int Mod)
        {
            src = Src;
            mod = Mod;
        }

    }
    class chainOfStrings
    {
        internal List<TwoIntFields> chain { get; set; }
        internal int i { get; set; }
        internal int j { get; set; }

        //конструкторы
        internal chainOfStrings()
        {
            chain = new List<TwoIntFields>();
            i = -1;
            j = -1;
        }
        internal chainOfStrings(List<TwoIntFields> list)
        {
            chain = list;
            i = list[list.Count - 1].src;
            j = list[list.Count - 1].mod;
        }
        internal chainOfStrings(chainOfStrings ch)
        {
            chain = ch.chain;
            i = ch.i;
            j = ch.j;
        }

        //методы
        internal void Add(TwoIntFields field)
        {
            chain.Add(field);
            i = field.src;
            j = field.mod;
        }
        internal int Count()
        {
            return chain.Count;
        } 
    }
    //содержит в себе границы двух диапазонов
    //можно было бы сделать struct, а не class, но лучше пусть он будет в куче, чем захламляет стек
    class TwoRanges
    {
        internal int range1start { get; set; }
        internal int range1end { get; set; }

        internal int range2start { get; set; }
        internal int range2end { get; set; }

        internal TwoRanges(int r1s, int r1e, int r2s, int r2e)
        {
            range1start = r1s;
            range1end = r1e;
            range2start = r2s;
            range2end = r2e;
        }
    }
}