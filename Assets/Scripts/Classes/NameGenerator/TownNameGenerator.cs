using Libraries;
using UnityEngine;

namespace Classes.NameGenerator
{
    public class TownNameGenerator : BaseNameGenerator
    {
        /// <summary>
        /// Author: https://www.fantasynamegenerators.com/scripts/underwaterTowns.js
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public override string Gen(string include = null)
        {
            string[] t = new[] {"underwater","ghost","dwarf","elf","sky","orc","steam","fantasy","viking"};
            return LClass.s.nameGenerators[t[Random.Range(0, t.Length - 1)]].Gen(include);
        }
    }
}