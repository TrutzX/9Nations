using System;

namespace Libraries.FActions
{
    [Serializable]
    public class FDataAction : BaseData
    {
        public int cost;
        public bool useUnderConstruction;
        public bool onlyOwner;
        public string sound;
        public bool mapElement;
        public string field;
        public string animation;
        public bool interaction;

        public FDataAction()
        {
            cost = 5;
            sound = "click";
            onlyOwner = true;
        }
    }
}