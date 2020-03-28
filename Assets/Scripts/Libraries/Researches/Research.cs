using System;
using System.Collections.Generic;
using reqs;

namespace Libraries.Researches
{
    [Serializable]
    public class Research : BaseData
    {
        public List<string> elements;
        
        public Research()
        {
            elements = new List<string>();
        }
    
    }
}
