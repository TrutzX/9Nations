using System;
using System.Collections.Generic;
using Buildings;
using Classes;
using Classes.Actions;
using JetBrains.Annotations;
using Libraries.FActions.General;
using Players;
using reqs;
using Tools;
using UnityEngine;
using UnityEngine.Assertions;

namespace Libraries.FActions
{
    [Serializable]  
    public class ActionHolder
    {
        public ActionEvent trigger;
        public bool triggerWait;
        public int triggerCount;
        public int cost;
        public string id;
        public ReqHolder req;
        public Dictionary<string, string> data;
        
        public ActionHolder()
        {
            req = new ReqHolder();
            data = new Dictionary<string, string>();
        }

        public BasePerformAction PerformAction()
        {
            return LClass.s.GetNewAction(id);
        }

        public FDataAction DataAction()
        {
            return L.b.actions[id];
        }
        
        public (bool remove, string mess) Perform(ActionEvent evt, Player player, [CanBeNull]MapElementInfo info, NVector pos)
        {
            bool remove = false;
            FDataAction action = DataAction();
            
            Assert.IsNotNull(action,$"Action {id} is missing.");
            Assert.IsNotNull(info,$"MapElementInfo is missing.");
            
            //has ap?
            if (cost > info.data.ap)
            {
                return (false, $"Action {action.Name()} need {cost - info.data.ap} AP more. Please wait a round to refill your AP.");
            }

            //check pref
            Debug.Log($"call {evt} {action.id} with {req.reqs.Count} reqs");
            //can use?
            if (!req.Check(player, info, pos))
            {
                return (false, req.Desc(player, info, pos));
            }
                
            //has a counter?
            if (triggerCount > 0)
            {
                triggerCount--;
                    
                //wait?
                if (triggerCount > 0 && triggerWait)
                {
                    return (false, null);
                }
                    
                //finish && remove?
                if (triggerCount <= 0)
                {
                    remove = true;
                }
            }

            info.data.ap -= cost;
            PerformAction().PerformCheck(evt, player, info, pos, this);
            return (remove, null);
        }
        
        public (bool remove, string mess) Perform(ActionEvent evt, Player player)
        {
            bool remove = false;
            FDataAction action = DataAction();
            
            Assert.IsNotNull(action,$"Action {id} is missing.");

            //check pref
            Debug.Log($"call {action.id}");
            //can use?
            if (!req.Check(player))
            {
                return (false, req.Desc(player));
            }
                
            //has a counter?
            if (triggerCount > 0)
            {
                triggerCount--;
                    
                //wait?
                if (triggerCount > 0 && triggerWait)
                {
                    return (false, null);
                }
                    
                //finish && remove?
                if (triggerCount <= 0)
                {
                    remove = true;
                }
            }

            PerformAction().PerformCheck(evt, player, this);
            return (remove, null);
        }

        public override string ToString()
        {
            return $"ActionHolder {id}";
        }
    }
}