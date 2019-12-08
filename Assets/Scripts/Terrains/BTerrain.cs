using System;
using System.Collections.Generic;
using Libraries;
using UI;
using UnityEngine;

namespace Terrains
{
    [Serializable]
    public class BTerrain : BaseData
    {
        public int Walk;
        public int Fly;
        public int Swim;
        public int DefaultTile;
        public string Winter;
        public string Category;
        public Dictionary<string, string> Modi;

        public BTerrain()
        {
            Walk = 10;
            Fly = 10;
            Swim = 10;
            DefaultTile = -1;
            Modi = new Dictionary<string, string>();
        }
        
        public override void ShowDetail(PanelBuilder panel)
        {
            base.ShowDetail(panel);
            if (!string.IsNullOrEmpty(Winter))
            {
                BTerrain w = L.b.terrain[Winter];
                panel.AddImageLabel($"Winter: {w.Name}", w.Sprite());
            }
            panel.AddHeaderLabel("Movement cost");
            panel.AddLabel("Walk: "+(Walk==0?"not passable":$"{Walk} AP"));
            panel.AddLabel($"Fly: "+(Fly==0?"not passable":$"{Fly} AP"));
            panel.AddLabel($"Swim: "+(Swim==0?"not passable":$"{Swim} AP"));
            panel.AddModi("Modifiers",Modi);
        }
        
        public int MoveCost(string moveTyp)
        {
            return moveTyp=="walk"?Walk:moveTyp=="fly"?Fly:Swim;
        }
    }
}