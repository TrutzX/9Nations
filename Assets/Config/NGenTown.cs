using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author sven
 *
 */
public class NGenTown {

	private NGenTown() {}

	public static string GetTownName(string type) {
		switch (type) {
		case "underwater":
			return underwaterTown();
		case "ghost":
			return ghostTown();
		case "dwarf":
			return dwarfTown();
		case "elf":
			return elfTown();
		case "sky":
			return skyTown();
		case "orc":
			return orcTown();
		case "steam":
			return steamTown();
		case "north":
			return fantasyTown();
		case "viking":
			return vikingTown();
		default:
			Debug.Log($"town type {type} unknown");
			return fantasyTown();
		}
	}

	/**
	 * Get an random element from this array
	 * 
	 * @param ary
	 * @return
	 */
	private static string getRndA(string[] ary)
	{
		return ary[Random.Range(0,ary.Length-1)];
	}

	/**
	 * @author https://www.fantasynamegenerators.com/scripts/underwaterTowns.js
	 * @return
	 */
	private static string underwaterTown() {
		string[] nm1 = { "Aby", "Abys", "Ache", "Acio", "Aeg", "Amphi", "Anu", "Aqu", "Aqua", "Aqui", "Asha", "Ashe", "Atla", "Azha", "Azu", "Beli", "Bery",
				"Boy", "Bri", "Cae", "Caenu", "Cala", "Cata", "Cla", "Coa", "Coara", "Cora", "Delph", "Do", "Ebi", "Expa", "Flu", "Gey", "Gla", "Glaci",
				"Hippo", "Hy", "Hyd", "Jutu", "Levi", "Levia", "Limu", "Liqi", "Liqu", "Liqua", "Liqui", "Litto", "Mari", "Mer", "Mimi", "Nata", "Nau", "Nauti",
				"Nava", "Nep", "Neph", "Nept", "Neptu", "Nerei", "Neri", "Njo", "Njor", "Oce", "Ocea", "Osi", "Paci", "Palae", "Pela", "Pose", "Posei", "Pura",
				"Puri", "Rive", "Sala", "Sali", "Saph", "Saphi", "Scy", "Sequa", "Si", "Sire", "Squa", "Te", "Tempe", "Teth", "Tha", "Thala", "Thau", "The",
				"Tri", "Trite", "Trito", "Tsu", "Tsuna", "Ty", "Typh", "Va", "Vapo", "Voltu", "Wata" };
		string[] nm2 = { "cada", "cadis", "cia", "cique", "cis", "dor", "dore", "gia", "lean", "lin", "lina", "lis", "loch", "lona", "lor", "lora", "lore",
				"lune", "mari", "mon", "mond", "na", "nas", "ne", "nea", "nia", "nis", "noch", "pis", "ra", "rai", "ran", "rei", "rem", "ren", "reth", "rey",
				"ri", "ria", "ril", "rin", "ris", "rius", "rus", "sa", "tas", "tesh", "thas", "theas", "this", "thys", "tia", "tin", "tis", "ton", "tria",
				"via" };

		return getRndA(nm1) + getRndA(nm2);
	}

