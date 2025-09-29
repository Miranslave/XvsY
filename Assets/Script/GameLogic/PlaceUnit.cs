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
       
        
        [Header("debug testing")]
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private GameObject _player;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private UnitFactory _unitFactory;
        

  
        public void Start()
        {
            _grid = GameObject.FindGameObjectWithTag("Grid");
            _unitFactory = GameObject.FindGameObjectWithTag("UnitFactory").GetComponent<UnitFactory>();
            _player = GameObject.FindGameObjectWithTag("Player");
            //_unit = prefab.GetComponent<Unit>();
            _playerManager = _player.GetComponent<PlayerManager>();
            _gridManager = _grid.GetComponent<GridManager>();
        }

        public void ChangeCursor()
        {
            if(rolledUnitPrefab)
                _gridManager.ChangeHighlight(rolledUnitPrefab,this);
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
