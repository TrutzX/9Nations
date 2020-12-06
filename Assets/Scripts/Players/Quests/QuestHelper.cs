using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.Res;
using Libraries.Units;
using Tools;
using UI;
using UnityEngine;

namespace Players.Quests
{
    public static class QuestHelper
    {

        public static Quest Lose()
        {
            return Action("endGameLose");
        }
        
        public static Quest Win()
        {
            return Action("endGameWin");
        }

        public static Quest Action(string id)
        {
            FDataAction da = L.b.actions[id];
            Quest q = new Quest(da.id, da.Name(), da.Icon).AddAction(id,"");
            return q;
        }

        public static Quest AddNoUnitTown(Quest q)
        {
            q.AddReq("townCount", "<0").AddReq("unitCount", "<0").AddReq("round", ">1");
            return q;
        }

        public static Quest Build(DataBuilding b)
        {
            Quest q = new Quest(b.id,"Build a "+b.Name(),b.Icon);
            q.desc = TextHelper.IconLabel(b.Icon, "Build a "+b.Name());
            q.main = true;
            q.AddReq("building", ">1:"+b.id);
            return q;
        }

        public static Quest Unit(DataUnit b)
        {
            Quest q = new Quest(b.id,"Train a "+b.Name(),b.Icon);
            q.desc = TextHelper.IconLabel(b.Icon, "Train a "+b.Name());
            q.main = true;
            q.AddReq("unit", ">1:"+b.id);
            return q;
        }

        public static Quest Res(Resource b, int count=1)
        {
            Quest q = new Quest(b.id,"Produce "+b.Text(count),b.Icon);
            q.desc = TextHelper.IconLabel(b.Icon, "Produce "+b.Text(count));
            q.main = true;
            q.AddReq("res", ">"+count+":"+b.id);
            return q;
        }

        public static string Version(string v)
        {
            return v != Application.version?$"This scenario was developed for version 0.24. (You have {Application.version})":null;
        }
    }
}