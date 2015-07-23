using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMerge
{
    static class ExtensionMethods
    {
        //обрезает отступы у строки str
        public static string Cut(this string str)
        {
            if (str.Length>0)
            {
               
                int start = 0;
                int end = str.Length - 1;

                while ((start<str.Length)&&((str[start] == '\t') || (str[start] == ' ')))
                {
                    if (start < str.Length)
                    {
                        start++;
                    }
                }
                if (start != str.Length)
                {
                    while ((end >= 0) && ((str[end] == '\t') || (str[end] == ' ')))
                    {
                        if (end >= 0)
                        {
                            end--;
                        }
                    }
                }
                return str.Substring(start, end - start + 1); 
            }
            else
            {
                return str;
            }
        }

        //возвращает длину строки без отступов
        public static int CutLenght(this string str)
        {
            return str.Cut().Length;
        }
    }
}
