using System;
using System.Collections.Generic;
using System.Text;
      
namespace AutoMerge
{
    static class FileAnalysis
    {
        /*глубокий анализ файла
         * [in] first - коллекция строк исходного файла
         * [in] second - коллекция строк модифицированного файла
         * [out] - характеристика файла
        */
        internal static Queue<int> deepFileAnalysis(List<string> first, List<string> second)
        {
            //содержит описание для каждой строки модифицированного файла
            Queue<int> characteristics = new Queue<int>();

            //матрица состояний для каждой строки файлов строки матрицы - строки первого файла, столбцы - строки второго
            int matrixRows = first.Count;
            int matrixColumns = second.Count;
            int[,] matrix = new int[matrixRows, matrixColumns];
            //заполняем матрицу
            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixColumns; j++)
                {
                    if (first[i].Cut() == second[j].Cut())
                    {
                        matrix[i, j] = 100;
                        
                    }
                    else 
                    {
                        matrix[i, j] = StringAnalysis.deepStringsAnalysis(first[i], second[j]);
                    }
                }
            }
            //создание самой длинной последовательности из полностью совпавших строк
            //если таковых нет, то используются строки с совпадением больше 90%, если и таких нет, то коэф совпадения уменьшается на 10 %, пока не найдется совпадение,
            //но не ниже уровня, разделяющего измененные и добавленные файлы

            //построение списка наиболее совпадающих строк
            List<TwoIntFields> highCoincidedElements = new List<TwoIntFields>(findHighCoincidence(matrix, matrixRows, matrixColumns));

