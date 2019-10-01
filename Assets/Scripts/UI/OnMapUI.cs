using Players;
using Scenario;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OnMapUI : MonoBehaviour
    {
        private static OnMapUI self;

        public UnitUI unitUI;
        public BuildingUI buildingUI;

        public static OnMapUI Get()
        {
            return self;
        }

        public TextMeshProUGUI ressround;

        public GameObject topButton;
        public Text topButtonText;
        
        public GameObject bottomButton;
        public Text bottomButtonText;
    
        // Start is called before the first frame update
        void Start()
        {
            self = this;
        }

        public void SetRessRoundMessage(string text)
        {
            ressround.text = text;
        }

        public void SetMenuMessage(string text)
        {
            topButtonText.text = text;
        }
    

        public void UpdatePanelXY(int x, int y)
        {
            
            //can view?
            if (Data.features.fog.Bool() && !PlayerMgmt.ActPlayer().fog.visible[x,y])
            {
                unitUI.UpdatePanel(null);
                buildingUI.UpdatePanel(null);
                return;
            }
            
            unitUI.UpdatePanel(UnitMgmt.At(x,y));
            buildingUI.UpdatePanel(BuildingMgmt.At(x,y));
        }

        public void UpdatePanelOnMouse()
        {
            Vector2 p = MapMgmt.GetMouseMapXY();
            int x = (int) p.x;
            int y = (int) p.y;

            if (!MapMgmt.Valide(x,y))
            {
                NAudio.PlayBuzzer();
                return;
            }
            
            UpdatePanelXY(x, y);
        
            //center mouse?
            if (Data.features.centermouse.Bool())
            {
                CameraMove.Get().MoveTo(x, y);
            }
        }
    }
}
