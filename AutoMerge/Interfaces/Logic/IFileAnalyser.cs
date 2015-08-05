using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMerge.Interfaces.Types;

namespace AutoMerge.Interfaces.Logic
{
    public interface IFileAnalyser
    {
        IStringChangeTypeSequence CompareFiles(ITextFile source, ITextFile modification);
    }
}
