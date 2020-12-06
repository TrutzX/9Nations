using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.Campaigns;
using Libraries.Terrains;
using Libraries.Units;
using Players;
using Players.Quests;
using Tools;
using UnityEngine;

namespace Classes.Scenarios
{
    public class Tutorial2ShadowScenario : IRun
    {
        public void Run()
        {
            //L.b.gameOptions["fog"].SetValue("false");
            //add player
            int pid = S.Players().CreatePlayer(System.Environment.UserName, "forger");
            Player p = S.Player(pid);
            p.elements.elements.Add("shadow");
            
            //and unit
            S.Unit().Create(pid, "shadow", new NVector(14,10, 0));
            
            //win
            Quest q = QuestHelper.Win();
            q.desc = TextHelper.RichText("To win this tutorial, you need to develop the earth element. Follow the other quests.",QuestHelper.Version("0.24"));
            q.AddReq("element", "earth");
            p.quests.Add(q);
            
            q = new Quest("foundTown","Found your town","foundTown");
            q.desc = TextHelper.RichText(
                "Welcome to the underground.","As the element says, you now begin under the shadow. But the shadow element can easily switch between levels and even float through deep walls. Find a suitable place for your first city and found it.",
                "If a place is blocked by a deep wall, you can use terraform to transform it.",
                TextHelper.Header("Task"), TextHelper.IconLabel("foundTown","Find a good place and found your town."));
            q.AddReq("building", ">1:shall1");
            q.main = true;
            p.quests.Add(q);

            var worker = L.b.units["sworker"];
            var explorer = L.b.units["sexplorer"];
            q = new Quest("unit",$"{worker.Name()} & {explorer.Name()}","train");
            q.desc = TextHelper.RichText(
                "First you'll need units again.","The Explorer can go through walls, explore the area and claim unknown fields.","The worker can construct buildings, remove the wall and collect resources, so train both of them now.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(worker.Icon,$"Train a {worker.Name()}"), TextHelper.IconLabel(explorer.Icon,$"Train a {explorer.Name()}"));
            q.AddReq("unit", ">1:"+worker.id);
            q.AddReq("unit", ">1:"+explorer.id);
            q.AddReq("questFinish", "foundTown");
            q.main = true;
            p.quests.Add(q);
            
            //build buildings
            var sfarm = L.b.buildings["sfarm"];
            var sdirt = L.b.buildings["sdirt"];
            var quarry = L.b.buildings["quarry"];
            var fisher = L.b.buildings["fisher"];
            var sandG = L.b.buildings["sandG"];
            var tent = L.b.buildings["tent"];
            
            q = new Quest("building","Build the town","build");
            q.desc = TextHelper.RichText(
                "As with the element of light, you first need construction material and food.",$"Sand from {sandG.Name()} and stone from {quarry.Name()} are used for construction materials.",
                $"Food is available from the {fisher.Name()} or the {sfarm.Name()}. But the {sfarm.Name()} needs dirt from {sdirt.Name()}.", $"The workers live in {tent.Name()} again.",$"If the fields are too far away from the town hall, they must be taken into occupation first. The {explorer.Name()} can do this via claim.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(quarry.Icon,$"Build a {quarry.Name()}"), TextHelper.IconLabel(sdirt.Icon,$"Build a {sdirt.Name()}"),
                TextHelper.IconLabel(sfarm.Icon,$"Build a {sfarm.Name()}"), TextHelper.IconLabel(tent.Icon,$"Build a {tent.Name()}"));
            q.AddReq("building", ">1:"+quarry.id);
            q.AddReq("building", ">1:"+sdirt.id);
            q.AddReq("building", ">1:"+sfarm.id);
            q.AddReq("building", ">1:"+tent.id);
            q.AddReq("questFinish", "unit");
            q.main = true;
            p.quests.Add(q);
            
            // level 2
            DataBuilding shall2 = L.b.buildings["shall2"];
            q = new Quest("shall2","Upgrade to "+shall2.Name(),shall2.Icon);
            q.desc = TextHelper.RichText(
                "Unlike light, shadow knows no research. That's why you can upgrade the town hall right now.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(shall2.Icon, "Upgrade the town hall to "+shall2.Name()));
            q.AddReq("questFinish", "building");
            q.AddReq("building", ">1:"+shall2.id+":true");
            q.main = true;
            p.quests.Add(q);
            
            // usage Town
            DataBuilding sshrine = L.b.buildings["sshrine"];
            q = new Quest("need","Find the needs",p.Coat().flag);
            q.desc = TextHelper.RichText(
                "A city can only grow if its inhabitants have fulfilled necessary usages. Depending on the level of the city, the inhabitants have different requirements. Since the city has now increased one level, there are new requirements.",
                "Open the menu and check the usages. Build the appropriate building.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(p.Coat().flag, "Open Kingdom overview"), TextHelper.IconLabel("foundTown", "Open your town"), TextHelper.IconLabel("usage", "Find the usages"), TextHelper.IconLabel("build", "Build appropriate the building"));
            q.AddReq("questFinish", "shall2");
            q.AddReq("building", ">1:"+sshrine.id);
            q.main = true;
            p.quests.Add(q);
            
            // development
            var earth = L.b.elements["earth"];
            q = new Quest("earth","Evolve to a new element","religion");
            q.desc = TextHelper.RichText(    
                "Once the shrine is built, the gods can be worshipped and the nation can develop.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel("religion", "Evolve to a new element"));
            q.AddReq("questFinish", "need");
            q.AddReq("element", "earth");
            q.main = true;
            p.quests.Add(q);
            
        }

        public string ID()
        {
            return "tutorial2";
        }
    }
}