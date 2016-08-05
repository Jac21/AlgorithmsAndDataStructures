using System;
using System.Collections.Generic;
using System.Linq;

namespace Quicksort
{
    public class QSorter
    {
        // init empty enumerable list object
        private static IEnumerable<IComparable> _empty;

        public QSorter()
        {
            _empty = new List<IComparable>();
        }

        /// <summary>
        /// Recursive method that intakes enumerable, if it contains something, 
        /// set pivot to first elemtn, compare and sort
        /// </summary>
        /// <param name="iEnumerable"></param>
        /// <returns></returns>
        public IEnumerable<IComparable> QSort(IEnumerable<IComparable> iEnumerable)
        {
            if (iEnumerable.Any())
            {
                var pivot = iEnumerable.First();

                return QSort(iEnumerable.Where(anItem => pivot.CompareTo(anItem) > 0)).
                    Concat(iEnumerable.Where(anItem => pivot.CompareTo(anItem) == 0)).
                    Concat(QSort(iEnumerable.Where(anItem => pivot.CompareTo(anItem) < 0)));
            }

            return _empty;
        } 
    }
}
