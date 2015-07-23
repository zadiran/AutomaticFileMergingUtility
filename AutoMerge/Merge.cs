using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

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

            Queue<int> result1 = new Queue<int>();
            Queue<int> result2 = new Queue<int>();
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
            int value1 = -1;
            int value2 = -1;

            bool isNeededToDequeue1 = true;
            bool isNeededToDequeue2 = true;
            Queue<int> charact1 = new Queue<int>(result1);
            Queue<int> charact2 = new Queue<int>(result2);

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
                    value1 = -1;
                    isNeededToDequeue1 = false;
                }
                if ((charact2.Count > 0) && isNeededToDequeue2)
                {
                    value2 = charact2.Dequeue();
                    isNeededToDequeue2 = false;
                }
                else if ((charact2.Count == 0) && isNeededToDequeue2)
                {
                    value2 = -1;
                    isNeededToDequeue2 = false;
                }
                //если одна из очередей закончилась быстрее, чем надо
                if ((value1 == -1) || (value2 == -1))
                {
                    if ((value1 == -1) && (value2 > -1))
                    {
                        if (value2==0)
                        {
                            isNeededToDequeue2 = true;
                        }
                        else if ((stringsMod2.Count > 0) &&( (value2 == 2) || (value2 == 3) ||(value2 == 1) ))
                        {
                            output.text.Enqueue(stringsMod2.Dequeue());
                        }
                            isNeededToDequeue2 = true;
                    }
                     if ((value2 == -1) && (value1 > -1))
                    {
                        if (value1 == 0)
                        {
                            isNeededToDequeue1 = true;
                        }
                        else if ((stringsMod1.Count>0)&& ((value1 == 1) || (value1 == 2) || (value1 == 3)))
                        {
                            output.text.Enqueue(stringsMod1.Dequeue());
                        }
                            isNeededToDequeue1 = true;
                    }
                }
                //если среди извлеченных значений есть соответствующие добавленным строкам, записываем соответствующую строку в output и даем сигнал извлечь следующее значение
                else if ((value1 == 1) || (value2 == 1))
                {
                    if ((value1 == 1) && (value2 == 1))
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
                    else if (value1 == 1)
                    {
                        output.text.Enqueue(stringsMod1.Dequeue());
                        isNeededToDequeue1 = true;
                    }
                    else if (value2 == 1)
                    {
                        output.text.Enqueue(stringsMod2.Dequeue());
                        isNeededToDequeue2 = true;
                    }
                }
                
                //если оба элемента удаленные, пропускаем
                else if ((value1 == 0) && (value2 == 0))
                {
                    isNeededToDequeue1 = true;
                    isNeededToDequeue2 = true;
                }
                //если один из элементов указывает на удалённую строку, а другой на изменённую/неизменную
                else if ((value1 == 0) || (value2 == 0))
                {
                    if ((value1 == 0) && (value2 == 2) )
                    {
                        output.text.Enqueue("//Conflict: first file isn't contain this string, second - contain it changed");
                        stringsMod2.Dequeue();
                        isNeededToDequeue1 = true;
                        isNeededToDequeue2 = true;
                    }
                    else if ((value1 == 2)  && (value2 == 0))
                    {
                        output.text.Enqueue("//Conflict: first file contain it changed, second - isn't contain this string");
                        stringsMod1.Dequeue();
                        isNeededToDequeue1 = true;
                        isNeededToDequeue2 = true;
                    }
                    if ((value1 == 0) && (value2 == 3))
                    {
                        stringsMod2.Dequeue();
                        isNeededToDequeue1 = true;
                        isNeededToDequeue2 = true;
                    }
                    else if ( (value1 == 3) && (value2 == 0))
                    {
                        stringsMod1.Dequeue();
                        isNeededToDequeue1 = true;
                        isNeededToDequeue2 = true;
                    }
                }
                //если оба элемента изменены, извлекаем их каждый из своей очереди, а в Output пишем исключение
                else if ((value1 == 2) && (value2 == 2))
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
                else if ((value1 == 2) && (value2 == 3))
                {
                    output.text.Enqueue(stringsMod1.Dequeue());
                    stringsMod2.Dequeue();
                    isNeededToDequeue1 = true;
                    isNeededToDequeue2 = true;
                }
                else if ((value1 == 3) && (value2 == 2))
                {
                    output.text.Enqueue(stringsMod2.Dequeue());
                    stringsMod1.Dequeue();                    
                    isNeededToDequeue1 = true;
                    isNeededToDequeue2 = true;
                }
                //если оба элемента не изменены, извлекаем их каждый из своей очереди, а в Output пишем один из них
                else if ((value1 == 3) && (value2 == 3))
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
