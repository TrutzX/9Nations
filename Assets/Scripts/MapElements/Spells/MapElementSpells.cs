using System;
using System.Collections.Generic;
using Libraries;
using Tools;

namespace MapElements.Spells
{
    [Serializable]
    public class MapElementSpells
    {
        public int total;
        public Dictionary<string, int> count;
        public List<string> known;

        public MapElementSpells()
        {
            count = new Dictionary<string, int>();
            known = new List<string>();
            total = 0;
        }

        public void Cast(string spell)
        {
            total++;
            count[spell]++;
        }

        public void Learn(string spell)
        {
            known.Add(spell);
            count[spell] = 0;
            
        }

        public bool Know(string spell)
        {
            return known.Contains(spell);
        }

        public int CalcChance(string id)
        {
            var spell = L.b.spells[id];
            //calc base
            int start = 100 - total + spell.difficult;
            if (count.ContainsKey(id) && count[id] > 0)
            {
                start /= count[id];
            }

            return ConvertHelper.Between(100-start, 0, 100);
        }
    }
}