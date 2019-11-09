using System.Collections.Generic;
using Actions;
using Buildings;
using DataTypes;
using Help;
using Players;
using reqs;
using UI;
using UnityEngine;

namespace MapActions
{
    public class InteractionHelper
    {
        public static void ShowWindow(MapElementInfo self, MapElementInfo nonSelf)
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Interaction","Perform");

            Unit unit = Data.unit[self.data.type];
            
            foreach(MapAction m in Data.mapAction)
            {
                //can perform?
                if (!unit.GetMapActions().ContainsKey(m.id))
                {
                    continue;
                }

                Dictionary<string, string> reqs = m.GenSelfReq();
                if (ReqHelper.CheckOnlyFinal(PlayerMgmt.ActPlayer(), reqs, self, self.X(), self.Y()) && ReqHelper.CheckOnlyFinal(PlayerMgmt.ActPlayer(), m.GenNonSelfReq(), nonSelf, nonSelf.X(), nonSelf.Y()))
                {
                    MapActionSplitElement be = new MapActionSplitElement(m, self, nonSelf);
                    be.disabled = ReqHelper.Desc(PlayerMgmt.ActPlayer(), reqs, self, self.X(), self.Y()) ?? ReqHelper.Desc(PlayerMgmt.ActPlayer(), m.GenNonSelfReq(), nonSelf, nonSelf.X(), nonSelf.Y());
                    be.audioPerform = m.sound;
                    b.AddElement(be);
                    //win.AddBuilding(build.id);
                }
            }

            //is empty?
            if (b.ElementCount() == 0)
            {
                b.AddElement(new MapActionSplitElement(Data.mapAction.leave, self, nonSelf));
            }
            
            b.Finish();
        }

        
    }
    
   
}