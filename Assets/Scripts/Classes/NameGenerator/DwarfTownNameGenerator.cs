namespace Classes.NameGenerator
{
    public class DwarfTownNameGenerator : BaseNameGenerator
    {

	    public DwarfTownNameGenerator() : base("dwarf")
	    {
	    }
	    /// <summary>
	    /// Author: https://www.fantasynamegenerators.com/scripts/dwarfTowns.js
	    /// </summary>
	    /// <param name="include"></param>
	    /// <returns></returns>
        public override string Gen(string include = null)
        {
            string[] nm1 = { "B", "D", "Dh", "Bh", "G", "H", "K", "Kh", "M", "N", "Th", "V" };
			string[] nm2 = { "ag", "agh", "al", "am", "an", "ar", "arn", "eg", "egh", "el", "em", "en", "er", "ern", "ig", "igh", "il", "im", "in", "ir", "irn",
				"og", "ogh", "ol", "om", "on", "or", "orn", "ug", "ugh", "ul", "um", "un", "ur", "urn" };
			string[] nm3 = { " Badihr", " Badir", " Baduhr", " Badur", " Boldahr", " Boldar", " Boldihr", " Boldir", " Boldohr", " Boldor", " Boram", " Boramm",
				" Borim", " Borimm", " Buldahr", " Buldar", " Buldihr", " Buldohr", " Buldor", " Burim", " Burimm", " Darahl", " Daral", " Darihm", " Darim",
				" Darohm", " Darom", " Daruhl", " Daruhm", " Darul", " Darum", " Dorahl", " Doral", " Doruhl", " Dorul", " Durahl", " Dural", " Faldihr",
				" Faldir", " Falduhr", " Faldur", " Faruhm", " Farum", " Furuhm", " Furum", " Garohm", " Garom", " Garuhm", " Garum", " Gurihm", " Guruhm",
				" Gurum", " Kahldur", " Kalduhr", " Kohldur", " Kolduhr", " Kuldihr", " Kuldir", " Kuldohr", " Kuldor", " Laduhr", " Ladur", " Lodahr",
				" Lodar", " Lodihr", " Lodir", " Loduhr", " Lodur", " Maldir", " Malduhr", " Maldur", " Moldir", " Molduhr", " Moldur", " Olihm", " Oluhm",
				" Tarihr", " Taruhm", " Taruhr", " Tarum", " Tharim", " Tharum", " Thoram", " Thorim", " Thorum", " Thurim", " Thurum", " Todihr", " Todir",
				" Toduhr", " Todur", " Toruhm", " Torum", " Turuhm", " Turum", " Ulihm", " Uluhm", " Ulum", " Wahrum", " Wohrum", " Wuhrum", "ahm", "alduhr",
				"aldur", "am", "aruhm", "arum", "badihr", "badir", "baduhr", "badur", "bihr", "bohr", "boldahr", "boldar", "boldihr", "boldir", "boldohr",
				"boldor", "bor", "boram", "boramm", "borim", "borimm", "buhr", "buldahr", "buldar", "buldihr", "buldohr", "buldor", "bur", "burim", "burimm",
				"dahn", "dan", "darahl", "daral", "darihm", "darim", "darohm", "darom", "darth", "daruhl", "daruhm", "darul", "darum", "dihm", "dihr", "dim",
				"dirth", "dohr", "dor", "dorahl", "doral", "dorth", "doruhl", "dorul", "duahr", "duar", "duhm", "duhn", "duhr", "dum", "dun", "dur", "durahl",
				"dural", "eduhr", "edur", "elduhr", "eldur", "eruhm", "erum", "faldihr", "faldir", "falduhr", "faldur", "faruhm", "farum", "fuhn", "furuhm",
				"furum", "galir", "galor", "gan", "gari", "garohm", "garom", "garuhm", "garum", "golar", "golir", "gon", "gran", "grim", "grin", "grom", "gron",
				"grum", "grun", "gulir", "gulor", "gurihm", "guruhm", "gurum", "heim", "kahldur", "kahm", "kalduhr", "kihm", "kohldur", "kohm", "kolduhr",
				"kuhm", "kuldihr", "kuldir", "kuldohr", "kuldor", "laduhr", "ladur", "lodahr", "lodar", "lodihr", "lodir", "loduhr", "lodur", "olduhr", "oldur",
				"olihm", "oluhm", "oluhr", "olur", "ragh", "rahm", "ram", "rhia", "ria", "righ", "rihm", "rim", "rogh", "rugh", "ruhm", "rum", "tarihr",
				"taruhm", "taruhr", "tarum", "thiad", "thiod", "tihrm", "tirm", "todihr", "todir", "toduhr", "todur", "torhm", "torm", "toruhm", "torum",
				"tuhrm", "turm", "turuhm", "turum", "uhm", "ulihm", "ulihr", "ulir", "uluhm", "uluhr", "ulum", "ulur", "um", "wahr", "wahrum", "wihr", "wohr",
				"wohrum", "wuhr", "wuhrum", "yahr", "yar", "yaruhm", "yuhr", "yur" };

			return getRndA(nm1) + getRndA(nm2) + getRndA(nm3);
        }
    }
}