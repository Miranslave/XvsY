using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script
{
    public class PlaceUnit : MonoBehaviour
    {
        private GameObject _rolledUnitPrefab;
        public GameObject rolledUnitPrefab
        {
            get => _rolledUnitPrefab;
            set
            {
                if (_rolledUnitPrefab != value)
                {
                    _rolledUnitPrefab = value;
                    UpdatePreview();
                }
            }
        }
     
        
        
        public Image preview;
        public Sprite placeholder;
        private Unit _unit;
        private GameObject _grid;
        private GridManager _gridManager;
        private GameObject _player;
        private PlayerManager _playerManager;

        

  
        public void Start()
        {
            _grid = GameObject.FindGameObjectWithTag("Grid");
            _player = GameObject.FindGameObjectWithTag("Player");
            //_unit = prefab.GetComponent<Unit>();
            _playerManager = _player.GetComponent<PlayerManager>();
            _gridManager = _grid.GetComponent<GridManager>();
        }

        public void ChangeCursor()
        {
            if(rolledUnitPrefab)
                _gridManager.ChangeHighlight(rolledUnitPrefab);
        }

        public void CleanCurrentPrefab()
        {
            rolledUnitPrefab = null;
        }
        
        private void UpdatePreview()
        {
            if (preview == null)return;
            if (_rolledUnitPrefab == null)
            {
                preview.sprite = placeholder;
                //preview.color = Color.clear; // cache si pas d’unité
            }
            else
            {
                // Essaye de récupérer un sprite depuis ton prefab
                SpriteRenderer sr = _rolledUnitPrefab.GetComponentInChildren<SpriteRenderer>();
                if (sr != null)
                {
                    preview.sprite = sr.sprite;
                    preview.color = Color.white;
                }
            }
        }
        
        
    
    }
}
