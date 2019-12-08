using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Tools;
using UI;
using UnityEngine;

namespace Improvements
{
    [Serializable]
    public class Improvement : BaseData
    {
        public Dictionary<string, string> Modi;
        public string File;
        public bool Combine;

        public Improvement()
        {
            Modi = new Dictionary<string, string>();
        }
        
        public override Sprite Sprite()
        {
            return SpriteHelper.Load(File+(Combine?"14":""));
        }

        public Sprite CalcSprite(Vector3Int pos)
        {
            if (!Combine)
            {
                return Sprite();
            }
            
            string[,] i = GameMgmt.Get().data.map.levels[pos.z].Improvement;
            bool north = !GameHelper.Valide(VectorHelper.Add(pos,0,1)) || i[pos.x,pos.y+1]==Id;
            bool east = !GameHelper.Valide(VectorHelper.Add(pos,1,0)) || i[pos.x+1,pos.y]==Id;
            bool south = !GameHelper.Valide(VectorHelper.Add(pos,0,-1)) || i[pos.x,pos.y-1]==Id;
            bool west = !GameHelper.Valide(VectorHelper.Add(pos,-1,0)) || i[pos.x-1,pos.y]==Id;
            
            //Debug.Log($"{pos} north:{north}, east:{east}, south:{south}, west:{west}");
            
            return SpriteHelper.Load(File+ImprovementHelper.GetId(north, east, south, west));
        }
        
        public override void ShowDetail(PanelBuilder panel)
        {
            base.ShowDetail(panel);
            panel.AddModi("Modifiers",Modi);
        }
    }
}
