using Tools;

namespace Classes.NameGenerator
{
    public class FantasyTownNameGenerator : BaseNameGenerator
    {

	    public FantasyTownNameGenerator() : base("fantasy")
	    {
	    }
	    /// <summary>
	    /// Author: https://www.fantasynamegenerators.com/scripts/fantasyTowns.js
	    /// </summary>
	    /// <param name="include"></param>
	    /// <returns></returns>
        public override string Gen(string include = null)
        {
            string[] nm1 = { "amber", "angel", "spirit", "basin", "lagoon", "basin", "arrow", "autumn", "bare", "bay", "beach", "bear", "bell", "black", "bleak",
				"blind", "bone", "boulder", "bridge", "brine", "brittle", "bronze", "castle", "cave", "chill", "clay", "clear", "cliff", "cloud", "cold",
				"crag", "crow", "crystal", "curse", "dark", "dawn", "dead", "deep", "deer", "demon", "dew", "dim", "dire", "dirt", "dog", "dragon", "dry",
				"dusk", "dust", "eagle", "earth", "east", "ebon", "edge", "elder", "ember", "ever", "fair", "fall", "false", "far", "fay", "fear", "flame",
				"flat", "frey", "frost", "ghost", "glimmer", "gloom", "gold", "grass", "gray", "green", "grim", "grime", "hazel", "heart", "high", "hollow",
				"honey", "hound", "ice", "iron", "kil", "knight", "lake", "last", "light", "lime", "little", "lost", "mad", "mage", "maple", "mid", "might",
				"mill", "mist", "moon", "moss", "mud", "mute", "myth", "never", "new", "night", "north", "oaken", "ocean", "old", "ox", "pearl", "pine", "pond",
				"pure", "quick", "rage", "raven", "red", "rime", "river", "rock", "rogue", "rose", "rust", "salt", "sand", "scorch", "shade", "shadow",
				"shimmer", "shroud", "silent", "silk", "silver", "sleek", "sleet", "sly", "small", "smooth", "snake", "snow", "south", "spring", "stag", "star",
				"steam", "steel", "steep", "still", "stone", "storm", "summer", "sun", "swamp", "swan", "swift", "thorn", "timber", "trade", "west", "whale",
				"whit", "white", "wild", "wilde", "wind", "winter", "wolf" };
			string[] nm2 = { "acre", "band", "barrow", "bay", "bell", "born", "borough", "bourne", "breach", "break", "brook", "burgh", "burn", "bury", "cairn",
				"call", "chill", "cliff", "coast", "crest", "cross", "dale", "denn", "drift", "fair", "fall", "falls", "fell", "field", "ford", "forest",
				"fort", "front", "frost", "garde", "gate", "glen", "grasp", "grave", "grove", "guard", "gulch", "gulf", "hall", "hallow", "ham", "hand",
				"harbor", "haven", "helm", "hill", "hold", "holde", "hollow", "horn", "host", "keep", "land", "light", "maw", "meadow", "mere", "mire", "mond",
				"moor", "more", "mount", "mouth", "pass", "peak", "point", "pond", "port", "post", "reach", "rest", "rock", "run", "scar", "shade", "shear",
				"shell", "shield", "shore", "shire", "side", "spell", "spire", "stall", "wich", "minster", "star", "storm", "strand", "summit", "tide", "town",
				"vale", "valley", "vault", "vein", "view", "ville", "wall", "wallow", "ward", "watch", "water", "well", "wharf", "wick", "wind", "wood",
				"yard" };

			string rnd = getRndA(nm1);
			string rnd2 = getRndA(nm2);
			while (rnd == rnd2) {
				rnd2 = getRndA(nm2);
			}
			return TextHelper.Cap(rnd + rnd2);
        }
    }
}