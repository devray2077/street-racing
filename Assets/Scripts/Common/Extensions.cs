
using System.Collections.Generic;
using UnityEngine;

namespace StreetRacing
{
    public static class Extensions
    {
        public static T GetRandom<T>(this IList<T> array)
        {
            if (array.Count == 0)
            {
                return default;
            }

            var randomIndex = Random.Range(0, array.Count);
            return array[randomIndex];
        }
    }
}
