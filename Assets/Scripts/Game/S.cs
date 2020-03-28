using Buildings;
using Libraries.Rounds;
using Towns;
using Units;

namespace Game
{
    public class S
    {
        public static TownMgmt Towns()
        {
            return GameMgmt.Get().data.towns;
        }
        public static GameRoundMgmt Round()
        {
            return GameMgmt.Get().gameRound;
        }
        
        public static BuildingMgmt Building()
        {
            return GameMgmt.Get().building;
        }
        
        public static UnitMgmt Unit()
        {
            return GameMgmt.Get().unit;
        }
    }
}