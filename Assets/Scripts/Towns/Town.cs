using System;
using System.Collections.Generic;
using System.Linq;
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
        public int usages;

        [SerializeField] private Dictionary<string, double> res;
        public RoundResStatistic resStatistic;
        
        public Dictionary<string, string> modi;
        public string usageMess;
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
            
            res = new Dictionary<string, double>();
            res[C.Worker] = 0;
            res[C.Inhabitant] = 0;
            resStatistic = new RoundResStatistic();
            resStatistic.NextRound();
            
            modi = new Dictionary<string, string>();
            
            if (L.b.gameOptions["usageTown"].Bool())
                modi["produce"] = "125%";
        }

        public void AddRes(string id, double amount, ResType type)
        {
            if (!res.ContainsKey(id))
            {
                res[id] = amount;
            } else
                res[id] += amount;
            
            //add to points?
            if (type == ResType.Produce)
            {
                if (amount > 0)
                    Player().points += (int) Math.Round(L.b.res[id].price * amount);
            }
                
            resStatistic.AddRess(id, amount, type);
            
            //check
            if (res[id] < 0)
            {
                throw new ArgumentException($"Res {id} is negative with {res[id]}");
            }
        }

        public int GetRes(string id)
        {
            return res.ContainsKey(id)?(int) res[id]:0;
        }

        public int GetCombineRes(string id)
        {
            int total = 0;
            bool known = false;
            foreach (var r in L.b.res.GetAllByCategory(id))
            {
                    if (res.ContainsKey(r.id))
                    {
                        Debug.Log("comb for "+id+" is "+r.id+" with "+res[r.id]);
                        total += (int) res[r.id];
                        known = true;
                    }
            }

            return !known?-1:total;
        }

        public void AddCombineRes(string id, double amount, ResType type)
        {
            //find list
            var parts = L.b.res.Values().Where(r => r.combine == id && KnowRes(r.id)).ToList();

            if (parts.Count == 0)
            {
                Debug.LogError($"Can not add combined res {id} with amount {amount}, because no res is known.");
                return;
            }
            
            //add it?
            if (amount > 0)
            {
                foreach (var r in parts)
                {
                    AddRes(r.id,amount / parts.Count,type);
                }
            }
            
            //remove it
            double par = amount / GetCombineRes(id);
            foreach (var r in parts)
            {
                AddRes(r.id,par*GetRes(id),type);
            }
        }

        public bool KnowRes(string id)
        {
            return res.ContainsKey(id);
        }

        public void Evolve(int l)
        {
            this.level += l;
            
            //inform
            Player().info.Add(new Info($"Develop {name} to {GetTownLevelName()}","foundTown"));
        }
        
        public string GetTownLevelName()
        {
            if (level > Player().Nation().TownNameLevel.Count)
            {
                return $"Missing town level name ({level})";
            }
            return Player().Nation().TownNameLevel[level - 1];
        }

        public string TownTitle()
        {
            return S.T("townTitle", GetTownLevelName(), name);
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
                panel.AddSubLabel("Owner",Player().name,Player().Coat().Icon);
                return;
            }
            
            panel.AddInputRandom("town name", name, val => name = val,
                () => LClass.s.NameGenerator(Player().Nation().TownNameGenerator));

            panel.AddLabel(GetTownLevelName());
            L.b.res[C.Inhabitant].AddImageLabel(panel, $"{GetRes(C.Inhabitant)}/{MaxInhabitantsAndWorker().maxInhabitants}");
            //panel.AddSubLabel(L.b.res["inhabitant"].name,$"{}/{}",L.b.res["inhabitant"].Icon);
            //ShowRes(panel);

            ShowCombineRes(panel);

            panel.AddModi(modi);
        }

        public void ShowCombineRes(PanelBuilder panel)
        {
            bool found = false;

            foreach (var r in L.b.res.Values())
            {
                if (String.IsNullOrEmpty(r.combine))
                    continue;

                var erg = GetCombineRes(r.combine);

                //found?
                if (erg >= 0)
                {
                    if (!found)
                        panel.AddHeaderLabelT("resources");

                    r.AddImageLabel(panel, erg);

                    found = true;
                }
            }
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
            usageMess = null;
            usages = 0;
            resStatistic.NextRound();
            UsageTown();
            InhabitantGrow();
        }

        private void InhabitantGrow()
        {
            var n = MaxInhabitantsAndWorker();
            
            //disabled?
            if (!L.b.gameOptions["inhabitantGrow"].Bool())
            {
                res[C.Inhabitant] = n.maxInhabitants;
                res[C.Worker] = Math.Max(0,n.maxInhabitants-n.buildingWorker);
                return;
            }
            
            //grow it
            int inhabitant = GetRes(C.Inhabitant);
            
            //overpop?
            if (inhabitant > n.maxInhabitants)
            {
                usageMess = S.T("inhabitantGrowOverPop", TownTitle(),
                    L.b.items[C.Inhabitant].Plural(inhabitant - n.maxInhabitants));
                Player().info.Add(new Info(usageMess,"inhabitant"));
                res[C.Inhabitant] -= level;
            }
            
            //can grow?
            if (n.maxInhabitants > inhabitant)
            {
                if (level >= usages)
                {
                    inhabitant += level;
                    res[C.Inhabitant] = Math.Min(n.maxInhabitants, inhabitant);
                }
                else
                {
                    usageMess = S.T("inhabitantGrowMissingUsage", TownTitle());
                    Player().info.Add(new Info(usageMess,"inhabitant"));
                }
            }
            
            //enough worker?
            if (n.buildingWorker > inhabitant)
            {
                modi["produce"] = (int) (Math.Max(inhabitant*1d/n.buildingWorker, 0.25d) * 100) + "%";
                usageMess = S.T("inhabitantGrowMissingWorker", TownTitle(), n.buildingWorker - inhabitant,
                    modi["produce"]);
                Player().info.Add(new Info(usageMess,"inhabitant"));
            }
            
            res[C.Worker] = Math.Max(0,inhabitant-n.buildingWorker);
        }

        private void UsageTown()
        {
            //disabled?
            if (!L.b.gameOptions["usageTown"].Bool())
            {
                if (res[C.Inhabitant] >= MaxInhabitantsAndWorker().buildingWorker)
                {
                    modi["produce"] = "100%";
                }
                return;
            }
            
            //find usage count
            foreach (Usage usage in L.b.usages.Values())
            {
                //can use?
                if (!usage.req.Check(Player()))
                {
                    continue;
                }

                usages += usage.factor;
            }

            //factor = wieviel muss erfüllt werden, pro Level kann eins ignoriert werden
            
            int inhabitant = GetRes(C.Inhabitant);
            //use usage
            foreach (Usage usage in L.b.usages.Values())
            {
                //can use?
                if (!usage.req.Check(Player()))
                {
                    continue;
                }

                var r = L.b.res[usage.id];
                
                int amount = (int) Math.Round(usage.rate * inhabitant);
                bool comb = !string.IsNullOrEmpty(r.combine);
                int hasAmount = comb?GetCombineRes(usage.id):GetRes(usage.id);
                
                    //need res?
                    if (amount < 0 && amount * -1 > hasAmount)
                    {
                        amount -= hasAmount;
                        if (comb)
                            AddCombineRes(usage.id, hasAmount, ResType.Consum);
                        else
                            AddRes(usage.id, hasAmount, ResType.Consum);
                        usageMess = S.T("UsageTownRes", TownTitle(), r.Text(amount));
                        Player().info.Add(new Info(usageMess, r.Icon));
                    }
                    else
                    {
                        //has enough
                        if (amount != 0)
                            if (comb)
                                AddCombineRes(usage.id, amount, ResType.Produce);
                            else
                                AddRes(usage.id, amount, ResType.Produce);
                        
                        usages -= 1;
                    }
                
            }
            
            if (level >= usages)
            {
                decimal prod = ConvertHelper.Proc(modi["produce"]);
                prod += (decimal) 0.01;
                prod = Math.Max(Decimal.Parse("0.25"), Math.Min(prod, Decimal.Parse("1.5")));
                modi["produce"] = (int) (prod * 100) + "%";
            }
            
            //Debug.Log("prod"+modi["produce"]);
        }

        public void ShowRes(PanelBuilder panel)
        {
            if (res.Count > 0)
                panel.AddHeaderLabelT("resources");

            foreach (var r in res)
            {
                var res = L.b.res[r.Key];
                if (!res.special)
                    res.AddImageLabel(panel, (int) r.Value);
            }
        }

        public Player Player()
        {
            return S.Player(playerId);
        }

        public bool ActPlayerIsOwner()
        {
            return playerId == S.ActPlayerID();
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

            if (L.b.gameOptions["usageTown"].Bool())
            {
                wbs.Add(new TownUsageSplitElement(this));
            }
            
            LSys.tem.helps.AddHelp("town", wbs);
            wbs.Finish();
        }
    }

    public enum ResType
    {
        Produce, Gift, Construction, Trade, Consum, Equip
    }
}