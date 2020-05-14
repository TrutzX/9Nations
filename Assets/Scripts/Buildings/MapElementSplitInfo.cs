using System.Linq;
using Players;
using UI;
using UI.Show;

namespace Buildings
{
    class MapElementSplitInfo : SplitElement
    {
        private readonly MapElementInfo _info;
        
        public MapElementSplitInfo(MapElementInfo info) : base(info.name,info.baseData.Sprite())
        {
            this._info = info;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddImageLabel(_info.name, _info.Sprite());
            panel.AddHeaderLabel("Information");
            //diff unit?
            if (!_info.Owner(PlayerMgmt.ActPlayerID()))
            {
                panel.AddSubLabel("Owner",_info.Player().name, _info.Player().Coat().Icon);
                panel.AddSubLabel("HP",$"??/{_info.baseData.hp}","hp");
                panel.AddSubLabel("AP",$"??/{_info.baseData.ap}","ap");
                return;
            }
            
            panel.RichText(_info.data.lastInfo);
            panel.AddSubLabel("HP",$"{_info.data.hp}/{_info.data.hpMax}","hp");
            panel.AddSubLabel("AP",$"{_info.data.ap}/{_info.data.apMax}","ap");
            
            Construction con = _info.GetComponent<Construction>();
            if (con != null)
            {
                panel.AddRes("Under construction",_info.data.construction.ToDictionary(entry => entry.Key,entry => entry.Value));
                panel.AddLabel("Missing resources");
            }
        }

        public override void Perform()
        {
        }
    }
}