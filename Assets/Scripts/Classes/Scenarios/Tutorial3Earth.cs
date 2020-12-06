using Game;
using Libraries;
using MapElements.Buildings;
using MapElements.Units;
using Players;
using Players.Quests;
using Tools;
using Towns;
using UnityEngine;

namespace Classes.Scenarios
{
    public class Tutorial3Earth : IRun
    {
        public void Run()
        {
            //L.b.gameOptions["fog"].SetValue("false");
            //add player
            int pid = S.Players().CreatePlayer(System.Environment.UserName, "forger");
            Player p = S.Player(pid);
            p.elements.elements.Add("shadow");
            //p.elements.elements.Add("earth"); //TODO REMOVE
            
            //create flower
            var flower = L.b.res.GetOrCreate("flower");
            flower.Icon = "!Icons/base:flower";
            Swamp(p, new NVector(17, 11, 1));
            Swamp(p, new NVector(17, 12, 1));
            Swamp(p, new NVector(16, 11, 1));
            
            //create greenhouse
            var nursery = L.b.buildings.GetOrCreate("nursery");
            nursery.Icon = "!Building/farm:nursery";
            nursery.req.Add("terrain","swamp");
            nursery.action.Add("produce","turn;flower:1");
            nursery.cost["construction"] = 5;
            nursery.cost["brick"] = 2;
            nursery.buildTime = 4;
            nursery.category = "earth";
            
            //change evolve
            L.b.actions["evolve"].cost = 15;
            
            //create town
            var center = new NVector(20,15,0);
            int tid = S.Towns().Create(LClass.s.NameGenerator("dwarf"), pid, center, false);
            var t = S.Town(tid);
            
            //add buildings
            Build(tid, "shall2", center);
            Build(tid, "sshrine", center.DiffX(-2)).dataBuilding.action.Get("evolve").cost = 15;
            S.Building().Create(tid, "sfarm", center.DiffY(2)).FinishConstruct();
            S.Building().Create(tid, "quarry", center.DiffY(-2)).FinishConstruct();
            Build(tid, "sandG", center.DiffX(1).DiffY(1));
            Build(tid, "sdirt", center.DiffX(2).DiffY(1));
            Build(tid, "tent", center.DiffX(-2).DiffY(1));

            t.AddRes("worker",7,ResType.Gift);
            t.AddRes("mushroom",5,ResType.Gift);
            t.AddRes("tool",20,ResType.Gift);
            
            //add units
            Unit(pid, "sexplorer", center);
            Unit(pid, "sworker", center.DiffX(-1));
            
            /*t.AddRes("copper",10,ResType.Gift); //TODO REMOVE
            t.AddRes("pottery",10,ResType.Gift); //TODO REMOVE
            Unit(pid, "geologist", center.DiffX(1)); //TODO REMOVE
            Unit(pid, "settler", center.DiffY(-4)); //TODO REMOVE
            Build(tid, "stair", center.DiffY(1).DiffX(-1)); //TODO REMOVE
            Build(tid, "toolshop", center.DiffY(-1).DiffX(1)); //TODO REMOVE
            Build(tid, "mine", center.DiffY(-1), false); //TODO REMOVE
            Build(tid, "athenaeum", center.DiffX(-1)); //TODO REMOVE
            p.research.Set("ehall",true); //TODO REMOVE
            t.AddRes("stone",10,ResType.Gift); //TODO REMOVE
            Build(tid, "equarters", center.DiffY(-3)); //TODO REMOVE

            
            Unit(pid, "emage", center.DiffY(-2)); //TODO REMOVE*/
            
            //remove notifications
            p.info.infos.Clear();
            
            //add units
            
            //and unit
            //S.Unit().Create(pid, "shadow", new NVector(14,10, 0));
            
            //win
            Quest q = QuestHelper.Win();
            q.desc = TextHelper.RichText("To win this tutorial, you need to collect a rare flower for your scheduled wedding. The flower grow on the other side of the river",QuestHelper.Version("0.24"));
            q.AddReq("res", ">1:flower");
            q.main = true;
            p.quests.Add(q);
            
            //develop earth
            q = new Quest("earth","Evolve to a new element","religion");
            q.desc = TextHelper.RichText(    
                "Worship the gods in the shrine can and develop the element earth. It need maybe some attempts. (Evolve cost much less ap in this tutorial.)",
                TextHelper.Header("Tasks"), TextHelper.IconLabel("religion", "Evolve to a new element"));
            q.main = true;
            p.quests.Add(q);
            
            //research
            var athenaeum = L.b.buildings["athenaeum"];
            q = QuestHelper.Build(athenaeum);
            q.desc = TextHelper.RichText("You have successfully developed. With the further development you also have new possibilities.","Unlike the shadow element, you now need research.",
                TextHelper.Header("Tasks"), q.desc);
            q.AddReq("element", "earth");
            q.AddReq("questFinish", "earth");
            q.main = true;
            p.quests.Add(q);
            
            //geologe
            var geologe = L.b.units["geologist"];
            q = QuestHelper.Unit(geologe);
            q.desc = TextHelper.RichText("Now it is time to produce the tools. The copper is mined in a mine and processed into tools in a factory. Train a geologist to search the mountains for copper.",
                TextHelper.Header("Tasks"), q.desc, TextHelper.IconLabel("examine","Move to a stone and examine it"), TextHelper.IconLabel("overlay","In the overlay you can see your results"));
            q.AddReq("questFinish", "athenaeum");
            q.main = true;
            p.quests.Add(q);
            
            //mine
            var toolshop = L.b.buildings["toolshop"];
            var mine = L.b.buildings["mine"];
            q = QuestHelper.Build(toolshop);
            q.desc = TextHelper.RichText("You found a perfect place for a mine. For this purpose a mine and a tool factory is needed.","Copper mining in the mine also needs to be researched (shadow, shadow, shadow).","The tool factory still needs to be researched (shadow, shadow).",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(mine.Icon, "Build a "+mine.Name()), q.desc);
            q.AddReq("building", ">1:"+mine.id);
            q.AddReq("questFinish", "geologist");
            q.main = true;
            p.quests.Add(q);
            
            //produce copper
            var copper = L.b.res["copper"];
            q = QuestHelper.Res(copper);
            q.desc = TextHelper.RichText(    
                "Once the buildings are in function, they still need to know what they are to produce.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel("craft","Open the craft menu in the mine and produce copper"), TextHelper.IconLabel("craft","Open the craft menu in the tool factory and produce tools"), q.desc);
            q.AddReq("questFinish", "toolshop");
            q.main = true;
            p.quests.Add(q);
            
            //reach level 3
            var ehall = L.b.buildings["ehall"];
            q = QuestHelper.Build(ehall);
            q.desc = TextHelper.RichText(    
                "The basic production stands. We can now expand our settlement.",$"Research the {ehall.Name()} and upgrade your settlement.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel("research",$"Research the {ehall.Name()}"), q.desc);
            q.AddReq("questFinish", "copper");
            q.main = true;
            p.quests.Add(q);
            
            //pottery
            var pottery = L.b.res["pottery"];
            var pot = L.b.buildings["pottery"];
            q = QuestHelper.Res(pottery);
            q.desc = TextHelper.RichText(    
                "Congratulations, your settlement has become a city. But now your residents have more demands, for example for pottery. You can see the demands in XX.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(pot.Icon,$"Build the {pot.Name()}."), q.desc);
            q.AddReq("questFinish", "ehall");
            q.main = true;
            p.quests.Add(q);
             
            //settler
            var settler = L.b.units["settler"];
            var equarters = L.b.buildings["equarters"];
            q = QuestHelper.Unit(settler);
            q.desc = TextHelper.RichText(    
                $"To pick the flower, we need a new city where we can build the nursery, so train a new settler in your {equarters.Name()}.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(equarters.Icon,$"Build the {equarters.Name()} (Maybe you need to research it before)."), q.desc);
            q.AddReq("questFinish", "pottery");
            q.main = true;
            p.quests.Add(q);
            
            //town
            q = new Quest("town","Found a new town","foundTown");
            q.desc = TextHelper.RichText(    
                "The settler must walk near the swamp and found the city there. The swamp is already highlighted in the map. Via a staircase he gets to the surface. With R & F you can switch the view of the level.",
                TextHelper.Header("Tasks"), TextHelper.IconLabel(q.Icon,q.name));
            q.AddReq("townCount", ">2");
            q.AddReq("questFinish", "settler");
            q.main = true;
            p.quests.Add(q);
            
            //town
            q = QuestHelper.Build(nursery);
            q.desc = TextHelper.RichText(    
                "The last task is to build a nursery. For this you have to send some resources to your new city or build the corresponding industries.",
                TextHelper.Header("Tasks"), q.desc);
            q.AddReq("questFinish", "town");
            q.main = true;
            p.quests.Add(q);
            
        }

        private BuildingInfo Build(int tid, string id, NVector pos, bool remove=true)
        {
            if (remove)
                GameMgmt.Get().newMap.levels[pos.level].SetTile(new Vector3Int(pos.x,pos.y,1), null);
            var b = S.Building().Create(tid, id, pos);
            b.FinishConstruct();
            return b;
        }

        private UnitInfo Unit(int pid, string id, NVector pos)
        {
            GameMgmt.Get().newMap.levels[pos.level].SetTile(new Vector3Int(pos.x,pos.y,1), null);
            var b = S.Unit().Create(pid, id, pos);
            b.FinishConstruct();
            return b;
        }

        private void Swamp(Player p, NVector pos)
        {
            GameMgmt.Get().newMap.levels[pos.level].SetTile(new Vector3Int(pos.x,pos.y,1), L.b.terrains["swamp"]);
            p.fog.Clear(pos);
        }
        
        
        public string ID()
        {
            return "tutorial3";
        }
    }
}