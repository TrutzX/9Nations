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
    public class Tutorial2Scenario : IRun
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
            q.desc = TextHelper.RichText("To win this tutorial, you need to follow the other quests.","This tutorial was concepted for version 0.24.","You can find more tutorials from the main game");
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
            
            
            
            //erklären Ressourcen verbrauch
            
            //mushrooms produzieren
            
            //2. town level
            
            //shrine bauen
            
            
            //L.b.gameOptions["usageTown"].SetValue("false");
            //L.b.gameOptions["inhabitantGrow"].SetValue("false");
            /*
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
            
            

            string round = TextHelper.IconLabel("round", "End the round (space)");
            
            
            
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
            q = Build(samp);
            q.desc = TextHelper.RichText($"You can only grow with enough resources. The basics are water, food (meat, fish) and construction material (wood, cobble stones). Move your {light.Name()} or your {lworker.Name()} near the water, finish the round and then click the build action and build the {samp.Name()}. It will produce water and a litte fish (food).",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(water.Icon,$"Move near the {water.Name()}"), q.desc);
            q.AddReq("questFinish", lworker.id);
            p.quests.Add(q);

            //build wood
            q = Build(L.b.buildings["logger"]);
            q.desc = TextHelper.RichText("Great, you start produce food, now you need construction material, e.g. wood. Move your unit in the forest and build the logger.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(forest.Icon,$"Move to the {forest.Name()}"), q.desc);
            q.AddReq("questFinish", samp.id);
            p.quests.Add(q);

            //build stone
            var cob = L.b.buildings["cobblestoneC"];
            q = Build(cob);
            q.desc = TextHelper.RichText($"After that you need some cobblestone for some buildings. Move your unit on the hill and build the ${cob.Name()}.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(hill.Icon,$"Move in the {hill.Name()}"), q.desc);
            q.AddReq("questFinish", "logger");
            p.quests.Add(q);

            var tent = L.b.buildings["tent"];
            q = Build(tent);
            q.desc = TextHelper.RichText($"We also need some place for the inhabitants of this town. Move your unit on a free field and build the ${tent.Name()}.",
                TextHelper.Header("Tasks"), q.desc);
            q.AddReq("questFinish", "cobblestoneC");
            p.quests.Add(q);
            
            //build research
            DataBuilding library = L.b.buildings["library"];
            q = Build(library);
            q.desc = TextHelper.RichText($"So the basics are set. To build a temple, we need to evolve the town.",$"But we need to research it before in the {library.Name()}. Move near your village hall and build the {library.Name()}.",
                "You have now 2 construction material: wood and cobblestone. You can choose which one to use or a mix of them. If you build with wood, the building is finished faster, but has less HP. Stone is very robust, but takes a long time to build.",
                TextHelper.Header("Tasks"), q.desc);
            q.AddReq("questFinish", "tent");
            p.quests.Add(q);

            //research something
            DataBuilding lhall2 = L.b.buildings["lhall2"];
            q = new Quest("research","Research the "+lhall2.Name(),"research");
            q.desc = TextHelper.RichText(
                "In 9 nations, research is carried out in elements in which we want to develop further. The bigger village hall is in the area of 2x the light element. Then upgrade the village hall.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(library.Icon,$"Wait until the {library.Name()} is finish build"), TextHelper.IconLabel("research","Open research menu"), TextHelper.IconLabel(L.b.elements["light"].Icon,"Select 2x light as area"), 
                TextHelper.IconLabel("research","Finish the research"), TextHelper.IconLabel(lhall2.Icon, "Upgrade the townhall to "+lhall2.Name()));
            q.AddReq("questFinish", "library");
            q.AddReq("building", ">1:"+lhall2.id);
            q.main = true;
            p.quests.Add(q);
            
            //build the temple
            DataBuilding temple = L.b.buildings["lshrine"];
            q = Build(temple);
            q.desc = TextHelper.RichText(
                "Your city has now moved on. You now have new opportunities, but your inhabitants also have new needs.",$"To really evolve, you need a {temple.Name()} to better explore the cosmos.",
                TextHelper.Header("Tasks"), q.desc);
            q.AddReq("questFinish", "lhall2");
            q.main = true;
            p.quests.Add(q);
            */
        }

        public string ID()
        {
            return "tutorial2";
        }

        private Quest Build(DataBuilding b)
        {
            Quest q = new Quest(b.id,"Build a "+b.Name(),b.Icon);
            q.desc = TextHelper.IconLabel(b.Icon, "Build a "+b.Name());
            q.main = true;
            q.AddReq("building", ">1:"+b.id);
            return q;
        }
    }
}