	/**
	 * @author https://www.fantasynamegenerators.com/scripts/underwaterTowns.js
	 * @return
	 */
	private static string vikingTown() {
		string[] nm1 = { "Aoalvik", "Afvaldsnes", "Agoanes", "Agoir", "Akrafjall", "Akranes", "Akrar", "Alfgeirsvellir", "Alfsnes", "Alfsoss", "Almannafljot",
				"Almannagja", "Almdalir", "Almenningar", "Alpta", "Alptafjror", "Alptanes", "Alptaver", "Alviora", "Alost", "Ambattara", "Anabrekka",
				"Andakilsa", "Andarkelda", "Ar", "Arastaoir", "Arnallsstaoir", "Arnarboeli", "Arnarfell", "Arnarfjoror", "Arnarholt", "Arnarhvall", "Arnarnes",
				"Arnarthufa", "Arnbjargarloekr", "Arneioarstaoir", "Arnlaugsfjoror", "Asar", "Asbjarnarnes", "Asbjarnarstaoir", "Asbjarnarvik", "Asgautsstaoir",
				"Asgeirsa", "Ashildarmyrr", "Askelshofoi", "Asmundarleioi", "Asolfsleioi", "Asolfsskali", "Atlahaugr", "Atley", "Auoartoptir", "Auobrekka",
				"Auokulustaoir", "Auonar", "Auoolfsstaoir", "Auosstaoir", "Auounarstaoir", "Augastaoir", "Aurrioaa", "Aurrioaaross", "Austfiroingafjoroungr",
				"Austfiroir", "Austrvegr", "Axlarhagi", "Bakkarholt", "Balkastaoir", "Ballara", "Baro", "Baroardalr", "Baroargata", "Baroastrond", "Barosnes",
				"Barosvik", "Baugsstaoir", "Beigaoarhvall", "Beigaldi", "Bekansstaoir", "Belgsdalr", "Belgsstaoir", "Berg", "Berghlioir", "Berghylr",
				"Bergthorshvall", "Berjadalsa", "Bersastaoir", "Berserkseyrr", "Boroeyrr", "Berufell", "Berufjoror", "Beruvik", "Beruvikrhraun", "Bildsa",
				"Bildsfell", "Biskupstunga", "Bitra", "Bitrufjoror", "Bjallabrekka", "Bjargaoss", "Bragaoss", "Bjarkey", "Bjarmaland", "Bjarnardalr",
				"Bjarnarfjoror", "Bjarnarhofn", "Bjarnarnes", "Bjarnastaoir", "Bjarneyjafloi", "Bjorgyn", "Blanda", "Blonduoss", "Blaserkr", "Blaskeggsa",
				"Blaskogsa", "Blaskogar", "Blesastaoir", "Blundsvatn", "Blondudalr", "Blonduhlio", "Blondukvislir", "Bolstaoara", "Bolstaor", "Bolungavik",
				"Boroeyrr", "Borg", "Borgardalr", "Borgarfjoror", "Borgarholt", "Borgarhraun", "Borgarhofn", "Borgarloekr", "Borgarsandr", "Borgund",
				"Botarskaro", "Botarskal", "Botn", "Botnsa", "Bragaoss", "Bramsloekr", "Brattahlio", "Brattsholt", "Brautarholt", "Bravollr", "Breioa",
				"Breioabolstaor", "Breioafjoror", "Breioamyrr", "Breioarsandr", "Breioavao", "Breioavik", "Breiodalr", "Brekka", "Fagrabrekka", "Brekkur",
				"Sumarlioaboer", "Brenna", "Brenningr", "Brimilsvellir", "Brimnesskogar", "Brjansloekr", "Bramsloekr", "Brokey", "Brunahaugr", "Brunastaoir",
				"Brynjudalr", "Brynjudalsa", "Buoardalr", "Buoardalsa", "Buland", "Bulandshofoi", "Bulandsnes", "Burfell", "Byrgisvik", "Baegisa", "Boejarsker",
				"Boer", "Boomooshorn", "Boomooshraun", "Boomoostunga", "Booolfs skytja", "Boovarsdalr", "Boovarsholar", "Boovarsholt", "Boovarstoptir",
				"Dalalond", "Dalir", "Dalsfjoror", "Dalsmynni", "Deildara ", "Deildarey", "Deildargil", "Deildarhjalli", "Digranes", "Dimunarvagr", "Djupa",
				"Djupadalr", "Djupadalslond", "Djupafjoror", "Dofrar", "Drangaland", "Drangar", "Drangavik", "Drapuhlio", "Drifandi", "Dritsker",
				"Drumboddsstaoir", "Dufansdalr", "Dufunefsskeio", "Dufthaksholt", "Dufthaksskor", "Dumbshaf", "Dyflinn", "Dyflinnarskiri", "Dynskogar",
				"Dyrafjoror", "Doguroara", "Doguroarnes", "Egilsstaoir", "Eio", "Eilifsdalsa", "Einarsfjoror", "Einarshofn", "Einarssker", "Einarsstaoir",
				"Einhyrningsmork", "Eiriksey", "Eiriksfjoror", "Eiriksholmar", "Eiriksstaoir", "Eiriksvagr", "Eldgrimsstaoir", "Eldueio", "Ellioaaross",
				"Ellioaey", "Enga", "Engihlio", "Enni", "Esjuberg", "Eskifjoror", "Eskiholt", "Espiholl inn syori", "Eyjafjaroara",
				"Eyjafjaroarstrond in eystri", "Eyjafjoll", "Eyjafjoror", "Eyjara", "Eyjardalsa", "Eyjasandr", "Eyjasund", "Eyjasveit", "Landeyjar", "Eyrar",
				"Eyrarbakki", "Eyrarfell", "Eyrarland", "Eyrr", "Hrafnseyri", "Flateyri", "Narfeyri", "Ondveroareyri", "Eystribyggo", "Eystridalr", "Eyvindara",
				"Eyvindardalr", "Eyvindarfjoror", "Eyvindarholar", "Eyvindarmuli", "Fabeinsa", "Fagrabrekka", "Fagradalr", "Fagradalsa", "Fagradalsaross",
				"Fagraskogr", "Fagravik", "Faskruosfjoror", "Faxaoss", "Feldarholmr", "Fellshverfi", "Fellsmuli", "Fellsskogar", "Ferstikla", "Fiflavellir",
				"Finnafjoror", "Finnmork", "Firoafylki", "Firoir", "Fiska", "Fiskiloekr", "Fitjar", "Fjalafylki", "Fjalir", "Fjall", "Alpafjoll", "Fjaroara",
				"Fjaora", "Fjarora", "Fjaroarhorn", "Fjoll", "Eyjafjoll", "Flangastaoir", "Fjoll", "Eyjafjoll", "Flangastaoir", "Flatatunga", "Flateyjardalr",
				"Fljot", "Markarfljot", "Skjalfandafljot", "Fljotsa", "Fljotsdalr", "Fljotshlio", "Fljotshverfi", "Floi", "Flokadalr", "Flokadalsa",
				"Flokavaroi", "Flugumyrr", "Fnjoskadalr", "Fnjoskadalsa", "Fnjoska", "Folafotr", "Forsa", "Forsfjoror", "Forsvollr", "Forsoeludalr", "Framnes",
				"Frioleifsdalr", "Frioleifsdalsa", "Friomundara", "Frooa", "Frooaross", "Fulaloekr", "Fyllarloekr", "Fura", "Faereyjar", "Galmastrond",
				"Galmansstrond", "Galmarstrond", "Gamlastrond", "Galtarhamarr", "Garoar", "Jorundarholt", "Garoarsholmr", "Garpsdalr", "Gata", "Gaular",
				"Gaulverjaboer", "Gautland", "Gautsdalr", "Geiradalr", "Geirastaoir", "Geirhildarvatn", "Geirland", "Geirlandsa", "Geirmundarstaoir",
				"Saemundarstaoir", "Geirmundarvagr", "Geirolfsgnupr", "Geirsa", "Geirshlio", "Geirsholmr", "Geirvor", "Geirthjofsfjoror", "Geitland", "Gerpir",
				"Gil", "Gila", "Gilhagi", "Gilja", "Gilsa", "Gilsbakki", "Gilsfjoror", "Gislavotn", "Gislavatn", "Glaumsteinn", "Glera nyrori", "Glera syori",
				"Gljufra", "Glooafeykisa", "Gnupa", "Gnupar", "Gnupr", "Gnupi", "Gnupudalr", "Gnupufell", "Gnupverjahreppr", "Goodalir", "Guodalir",
				"Gotaloekr", "Grenitresnes", "Grenivik", "Grenjaoarstaoir", "Grenmarr", "Grimsa", "Grimsaross", "Grimsdalr", "Grimsey", "Grimsgil", "Grimsnes",
				"Grimulfsvotn", "Grindaloekr", "Grindavik", "Grindill", "Grillir", "Grindr", "Grisartunga", "Grjota", "Grjotvallarmuli", "Grones", "Groustaoir",
				"Grund", "Grunnafjoror", "Groenavatn", "Groeningr", "Grof", "Guobrandsstaoir", "Guodalir", "Guolaugshofoi", "Gufua", "Gufa", "Gufaross",
				"Gufudalr", "Gufufjoror", "Gufunes", "Gufuskalar", "Gullberastaoir", "Gunnarsholt", "Gunnbjarnarsker ", "Gunnlaugsstaoir", "Gunnolfsa",
				"Gunnolfsfell", "Gunnolfsvik", "Gunnsteinar", "Gunnsteinsstaoir", "Gygjarsporsa", "Gyldarhagi", "Galdrahagi", "Gonguskaro", "Gonguskarosa",
				"Gonguskarosaross", "Haddingjadalr", "Hafgrimsfjoror", "Hafnarfjall", "Hafnarfjoll", "Hafnarfjoror", "Hafnarlond", "Hafnaross ", "Hafr",
				"Hafrafell", "Hafragil", "Hafranes", "Hafrsa", "Hafrsfjoror", "Hafsbotn", "Hafsloekr", "Hagagaror", "Haganes", "Hagbarosholmr", "Hagi",
				"Fornhagi", "Hakaskaro", "Hakonarhella", "Hakonarstaoir", "Hallarmuli", "Halland", "Hallbjarnarvorour", "Hallgeirsey", "Hallkelsholar",
				"Hallkelsstaoir", "Hallsteinsnes", "Halogaland", "Hals", "Halsaland", "Halsar", "Halsgrof", "Hamarr", "Hamarsa", "Hamrar", "Hamundarstaoir",
				"Hanatun", "Hareksstaoir", "Hasteinssund", "Haugar", "Haugavao", "Haugr", "Haukadalr", "Haukagil", "Hauksgrafir", "Hauksstaoir", "Havararlon",
				"Heoinshofoi", "Hefn", "Hegranes", "Heggsgeroismuli", "Heioaboer", "Heior", "Heinabergsa", "Heinabergsar", "Helgafell", "Helgafjall",
				"Helgahraun", "Helgahvall", "Helgasker", "Helgastaoir", "Helgavatn", "Helgavatni", "Hellisdalr", "Hellisfitjar", "Hellisfjoror", "Hellishraun",
				"Hengiforsa", "Herfuroa", "Hergilsey", "Herjolfsdalr", "Herjolfsfjoror", "Herjolfshofn", "Herjolfsnes", "Hernar", "Hestfjoror", "Hestr",
				"Heynes", "Hildisey", "Hisargafl", "Hita", "Hitara", "Hitardalr", "Hjallaland", "Hjallanes", "Hjalli", "Hjaltadalr", "Hjaltaeyrr",
				"Hjaltdaelalaut", "Hjaltland", "Hjaroarholt", "Hjaroarnes", "Herdisarnes", "Hjaroarvatn", "Hjorleifshofoi", "Hlaohamarr", "Hleiorargaror",
				"Hlio", "Jokulsarhlio", "Hlioir", "Hlioum", "Hlioarendi", "Hlioarenda", "Hlioarlond", "Hlymrek", "Hloouvik", "Hnjoska", "Hnjoskadalsa",
				"Hnjoskadalr", "Hofgaroar", "Hofsa", "Hofsfell", "Hofsland", "Hofslond", "Hofstaoir", "Hofsteigr", "Hofsvagr", "Holar", "Hreppholar",
				"Klaustrholar", "Vestrhopsholar", "Krumsholar", "Holl", "Holmgaror", "Holmkelsa", "Holmr", "Holmsa", "Holmslatr", "Holm", "Holmslond",
				"Holmlond", "Holt", "Eyjafjollum", "Holtastaoir", "Horn it eystra", "Horn it vestra", "Hornafjaroarstrond", "Hornafjoror", "Hornstrandir",
				"Hrafnagil", "Hrafnagja", "Hrafnista", "Hrafnkelsdalr", "Hrafnsfjoror", "Hrafnstoptir", "Hranafall", "Hranastaoir", "Hraun", "Berserkjahraun",
				"Hraunaheior", "Hraunadalr", "Hraungeroi", "Hraungeroingahreppr", "Hraunhafnara", "Hraunhofn", "Hraunsass", "Hraunsfjoror", "Hraunsholtsloekr",
				"Hraunsloekr", "Hreouvatn", "Hreggsgeroismuli", "Heggsgeroismuli", "Hreioarsgeroi", "Hringariki", "Hringdalir", "Hringsstaoir", "Hrip",
				"Hrisar", "Hrisateigr", "Hrisey", "Hroarsholt", "Hroarsloekr", "Hroarsholtsloekr", "Hrokkelsstaoir", "Hroksholt", "Hrolleifsdalr",
				"Hrolleifsdalsa", "Hrunamannahreppr", "Hrutafjaroardalr", "Hrutafjaroarstrond in eystri", "Hrutafjoror", "Hrutsstaoir", "Hraereksgil",
				"Hunavatn", "Hunavatnsthing", "Hundadalr", "Hundsnes", "Husagaror", "Husavik", "Husnar", "Hvaleyrr", "Hvalfjaroarstrond", "Hvalfjoror", "Hvall",
				"Storolfshvall", "Hvallatr", "Hvalsey", "Hvalseyjar", "Hvalsnesskriour", "Hvalvatnsfjoror", "Hvammr", "Hvanna", "Hvanndalir", "Hvanneyrr",
				"Hvarf", "Hvarfsgnipa", "Hvassahraun", "Hvatastaoir", "Hvinir", "Hvinisfjoror", "Hvinverjadalr", "Hvita", "Hvitarsioa", "Hvitbjorg",
				"Hvitramannaland", "Haell", "Hofoabrekka", "Hofoalond", "Hofoarsandr", "Hofoastrond", "Hofoi", "Hjorleifshofoi", "Bulandshofoi", "Hofn",
				"Hognastaoir", "Hokustaoir", "Horoadalr", "Horoadalsa", "Horoaholar", "Horoaland", "Horga", "Horgardalr", "Horgardalsa", "Horgsholt",
				"Hoskuldsar", "Hoskuldsloekr", "Hoskuldsstaoir", "Hoskuldsvatn", "Iafjoror", "Ingimundarholt", "Ingjaldsgnupr", "Ingjaldshvall",
				"Ingjaldssandr", "Ingolfsfell", "Ingolfsfjoror", "Ingolfshofoahverfi", "Ingolfshofoi", "Ira", "Iara", "Isafjaroardjup", "Isafjoror",
				"Islandshaf", "Isleifsstaoir", "Isrooarstaoir", "Jaoarr", "Jafnaskaro", "Jannaskaro", "Jamtaland", "Jarofallsgil", "Jarolangsstaoir",
				"Jolgeirsstaoir", "Jorunnarstaoir", "Jokulsa", "Breioamerkursandi", "Solheimasandi", "Fjollum", "Jokulsdalr", "Jokulsfell", "Jokulsfiroir",
				"Jokulvikra", "Jokulsvikra", "Solheimasandi", "Jokulsa", "Jolduhlaup", "Joldusteinn", "Jorundarfell", "Jorundarholt", "Josureio", "Kalda",
				"Kaldaross", "Kaldakinn", "Kaldaklofsa", "Kaldakvisl", "Tungufljot", "Kaldbakr", "Kaldbaksvik", "Kalfa", "Kalfagrafir", "Kalfborgara",
				"Kolborgara", "Kalfskinn", "Kallnesingahreppr", "Kalmansa", "Kalmarsa", "Kalmanstunga", "Kambakista", "Kambr", "Kambsdalr", "Kambsnes",
				"Kampaholt", "Karlafjoror", "Karlastaoir", "Karlsa", "Karlsbrekka", "Hromundarstaoir", "Karlsdalr", "Karlsfell", "Karnsa", "Karnsarland",
				"Karsstaoir", "Katanes", "Keflavik", "Keldudalr", "Keldugnupr", "Kelduhverfi", "Keldunes", "Kerlingara", "Kerlingarfjoror", "Kerseyrr",
				"Kjorseyrr", "Ketilseyrr", "Ketilsfjoror", "Ketilsstaoir", "Kiojaberg", "Kiojafell", "Kiojaklettr", "Kiojaleit", "Kirkjubolstaor", "Kirkjuboer",
				"Kirkjufell", "Kirkjufjoror", "Kirkjusandr", "Kjalarnes", "Kjalkafjoror", "Kjallaksholl", "Kjallaksstaoir", "Kjaransvik", "Kjarra",
				"Kjarradalr", "Kjos", "Kjolr", "Kjolvararstaoir", "Kleifar", "Kleifarlond", "Klofasteinar", "Klofningar", "Knafaholar", "Knappadalr",
				"Knappsstaoir", "Knarrarnes", "Knefilsdalsa", "Kolbeinsaross", "Kolbeinsdalr", "Kolbeinsey", "Kolbeinsstaoir", "Kolbeinsvik", "Kolgrafafjoror",
				"Kolgrafir", "Kolkumyrar", "Kollafjaroarheior", "Kollafjoror", "Kollavik", "Kollshamarr", "Kollshamrar", "Kollsloekr", "Kollsveinsstaoir",
				"Kollsvik", "Kolssonafell", "Kopanes", "Koranes", "Kraunaheior", "Krokr", "Kroksdalr", "Kroksfjaroarmuli", "Kroksfjaroarnes", "Kroksfjoror",
				"Kroppr", "Krossa", "Krossass", "Krossavik", "Krossholar", "Krysuvik", "Krisuvik", "Kraeklingahlio", "Krofluhellir", "Kuoafljot",
				"Kvernvagastrond", "Kvia", "Kviabekkr", "Kvigandafjoror", "Kvigandisfjoror", "Kvigandanes", "Kviguvagabjorg", "Kylansholar", "Kylansholmar",
				"Lagarfljot", "Lagarfljotsstrandir", "Lagey", "Lambafellsa", "Lambastaoir", "Landamot", "Landbrot", "Langa", "Langadalr", "Langadalsa",
				"Langaholt", "Langanes", "Langavatnsdalr", "Laugar", "Laugarbrekka", "Laugardalr", "Laxa", "Laxardalr", "Leioolfsfell", "Leioolfsstaoir",
				"Leikskalar", "Leira", "Leirhofn", "Leiruloekr", "Leiruvagr", "Lioandisnes", "Linakradalr", "Ljosavatn", "Ljosavatnsskaro", "Ljotarstaoir",
				"Ljotolfsstaoir", "Loomundarfjoror", "Loomundarhvammr", "Lofot", "Lomagnupslond", "Lon", "Skipalon", "Lonland", "Lonlond", "Lonsheior",
				"Lunansholt", "Lundar", "Lundarbrekka", "Lundr", "Lysa", "Loekjarbotnar", "Longuhlio", "Mana", "Manafell", "Manarfell", "Manavik", "Manathufa",
				"Mannafallsbrekka", "Mannafallsbrekkur", "Marboeli", "Hanatun", "Markarfljot", "Masstaoir", "Mavahlio", "Meoalfarssund", "Meoalfell",
				"Meoalfellsstrond", "Fellsstrond", "Meoallond", "Melahverfi", "Melar", "Melrakkadalr", "Melrakkanes", "Merkigil", "Merkrhraun", "Merrhaefi",
				"Mioengi", "Miofell", "Miofjoror", "Miohus", "Miojokull", "Mioskali", "Migandi", "Mikilsstaoir", "Miklagaror", "Miklagil", "Minthakseyrr",
				"Mjosyndi", "Mjovadalr", "Mjovadalsa", "Mjovafjoror", "Mjola", "Mjors", "Moberg", "Mobergsbrekkur", "Mooolfsgnupr", "Moeioarhvall", "Mogilsa",
				"Mogilsloekr", "Moldatun", "Mor", "Mosfell", "Mostr", "Mulafell", "Muli", "Munaoarnes", "Mydalr", "Mydalsa", "Myrar", "Myrka", "Myrr", "Myvatn",
				"Maelifell", "Maelifellsa", "Maelifellsdalr", "Maelifellsgil", "Maerin", "Moorufell", "Mooruvellir", "Mork", "Narfasker", "Nattfaravik",
				"Naumdaelafylki", "Naumudalr", "Nautabu", "Neshraun", "Njaroey", "Njarovik", "Norofjoror", "Norolendingafjoroungr", "Noromoerr", "Norora",
				"Nororardalr", "Nororlond", "Norotunga", "Norvegr", "Nykomi", "Oddgeirsholar", "Oddi", "Oddsass", "Odeila", "Ofeigsfjoror", "Ofeigsstaoir",
				"Ofrustaoir", "Ofoera", "Olafsdalr", "Olafsfjoror", "Olafsvellir", "OlafsvikSnaef", "Oleifsborg", "Oleifsbjorg", "Orkneyjar", "Ormsa",
				"Ormarsa", "Ormsdalr", "Ormsstaoir", "Orrastaoir", "Orrostudalr", "Osar", "Unaoss", "Osfjoll", "Osomi", "Oss", "Osta", "Osvifsloekr", "Papey",
				"Papyli", "Patreksfjoror", "Pettlandsfjoror", "Ranga", "Rangaoarvaroa", "Rangaross", "Rangarvellir", "Raptaloekr", "Rauoa",
				"Rauoabjarnarstaoir", "Rauoafell it eystra", "Rauoagnupr", "Rauoaloekr", "Rauoamelr inn ytri", "Rauoamelslond", "Rauoasandr", "Rauoaskrioa",
				"Rauoaskrioulond", "Rauoaskriour", "Rauokollsstaoir", "Rauosgil", "Raufarfell it eystra", "Raufarnes", "Raumsdalr", "Raumsdaelafylki",
				"Refsstaoir", "Reistara", "Reistargnupr", "Reyoarfell", "Reyoarfjall", "Reyoarfjoror", "Reyoarmuli", "Reyoarvatn", "Reykir inir efri",
				"Kopareykir", "Reykjaa", "Reykjadalr", "Reykjadalsa", "Reykjahlio", "Reykjaholar", "Reykjaholt", "Reykjanes", "Reykjarfjoror", "Reykjarholl",
				"Reykjavellir ", "Reynir", "Reynisnes", "Reyrvollr", "Rooreksgil", "Rogaland", "Rosmhvalanes", "Rykinsvik", "Rytagnupr", "Rond", "Salteyraross",
				"Sanda", "Sandbrekka", "Sanddalr", "Sandeyrara", "Sandfell", "Sandgil", "Sandholaferja", "Sandloekr", "Sandnes", "Sandvik", "Sauoa",
				"Sauoadalr", "Sauoafellslond", "Sauoanes", "Saurbaer", "Saxahvall", "Selaeyrr", "Selalon", "Selardalr", "Selasund", "Selfors", "Seljalandsa",
				"Seljalandsmuli", "Seljasund", "Selsloekr", "Seltjarnarnes", "Selvagr", "Seyoarfjoror", "Seyoisfjoror", "Sioa", "Siglufjofor", "Siglunes",
				"Sigluvik", "Sigmundarakr", "Sigmundarnes", "Sigmundarstaoir", "Signyjarbruor", "Signyjarbuoir", "Signyjarstaoir", "Silfrastaoahlio",
				"Sireksstaoir", "Sjoland", "Sjonafjoror", "Skagafjoror", "Skagastrond", "Skagi", "Skal", "Skalabrekka", "Skalafell", "Skalaholt", "Skalamyrr",
				"Skalanes", "Skalavik", "Skaldskelmisdalr", "Skali", "Skallanes", "Skalmarkelda", "Skalmarnes", "Skaney", "Skapta", "Skaptafellsthing",
				"Skaptaholt", "Skaro", "Svignaskaro", "Skarosbrekka", "Skarfanes", "Skarfsnes", "Skeggjastaoir", "Skeio", "Skeiosbrekkur", "Skeljabrekka",
				"Skioadalr", "Skioastaoir", "Skjaldabjarnarvik", "Skjaldey", "Skjalfandafljotsoss", "Skjalfandi", "Skjalgdalsa", "Skjoldolfsnes",
				"Skjoldolfsstaoir", "Skogahverfi", "Skogar inir eystri", "Skogarstrond", "Skorradalr", "Skorraey", "Skorraholt", "Skorravik", "Skramuhlaupsa",
				"Skrattafell", "Skraumuhlaupsa", "Skrionisenni", "Skrioinsenni", "Skrioudalr", "Skruoey", "Skruor", "Skufsloekr", "Skuggabjorg", "Skulastaoir",
				"Skutilsfjoror", "Skoro", "Skotufjoror" };

		return getRndA(nm1);
	}

