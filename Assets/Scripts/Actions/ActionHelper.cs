using System.Collections.Generic;
using DataTypes;
using Game;
using reqs;
using UnityEngine;

namespace Actions
{
    public class ActionHelper
    {
        public static Dictionary<string, string> GenReq(NAction action)
        {
            return ReqHelper.GetReq(action.req1,action.req2);
        }
    }
}