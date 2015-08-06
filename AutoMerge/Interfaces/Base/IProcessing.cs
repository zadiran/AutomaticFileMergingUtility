using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMerge.Interfaces.Logic;
using AutoMerge.Interfaces.Types;

namespace AutoMerge.Interfaces.Base
{
    public interface IProcessing
    {
        ITextFile ProcessFiles(ITextFile source, IEnumerable<ITextFile> modifications);  
    }
}
