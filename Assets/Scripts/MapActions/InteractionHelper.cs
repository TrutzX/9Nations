using System.Collections.Generic;
using Actions;
using Buildings;
using DataTypes;
using Help;
using Libraries;
using Libraries.Units;
using Players;
using reqs;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace MapActions
{
    public class InteractionHelper
    {
        public static void ShowWindow(MapElementInfo self, MapElementInfo nonSelf)
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Interaction","Perform");

            DataUnit unit = L.b.units[self.data.type];
            
            foreach(MapAction m in Data.mapAction)
            {
                //can perform?
                //TODO exist?
                //if (!unit.action.actions.Exists(m.id))
                {
                    //continue;
                }

                Dictionary<string, string> reqs = m.GenSelfReq();
                if (ReqHelper.CheckOnlyFinal(PlayerMgmt.ActPlayer(), reqs, self, self.Pos()) && ReqHelper.CheckOnlyFinal(PlayerMgmt.ActPlayer(), m.GenNonSelfReq(), nonSelf, nonSelf.Pos()))
                {
                    MapActionSplitElement be = new MapActionSplitElement(m, self, nonSelf);
                    be.disabled = ReqHelper.Desc(PlayerMgmt.ActPlayer(), reqs, self, self.Pos()) ?? ReqHelper.Desc(PlayerMgmt.ActPlayer(), m.GenNonSelfReq(), nonSelf, nonSelf.Pos());
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