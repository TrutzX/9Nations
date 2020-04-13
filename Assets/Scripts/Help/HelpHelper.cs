
using Game;
using Improvements;
using Libraries;
using Libraries.Buildings;
using Libraries.Campaigns;
using Libraries.Elements;
using Libraries.FightModis;
using Libraries.Improvements;
using Libraries.Maps;
using Libraries.Res;
using Libraries.Rounds;
using Libraries.Terrains;
using Libraries.Units;
using Libraries.Usages;
using Maps;
using Options;
using UI;
using UnityEngine;

namespace Help
{
    public class HelpHelper
    {
        public static void ShowLexicon()
        {
            WindowTabBuilder t = WindowTabBuilder.Create("Lexicon");
            t.Add(new HelpSplitTab());
            t.Add(new LexiconSplitTab<DataUnit>(L.b.units));
            t.Add(new LexiconSplitTab<DataBuilding>(L.b.buildings));
            t.Add(new LexiconSplitTab<Resource>(L.b.res));
            t.Add(new LexiconSplitTab<DataTerrain>(L.b.terrains));
            t.Add(new LexiconSplitTab<Improvement>(L.b.improvements));
            t.Add(new LexiconSplitTab<DataMap>(LSys.tem.maps));
            t.Add(new LexiconSplitTab<Round>(L.b.rounds));
            t.Add(new LexiconSplitTab<Element>(L.b.elements));
            t.Add(new LexiconSplitTab<Usage>(L.b.usages));
            t.Add(new LexiconSplitTab<FightModi>(L.b.fightModis));

            if (S.Debug())
            {
                t.Add(new LexiconSplitTab<Campaign>(LSys.tem.campaigns));
                t.Add(new LexiconSplitTab<Scenario>(LSys.tem.scenarios));
            }
            
            t.Finish();
        }
    }
}