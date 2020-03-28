using UnityEngine;

namespace Classes.NameGenerator
{
    public abstract class BaseNameGenerator
    {
        public string id;
        
        public abstract string Gen(string include = null);

        /// <summary>
        /// Get an random element from this array
        /// </summary>
        /// <param name="ary"></param>
        /// <returns></returns>
        protected static string getRndA(string[] ary)
        {
            return ary[Random.Range(0,ary.Length-1)];
        }
    }
}
