using DataTypes;
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

            b.Finish();
        }

        
    }
}