            //если последовательность пуста, все строки из исходного файла удалены,  все строки в модифицированном файле - добавленные
            if (highCoincidedElements.Count == 0)
            {
                //файл полностью новый
                characteristics.Clear();
                for (int i = 0; i < second.Count; i++)
                {
                    characteristics.Enqueue(1);
                }
            }
            else
            {                
                //находим самую длинную цепочку среди этих строк
                List<TwoIntFields> longest = new List<TwoIntFields>(findLongestChain(highCoincidedElements));

                //на её основании строим карту модифицируемого файла
                if (characteristics.Count == 0)
                {
                    characteristics = makeCharacteristics(longest, matrix, matrixRows, matrixColumns);
                }
            }
            return characteristics;
        }

        /*быстрый вариант предыдущей функции*/
        internal static Queue<int> fastFileAnalysis(List<string> first, List<string> second)
        {
            //содержит описание для каждой строки модифицированного файла
            Queue<int> characteristics = new Queue<int>();
            
            //ищем 100% совпавшие строки в первый раз
            List<TwoIntFields> list = new List<TwoIntFields>();
            int min = first.Count;
            if ((first.Count!=second.Count)&&(second.Count<min))
            {

                min = second.Count;               
            }
            for (int i = 0; i < min-1; i++)
            {
                if(first[i]==second[i])
                {
                    list.Add(new TwoIntFields(i, i));
                    list[list.Count - 1].type = 3;
                }
            }
            if (list.Count==0)
            {
                int lowLevel = 0;
                for (int i = 0; i < first.Count; i++)
                {
                    bool isFound = false;
                    for (int j = lowLevel; j < second.Count; j++)
                    {
                        if (!isFound&&(first[i]== second[j]))
                        {
                            list.Add(new TwoIntFields(i, j));
                            list[list.Count - 1].type = 3;
                            lowLevel = j+1;
                            isFound = true;
                        }
                    }
                }
            }
            
            //заполняем пробелы
            int srcInd = 0;          //индекс в first
            int modInd = 0;          //индекс в second
            int listInd = 0;          //индекс в list
            List<TwoIntFields> tmp = new List<TwoIntFields>();

            while(modInd<second.Count)
            {
                //если список 100 еще не кончился
                if (listInd<list.Count)
                {
                    //проверяем, mi на принадлежность к нему
                    //если равно
                    if (modInd==list[listInd].mod)
                    {
                        listInd++;
                        //переходим к следующему элементу 100
                    }
                    //если же не равно
                    else
                    {
                        //ищем совпадения в first
                        bool isFound=false;     //нашли ли
                        int srcTmp = srcInd;    // делаем копию срцинд, на случай, если не найдём
                        while(!isFound&&(srcInd<list[listInd].src))
                        {
                            //если есть похожесть
                            int res = StringAnalysis.fastCompareStrings(first[srcInd], second[modInd]);
                            if (res>50)
                            {
                                //то круто
                                
                                //а если она еще и полная, то просто шикарно 
                                if (res == 100)
                                {
                                    tmp.Add(new TwoIntFields(srcInd, modInd));
                                    tmp[tmp.Count - 1].type = 3;
                                }
                                else
                                {
                                    tmp.Add(new TwoIntFields(srcInd, modInd));
                                    tmp[tmp.Count - 1].type = 2;
                                }
                                isFound = true;
                            }
                            srcInd++;
                        }
                        if (!isFound)
                        {
                            //если же мы не нашли такого элемента
                            //откатываем срцинд к предыдущему значению
                            srcInd = srcTmp;
                            //добавляем мод как новый
                            tmp.Add(new TwoIntFields(-1, modInd));
                            tmp[tmp.Count - 1].type = 1;
                        }

                    }
                }
                else
                {
                    //тут уже сверяем все с концами последовательностей
                    if ((srcInd<first.Count))
                    {
                        bool isFound = false;     //нашли ли
                        int srcTmp = srcInd;    // делаем копию срцинд, на случай, если не найдём
                        while (!isFound && (srcInd < first.Count))
                        {
                            //если есть похожесть
                            int res = StringAnalysis.fastCompareStrings(first[srcInd], second[modInd]);
                            if (res > 50)
                            {
                                //то круто
                                if (res == 100)
                                {
                                    tmp.Add(new TwoIntFields(srcInd, modInd));
                                    tmp[tmp.Count - 1].type = 3;
                                }
                                else
                                {
                                    tmp.Add(new TwoIntFields(srcInd, modInd));
                                    tmp[tmp.Count - 1].type = 2;
                                }
                                isFound = true;
                            }
                            srcInd++;
                        }
                        if (!isFound)
                        {
                            //если же мы не нашли такого элемента
                            //откатываем срцинд к предыдущему значению
                            srcInd = srcTmp;
                            //добавляем мод как новый
                            tmp.Add(new TwoIntFields(-1, modInd));
                            tmp[tmp.Count - 1].type = 1;
                        }
                    }
                    else if ((srcInd>=first.Count))
                    {
                        //если в срц больше никого нет, добавляем мод в список как добавленный элемент
                        tmp.Add(new TwoIntFields(-1, modInd));
                        tmp[tmp.Count - 1].type = 1;
                    }

                }
                modInd++;
            }
            list.AddRange( tmp);
            list.Sort();
            //теперь туда нужно вставить удалённые строки
            int prevIValue = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].src-1 > prevIValue)
                {
                    list.Insert(i, new TwoIntFields(prevIValue + 1, -1));
                    i++;
                    prevIValue++;
                }
            }
            foreach (TwoIntFields item in list)
            {
                characteristics.Enqueue(item.type);
            }

            return characteristics;
        }
        /*поиск самой длинной цепочки среди заданных
         * [in] - elements - список цепочец
         * [out] - самая длинная из них */
        static List<TwoIntFields> findLongestChain(List<TwoIntFields> elements)
        {
            foreach (TwoIntFields element in elements)
            {
                element.type = 3;
            }
            List<chainOfStrings> chains = new List<chainOfStrings>();

            int c = 0;
            //если цепочек несколько, сравниваем их между собой, иначе - возвращаем единственную содержащуюся
            if (elements.Count > 1)
            {
                while (elementWithTypeExist(3, elements))
                {
                    int index = findElementWithType(3, elements);

                    chains.Add(new chainOfStrings());
                    chains[c].Add(elements[0]);
                    elements[index].type = 1;

                    for (int m = 0; m < elements.Count; m++)
                    {
                        for (int n = 0; n < chains.Count; n++)
                        {
                            if ((elements[m].src > chains[n].i) && (elements[m].mod > chains[n].j))
                            {
                                chains[n].Add(elements[m]);
                                elements[m].type = 1;
                            }

                        }
                    }
                    c++;
                }
               
                //выбор самой длинной цепочки
                List<TwoIntFields> longestChain;
                int indexOfLongest = -1;
                int maxLenght = 0;
                for (int i = 0; i < chains.Count; i++)
                {
                    if (chains[i].Count() > maxLenght)
                    {
                        indexOfLongest = i;
                    }
                }
                longestChain = new List<TwoIntFields>(chains[indexOfLongest].chain);
                return longestChain;
            }
            else
            {
                if (elements.Count>0)
                {
                    if (chains.Count>0)
                    {
                       List<TwoIntFields> longestChain = new List<TwoIntFields>(chains[0].chain);
                return longestChain; 
                    }
                    else
                    {
                        return new List<TwoIntFields>();
                    }
                    
                }
                else
                {
                    return  new List<TwoIntFields>();
                }

            }

        }

        /*возвращает первый элемент в коллекции, имеющий заданный тип
         * [in] type - искомый тип
         * [in] list - коллекция элементов, имеющих типизацию
         * [out] - первое вхождение элемента с заданным типом*/
        static int findElementWithType(int type, List<TwoIntFields> list)
        {
            int index = -1;
            bool isFound = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (!isFound && (list[i].type == type))
                {
                    isFound = true;
                    index = i;
                }
            }
            return index;
        }

        /*возвращает признак существования элемента с искомым типом в коллекции
         * [in] type - искомый тип
         * [in] list - коллекция элементов, имеющих типизацию
         * [out] - признак существования элемента с искомым типом в коллекции*/
        static bool elementWithTypeExist(int type, List<TwoIntFields> list)
        {
            bool isExist = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (!isExist && (list[i].type == type))
                {
                    isExist = true;
                }
            }
            return isExist;
        }

        /*строит характеристику файла с точки зрения наличия в нем строк различных типов
         * 0 - удалённая строка
         * 1 - добавленная строка
         * 2 - изменённая строка
         * 3 - неизменная строка
         * [in] chain - цепочка наиболее совпадающих строк в модифицированном файле по отношению к исходному
         * [in] matrix - таблица, содержщая коэффициенты совпадения для каждой строки каждого файла
         * [in] rows - кол-во строк matrix
         * [in] cols - кол-во столбцов matrix
         * [out] - характеристика файла
         */
        static Queue<int> makeCharacteristics(List<TwoIntFields> chain, int[,] matrix, int rows, int cols)
        {
            Queue<int> charact = new Queue<int>();

            //на основе цепочки самых соовпавших элементов строим список незаплненных диапазонов
            List<TwoRanges> ranges = new List<TwoRanges>();
            if ((rows > 1) && (cols > 1) && (chain[0].mod > 0) && (chain[0].src > 0))
            {
                ranges.Add(new TwoRanges(0, chain[0].mod - 1, 0, chain[0].src - 1));
            }
            if (chain.Count > 1)
            {
                for (int i = 0; i < chain.Count - 1; i++)
                {
                    if ((rows > 1) && (cols > 1)&&((chain[i + 1].mod - chain[i].mod) > 1) && ((chain[i + 1].src - chain[i].src) > 1))
                    {
                        ranges.Add(new TwoRanges(chain[i].mod + 1, chain[i + 1].mod - 1, chain[i].src + 1, chain[i + 1].src - 1));
                    }
                }
            }
            if ((rows > 1) && (cols > 1)&&((chain[chain.Count - 1].mod) < (cols - 1)) && ((chain[chain.Count - 1].src) < (rows - 1)))
            {
                ranges.Add(new TwoRanges(chain[chain.Count - 1].mod + 1, cols - 1, chain[chain.Count - 1].src + 1, rows - 1));
            }
            else
            {
                //ranges.Add(new TwoRanges(0,0,0,0));
            }

            List<TwoIntFields> notChanged = new List<TwoIntFields>();
            List<TwoIntFields> changed = new List<TwoIntFields>();
            List<int> added = new List<int>();
            List<int> deleted = new List<int>();

            //заполняем списки элементами из chain
            //notChanged больше модифицироваться не будет, 
            //в changed могут добавляться новфе элементы по ходу работы алгоритма
            if ((rows > 0) && (cols > 0)&&(matrix[chain[0].src, chain[0].mod] == 100))
            {
                notChanged.AddRange(chain);
            }
            else
            {
                changed.AddRange(chain);
            }

            //заполняем changed, анализируя ranges в дальнейшем к ним могут добавляться элементы
            for (int j = 0; j < cols; j++)
            {
                foreach (TwoRanges range in ranges)
                {
                    if (j == range.range1start)
                    {
                        if((range.range1start==range.range1end)&&(range.range2start==range.range2end))
                        {
                            int m =matrix[range.range2start,range.range1start];
                            if ((m>=50)&&(m<100))
                            {
                                changed.Add(new TwoIntFields(range.range2start, range.range1start));
                            }
                        }
                        else if (range.range1start == range.range1end)
                        {
                            bool notFound = true;
                            for (int i = range.range2start; i <= range.range2end; i++)
                            {
                                int m = matrix[i,range.range1start];
                                if ((m>=50)&&(m<100)&&notFound)
                                {
                                    changed.Add(new TwoIntFields(i, range.range1start));
                                    notFound = false;
                                }
                            }
                        }
                        else if (range.range2start == range.range2end)
                        {
                            bool notFound = true;
                            for (int i = range.range1start; i <= range.range1end; i++)
                            {
                                int m = matrix[range.range2start, i];
                                if ((m >= 50) && (m < 100) && notFound)
                                {
                                    changed.Add(new TwoIntFields(range.range2start, i));
                                    notFound = false;
                                }
                            }
                        }
                        else
                        {
                            int minRowValue = range.range2start;
                            int minColValue = range.range1start;
                            for (int k = minColValue; k <= range.range1end; k++)
                            {
                                bool notFound = true;
                                for (int m = minRowValue; m <= range.range2end; m++)
                                {
                                    if ((matrix[m,k]>=50)&&(matrix[m,k]<100)&&notFound)
                                    {
                                        changed.Add(new TwoIntFields(m, k));
                                        minRowValue = m;
                                        notFound = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
           
            bool notInNotChangedList = true;
            bool notInChangedList = true;
            //теперь на основе changed  и notChanged заполняем added
            for (int m = 0; m < cols; m++)
            {
                //если m не входит ни в один из списков, то включаем ее в added
                notInNotChangedList = true;
                notInChangedList = true;

                foreach (TwoIntFields f in notChanged)
                {
                    if (notInNotChangedList && (f.mod == m))
                    {
                        notInNotChangedList = false;
                    }
                }
                foreach (TwoIntFields f in changed)
                {
                    if (notInChangedList && (f.mod == m))
                    {
                        notInChangedList = false;
                    }
                }
                if (notInChangedList && notInNotChangedList)
                {
                    added.Add(m);
                }
            }
           
            //ищем все удаленные строки
            for (int m = 0; m < rows; m++)
            {
                //если m не входит ни в один из списков, то включаем ее в added

                notInNotChangedList = true;
                notInChangedList = true;
                foreach (TwoIntFields f in notChanged)
                {
                    if (notInNotChangedList && (f.src == m))
                    {
                        notInNotChangedList = false;
                    }
                }

                foreach (TwoIntFields f in changed)
                {
                    if (notInChangedList && (f.src == m))
                    {
                        notInChangedList = false;
                    }
                }
                if (notInChangedList && notInNotChangedList)
                {
                    deleted.Add(m);
                }
            }
            added.Sort();
            deleted.Sort();
            
            //на данный момент у нас имееются наборы рафинированных элементов
            // для начала сведём changed и notChanged
            List<TwoIntFields> bothFounded = new List<TwoIntFields>(changed);
            bothFounded.AddRange(notChanged);

            //теперь соединим bothFounded c added и deleted
            //сначала added
            foreach (int j in added)
            {
                bothFounded.Add(new TwoIntFields(-1, j));
            }

            bothFounded.Sort();

            //теперь deleted
            List<int> deletedSequence = new List<int>();
            while (deleted.Count > 0)
            {
                deletedSequence.Add(deleted[0]);
                if (deleted.Count > 1)
                {
                    bool sequenceNotBroken = true;
                    for (int i = 1; i < deleted.Count; i++)
                    {
                        if ((deleted[i] - deleted[i - 1] == 1) && (sequenceNotBroken))
                        {
                            deletedSequence.Add(deleted[i]);
                        }
                        else
                        {
                            sequenceNotBroken = false;
                        }
                    }
                }
                List<TwoIntFields> delSeq = new List<TwoIntFields>();
                foreach (int element in deletedSequence)
                {
                    delSeq.Add(new TwoIntFields(element, -1));
                }
                //ищем место для этой последовательности и вставляем
                if (deletedSequence[0] == 0)
                {
                    bothFounded.InsertRange(0, delSeq);
                }
                else
                {
                    bool inserted = false;
                    int i = 0;
                    while (i < bothFounded.Count - 1)
                    {
                        if ((delSeq[0].src > bothFounded[i].src) && (delSeq[0].src < bothFounded[i + 1].src) && !inserted)
                        {
                            inserted = true;
                            bothFounded.InsertRange(i + 1, delSeq);
                        }
                        i++;
                    }


                }
                deleted.RemoveRange(0, deletedSequence.Count);
                deletedSequence.Clear();
            }

            // а теперь на основе bothFounded строим карту файла
            charact.Clear();
            foreach (TwoIntFields element in bothFounded)
            {
                if (element.mod == -1)
                {
                    charact.Enqueue(0);
                }
                else if (element.src == -1)
                {
                    charact.Enqueue(1);
                }
                else if ((matrix[element.src, element.mod] >= 50) && (matrix[element.src, element.mod] < 100))
                {
                    charact.Enqueue(2);
                }
                else if (matrix[element.src, element.mod] == 100)
                {
                    charact.Enqueue(3);
                }
            }
            
            return charact;
        }

        /*возвращает коллекцию элементов, имеющих наибольший коэффициент совпадения для строк из двух файлов
         * [in] matrix - таблица коэффициентов
         * [in] rows - кол-во строк таблицы
         * [in] cols - кол-во столбцов таблицы
         * [out] - коллекция элементов, имеющих наибольший коэффициент совпадения для строк из двух файлов
         */
        static List<TwoIntFields> findHighCoincidence(int[,] matrix, int rows, int cols)
        {
            //ключ - индекс строки исходного файла, значение - индекс строки модифицированного файла
            List<TwoIntFields> dictionary = new List<TwoIntFields>();

            //определение наибольшего совпадения
            int maxCoincidence = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] > maxCoincidence)
                    {
                        maxCoincidence = matrix[i, j];
                    }
                }
            }
            maxCoincidence = 10 * ((int)(maxCoincidence / 10));

            //если максимальное совпадение >=50%, выборка всех таких элементов
            if (maxCoincidence >= 50)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (matrix[i, j] >= maxCoincidence)
                        {
                            dictionary.Add(new TwoIntFields(i, j));
                        }
                    }
                }
            }

            return dictionary;
        }
    }
}






