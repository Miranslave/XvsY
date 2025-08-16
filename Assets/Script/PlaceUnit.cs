using System;
using UnityEngine;

namespace Script
{
    public class PlaceUnit : MonoBehaviour
    {
        public GameObject prefab;
        private Unit _unit;
        private GameObject _grid;
        private GridManager _gridManager;
        private GameObject _player;
        private PlayerManager _playerManager;
        
        public void Start()
        {
            _grid = GameObject.FindGameObjectWithTag("Grid");
            _player = GameObject.FindGameObjectWithTag("Player");
            _unit = prefab.GetComponent<Unit>();
            _playerManager = _player.GetComponent<PlayerManager>();
            _gridManager = _grid.GetComponent<GridManager>();
        }

        public void ChangeCursor()
        {
            if (_playerManager.Money < _unit.cost)
            {
                Debug.Log("No money");
                return;
            }
            else
            {
                _gridManager.ChangeHighlight(prefab);
            }
            
        }
        
    
    }
}
