using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Game.Services
{
    public static class Extensions
    {
        public static bool CheckForTicTacToeWin(this List<List<int>> collection) => collection.Any(row => row.All(x => x == 0) || row.All(x => x == 1));

        public static List<List<int>> TransformRowsIntoColumns(this List<List<int>> collection) => collection
            .SelectMany(inner => inner.Select((item, index) => new { item, index }))
            .GroupBy(i => i.index, i => i.item)
            .Select(g => g.ToList())
            .ToList();

        public static List<List<int>> GetDiagonalsMatrix(this List<List<int>> collection)
        {
            return new List<List<int>>
            {
                Enumerable.Range(0, collection[0].Count).Select(i => collection[i][i]).ToList(),
                Enumerable.Range(0, collection[0].Count).Select(i => collection[i][collection[0].Count-1-i]).ToList(),
            };
        }
    }
}
