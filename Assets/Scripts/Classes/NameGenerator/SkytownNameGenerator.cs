using Tools;
using UnityEngine;

namespace Classes.NameGenerator
{
    public class SkyTownNameGenerator : BaseNameGenerator
    {

	    public SkyTownNameGenerator() : base("sky")
	    {
	    }
	    /// <summary>
	    /// Author: https://www.fantasynamegenerators.com/scripts/skyCities.js
	    /// </summary>
	    /// <param name="include"></param>
	    /// <returns></returns>
        public override string Gen(string include = null)
        {
	        string[] nm1 = { "", "", "", "", "", "b", "c", "d", "dh", "f", "g", "h", "l", "m", "n", "ph", "s", "sh", "th", "v", "w" };
			string[] nm2 = { "a", "e", "i", "o", "u", "a", "e", "o", "a", "e", "i", "o", "u", "a", "e", "o", "ea", "ae", "ia", "ai", "eo" };
			string[] nm3 = { "b", "b", "f", "f", "ff", "g", "g", "h", "h", "j", "j", "l", "l", "ll", "m", "m", "mm", "n", "n", "nn", "r", "r", "s", "s", "ss", "th",
				"th", "v", "v", "b", "bh", "bl", "bs", "br", "f", "ff", "fl", "fr", "g", "gh", "gn", "gl", "h", "hn", "hl", "hm", "j", "l", "lf", "ll", "lt",
				"lc", "lb", "ld", "lm", "ln", "lr", "lw", "m", "mm", "mn", "mr", "n", "nn", "ns", "nth", "nt", "nm", "nf", "nph", "pr", "phr", "r", "rl", "rm",
				"rn", "s", "sf", "sh", "sp", "st", "sw", "ss", "sn", "sm", "th", "v" };
			string[] nm4 = { "", "", "", "", "", "sh", "ph", "h", "l", "m", "n", "r", "s", "th" };

			string[] nm7 = { "Aer", "Aera", "Aere", "Aeri", "Air", "Ar", "Aro", "Atmo", "Avi", "Avia", "Avis", "Azu", "Brey", "Cele", "Celes", "Chi", "Chinoo",
				"Cir", "Circo", "Clo", "Clod", "Clou", "Cloud", "Cyclo", "Empear", "Exa", "Exalo", "Flur", "Gal", "Gale", "Hali", "Halo", "Huri", "Huric",
				"Impe", "Imper", "Mis", "Mur", "Oxy", "Ozo", "Sky", "Skye", "Son", "Sona", "Soni", "Stra", "Tempe", "Tempes", "Tro", "Tropo", "Tum", "Tumu",
				"Tumul", "Ven", "Venti", "Vol", "Vola", "Vox", "Xy", "Zeph", "Zephy" };
			string[] nm8 = { "polis", "more", "bay", "bell", "bury", "cairn", "call", "crest", "cross", "drift", "ham", "helm", "hold", "holde", "mere", "mire",
				"mond", "moor", "more", "rest", "run", "wich", "star", "storm", "strand", "summit", "tide", "wallow", "ward", "watch", "well" };

			string[] nm9 = { "Aera", "Aeranas", "Aeria", "Aeris", "Aeros", "Ara", "Aros", "Atmos", "Auris", "Aurora", "Avia", "Avian", "Avis", "Azur", "Azura",
				"Azuros", "Borealis", "Breyze", "Celes", "Cerul", "Cerulea", "Cerulis", "Cerulle", "Cerullis", "Chinook", "Circos", "Cirrus", "Clode",
				"Empearal", "Ether", "Ethis", "Ethos", "Exalos", "Flurris", "Gale", "Gayle", "Halitos", "Halitus", "Halo", "Halos", "Helios", "Horizon",
				"Huricus", "Imperos", "Mistral", "Mulus", "Murmus", "Nimbus", "Orion", "Oxyn", "Ozon", "Sonas", "Sonis", "Sono", "Sonus", "Spheris", "Spheros",
				"Stratos", "Tempeste", "Tempestus", "Tropos", "Tumul", "Tumulus", "Ventis", "Volance", "Volaris", "Vox", "Voxis", "Welkis", "Xygen", "Zephyr",
				"Zephys", "Zures", "Zuros" };
			string[] nm10 = { "New ", "Nova ", "", "", "", "", "", "", "" };

			string name = null;
			int i = Random.Range(0, 9);

			if (i < 2) {
				name = "The city of " + getRndA(nm1) + getRndA(nm2) + getRndA(nm3) + getRndA(nm2) + getRndA(nm4);
			} else if (i < 4) {
				name = "The city of " + getRndA(nm1) + getRndA(nm2) + getRndA(nm3) + getRndA(nm2) + getRndA(nm3) + getRndA(nm2) + getRndA(nm4);
			} else if (i < 7) {
				name = getRndA(nm7) + getRndA(nm8);
			} else {
				name = getRndA(nm10) + getRndA(nm9);
			}

			return TextHelper.Cap(name);
        }
    }
}