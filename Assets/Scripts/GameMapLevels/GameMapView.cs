using Game;
using UI;
using UnityEngine;

namespace GameMapLevels
{
    public class GameMapView
    {
        private readonly GameMap _map;
        private int _activeLevel;

        public GameMapView(GameMap map)
        {
            this._map = map;
        }

        private bool Check(int level)
        {
            if (level < 0 || level >= _map.levels.Count)
            {
                OnMapUI.Get().SetMenuMessageError("This level does not exist.");
                return false;
            }

            return true;
        }

        public int ActiveLevel => _activeLevel;

        public void Init()
        {
            _activeLevel = GameMgmt.Get().data.map.standard;
            //which is the standard layer?
            for (int i = 0; i < _map.levels.Count; i++)
            {
                _map[i].gameObject.SetActive(_activeLevel==i);
            }
        }
        
        public void View(int level)
        {
            if (!Check(level)) return;
            if (level == _activeLevel) return;
            
            //hide active level
            _map[_activeLevel].gameObject.SetActive(false);
            _activeLevel = level;
            _map[_activeLevel].gameObject.SetActive(true);
            
        }

        public void ViewAdd(int addLevel)
        {
            View(_activeLevel+addLevel);
        }
    }
}