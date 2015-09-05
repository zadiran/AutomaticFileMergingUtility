using System.Collections.Generic;

namespace Base.Types
{
    public interface ITextFile
    {
        Queue<string> Queue { get; set; }

        IList<string> List { get; set; }

        string LinesOneByOne { get; }
    }
}
