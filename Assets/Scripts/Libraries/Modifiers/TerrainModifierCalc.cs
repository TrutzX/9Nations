using Game;
using Libraries.Terrains;
using Players;
using Tools;

namespace Libraries.Modifiers
{
    public class TerrainModifierCalc : BaseModifierCalc
    {
        public override string Desc(string data)
        {
            string[] d = data.Split(';');

            return base.Desc(d[0]) + " on " + L.b.terrains[d[2]].Name();
        }

        public override bool Check(string data, Player player, NVector pos)
        {
            string[] d = data.Split(';');
            DataTerrain terr = GameMgmt.Get().newMap.Terrain(pos); 
            return terr.id == d[2];
        }
        
        public override void ParseModi(string data, ref int val, ref int proc)
        {
            string[] d = data.Split(';');
            base.ParseModi(d[0], ref val, ref proc);
        }
    }
}