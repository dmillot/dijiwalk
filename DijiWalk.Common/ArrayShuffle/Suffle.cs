using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DijiWalk.Common.ArrayShuffle
{
    public static class Suffle
    {

        public static List<List<int>> Shuffle(this List<int> list, int nbToGenerate)
        {
            var result = new List<List<int>>();
            var currentList = list;
            Random rand = new Random();
            for (int i = 0; i < nbToGenerate; i++)
            {
                var compare = result.Select(x => x.SequenceEqual(list.OrderBy(c => rand.Next()))).ToList();
                if(!compare.Any(x => x))
                {
                    result.Add(list.OrderBy(c => rand.Next()).ToList());
                }
            }

            return result;
        }
    }
}
