using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class NRandom<T>
    {
        public static T Rand(List<T> list)
        {
            return list[Random.Range(0, list.Count-1)];
        }
    }
}