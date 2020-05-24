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
    public class TutorialBasicScenario : IRun
    {
        public void Run()
        {
            DataTerrain grass = L.b.terrains["grass"];
            DataTerrain forest = L.b.terrains["forest"];
            DataTerrain hill = L.b.terrains["hill"];
            
            //rebuild map
            //TODO GameMgmt.Get().map.SetTile(0,15,9,Data.nTerrain.hill.id);
            GameMgmt.Get().newMap.levels[0].SetTile(new Vector3Int(15,9,1), grass);
            GameMgmt.Get().newMap.levels[0].SetTile(new Vector3Int(17,9,1), forest);
            GameMgmt.Get().newMap.levels[0].SetTile(new Vector3Int(17,10,1), forest);

            //add player
            int pid = S.Players().CreatePlayer("user", "north");
            Player p = S.Player(pid);
            p.elements.elements.Add("light");
            
            //and unit
            S.Unit().Create(pid, "light", new NVector(16,10, 0));

            string round = TextHelper.IconLabel("round", "End the round");
            
            //win
            Quest q = QuestHelper.Win();
            q.Desc = "To win this tutorial, you need to follow the other quests.";
            q.AddReq("building", ">1:lhall2");
            p.quests.Add(q);
            
            //show quest menu
            q = new Quest("menu","Show the quest menu","base:quest");
            q.Desc = TextHelper.RichText(
                "Welcome to 9 Nations, 9 Nations is a turn-based strategy game. In this tutorial you will learn the basics. All tasks are displayed in the quest menu. Go to the quest menu (F3), which you see in the upper right corner, and see your first quest.",
                TextHelper.Header("Task"), TextHelper.IconLabel("base:quest","Open the quest menu and find your first task"));
            q.main = true;
            p.quests.Add(q);

            //move
            DataUnit light = L.b.units["light"];
            q = new Quest("move",$"Move the {light.Name()}","base:move");
            q.Desc = TextHelper.RichText(
                "You see your first unit: the element light. Click on it. Then you see on the bottom his actions, click on the walk icon and walk one field down or press Arrow down. ",
                "After that you consume all your action points (AP) for this turn. Finish your round. Press the hourglass icon in the bottom middle or press space. ",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(light.Icon,$"Select your {light.Name()}."), TextHelper.IconLabel("base:move","Move the light down (Arrow down) or with Move Action."), round);
            q.AddReq("fogField","0;16;8");
            p.quests.Add(q);

            //found town
            q = new Quest("found","Found your first town","foundTown");
            q.Desc = TextHelper.RichText(
                "This is a perfect spot for a new town. Click on the icon and found it.", "Your Kingdom is based on different towns. Every town has his own building and resources. Finish your round.", "The goal of this scenario will be to upgrade the town hall.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel("foundTown","Found your town."), round);
            q.AddReq("town", ">1");
            q.AddReq("questFinish", "move");
            q.main = true;
            p.quests.Add(q);
            
            //first unit
            DataBuilding lhall = L.b.buildings["lhall1"];
            DataUnit lworker = L.b.units["lworker"];
            q = new Quest(lworker.id,"Train your first unit",lworker.Icon);
            q.Desc = TextHelper.RichText(
                "At the same time the town hall was built, which manages all resources of your settlement.",
                $"Move the {light.Name()} away from the town hall and train a worker.",
                "To build your empire you need units to perform the basic actions. The main task of workers is to construct buildings and collect resources.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(light.Icon,$"Move the {light.Name()} away"), TextHelper.IconLabel("train",$"Call in the {lhall.Name()} the train action"), TextHelper.IconLabel(lworker.Icon,$"Train a {lworker.Name()}"));
            q.AddReq("unit", $">1:{lworker.id}");
            q.AddReq("questFinish", "found");
            q.main = true;
            p.quests.Add(q);
            
            //build food
            q = Build(L.b.buildings["hunter"]);
            q.Desc = TextHelper.RichText("You can only grow with enough resources. The basics are food, wood and stones. Move your king left to the grass, finish the round and then click the build action and build the hunter.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(grass.Icon,$"Move to the {grass.Name()}"), q.Desc);
            q.AddReq("questFinish", lworker.id);
            p.quests.Add(q);

            //build wood
            q = Build(L.b.buildings["logger"]);
            q.Desc = TextHelper.RichText("Great, you start produce food, now you need wood. Move your king in the forest and build the logger.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(forest.Icon,$"Move to the {forest.Name()}"), q.Desc);
            q.AddReq("questFinish", "hunter");
            p.quests.Add(q);

            //build stone
            q = Build(L.b.buildings["cobblestoneC"]);
            q.Desc = TextHelper.RichText("After that you need some stone for the library. Move your king on the hill and build the cobblestone collector.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(hill.Icon,$"Move in the {hill.Icon}"), q.Desc);
            q.AddReq("questFinish", "logger");
            p.quests.Add(q);

            //build research
            DataBuilding library = L.b.buildings["library"];
            q = Build(library);
            q.Desc = TextHelper.RichText("Now you have every resources for the library. Move near your tent and build the library.",
                TextHelper.Header("Tasks"), q.Desc);
            q.AddReq("questFinish", "cobblestoneC");
            p.quests.Add(q);

            //research something
            q = new Quest("research","Research the bigger village hall","research");
            q.Desc = TextHelper.RichText(
                "In 9 nations, research is carried out in areas in which we want to develop further. The bigger village hall is in the area of life. Then upgrade the village hall to win the tutorial.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(library.Icon,$"Wait until the {library.Name()} is finish build"), TextHelper.IconLabel("research","Open research menu"), TextHelper.IconLabel(L.b.elements["life"].Icon,"Select life as area"), round);
            q.AddReq("questFinish", "library");
            q.main = true;
            p.quests.Add(q);
        }

        public string ID()
        {
            return "tutorialbasic";
        }

        private Quest Build(DataBuilding b)
        {
            Quest q = new Quest(b.id,"Build a "+b.Name(),b.Icon);
            q.Desc = TextHelper.IconLabel(b.Icon, "Build a "+b.Name());
            q.main = true;
            q.AddReq("building", ">1:"+b.id);
            return q;
        }
    }
}