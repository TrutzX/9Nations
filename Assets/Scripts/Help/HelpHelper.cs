using DataTypes;
using Improvements;
using InputAction;
using Libraries;
using Maps;
using Terrains;
using UI;
using UnityEngine;

namespace Help
{
    public class HelpHelper
    {
        public static void ShowHelpWindow()
        {
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Help window",null);

            foreach(DataTypes.Help h in Data.help)
            {
                b.AddElement(new HelpSplitElement(h));
            }
            
            b.AddElement(new InputOptionSplitElement());

            b.Finish();
        }

        public static void ShowLexicon()
        {
            WindowTabBuilder t = WindowTabBuilder.Create("Lexicon");
            t.Add(new HelpSplitTab());
            t.Add(new LexiconSplitTab<Improvement>(L.b.improvements));
            t.Add(new LexiconSplitTab<NMap>(L.b.maps));
            t.Add(new LexiconSplitTab<BTerrain>(L.b.terrain));
        
            t.Finish();
        }
    }
}