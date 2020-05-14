using System;
using Libraries.Maps;

namespace Libraries.Overlays
{
    [Serializable]
    public class OverlayMgmt : BaseMgmt<Overlay>
    {
        public OverlayMgmt() : base("overlay") { }
        
    }
}