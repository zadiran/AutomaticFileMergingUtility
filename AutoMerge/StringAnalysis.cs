using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMerge
{
    static class StringAnalysis
    {
        /*Функция сравнения строк
         * [in] first - первая строка
         * [in] second - вторая строка
         * [out] - степень совпадения, выпаженная в %*/
        internal static int deepStringsAnalysis(string first, string second)
        {
            //степень совпадения строк
            int coincidence = -1;
           // first = "List<int> piecesrtrtrLenghts = new List<int>();";
           // second = "List<int> psdfiecesLengsdfsdhts = new List<int>();";

            first = first.Cut();
            second = second.Cut();
            if (first == second)
            {
                coincidence = 100;
            }
            else
            {
                //составление списка всех последовательностей элементов second, для которых имеется эквивалентная последовательность в first
                List<Sequence> sequences = new List<Sequence>();
                for (int i = 0; i < first.Length; i++)
                {
                    char ch1 = first[i];

                    for (int j = 0; j < second.Length; j++)
                    {
                        if (ch1 == second[j])
                        {
                            //если первый символ в строке, то для него нет последовательности перед ним 
                            //и он первоначально не входит ни в какую из последовательностей
                            if (j == 0)
                            {
                                sequences.Add(new Sequence(j, j, i, i));
                            }
                            //если не первый символ в строке
                            else
                            {
                                //если еще не было определено ни одной последовательности
                                if (sequences.Count == 0)
                                {
                                    sequences.Add(new Sequence(j, j, i, i));
                                }
                                else
                                {
                                    //находим последовательность, если такая есть, которая находится перед j элементом строки second
                                    bool isSeqFound = false;
                                    for (int k = 0; k < sequences.Count; k++)
                                    {
                                        if (sequences[k].end == j - 1)
                                        {
                                            isSeqFound = true;
                                            //если последовательность не в конце
                                            if ((sequences[k].sourceEnd != first.Length - 1) && (sequences[k].end != second.Length - 1))
                                            {
                                                if ((second[j - 1] == first[sequences[k].sourceEnd]) && (second[j] == first[sequences[k].sourceEnd + 1]))
                                                {
                                                    sequences[k].increaseLenght();
                                                }
                                            }
                                            else
                                            {
                                                sequences.Add(new Sequence(j, j, i, i));
                                            }
                                        }
                                    }
                                    if (!isSeqFound)
                                    {
                                        sequences.Add(new Sequence(j, j, i, i));
                                    }
                                }
                            }
                        }
                    }
                }
                //следующий код строит из всех последовательностей самую длинную цепочку, выкидывая ненужные
                List<Sequence> longest;
                if (sequences.Count > 0)
                {
                    SequenceOfSequences list = new SequenceOfSequences(sequences);
                    longest = new List<Sequence>(list.analysis());
                    coincidence = calculateCoincidence(first, second, longest);
                }
                else
                {
                    //вернуть что строки полностью не равны
                    coincidence = 0;
                }
            }
            return coincidence;
        }

        /*вычисляет степень совпадения на основании списка цепочек общих символов строк
         * [in] str1 - первая строка
         * [in] str2 - вторая строка
         * [in] sequences - список цепочек общих символов строк
         * [out] -степень совпадения, выпаженная в %*/
        static int calculateCoincidence(string str1, string str2, List<Sequence> sequences)
        {
            //самый главный показатель
            int coincidence;

            //количество символов из первой строки, найденное во второй в процентном соотношении
            int sequenceLenght = 0;
            foreach (Sequence seq in sequences)
            {
                sequenceLenght += seq.lenght;
            }
            int percentOfSymbols = (int)(100 * sequenceLenght / str1.Length);

            //количество добавленных символов в процентном отношении к длине второй строки
            int percentOfAddedSymbols = (int)(100 * (str2.Length - sequenceLenght) / str2.Length);

            coincidence = (int)(percentOfSymbols - percentOfAddedSymbols / 10);
            return coincidence;
        }

        /*Анализирует строку из модифицированного файла, сравнивая её со строкой из исходного по количеству совпадающих символов
         *[in] first - строка исходного файла
         *[in] second - строка модифицированного файла
         *[out] - степень совпадения строк 100*(кол-во символов из первой строки, найд. во второй/длина первой строки )*/
        internal static int fastCompareStrings(string first, string second)
        {

            first = first.Cut();                 //удаление отступов
            second = second.Cut();               //

            if(first == second)
            {
                return 100;
            }
            int grade = first.Length;           //количество совпавших символов, первоначально равно длине исходной строки, 
            //уменьшается, если очередной символ из первой строки не найден во второй
            //с учетом соблюдения расположения символов 

            int numberOfLostSymbols = 0;
            bool isSymbolFound;
            int currentStartIndex = 0;          //номер символа строки, с которого начинается очередной её обход, соответствует
            //следующему символу за последним найденым

            foreach (char ch1 in first)
            {
                isSymbolFound = false;

                for (int i = currentStartIndex; i < second.Length; i++)
                {
                    if ((!isSymbolFound) && (ch1 == second[i]))
                    {
                        isSymbolFound = true;
                        currentStartIndex = i + 1;
                    }
                }

                if (!isSymbolFound)
                {
                    numberOfLostSymbols++;
                }

            }

            if (grade == 0)
            {
                return 0;
            }
            else
            {
                return (int)(100 * (grade - numberOfLostSymbols / grade) - (second.Length - (grade - numberOfLostSymbols)) / 10);
            }
        }
    }
}
