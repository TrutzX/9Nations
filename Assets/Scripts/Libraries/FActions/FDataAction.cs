using System;
using reqs;

namespace Libraries.FActions.General
{
    [Serializable]
    public class FDataAction : BaseData
    {
        public int cost;
        public bool useUnderConstruction;
        public bool onlyOwner;
        public string sound;

        public FDataAction()
        {
            cost = 5;
            sound = "click";
            onlyOwner = true;
        }
    }
}