	/**
	 * @author https://www.fantasynamegenerators.com/scripts/ghostTownNames.js
	 * @return
	 */
	private static string fantasyTown() {

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
		return TextHelper.cap(rnd + rnd2);

	}

	/**
	 * @author https://www.fantasynamegenerators.com/scripts/ghostTownNames.js
	 * @return
	 */
	private static string ghostTown() {

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
		return TextHelper.cap(rnd + rnd2);

	}

	/**
	 * https://www.fantasynamegenerators.com/scripts/dwarfTowns.js
	 * 
	 * @return
	 */
	private static string dwarfTown() {
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

	/**
	 * https://www.fantasynamegenerators.com/scripts/elfTowns.js
	 * 
	 * @return
	 */
	private static string elfTown() {
		string[] nm1 = { "A", "A'", "Af", "Al", "All", "Aly", "Am", "Amy", "An", "Any", "Ar", "As", "Ash", "Asy", "Ay", "Ca", "Cy", "E", "E'", "Ef", "El",
				"Ell", "Ely", "Em", "Emy", "En", "Eny", "Er", "Es", "Esh", "Esy", "Ey", "F", "Fa", "Fel", "Fil", "Fy", "Fyl", "Ga", "Gal", "Ha", "He", "Hy",
				"I", "If", "Il", "Ill", "Ily", "Im", "Imy", "In", "Iny", "Ir", "Is", "Ish", "Isy", "Iy", "Ja", "Ji", "K", "Ka", "Ke", "Ky", "L", "La", "Le",
				"Lel", "Lil", "Ly", "Lyl", "M", "Ma", "Math", "Me", "Mel", "Mil", "Mor", "My", "Myl", "Myt", "Myth", "N", "Na", "Ne", "Nel", "Nil", "Ny", "Nyl",
				"Nyt", "Nyth", "O", "O'", "Of", "Ol", "Oll", "Oly", "Om", "Omy", "On", "Ony", "Or", "Os", "Osh", "Osy", "Oy", "Ra", "Re", "Ry", "S", "Sa",
				"Sel", "Sh", "Sha", "She", "Shyl", "Sil", "Sy", "Syl", "Th", "Tha", "The", "Thel", "Thil", "Thy", "Thyl", "U", "Ul", "Ull", "Uly", "Um", "Umy",
				"Un", "Uny", "Uy", "W", "Wa", "We", "Y", "Y'", "Ya", "Ye", "Yl", "Yll" };
		string[] nm2 = { "al", "el", "en", "an", "ana", "ena", "aena", "a", "i", "ren", "ran", "eth", "ath", "a", "e", "o", "h", "ha", "he", "ho", "f", "fa",
				"fe", "l", "le", "la", "m", "me", "ma", "ne", "na", "n", "s", "sa", "se", "ve", "va" };
		string[] nm3 = { " Aethel", " Aiqua", " Alari", " Alora", " Ancalen", " Anore", " Asari", " Dorei", " Dorthore", " Edhil", " Esari", " Lenora",
				" Serin", " Serine", " Shaeras", " Taesi", " Thalas", " Thalor", " Thalore", " Themar", " Tirion", " Unarith", " Belanore", " Caelora",
				" Nalore", " Entheas", " Ennore", " Elunore", " Allanar", " Ortheiad", "bel", "belle", "dell", "dorei", "groth", "hil", "hona", "hone", "kadi",
				"lean", "lenor", "lenora", "lian", "lin", "lion", "lon", "lona", "lond", "lone", "luma", "lume", "luna", "lune", "mel", "melle", "naes", "nas",
				"neas", "nor", "nora", "nore", "noris", "qua", "rion", "rius", "sari", "sera", "serin", "serine", "shara", "shys", "taesi", "talos", "thaes",
				"thalas", "thas", "theas", "themar", "thyr" };

		return getRndA(nm1) + getRndA(nm2) + getRndA(nm3);

	}

	/**
	 * https://www.fantasynamegenerators.com/scripts/orcTowns.js
	 * 
	 * @return
	 */
	private static string orcTown() {
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

		return TextHelper.cap(name);

	}

	/**
	 * https://www.fantasynamegenerators.com/scripts/steampunkCities.js
	 * 
	 * @return
	 */
	private static string steamTown() {
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

	/**
	 * https://www.fantasynamegenerators.com/scripts/skyCities.js
	 * 
	 * @return
	 */
	private static string skyTown() {
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

		return TextHelper.cap(name);
	}

}
