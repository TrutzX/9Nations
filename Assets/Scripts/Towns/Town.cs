using System;
using System.Collections.Generic;
using System.Net;
using Buildings;
using Classes;

using Game;
using Help;
using Libraries;
using Libraries.Buildings;
using Libraries.Coats;
using Libraries.Overlays;
using Libraries.Res;
using Libraries.Usages;
using Players;
using Players.Infos;
using Players.Kingdoms;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Nation = Libraries.Nations.Nation;

namespace Towns
{
    [Serializable]
    public class Town
    {
        public string name;
        public string coat;
        public int playerId;
        public int id;
        public NVector pos;
        public int level;

        [SerializeField] private Dictionary<string, int> res;
        [SerializeField] public Dictionary<string, string> modi;
        [SerializeField] public string usageMess;
        public MapOverlay overlay;

        /// <summary>
        /// Only for loading
        /// </summary>
        public Town(){}
        
        public Town(int id, int playerId, string name, NVector pos)
        {
            this.id = id;
            this.playerId = playerId;
            this.name = name;
            this.pos = pos.Clone();
            level = 1;
            overlay = new MapOverlay();
            
            res = new Dictionary<string, int>();
            res["worker"] = 0;
            res["inhabitant"] = 0;
            modi = new Dictionary<string, string>();
            modi["produce"] = "125%";
        }

        public void AddRes(string id, int amount, ResType type)
        {
            if (!res.ContainsKey(id))
            {
                res[id] = amount;
            } else
                res[id] += amount;
            
            //add to points?
            if (type == ResType.Produce && amount > 0)
            {
                Player().points += (int) Math.Round(L.b.res[id].price * amount);
            }
            
            //check
            if (res[id] < 0)
            {
                throw new ArgumentException($"Res {id} is negative with {res[id]}");
            }
        }

        public int GetRes(string id)
        {
            return res.ContainsKey(id)?res[id]:0;
        }

        public void Evolve(int level)
        {
            this.level += level;
            
            //inform
            Player().info.Add(new Info($"Develop {name} to {GetTownLevelName()}","foundtown"));
        }
        
        public string GetTownLevelName()
        {
            if (level > Player().Nation().TownNameLevel.Count)
            {
                return $"Missing town level name ({level})";
            }
            return Player().Nation().TownNameLevel[level - 1];
        }

        public Sprite GetIcon()
        {
            //todo add color
            return SpriteHelper.Load("foundTown");
        }

        public void ShowInfo(PanelBuilder panel)
        {
            if (!ActPlayerIsOwner())
            {
                panel.AddImageLabel(name, GetIcon());
                panel.AddLabel(GetTownLevelName());
                return;
            }
            
            panel.AddInputRandom("town name", name, val => name = val,
                () => LClass.s.NameGenerator(Player().Nation().TownNameGenerator));

            panel.AddLabel(GetTownLevelName());
            panel.AddRes("inhabitant",$"{GetRes("inhabitant")}/{MaxInhabitantsAndWorker().maxInhabitants}");
            //panel.AddSubLabel(L.b.res["inhabitant"].name,$"{}/{}",L.b.res["inhabitant"].Icon);
            ShowRes(panel);
            panel.AddModi("Town wide modifications",modi);
        }

        public Coat Coat()
        {
            return L.b.coats[coat];
        }

        public void PreNextRound()
        {
            
        }

        public (int maxInhabitants, int buildingWorker) MaxInhabitantsAndWorker()
        {
            int maxInhabitants = 0;
            int buildWorker = 0;
            //calculate inhabitants
            foreach(BuildingInfo buildingInfo in GameMgmt.Get().building.GetByTown(id))
            {
                if (buildingInfo.IsUnderConstruction()) continue;
                
                int w = buildingInfo.dataBuilding.worker;
                if (w > 0) maxInhabitants += w;
                else buildWorker -= w;
            }

            return (maxInhabitants, buildWorker);
        }
        
