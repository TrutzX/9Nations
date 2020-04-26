using Audio;
using Buildings;
using Classes.Actions;
using InputActions;
using Libraries;
using Libraries.FActions;
using Players;
using Players.Infos;
using Tools;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class OnMapUI : MonoBehaviour, IMapUI
    {
        private static OnMapUI self;

        public UnitUI unitUI;
        public BuildingUI buildingUI;
        public InfoUI InfoUi;
        public BottomUI bottomUI;

        public GameObject menuButton;
        public Text menuButtonText;
        
        public GameObject bottomButton;
        public Text bottomButtonText;

        private BaseActiveAction _action;
        
        public static OnMapUI Get()
        {
            return self;
        }
    
        // Start is called before the first frame update
        void Start()
        {
            self = this;
            bottomUI = new BottomUI();
        }

        public void UpdatePanel(NVector pos)
        {
            //remove old action?
            if (_action != null)
                SetActiveAction(null,false);

            //can view?
            if (S.Fog() && !PlayerMgmt.ActPlayer().fog.Visible(pos))
            {
                unitUI.UpdatePanel(null);
                buildingUI.UpdatePanel(null);
                return;
            }
            
            unitUI.UpdatePanel(S.Unit().At(pos));
            buildingUI.UpdatePanel(S.Building().At(pos));
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
            if (LSys.tem.options["centermouse"].Bool())
            {
                S.CameraMove().MoveTo(pos);
            }
        }

        public void SetActiveAction(BaseActiveAction activeAction, bool building)
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

        public void ShowPanelMessage(string text)
        {
            menuButtonText.color = Color.white;
            menuButtonText.text = text;
        }
        
        public void ShowPanelMessageError(string text)
        {
            menuButtonText.color = Color.magenta;
            menuButtonText.text = text;
            NAudio.PlayBuzzer();
        }
    }
}
