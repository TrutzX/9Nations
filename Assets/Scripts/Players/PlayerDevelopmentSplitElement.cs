using Libraries.Elements;
using UI;
using UI.Show;

namespace Players
{
    class PlayerDevelopmentSplitElement : SplitElement
    {
        private readonly Element _element;
        private readonly PlayerDevelopmentNation _pdn;
        
        public PlayerDevelopmentSplitElement(Element element, PlayerDevelopmentNation pdn) : base(element.Name(), element.Icon)
        {
            _element = element;
            _pdn = pdn;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            _element.ShowLexicon(panel);
        }

        public override void Perform()
        {
            _pdn.Develop(_element.id);
        }
    }
}