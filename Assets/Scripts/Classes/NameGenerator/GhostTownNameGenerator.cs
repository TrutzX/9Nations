using Tools;

namespace Classes.NameGenerator
{
    public class GhostTownNameGenerator : BaseNameGenerator
    {
	    /// <summary>
	    /// Author: https://www.fantasynamegenerators.com/scripts/ghostTownNames.js
	    /// </summary>
	    /// <param name="include"></param>
	    /// <returns></returns>
        public override string Gen(string include = null)
        {
	        string[] nm1 = { "ail", "alder", "amber", "arach", "ash", "ashen", "bane", "bare", "barren", "bitter", "black", "bleak", "bligh", "blight", "boon",
		        "brow", "burn", "cease", "char", "charring", "ebon", "onyx", "cinder", "clear", "cold", "crag", "cri", "crow", "dark", "dawn", "death", "deci",
		        "demo", "dew", "dia", "diabo", "dire", "dread", "dusk", "dust", "ember", "fall", "fallen", "far", "farrow", "fes", "fester", "fire", "flame",
		        "flaw", "fog", "fore", "forge", "forging", "frost", "full", "gaze", "ghos", "gloom", "glum", "glumming", "gore", "gray", "grim", "grimming",
		        "hard", "hazel", "il", "ill", "kil", "lo", "lon", "lone", "low", "mad", "mali", "mar", "mause", "maw", "mise", "mourn", "mourning", "mur",
		        "murk", "nec", "necro", "nether", "ni", "nigh", "night", "pyre", "reaper", "reaver", "ridge", "rip", "ripping", "saur", "scorch", "ser",
		        "serpen", "shadow", "shar", "shard", "shel", "shell", "sla", "slate", "sly", "spi", "spine", "talon", "thorn", "thorne", "vile", "vin", "vine",
		        "wear", "weep", "weeping", "wither", "woe", "wrath" };
	        string[] nm2 = { "borough", "brook", "brooke", "burg", "burgh", "burn", "bury", "fall", "ford", "fort", "gate", "helm", "mere", "mire", "moor", "more",
		        "moure", "mourn", "rest", "ridge", "thorn", "thorne", "ton", "town", "ville" };

	        string rnd = getRndA(nm1);
	        string rnd2 = getRndA(nm2);
	        while (rnd == rnd2) {
		        rnd2 = getRndA(nm2);
	        }
	        return TextHelper.Cap(rnd + rnd2);
        }
    }
}