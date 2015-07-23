using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMerge
{
    class Sequence
    { 
        //начало и конец главной последовательности
        internal int start { get; set; }
        internal int end { get; set; }

        //начало и конец ресурсной последовательности
        internal int sourceStart { get; set; }
        internal int sourceEnd { get; set; }

        //длина последовательности
        internal int lenght { get; set; }

        internal Sequence()
        {

        }
        internal Sequence(int st, int en, int sst, int sen)
        {
            start = st;
            end = en;
            sourceStart = sst;
            sourceEnd = sen;
            lenght = en - st + 1;
        }
        //увеличение длины последовательности на 1
        internal void increaseLenght()
        {
            end++;
            sourceEnd++;
            lenght++;
        }
    }
}
