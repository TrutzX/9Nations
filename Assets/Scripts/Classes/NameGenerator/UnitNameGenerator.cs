using System;
using Tools;
using Random = UnityEngine.Random;

namespace Classes.NameGenerator
{
    public class UnitNameGenerator : BaseNameGenerator
    {
	    /// <summary>
	    /// Author: view-source:https://www.fantasynamegenerators.com/scripts/titleNames.js
	    /// </summary>
	    /// <param name="include"></param>
	    /// <returns></returns>
        public override string Gen(string include = null)
        {
	        string[] nm1 = {"Queen", "Matriarch", "Mother", "Father", "Admiral", "Baron", "Blessed", "Caesar", "Captain", "Cardinal", "Chairman", "Chief", "Chieftain", "Commander", "Corporal", "Count", "Defender", "Divine", "Dom", "Duke", "Earl", "Elder", "Eminence", "Emperor", "Exarch", "General", "Governor", "Grand Master", "Guardian", "Headman", "Herald", "Imperator", "King", "Lord", "Master", "Palatine", "Paragon", "Patriarch", "Pharaoh", "President", "Prime", "Prince", "Protector", "Ruler", "Shogun", "Sultan"};
	        string[] nm2 = {"Arrows", "Ash", "Blue", "Bones", "Conviction", "Damned", "Darkness", "Dawn", "Death", "Demons", "Dragons", "Dreams", "Dusk", "Dwarves", "Elves", "Faith", "Fear", "Fire", "Fools", "Fortitude", "Gold", "Green", "Heaven", "Hell", "Ice", "Iron", "Justice", "Kingdoms", "Life", "Light", "Men", "Nations", "Nature", "New Kingdom", "Night", "Nightmares", "the Old Kingdom", "Orcs", "Order", "Peace", "Purity", "Realms", "Red", "Sand", "Ships", "Silver", "Skulls", "Snow", "Steel", "Swords", "Thieves", "Unity", "Universe", "Virtue", "War", "Watch", "Water", "the Dead", "the Desert", "the Dominion", "the Earth", "the East", "the Fields", "the Fleet", "the Forests", "the Future", "the Gods", "the Lakes", "the Lands", "the Living", "the Marsh", "the Millenium", "the Moon", "the Mountains", "the New Age", "the North", "the Ocean", "the People", "the Plains", "the Reach", "the Realm", "the Rivers", "the Seas", "the Skies", "the Small", "the South", "the Stars", "the Sun", "the Titans", "the Undead", "the Vale", "the Valleys", "the West", "the Wild", "the Winds", "the Wise", "the World"};
	        string[] nm3 = {"Administrator", "Assistant", "Baron", "Captain", "Chairman", "Chief", "Consul", "Curator", "Delegate", "Director", "Earl", "Exarch", "Governor", "Head", "Headman", "Lady", "Liaison", "Lord", "Master", "Matriarch", "Minister", "Noble", "Official", "Overlord", "Patriarch", "Prime", "Professor", "Secretary", "Sir", "Tribune"};
	        string[] nm4 = {"Affairs", "Agents", "Agriculture", "Alliances", "Armor", "Aviation", "Borders", "Bows", "City Planning", "Coin", "Commerce", "Communication", "Control", "Culture", "Death", "Defense", "Development", "Education", "Emissaries", "Employment", "Energy", "Farming", "Finance", "Fish", "Food", "Forestry", "Gold", "Guards", "Health", "Hope", "Housing", "Immigration", "Industry", "Information", "Iron", "Justice", "Labour", "Law", "Magic", "Medicine", "Merchants", "Mining", "Money", "Natural Resources", "Peace", "Planning", "Poison", "Protection", "Relations", "Resources", "Revenue", "Science", "Scouts", "Secrets", "Security", "Shields", "Ships", "Silver", "Slaves", "Spies", "Sports", "Steel", "Strategy", "Swords", "Tactics", "Technology", "Tourism", "Toxins", "Trade", "Transport", "Treasure", "Vigor", "Virtue", "War", "Warfare", "Water", "Weapons", "Well-Being", "the Future", "the Marine"};
	        string[] nm5 = {"Priestess", "High Priestess", "Abbot", "Angelic", "Apostle", "Archbishop", "Archdeacon", "Archpriest", "Bishop", "Cardinal", "Chancellor", "Chaplain", "Cleric", "Counselor", "Deacon", "Divine", "Doctor", "Elder", "Exalted", "Father", "Guru", "High Priest", "Imam", "Lama", "Mage", "Magi", "Matriarch", "Minister", "Missionary", "Monk", "Mother", "Novice", "Nun", "Paladin", "Pastor", "Patriarch", "Preacher", "Priest", "Rabbi", "Reverend", "Sage", "Saint", "Seer", "Sensei", "Shaman", "Templar", "Warlock", "Witch"};
	        string[] nm6 = {"Birth", "Bliss", "Blood", "Bones", "Darkness", "Death", "Devotion", "Divinity", "Dreams", "Eternity", "Faith", "Freedom", "Genesis", "Gold", "Gracy", "Healing", "Life", "Light", "Mercy", "Misery", "Nature", "Nightmares", "Peace", "Pestinence", "Piety", "Pure Hearts", "Purity", "Rebirth", "Sanctity", "Shadow", "Silence", "Silver", "Solitude", "Spirits", "Time", "Virtue", "Worship", "the Arcane", "the Blue", "the Dead", "the Earth", "the East", "the Flame", "the Gardens", "the King", "the Light", "the Living", "the Moon", "the Night", "the North", "the Phoenix", "the Realm", "the Red", "the Sands", "the Sea", "the South", "the Stars", "the Sun", "the Undead", "the Undying", "the Veil", "the Voice", "the Void", "the West", "the White", "the World"};

	        string name = null;
	        int i = Random.Range(0, 9);
	        if (i < 3) {
	            name = getRndA(nm1) + " of " + getRndA(nm2);
	        } else if (i < 7) {
	            name = getRndA(nm3) + " of " + getRndA(nm4);
	        } else {
	            name = getRndA(nm5) + " of " + getRndA(nm6);
	        }

	        return name;
        }
    }
}