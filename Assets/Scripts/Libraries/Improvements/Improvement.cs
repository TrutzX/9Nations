using System;
using System.Collections.Generic;
using Game;
using Improvements;
using Tools;
using UI;
using UnityEngine;

namespace Libraries.Improvements
{
    [Serializable]
    public class Improvement : BaseData
    {
        public Dictionary<string, string> modi;
        public string file;
        public bool combine;

        public Improvement()
        {
            modi = new Dictionary<string, string>();
        }
        
        public override Sprite Sprite()
        {
            return SpriteHelper.Load(file+(combine?"14":""));
        }

        public Sprite CalcSprite(NVector pos)
        {
            if (!combine)
            {
                return Sprite();
            }
            
            string[,] i = GameMgmt.Get().data.map.levels[pos.level].improvement;
            bool north = !pos.DiffY(1).Valid() || i[pos.x,pos.y+1]==id;
            bool east = !pos.DiffX(1).Valid() || i[pos.x+1,pos.y]==id;
            bool south = !pos.DiffY(-1).Valid() || i[pos.x,pos.y-1]==id;
            bool west = !pos.DiffX(-1).Valid() || i[pos.x-1,pos.y]==id;
            
            //Debug.Log($"{pos} north:{north}, east:{east}, south:{south}, west:{west}");
            
            return SpriteHelper.Load(file+ImprovementHelper.GetId(north, east, south, west));
        }
        
        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            panel.AddModi("Modifiers",modi);
        }
    }
}
