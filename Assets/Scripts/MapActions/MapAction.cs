using System.Collections.Generic;
using reqs;

namespace DataTypes
{
    public partial class MapAction
    {
        
        public Dictionary<string,string> GenSelfReq()
        {
            return ReqHelper.GetReq(reqSelf1,reqSelf2,ap>0?"ap:>"+ap:"");
        }
        
        public Dictionary<string,string> GenNonSelfReq()
        {
            return ReqHelper.GetReq(reqNonSelf1,reqNonSelf2);
        }
    }
}