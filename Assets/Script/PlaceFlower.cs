using System;
using UnityEngine;

namespace Script
{
    public class PlaceFlower : MonoBehaviour
    {
        public GameObject prefab;
        private Flower _flower;
        private GameObject _grid;
        private GridManager _gridManager;
        private GameObject _player;
        private PlayerManager _playerManager;
        
        public void Start()
        {
            _grid = GameObject.FindGameObjectWithTag("Grid");
            _player = GameObject.FindGameObjectWithTag("Player");
            _flower = prefab.GetComponent<Flower>();
            _playerManager = _player.GetComponent<PlayerManager>();
            _gridManager = _grid.GetComponent<GridManager>();
        }

        public void ChangeCursor()
        {
            if (_playerManager.Money < _flower.cost)
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
