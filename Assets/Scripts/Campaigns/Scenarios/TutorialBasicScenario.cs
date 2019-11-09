using DataTypes;
using Game;
using Maps;
using Players;
using Players.Quests;
using Tools;
using Units;

namespace Campaigns.Scenarios
{
    public class TutorialBasicScenario : IScenarioRun
    {
        public void Run()
        {
            //rebuild map
            GameMgmt.Get().map.SetTile(0,15,9,Data.nTerrain.hill.id);
            
            //add player
            int pid = PlayerMgmt.Get().CreatePlayer("user", "north");
            UnitMgmt.Get().Create(pid,Data.nation.north.leader, 16,10);
            Player p = PlayerMgmt.Get(pid);
            
            //win
            Quest q = QuestHelper.Win();
            q.desc = "To win this tutorial, you need to follow the other quests.";
            q.AddReq("building", ">1,ntownhall2");
            p.quests.Add(q);
            
            //show quest menu
            q = new Quest("menu","Show the quest menu","base:quest");
            q.desc = TextHelper.RichText(
                "Welcome to 9 Nations, 9 Nations is a turn-based strategy game. In this tutorial you will learn the basics. All tasks are displayed in the quest menu. Go to the quest menu (F3), which you see in the upper right corner, and see your first quest.",
                TextHelper.Header("Task"), TextHelper.IconLabel("base:quest","Open the quest menu and find your first task"));
            q.main = true;
            p.quests.Add(q);

            //move
            q = new Quest("move","Move the king","base:move");
            q.desc = TextHelper.RichText(
                "You see your king. Your most important unit. Click on him. Then you see on the bottom his actions, click on the walk icon and walk one field down or press Arrow down. ",
                "After that you consume all your action points (AP) for this turn. Finish your round. Press the hourglass icon in the bottom middle or press space. ",
                TextHelper.Header("Tasks"), TextHelper.IconLabel("u:"+Data.unit.nking.file,"Select your king."), TextHelper.IconLabel("base:move","Move the king down (Arrow down) or with Move Action."), TextHelper.IconLabel("icons:NextRound","End the round"));
            q.AddReq("fogField","16,8");
            p.quests.Add(q);

            //found town
            q = new Quest("found","Found your first town",Data.icons.foundTown.id);
            q.desc = TextHelper.RichText(
                "This is a perfect spot for a new town. Click on the icon and found it.", "Your Kingdom is based on different towns. Every town has his own building and resources. Finish your round.", "The goal of this scenario will be to upgrade the town hall.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(Data.icons.foundTown.id,"Found your town."), TextHelper.IconLabel("icons:NextRound","End the round"));
            q.AddReq("town", ">1");
            q.AddReq("questFinish", "move");
            q.main = true;
            p.quests.Add(q);

            //build food
            q = Build(Data.building.nhunter);
            q.desc = TextHelper.RichText("You can only grow with enough resources. The basics are food, wood and stones. Move your king left to the grass, finish the round and then click the build action and build the hunter.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(Data.nTerrain.grass.icon,"Move to the grass"), q.desc);
            q.AddReq("questFinish", "found");
            p.quests.Add(q);

            //build wood
            q = Build(Data.building.nlogger);
            q.desc = TextHelper.RichText("Great, you start produce food, now you need wood. Move your king in the forest and build the logger.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(Data.nTerrain.forest.icon,"Move in the forest"), q.desc);
            q.AddReq("questFinish", Data.building.nhunter.id);
            p.quests.Add(q);

            //build stone
            q = Build(Data.building.nquarry);
            q.desc = TextHelper.RichText("After that you need some stone for the library. Move your king on the hill and build the quarry.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(Data.nTerrain.hill.icon,"Move in the hill"), q.desc);
            q.AddReq("questFinish", Data.building.nlogger.id);
            p.quests.Add(q);

            //build research
            q = Build(Data.building.nlibrary);
            q.desc = TextHelper.RichText("Now you have every resources for the library. Move near your tent and build the library.",
                TextHelper.Header("Tasks"), q.desc);
            q.AddReq("questFinish", Data.building.nquarry.id);
            p.quests.Add(q);

            //research something
            q = new Quest("research","Research the bigger village hall",Data.icons.research.id);
            q.desc = TextHelper.RichText(
                "In 9 nations, research is carried out in areas in which we want to develop further. The bigger village hall is in the area of life. Then upgrade the village hall to win the tutorial.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel("b:"+Data.building.nlibrary.id,"Wait until the library is finish build"), TextHelper.IconLabel(Data.icons.research.id,"Open research menu"), TextHelper.IconLabel(Data.element.life.icon,"Select life as area"), TextHelper.IconLabel("icons:NextRound","Wait some rounds"));
            q.AddReq("questFinish", Data.building.nlibrary.id);
            q.main = true;
            p.quests.Add(q);
            


        }

        private Quest Build(Building b)
        {
            Quest q = new Quest(b.id,"Build a "+b.name,"b:"+b.id);
            q.desc = TextHelper.IconLabel("b:" + b.id, "Build a "+b.name);
            q.main = true;
            q.AddReq("building", ">1,"+b.id);
            return q;
        }
    }
}