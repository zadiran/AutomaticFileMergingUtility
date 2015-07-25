using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using AutoMerge.Enums;

namespace AutoMerge
{
    static class Merge
    {
        
		
        static internal TextFile buildOutputFile(TextFile sourceFile, TextFile mod1File, TextFile mod2File, int type)
        {
            
            
            TextFile output = new TextFile();
            output.text = new Queue<string>();

            Queue<string> stringsMod1 = new Queue<string>(mod1File.text);
            Queue<string> stringsMod2 = new Queue<string>(mod2File.text);

            var result1 = new Queue<StringChangeType>();
            var result2 = new Queue<StringChangeType>();
            if (type == 2)
            {
                result1 = FileAnalysis.deepFileAnalysis(sourceFile.getText(), mod1File.getText());
                result2 = FileAnalysis.deepFileAnalysis(sourceFile.getText(), mod2File.getText());
            }
            else if(type ==1)
            {
                result1 = FileAnalysis.fastFileAnalysis(sourceFile.getText(), mod1File.getText());
                result2 = FileAnalysis.fastFileAnalysis(sourceFile.getText(), mod2File.getText());
            }
            var value1 = StringChangeType.Unknown;
            var value2 = StringChangeType.Unknown;

            bool isNeededToDequeue1 = true;
            bool isNeededToDequeue2 = true;
            var charact1 = new Queue<StringChangeType>(result1);
            var charact2 = new Queue<StringChangeType>(result2);

            while ((charact1.Count > 0) || (charact2.Count > 0))
            {
                //извлекаем верхнее значение из каждой очереди
                if ((charact1.Count > 0) && isNeededToDequeue1)
                {
                    value1 = charact1.Dequeue();
                    isNeededToDequeue1 = false;
                }
                else if ((charact1.Count == 0) && isNeededToDequeue1)
                {
                    value1 = StringChangeType.Unknown;
                    isNeededToDequeue1 = false;
                }
                if ((charact2.Count > 0) && isNeededToDequeue2)
                {
                    value2 = charact2.Dequeue();
                    isNeededToDequeue2 = false;
                }
                else if ((charact2.Count == 0) && isNeededToDequeue2)
                {
                    value2 = StringChangeType.Unknown;
                    isNeededToDequeue2 = false;
                }
                //если одна из очередей закончилась быстрее, чем надо
                if ((value1 == StringChangeType.Unknown) || (value2 == StringChangeType.Unknown))
                {
                    if ((value1 == StringChangeType.Unknown) && (value2 != StringChangeType.Unknown))
                    {
                        if (value2 == StringChangeType.Deleted)
                        {
                            isNeededToDequeue2 = true;
                        }
                        else if ((stringsMod2.Count > 0) &&( (value2 == StringChangeType.Changed) || (value2 == StringChangeType.NotChanged) ||(value2 == StringChangeType.Added) ))
                        {
                            output.text.Enqueue(stringsMod2.Dequeue());
                        }
                            isNeededToDequeue2 = true;
                    }
                     if ((value2 == StringChangeType.Unknown) && (value1 != StringChangeType.Unknown))
                    {
                        if (value1 == StringChangeType.Deleted)
                        {
                            isNeededToDequeue1 = true;
                        }
                        else if ((stringsMod1.Count>0)&& ((value1 == StringChangeType.Added) || (value1 == StringChangeType.Changed) || (value1 == StringChangeType.NotChanged)))
                        {
                            output.text.Enqueue(stringsMod1.Dequeue());
                        }
                            isNeededToDequeue1 = true;
                    }
                }
                //если среди извлеченных значений есть соответствующие добавленным строкам, записываем соответствующую строку в output и даем сигнал извлечь следующее значение
                else if ((value1 == StringChangeType.Added) || (value2 == StringChangeType.Added))
                {
                    if ((value1 == StringChangeType.Added) && (value2 == StringChangeType.Added))
                    {
                        string str1 = stringsMod1.Dequeue();
                        string str2 = stringsMod2.Dequeue();
                        if (str1.Cut() == str2.Cut())
                        {  
                            output.text.Enqueue(str1);
                            isNeededToDequeue1 = true;
                            isNeededToDequeue2 = true;
                        }
                        else
                        {
                            output.text.Enqueue(str1);
                            output.text.Enqueue(str2);
                            isNeededToDequeue1 = true;
                            isNeededToDequeue2 = true;
                        }
                    }
                    else if (value1 == StringChangeType.Added)
                    {
                        output.text.Enqueue(stringsMod1.Dequeue());
                        isNeededToDequeue1 = true;
                    }
                    else if (value2 == StringChangeType.Added)
                    {
                        output.text.Enqueue(stringsMod2.Dequeue());
                        isNeededToDequeue2 = true;
                    }
                }
                
                //если оба элемента удаленные, пропускаем
                else if ((value1 == StringChangeType.Deleted) && (value2 == StringChangeType.Deleted))
                {
                    isNeededToDequeue1 = true;
                    isNeededToDequeue2 = true;
                }
                //если один из элементов указывает на удалённую строку, а другой на изменённую/неизменную
                else if ((value1 == StringChangeType.Deleted) || (value2 == StringChangeType.Deleted))
                {
                    if ((value1 == StringChangeType.Deleted) && (value2 == StringChangeType.Changed))
                    {
                        output.text.Enqueue("//Conflict: first file isn't contain this string, second - contain it changed");
                        stringsMod2.Dequeue();
                        isNeededToDequeue1 = true;
                        isNeededToDequeue2 = true;
                    }
                    else if ((value1 == StringChangeType.Changed)  && (value2 == StringChangeType.Deleted))
                    {
                        output.text.Enqueue("//Conflict: first file contain it changed, second - isn't contain this string");
                        stringsMod1.Dequeue();
                        isNeededToDequeue1 = true;
                        isNeededToDequeue2 = true;
                    }
                    if ((value1 == StringChangeType.Deleted) && (value2 == StringChangeType.NotChanged))
                    {
                        stringsMod2.Dequeue();
                        isNeededToDequeue1 = true;
                        isNeededToDequeue2 = true;
                    }
                    else if ((value1 == StringChangeType.NotChanged) && (value2 == StringChangeType.Deleted))
                    {
                        stringsMod1.Dequeue();
                        isNeededToDequeue1 = true;
                        isNeededToDequeue2 = true;
                    }
                }
                //если оба элемента изменены, извлекаем их каждый из своей очереди, а в Output пишем исключение
                else if ((value1 == StringChangeType.Changed) && (value2 == StringChangeType.Changed))
                {
                    string str1 = stringsMod1.Dequeue();
                    string str2 = stringsMod2.Dequeue();
                    if (str1.Cut()==str2.Cut())
                    {
                        output.text.Enqueue(str1);
                    }
                    else if (str1.Cut() != str2.Cut())
                    {
                        output.text.Enqueue("//Conflict: both files contain this string changed");
                    }
                    isNeededToDequeue1 = true;
                    isNeededToDequeue2 = true;
                }
                //если один из элементов изменён, а другой - нет, извлекаем их каждый из своей очереди, а в Output пишем исключение
                else if ((value1 == StringChangeType.Changed) && (value2 == StringChangeType.NotChanged))
                {
                    output.text.Enqueue(stringsMod1.Dequeue());
                    stringsMod2.Dequeue();
                    isNeededToDequeue1 = true;
                    isNeededToDequeue2 = true;
                }
                else if ((value1 == StringChangeType.NotChanged) && (value2 == StringChangeType.Changed))
                {
                    output.text.Enqueue(stringsMod2.Dequeue());
                    stringsMod1.Dequeue();                    
                    isNeededToDequeue1 = true;
                    isNeededToDequeue2 = true;
                }
                //если оба элемента не изменены, извлекаем их каждый из своей очереди, а в Output пишем один из них
                else if ((value1 == StringChangeType.NotChanged) && (value2 == StringChangeType.NotChanged))
                {
                    output.text.Enqueue(stringsMod1.Dequeue());
                    stringsMod2.Dequeue();
                    isNeededToDequeue1 = true;
                    isNeededToDequeue2 = true;
                }
                

            }
            return output;
        }


    }

    struct TextFile
    {
        //текст, содержащийся в файле
        internal Queue<string> text;

        internal TextFile(Queue<string> Text)
        {
            text = Text;
        }
        internal TextFile(TextFile Text)
        {
            this = Text;
        }
        //если требуется текст в виде списка
        internal List<string> getText()
        {
            List<string> txt = new List<string>();
            Queue<string> tmp = new Queue<string>(text);
            while (tmp.Count > 0)
            {
                txt.Add(tmp.Dequeue());
            }
            return txt;
        }
    }
}
