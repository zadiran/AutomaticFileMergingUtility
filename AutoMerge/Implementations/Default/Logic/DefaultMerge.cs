using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMerge.Interfaces.Logic;
using AutoMerge.Interfaces.Types;
using AutoMerge.DI;

namespace AutoMerge.Implementations.Default.Logic
{
    public class DefaultMerge : IMerge
    {
        
        #region properties

        public IFileAnalyser FileAnalyser { get; set; }
        
        #endregion
        
        #region constructors

        public DefaultMerge() { }

        #endregion

        #region interface methods

        public ITextFile MergeFiles(ITextFile source, IEnumerable<ITextFile> modifications)
        {
            var analysedFiles = new Dictionary<ITextFile, IFileAnalysisResult>();
            
            foreach (var modification in modifications)
            {
                analysedFiles.Add(modification, FileAnalyser.CompareFiles(source, modification)); 
            }
            return AssemblingFiles(analysedFiles);
        }

        #endregion

        #region private methods

        ITextFile AssemblingFiles(IDictionary<ITextFile, IFileAnalysisResult> source)
        {
            // Merging code coming soon
            return Supervisor.GetSupervisor.GetImplementation<ITextFile>();
        }
        #endregion
    }
}