        public void NextRound()
        {
            var n = MaxInhabitantsAndWorker();

            int inhabitant = GetRes("inhabitant");
            usageMess = null;
            
            decimal prod = n.buildingWorker==0?0:(decimal)inhabitant / n.buildingWorker;
            
            //overpop?
            if (inhabitant > n.maxInhabitants)
            {
                res["inhabitant"] = inhabitant = n.maxInhabitants;
            }
            
            //missing worker?
            if (inhabitant - n.buildingWorker < 0)
            {
                res["worker"] = 0;
                usageMess =
                    $"{GetTownLevelName()} {name} needs {n.buildingWorker - inhabitant} more workers. Productivity drops to {prod:P2}.";
                Player().info.Add(new Info(usageMess,"res"));
            }
            else
            {
                prod = Math.Min(ConvertHelper.Proc(modi["produce"])+Decimal.Parse("0.01"),Decimal.Parse("1.5"));
                res["worker"] = inhabitant-n.buildingWorker;
                
                //grow?
                if (inhabitant < n.maxInhabitants)
                {
                    inhabitant = Math.Min(n.maxInhabitants,inhabitant+(int)Math.Ceiling(res["worker"]/2f));
                    res["inhabitant"] = inhabitant;
                }
            }

            modi["produce"] = (int) (Math.Max(prod, Decimal.Parse("0.25")) * 100) + "%";
            
            CalcUsages();
        }

        private void CalcUsages()
        {
            decimal prod = ConvertHelper.Proc(modi["produce"]);
            int inhabitant = GetRes("inhabitant");
            //use usage
            foreach (Usage usage in L.b.usages.Values())
            {
                //can use?
                if (!usage.req.Check(Player()))
                {
                    continue;
                }

                int amount = (int) Math.Round(usage.rate * inhabitant);

                //need res?
                if (amount < 0 && amount * -1 > GetRes(usage.id))
                {
                    prod *= (GetRes(usage.id) * Decimal.One) / amount;
                    amount -= GetRes(usage.id);
                    AddRes(usage.id, GetRes(usage.id), ResType.Consum);
                    Resource r = L.b.res[usage.id];
                    usageMess =
                        $"The inhabitants from {GetTownLevelName()} {name} needs {amount} more {r.name}. Productivity drops to {prod:P2}.";
                    Player().info.Add(new Info(usageMess, r.Icon));
                }
                else
                {
                    AddRes(usage.id, amount, ResType.Produce);
                }
            }

            modi["produce"] = (int) (Math.Max(prod, Decimal.Parse("0.25")) * 100) + "%";
            //Debug.Log("prod"+modi["produce"]);
        }

        public void ShowRes(PanelBuilder panel)
        {
            if (res.Count > 0)
                panel.AddHeaderLabel("Resources");

            foreach (var r in res)
            {
                if (!L.b.res[r.Key].special)
                    panel.AddRes(r.Key,r.Value.ToString());
            }
        }

        public Player Player()
        {
            return PlayerMgmt.Get(playerId);
        }

        public bool ActPlayerIsOwner()
        {
            return playerId == PlayerMgmt.ActPlayerID();
        }
        
        public void ShowDetails()
        {
            WindowBuilderSplit wbs = WindowBuilderSplit.Create($"Details about {name}",null);
            wbs.Add(new TownGeneralSplitElement(this));
            if (S.Debug())
            {
                wbs.Add(new DebugTownSplitElement(this));
            }
            
            wbs.Add(new TownResSplitElement(this));
            wbs.Add(new CameraUnitSplitElement(wbs,this));
            wbs.Add(new CameraBuildingSplitElement(wbs,this));
            LSys.tem.helps.AddHelp("town", wbs);
            wbs.Finish();
        }
    }

    public enum ResType
    {
        Produce, Gift, Construction, Trade, Consum
    }
}