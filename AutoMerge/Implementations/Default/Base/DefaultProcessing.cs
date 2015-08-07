using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMerge.Interfaces.Base;
using AutoMerge.Interfaces.Logic;
using AutoMerge.Interfaces.Types;
using DI;

namespace AutoMerge.Implementations.Default.Base
{
    public class DefaultProcessing : IProcessing
    {        
        
        #region properties

        private IMerge Merge { get; set; }
        
        #endregion
        
        #region constructors

        public DefaultProcessing() { }

        #endregion

        #region interface methods

        public ITextFile ProcessFiles(ITextFile source, IEnumerable<ITextFile> modifications)
        {
            return Merge.MergeFiles(source, modifications);
        }

        #endregion
    }
}
