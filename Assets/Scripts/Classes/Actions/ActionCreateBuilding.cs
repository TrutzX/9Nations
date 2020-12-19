using Buildings;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using Players;
using Tools;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionCreateBuilding : ActionCreateUnit
    {
        public ActionCreateBuilding() : base("createBuilding"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            int playerId = holder.data.ContainsKey("player") ? ConvertHelper.Int(holder.data["player"]) : player.id;
            S.Building().Create(playerId, holder.data["type"], pos).FinishConstruct();
        }

        protected override BaseDataBuildingUnit Get(string type)
        {
            return L.b.buildings[type];
        }
    }
}