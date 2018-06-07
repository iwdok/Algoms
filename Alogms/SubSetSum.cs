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
            solution = new bool[set.Length, sum + 1];

            for (int i = 0; i < set.Length; i++)
            {
                solution[i, 0] = true;
            }

            for (int i = 0; i < set.Length; i++)
            {
                for (int j = 1; j <= sum; j++)
                {
                    if (i != 0)
                    {
                        solution[i, j] = solution[i - 1, j];
                        if (solution[i, j] == false && j >= set[i])
                        {
                            solution[i, j] = solution[i, j] || solution[i - 1, j - set[i]];
                        }
                    }
                    else
                    {
                        if (j == set[i])
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

        private void SubsetsRec(int i, int sum, List<int> p)
        {
            if (i == 0 && sum != 0 && solution[0, sum])
            {
                p.Add(set[i]);
                Subsets.Add(p);
                return;
            }

            if (i == 0 && sum == 0)
            {
                Subsets.Add(p);
                return;
            }

            if (i != 0 && solution[i - 1, sum])
            {
                List<int> b = p.GetRange(0, p.Count);
                SubsetsRec(i - 1, sum, b);
            }

            if (i != 0 && sum >= set[i] && solution[i - 1, sum - set[i]])
            {
                p.Add(set[i]);
                SubsetsRec(i - 1, sum - set[i], p);
            }
        }
    }
}
