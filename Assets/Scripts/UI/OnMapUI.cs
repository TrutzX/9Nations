using Actions;
using Maps;
using Players;
using Players.Infos;
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
        public InfoUI InfoUi;

        public GameObject topButton;
        public Text topButtonText;
        
        public GameObject bottomButton;
        public Text bottomButtonText;

        private ActiveAction _action;
        
        public static OnMapUI Get()
        {
            return self;
        }
    
        // Start is called before the first frame update
        void Start()
        {
            self = this;
        }

        public void SetMenuMessage(string text)
        {
            topButtonText.color = Color.white;
            topButtonText.text = text;
        }
        
        public void SetMenuMessageError(string text)
        {
            topButtonText.color = Color.magenta;
            topButtonText.text = text;
            NAudio.PlayBuzzer();
        }

        public void UpdatePanelXY(int x, int y)
        {
            //remove old action?
            if (_action != null)
                SetActiveAction(null,false);
            
            
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
            Vector2 p = GameMapMgmt.GetMouseMapXY();
            int x = (int) p.x;
            int y = (int) p.y;

            if (!GameMapMgmt.Valide(x,y))
            {
                NAudio.PlayBuzzer();
                return;
            }
            
            //active action?
            if (_action != null)
            {
                _action.Click(x,y);
            } else 
                UpdatePanelXY(x, y);
        
            //center mouse?
            if (Data.features.centermouse.Bool())
            {
                CameraMove.Get().MoveTo(x, y);
            }
        }

        public void SetActiveAction(ActiveAction activeAction, bool building)
        {
            //has an old action?
            if (_action != null)
            {
                _action.Remove();
            }
            
            _action = activeAction;

            if (building)
            {
                buildingUI.SetActiveAction(activeAction);
                return;
            }
            
            unitUI.SetActiveAction(activeAction);
        }
    }
}
