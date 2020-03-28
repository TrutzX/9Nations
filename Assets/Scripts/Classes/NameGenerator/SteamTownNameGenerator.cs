using Tools;
using UnityEngine;

namespace Classes.NameGenerator
{
    public class SteamTownNameGenerator : BaseNameGenerator
    {
	    /// <summary>
	    /// Author: https://www.fantasynamegenerators.com/scripts/orcTowns.js
	    /// </summary>
	    /// <param name="include"></param>
	    /// <returns></returns>
        public override string Gen(string include = null)
        {
	        string[] nm1 = { "", "", "", "", "", "b", "br", "bh", "ch", "d", "dr", "dh", "g", "gr", "gh", "k", "kr", "kh", "l", "m", "n", "q", "r", "v", "z", "vr",
		        "zr" };
	        string[] nm2 = { "a", "e", "i", "o", "u", "a", "o", "u" };
	        string[] nm3 = { "b", "cc", "d", "dd", "gg", "g", "r", "rr", "z", "zz", "b", "cc", "d", "dd", "gg", "g", "r", "rr", "z", "zz", "br", "cr", "dr", "dg",
		        "dz", "dgr", "dk", "gr", "gh", "gk", "gz", "gm", "gn", "gv", "lb", "lg", "lgr", "ldr", "lbr", "lk", "lz", "mm", "rg", "rm", "rdr", "rbr", "rd",
		        "rk", "rkr", "rgr", "rz", "shb", "shn", "zg", "zgr", "zd", "zr", "zdr" };
	        string[] nm4 = { "", "kh", "d", "dh", "g", "gh", "l", "n", "r", "rd", "z" };

	        string name = null;
	        int i = Random.Range(0, 9);

	        if (i < 2) {
		        name = getRndA(nm1) + getRndA(nm2) + nm4[Random.Range(0, 3) + 1] + "  " + getRndA(nm1) + getRndA(nm2) + getRndA(nm3) + getRndA(nm2)
		               + getRndA(nm4);
	        } else if (i < 6) {
		        name = getRndA(nm1) + getRndA(nm2) + getRndA(nm3) + getRndA(nm2) + getRndA(nm4);
	        } else if (i < 8) {
		        name = getRndA(nm1) + getRndA(nm2) + getRndA(nm3) + getRndA(nm2) + getRndA(nm3) + getRndA(nm2) + getRndA(nm4);
	        } else {
		        name = getRndA(nm1) + getRndA(nm2) + getRndA(nm3) + getRndA(nm2) + getRndA(nm4) + "  " + getRndA(nm1) + getRndA(nm2)
		               + nm4[Random.Range(0, 3) + 1];
	        }

	        return TextHelper.Cap(name);
        }
    }
}