using System.Collections.Generic;

namespace Alogms
{
    internal class SubSetSum
    {
        public List<List<int>> Subsets { get; private set; } // список подмножеств

        private bool[,] solution = null; // матрица
        private int[] set = null;

        public void Search(int[] set, int sum) // начало поиска
        {
            Subsets = new List<List<int>>(); // инициализвания списка подмножеств
            this.set = set; // взятие элементов

            if (set.Length == 0 || sum < 0) // если элементов нет либо сумма меньше нуля - выход
            {
                return;
            }

            BuildingSolution(sum); // построние матрицы 

            if (solution[set.Length - 1, sum] == false) // если решений не было найдено
            {
                return;
            }

            List<int> p = new List<int>(); // инициализация списка
            SubsetsRec(set.Length - 1, sum, p);
        }

        private void BuildingSolution(int sum)
        {
            solution = new bool[set.Length, sum + 1]; // объявление матрицы кол-во строк = кол-ву чисел в множестве, а столбцов = заданной сумме+1

            for (int i = 0; i < set.Length; i++) // заполнение первого столбца значениями true
            {
                solution[i, 0] = true;
            }

            for (int i = 0; i < set.Length; i++) // проход по всей матрице без первого столбца
            {
                for (int j = 1; j <= sum; j++) // проход по строке со второй позиции
                {
                    if (i != 0)
                    {
                        solution[i, j] = solution[i - 1, j]; // присваиваем значение ячейки на строку выше
                        if (solution[i, j] == false && j >= set[i]) // если в ячейке значение false и индекц текущего столбка меньше значения i-ого элемента входного массива
                        {
                            solution[i, j] = solution[i, j] || solution[i - 1, j - set[i]]; // значение ячейки равно или самой себе, или значению в строке выше и номер столбка равен текущему индексу минус занчение i-ого элемента входного массива
                        }
                    }
                    else // первая строка
                    {
                        if (j == set[i]) // если номер текущего столбца == значению нулевого элемента массива чисел
                        {
                            solution[i, j] = true;
                        }
                        else
                        {
                            solution[i, j] = false;
                        }
                    }
                }
            }
        }

        private void SubsetsRec(int i, int sum, List<int> subset)
        {
            if (i == 0 && sum != 0 && solution[0, sum])
            {
                subset.Add(set[i]);
                Subsets.Add(subset);
                return;
            }

            if (i == 0 && sum == 0)
            {
                Subsets.Add(subset);
                return;
            }

            if (i != 0 && solution[i - 1, sum])
            {
                List<int> recreate = subset.GetRange(0, subset.Count);
                SubsetsRec(i - 1, sum, recreate);
            }

            if (i != 0 && sum >= set[i] && solution[i - 1, sum - set[i]])
            {
                subset.Add(set[i]);
                SubsetsRec(i - 1, sum - set[i], subset);
            }
        }
    }
}
