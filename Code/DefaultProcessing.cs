using System;
using System.Collections.Generic;
using Base;
using Base.Types;
using Base.Logic;
using Base.Enums;

namespace Code
{
    public class DefaultProcessing : IProcessing
    {
        private IDictionary<ITextFile, IFileAnalysisResult> processedFiles { get; set; }


        public DefaultProcessing()
        {
            processedFiles = new Dictionary<ITextFile, IFileAnalysisResult>();
        }

        public IFileAnalyser FileAnalyser { get; set; }

        public IMerge MergeComponent { get; set; }

        public ITextFile ProcessFiles(ITextFile source, IEnumerable<ITextFile> modifications)
        {
            if (MergeComponent == null || FileAnalyser == null)
            {
                throw new InvalidOperationException();
            }
            
            FileAnalyser.SourceFile = source;

            foreach (var modification in modifications)
	        {
		        processedFiles.Add(modification, FileAnalyser.CompareToSourceFile(modification));
	        }

            return MergeComponent.MergeFiles(processedFiles);
        }
    }
}
