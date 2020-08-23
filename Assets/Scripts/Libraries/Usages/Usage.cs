using System;
using System.Collections.Generic;
using reqs;

namespace Libraries.Usages
{
    [Serializable]
    public class Usage : BaseData
    {
        public float rate;
        public int factor;

        public Usage()
        {
            factor = 1;
        }
    }
}
