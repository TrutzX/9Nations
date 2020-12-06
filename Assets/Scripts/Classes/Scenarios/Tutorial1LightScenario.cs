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
    public class Tutorial1LightScenario : IRun
    {
        public void Run()
        {
            L.b.gameOptions["usageTown"].SetValue("false");
            L.b.gameOptions["inhabitantGrow"].SetValue("false");
            
            DataTerrain water = L.b.terrains["water"];
            DataTerrain forest = L.b.terrains["forest"];
            DataTerrain hill = L.b.terrains["hill"];
            
            //rebuild map
            //TODO GameMgmt.Get().map.SetTile(0,15,9,Data.nTerrain.hill.id);
            //GameMgmt.Get().newMap.levels[1].SetTile(new Vector3Int(15,9,1), grass);
            GameMgmt.Get().newMap.levels[1].SetTile(new Vector3Int(17,9,1), forest);
            GameMgmt.Get().newMap.levels[1].SetTile(new Vector3Int(17,10,1), forest);

            //add player
            int pid = S.Players().CreatePlayer(System.Environment.UserName, "north");
            Player p = S.Player(pid);
            p.elements.elements.Add("light");
            
            //and unit
            S.Unit().Create(pid, "light", new NVector(16,10, 1));

            string round = TextHelper.IconLabel("round", "End the round (space)");
            
            //win
            Quest q = QuestHelper.Win();
            q.desc = TextHelper.RichText("To win this tutorial, you need to follow the other quests.",QuestHelper.Version("0.24"),"You can find more tutorials from the main game.");
            q.AddReq("building", ">1:lhall2");
            p.quests.Add(q);
            
            //show quest menu
            q = new Quest("menu","Show the quest menu","quest");
            q.desc = TextHelper.RichText(
                "Welcome to 9 Nations, 9 Nations is a turn-based strategy game. In this tutorial you will learn the basics.","All tasks are displayed in the quest menu. Go to the quest menu (F3), which you see in the upper right corner, and see your first quest.",
                TextHelper.Header("Task"), TextHelper.IconLabel("quest","Open the quest menu and find your first task"));
            q.main = true;
            p.quests.Add(q);

            //move map
            q = new Quest("moveCamera","Move the camera","map");
            q.desc = TextHelper.RichText(LSys.tem.helps["moveCamera"].Desc(),
                TextHelper.Header("Task"), TextHelper.IconLabel("quest","Move the map"));
            p.quests.Add(q);
            
            //move
            DataUnit light = L.b.units["light"];
            q = new Quest("moveLight",$"Move the {light.Name()}","move");
            q.desc = TextHelper.RichText(
                $"You see your first unit: the element {light.Name()}. Click on it. Then you see on the bottom left the actions, click on the walk icon and move 2 fields down or press two times arrow down. ",
                "After that you consume all your action points (AP) for this turn. Finish your round. Press the hourglass icon in the bottom middle or press space. ",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(light.Icon,$"Select your {light.Name()}."), TextHelper.IconLabel("move","Move the light 2 field down (Arrow down) or with move action."), round);
            q.AddReq("fogField","1;16;7");
            p.quests.Add(q);

            //found town
            q = new Quest("found","Found your first town","foundTown");
            q.desc = TextHelper.RichText(
                "Your Kingdom is based on different towns. Every town has his own building and resources.", "This is a perfect spot for a new town. Click on the icon and found it. Finish then your round.", "The goal of this scenario will be to build the temple in your city.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel("foundTown","Found your town."), round);
            q.AddReq("townCount", ">1");
            q.AddReq("questFinish", "moveLight");
            q.main = true;
            p.quests.Add(q);
            
            //first unit
            DataBuilding lhall = L.b.buildings["lhall1"];
            DataUnit lworker = L.b.units["lworker"];
            q = new Quest(lworker.id,"Train your first unit",lworker.Icon);
            q.desc = TextHelper.RichText(
                "At the same time the town hall was built, which manages all resources of your settlement.",$"The life task of the element {light.Name()} has been fulfilled and it will die in 5 rounds.",
                "To build your empire you need units to perform the basic actions. The main task of workers is to construct buildings and collect resources.",
                $"Move the {light.Name()} away from the town hall and train a {lworker.Name()}.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(light.Icon,$"Move the {light.Name()} away"), TextHelper.IconLabel("train",$"Call in the {lhall.Name()} lower right the train action"), TextHelper.IconLabel(lworker.Icon,$"Train a {lworker.Name()}"));
            q.AddReq("unit", $">1:{lworker.id}");
            q.AddReq("questFinish", "found");
            q.main = true;
            p.quests.Add(q);
            
            //build food
            var samp = L.b.buildings["samplingpoint"];
            q = QuestHelper.Build(samp);
            q.desc = TextHelper.RichText($"You can only grow with enough resources. The basics are water, food (meat, fish) and construction material (wood, cobble stones). Move your {light.Name()} or your {lworker.Name()} near the water, finish the round and then click the build action and build the {samp.Name()}. It will produce water and a litte fish (food).",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(water.Icon,$"Move near the {water.Name()}"), q.desc);
            q.AddReq("questFinish", lworker.id);
            p.quests.Add(q);

            //build wood
            q = QuestHelper.Build(L.b.buildings["logger"]);
            q.desc = TextHelper.RichText("Great, you start produce food, now you need construction material, e.g. wood. Move your unit in the forest and build the logger.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(forest.Icon,$"Move to the {forest.Name()}"), q.desc);
            q.AddReq("questFinish", samp.id);
            p.quests.Add(q);

            //build stone
            var cob = L.b.buildings["cobblestoneC"];
            q = QuestHelper.Build(cob);
            q.desc = TextHelper.RichText($"After that you need some cobblestone for some buildings. Move your unit on the hill and build the ${cob.Name()}.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(hill.Icon,$"Move in the {hill.Name()}"), q.desc);
            q.AddReq("questFinish", "logger");
            p.quests.Add(q);

            var tent = L.b.buildings["tent"];
            q = QuestHelper.Build(tent);
            q.desc = TextHelper.RichText($"We also need some place for the inhabitants of this town. Move your unit on a free field and build the ${tent.Name()}.",
                TextHelper.Header("Tasks"), q.desc);
            q.AddReq("questFinish", "cobblestoneC");
            p.quests.Add(q);
            
            //build research
            DataBuilding library = L.b.buildings["library"];
            q = QuestHelper.Build(library);
            q.desc = TextHelper.RichText($"So the basics are set. To build a temple, we need to evolve the town.",$"But we need to research it before in the {library.Name()}. Move near your village hall and build the {library.Name()}.",
                "You have now 2 construction material: wood and cobblestone. You can choose which one to use or a mix of them. If you build with wood, the building is finished faster, but has less HP. Stone is very robust, but takes a long time to build.",
                TextHelper.Header("Tasks"), q.desc);
            q.AddReq("questFinish", "tent");
            p.quests.Add(q);

            //research something
            DataBuilding lhall2 = L.b.buildings["lhall2"];
            q = new Quest("lhall2","Research the "+lhall2.Name(),"research");
            q.desc = TextHelper.RichText(
                "In 9 nations, research is carried out in elements in which we want to develop further. The bigger village hall is in the area of 2x the light element. Then upgrade the village hall and win the game.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(library.Icon,$"Wait until the {library.Name()} is finish build"), TextHelper.IconLabel("research","Open research menu"), TextHelper.IconLabel(L.b.elements["light"].Icon,"Select 2x light as area"), 
                TextHelper.IconLabel("research","Finish the research"), TextHelper.IconLabel(lhall2.Icon, "Upgrade the townhall to "+lhall2.Name()));
            q.AddReq("questFinish", "library");
            q.AddReq("building", ">1:"+lhall2.id);
            q.main = true;
            p.quests.Add(q);
            
        }

        public string ID()
        {
            return "tutorial1";
        }
    }
}