using System.Collections.Generic;

namespace Players
{
    public class PlayerSpells
    {
        public Dictionary<string, bool> spells;

        public PlayerSpells()
        {
            spells = new Dictionary<string, bool>();
        }
    }
}