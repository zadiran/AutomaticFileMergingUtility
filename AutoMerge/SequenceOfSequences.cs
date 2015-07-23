using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMerge
{
    class SequenceOfSequences
    {
        //данные
        List<SeqOfSeqElement> elements;
        List<Sequence> ourSequences;

        //конструктор
        internal SequenceOfSequences(List<Sequence> sequences)
        {
            //создание хранилища
            elements = new List<SeqOfSeqElement>();
            //сохранение копии списка sequences для внутренних нужд
            ourSequences = new List<Sequence>(sequences);
            //инициализация хранилища
            initialize(sequences);
        }
        /*функция, которая всё анализирует 
        * [out] - самая длинная цепочка последовательностей, которая может быть построена из данных*/
        internal List<Sequence> analysis()
        {
            while (existState(3))
            {
                //пробегаем по всем элементам и назначаем им необходимые для корректной работы параметры
                foreach (SeqOfSeqElement element in elements)
                {
                    if (element.type == 3)
                    {
                        element.type = 2;
                        element.isModified = false;
                    }
                }

                //построение цепочек
                elements.Sort();
                int count = elements.Count;
                for (int i = 0; i < count; i++)
                {
                    foreach (Sequence seq in ourSequences)
                    {
                        if (elements[i].end < seq.start)
                        {
                            if (elements[i].sourceEnd < seq.sourceStart)
                            {
                                elements.Add(elements[i]);
                                elements[i].isModified = true;

                                elements[elements.Count - 1].Add(seq);
                                elements[elements.Count - 1].type = 3;
                            }
                        }
                    }
                }

                
                foreach (SeqOfSeqElement element in elements)
                {
                    if (element.type == 2)
                    {
                        if (element.isModified)
                        {
                            element.type = 1;
                        }
                        else
                        {
                            element.type = 4;
                        }
                    }
                }
            }

            //выборка из списка самой длинной цепочки
            List<Sequence> list = findLongest(elements);
            return list;
        }

        /*поиск наибольшей последовательности
        * [in] - elements - список цепочек
        * [out] - самая длинная цепочка*/
        List<Sequence> findLongest(List<SeqOfSeqElement> elements)
        {
            List<Sequence> longest;
            int maxLenght = 0;
            int maxIndex = 0;
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].lenght() > maxLenght)
                {
                    maxLenght = elements[i].lenght();
                    maxIndex = i;
                }
            }
            longest = elements[maxIndex].getSequence();
            return longest;
        }
        //начальная инициализация 
        void initialize(List<Sequence> sequences)
        {
            foreach (Sequence seq in sequences)
            {
                elements.Add(new SeqOfSeqElement(seq));
            }
        }

        //функция, показывающая наличие элементов с состоянием state в elements
        bool existState(int state)
        {
            bool isExist = false;
            foreach (SeqOfSeqElement element in elements)
            {
                if (element.type == state)
                {
                    isExist = true;
                }
            }
            return isExist;
        }
    }

    //одна последовательность последовательностей 
    class SeqOfSeqElement : IComparable<SeqOfSeqElement>
    {
        public int CompareTo(SeqOfSeqElement other)
        {
            if (other == null)
            {
                return 1;
            }
            if (seqs[0].start.CompareTo(other.seqs[0].start) == 0)
            {
                return seqs[0].sourceStart.CompareTo(other.seqs[0].sourceStart);
            }
            else
            {
                return seqs[0].start.CompareTo(other.seqs[0].start);
            }
        }
        //данные
        List<Sequence> seqs;

        //определяет, является ли данная последовательность самой длинной
        //при инициализации элемента устанавливается в false, изменяется только функцией класса SequenceOfSequences, определяющей наиболее длинную цепочку
        internal bool isLongest { get; set; }
        /*тип данной последовательности
        * 4 - полная, т.е. эта последовательность уже готова и изменятся не будет
        * 3 - полная на данном этапе - в текущей итерации к ней добавлялись элементы
        * 2 - промежуточная - в данной итерации используется как шаблон для добавления к ней элементов
        * 1 - устаревшая - использовалась как шаблон в предыдущих итерациях, не стала полной*/
        internal int type { get; set; }

        //добавлялись ли в последовательность элементы
        internal bool isModified { get; set; }

        //координаты конца последнего элемента последовательности
        internal int end { get; set; }
        internal int sourceEnd { get; set; }
        //конструктор
        //принимает на входе sequence-элемент, который заносит в список первым
        internal SeqOfSeqElement(Sequence seq)
        {
            seqs = new List<Sequence>();
            seqs.Add(seq);
            type = 3;
            end = seq.end;
            sourceEnd = seq.sourceEnd;
            isLongest = false;
        }
        //возвращает содержащуюся цепочку последовательностей
        internal List<Sequence> getSequence()
        {
            return seqs;
        }
        //Добавляет к содержащейся цепочке один элемент
        internal void Add(Sequence sequence)
        {
            seqs.Add(sequence);
            end = sequence.end;
            sourceEnd = sequence.sourceEnd;
        }
        //возвращает длину содержащейся цепочки
        internal int lenght()
        {
            int len = 0;
            foreach (Sequence seq in seqs)
            {
                len += seq.lenght;
            }
            return len;
        }
    }
}
