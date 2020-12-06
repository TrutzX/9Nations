using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Game;
using Libraries;
using Libraries.Coats;
using Libraries.Overlays;
using Libraries.Usages;
using MapElements.Buildings;
using Players;
using Players.Infos;
using Players.Kingdoms;
using Tools;
using UI;
using UnityEngine;

namespace Towns
{
    [Serializable]
    public class Town : IGameRoundObject
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
        public TownInfoMgmt info;

        /// <summary>
        /// Only for loading
        /// </summary>
        public Town()
        {
        }

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
            info = new TownInfoMgmt();
            info.town = this;

            //todo add dynamic
            if (L.b.gameOptions["usageTown"].Bool())
                modi["produce"] = "125%";
        }

        public void Transfer(int pid)
        {
            //remove units
            foreach (var unit in S.Unit().GetByTown(id))
            {
                unit.data.townId = -1;
            }
            
            var pn = S.Player(pid);
            var po = S.Player(playerId);
            //inform
            info.Add(new Info(S.T("occupyTransferOwner",TownTitle(), pn.name),pn.Coat().flag));
            playerId = pid;
            info.Add(new Info(S.T("occupyTransferEnemy",TownTitle(), po.name),po.Coat().flag));
            
        }
        
        public void AddRes(string re, double amount, ResType type)
        {
            //normal res
            if (String.IsNullOrEmpty(L.b.res[re].combine))
            {

                if (!res.ContainsKey(re))
                {
                    res[re] = amount;
                }
                else
                    res[re] += amount;

                //add to points?
                if (type == ResType.Produce)
                {
                    if (amount > 0)
                        Player().points += (int) Math.Round(L.b.res[re].price * amount);
                }

                resStatistic.AddRess(re, amount, type);

                //check
                if (res[re] < 0)
                {
                    throw new ArgumentException($"Res {re} is negative with {res[re]}");
                }
                return;
            }
            
            //combine res
            
            //find list
            var parts = L.b.res.GetAllByCategory(re).Where(r => KnowRes(r.id)).ToList();

            if (parts.Count == 0)
            {
                Debug.LogError($"Can not add combined res {re} with amount {amount}, because no res is known.");
                return;
            }

            //add it?
            if (amount > 0)
            {
                foreach (var r in parts)
                {
                    AddRes(r.id, amount / parts.Count, type);
                }
            }

            //remove it
            double par = amount / Math.Max(GetRes(re), 1);
            foreach (var r in parts)
            {
                AddRes(r.id, par * GetRes(re), type);
            }
        }

        public int GetRes(string re)
        {
            if (String.IsNullOrEmpty(L.b.res[re].combine))
                return res.ContainsKey(re) ? (int) res[re] : 0;
            
            int total = 0;
            foreach (var r in L.b.res.GetAllByCategory(re))
            {
                if (KnowRes(r.id))
                {
                    //Debug.Log("comb for " + id + " is " + r.id + " with " + res[r.id]);
                    total += (int) res[r.id];
                }
            }

            return total;
        }

        public bool KnowRes(string re)
        {
            if (String.IsNullOrEmpty(L.b.res[re].combine))
                return res.ContainsKey(re);
            return L.b.res.GetAllByCategory(re).Any(r => KnowRes(r.id));
        }

        public void Evolve(int l)
        {
            this.level += l;

            //inform
            Player().info.Add(new Info($"Develop {name} to {GetTownLevelName()}", "foundTown"));
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
                panel.AddSubLabel("Owner", Player().name, Player().Coat().Icon);
                return;
            }

            panel.AddInputRandom("town name", name, val => name = val,
                () => LClass.s.NameGenerator(Player().Nation().TownNameGenerator));

            panel.AddLabel(GetTownLevelName());
            L.b.res[C.Inhabitant]
                .AddImageLabel(panel, $"{GetRes(C.Inhabitant)}/{MaxInhabitantsAndWorker().maxInhabitants}");
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

                var erg = GetRes(r.combine);

                //found?
                if (!found)
                    panel.AddHeaderLabelT("resources");

                r.AddImageLabel(panel, erg);

                found = true;
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
            foreach (BuildingInfo buildingInfo in GameMgmt.Get().building.GetByTown(id))
            {
                if (buildingInfo.IsUnderConstruction()) continue;

                int w = buildingInfo.dataBuilding.worker;
                if (w > 0) maxInhabitants += w;
                else buildWorker -= w;
            }

            return (maxInhabitants, buildWorker);
        }

        public void StartRound()
        {
        }

        public IEnumerator FinishRound()
        {
            yield break;
        }

        public IEnumerator NextRound()
        {
            usageMess = null;
            usages = 0;
            info.NextRound();
            resStatistic.NextRound();
            UsageTown();
            InhabitantGrow();
            yield return null;
        }

        public void AfterLoad()
        {
            info.town = this;
        }

        private void InhabitantGrow()
        {
            var n = MaxInhabitantsAndWorker();

            //disabled?
            if (!L.b.gameOptions["inhabitantGrow"].Bool())
            {
                res[C.Inhabitant] = n.maxInhabitants;
                res[C.Worker] = Math.Max(0, n.maxInhabitants - n.buildingWorker);
                return;
            }

            //grow it
            int inhabitant = GetRes(C.Inhabitant);

            //overpop?
            if (inhabitant > n.maxInhabitants)
            {
                usageMess = S.T("inhabitantGrowOverPop", TownTitle(),
                    L.b.res[C.Inhabitant].Plural(inhabitant - n.maxInhabitants));
                Player().info.Add(new Info(usageMess, "inhabitant"));
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
                    Player().info.Add(new Info(usageMess, "inhabitant"));
                }
            }

            //enough worker?
            if (n.buildingWorker > inhabitant)
            {
                modi["produce"] = (int) (Math.Max(inhabitant * 1d / n.buildingWorker, 0.25d) * 100) + "%";
                usageMess = S.T("inhabitantGrowMissingWorker", TownTitle(), n.buildingWorker - inhabitant,
                    modi["produce"]);
                Player().info.Add(new Info(usageMess, "inhabitant"));
            }

            res[C.Worker] = Math.Max(0, inhabitant - n.buildingWorker);
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
                int hasAmount = GetRes(usage.id);

                //need res?
                if (amount < 0 && amount * -1 > hasAmount)
                {
                    amount -= hasAmount;
                    if (hasAmount > 0)
                        AddRes(r.id, hasAmount, ResType.Consum);
                    usageMess = S.T("UsageTownRes", TownTitle(), r.Text(amount * -1));
                    Player().info.Add(new Info(usageMess, r.Icon));
                }
                else
                {
                    //Debug.LogWarning(name+":"+usage.id+" "+r.id + "! "+comb+"! " + amount + "! " + hasAmount);

                    //has enough
                    if (amount != 0)
                        AddRes(r.id, amount, amount < 0 ? ResType.Consum : ResType.Produce);

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
            WindowBuilderSplit wbs = WindowBuilderSplit.Create($"Details about {name}", null);
            wbs.Add(new TownGeneralSplitElement(this));
            if (S.Debug())
            {
                wbs.Add(new DebugTownSplitElement(this));
            }

            wbs.Add(new TownResSplitElement(this));
            wbs.Add(new CameraUnitSplitElement(wbs, this));
            wbs.Add(new CameraBuildingSplitElement(wbs, this));
            wbs.Add(new InfosSplitElement(info));

            if (L.b.gameOptions["usageTown"].Bool())
            {
                wbs.Add(new TownUsageSplitElement(this));
            }

            LSys.tem.helps.AddHelp("town", wbs);
            wbs.Finish();
        }
    }
}