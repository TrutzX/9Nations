using Buildings;
using Libraries.FActions;
using Players;
using Players.Infos;
using Tools;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Game
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

        public void UpdatePanel(NVector pos)
        {
            //remove old action?
            if (_action != null)
                SetActiveAction(null,false);

            //can view?
            if (Data.features.fog.Bool() && !PlayerMgmt.ActPlayer().fog.Visible(pos))
            {
                unitUI.UpdatePanel(null);
                buildingUI.UpdatePanel(null);
                return;
            }
            
            unitUI.UpdatePanel(S.Unit().At(pos));
            buildingUI.UpdatePanel(BuildingMgmt.At(pos));
        }

        public void UpdatePanelOnMouse()
        {
            Vector2 p = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            NVector pos = new NVector((int) p.x, (int) p.y, GameMgmt.Get().newMap.view.ActiveLevel);
            Debug.Log($"Click on {pos}");

            if (!pos.Valid())
            {
                NAudio.PlayBuzzer();
                return;
            }
            
            //active action?
            if (_action != null)
            {
                _action.Click(pos);
            } else 
                UpdatePanel(pos);
        
            //center mouse?
            if (Data.features.centermouse.Bool())
            {
                CameraMove.Get().MoveTo(pos);
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
