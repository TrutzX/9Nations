using System;
using UnityEngine;

namespace reqs
{
    public class ReqPlatform : BaseReqNeg
    {
        protected override bool CheckIntern(BaseReqArgument bra)
        {
            return Application.platform == Parse(bra);
        }

        private RuntimePlatform Parse(BaseReqArgument bra)
        {
            return (RuntimePlatform) Enum.Parse(typeof(RuntimePlatform), bra.sett, true);
        }
        
        protected override bool FinalIntern(BaseReqArgument bra)
        {
            return true;
        }

        protected override string DescIntern(BaseReqArgument bra)
        {
            return $"Need the platform {bra.sett}";
        }
    }
}