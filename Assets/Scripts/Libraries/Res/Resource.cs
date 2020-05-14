using System;
using System.Collections.Generic;

namespace Libraries.Res
{
    [Serializable]
    public class Resource : BaseData
    {
        public float price;
        public float weight;
        public bool special;
        public List<string> overlay;

        public Resource()
        {
            overlay = new List<string>();
        }
    }
}
