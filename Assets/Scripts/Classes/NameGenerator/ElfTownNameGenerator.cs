using Tools;
using UnityEngine;

namespace Classes.NameGenerator
{
    public class ElfTownNameGenerator : BaseNameGenerator
    {
	    /// <summary>
	    /// Author: https://www.fantasynamegenerators.com/scripts/steampunkCities.js
	    /// </summary>
	    /// <param name="include"></param>
	    /// <returns></returns>
        public override string Gen(string include = null)
        {
	        string[] nm1 = { "Aera", "Aero", "Aether", "Alder", "Arc", "Arca", "Ash", "Astro", "Automa", "Bacca", "Baro", "Beak", "Bel", "Bell", "Bene", "Bibbing",
				"Black", "Blag", "Bobbing", "Bol", "Bone", "Brass", "Broad", "Buckle", "Can", "Cant", "Caper", "Char", "Chaun", "Chisel", "Chro", "Chrono",
				"Cinder", "Cine", "Coal", "Cog", "Cokum", "Cooper", "Cove", "Cover", "Crank", "Crow", "Dapple", "Dark", "Dawn", "Deca", "Dillo", "Dipper",
				"Diri", "Dirigi", "Dobbin", "Drag", "Dread", "Dub", "Duc", "Duffer", "Dumplin", "Dusk", "Dyna", "Ebon", "Ember", "Ether", "Flam", "Flush",
				"Fogle", "Gaff", "Gallie", "Gammon", "Gatter", "Gear", "Gearing", "Gegor", "Giz", "Gizmo", "Glim", "Glimmer", "Glimming", "Glock", "Goggle",
				"Gouge", "Grap", "Graven", "Gray", "Grim", "Grime", "Grub", "Heat", "Heather", "Heli", "Hob", "Hobble", "Ichor", "Iron", "Ivor", "Ivory",
				"Jemmy", "Jugger", "Kanur", "Ken", "Kenning", "Kennuck", "Kife", "Kine", "Kino", "Knap", "Labo", "Lag", "Leaden", "Leg", "Lever", "Lill", "Lug",
				"Lugger", "Lushing", "Mag", "Meck", "Mecking", "Mel", "Mill", "Milling", "Min", "Mizzle", "Muffle", "Mumper", "Murk", "Nedding", "Nether",
				"Nobble", "Nom", "Nox", "Nubbik", "Obsidi", "Onyx", "Padding", "Pall", "Para", "Peri", "Pitch", "Plu", "Pneu", "Poly", "Pradding", "Prater",
				"Prong", "Rack", "Racket", "Rain", "Raven", "Reaming", "Reeb", "Rig", "Rip", "Riven", "Rook", "Rooker", "Rozzer", "Ruffle", "Scal", "Scran",
				"Scuttle", "Sere", "Shevi", "Skip", "Skipper", "Skipping", "Slate", "Sloe", "Slum", "Snell", "Snow", "Snoz", "Soot", "Speeler", "Spindle",
				"Steam", "Steel", "Swart", "Swelling", "Tatting", "Terra", "Tine", "Tinker", "Titfer", "Toff", "Toffing", "Tol", "Tooler", "Toper", "Topping",
				"Twirl", "Tyro", "Umber", "Van", "Velo", "Veloci", "Vex", "Voli", "Vox", "Wheal", "Whealing" };
			string[] nm2 = { "barrow", "borough", "bourne", "burg", "burgh", "burn", "bury", "cairn", "dale", "denn", "drift", "edge", "fall", "fell", "ford",
				"fort", "garde", "gate", "glen", "guard", "gue", "haben", "hagen", "hallow", "ham", "haven", "helm", "hold", "hollow", "mere", "mire", "moor",
				"more", "mourne", "point", "port", "rath", "stead", "stein", "storm", "sturm", "thain", "ton", "town", "vale", "wall", "wallow", "ward",
				"watch", "worth" };

			return getRndA(nm1) + getRndA(nm2);
        }
    }
}