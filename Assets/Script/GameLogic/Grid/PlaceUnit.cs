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
     
        
        
        public Image Race_preview;
        public Image Weapon_preview;
        public Image Capacity_preview;
        public Sprite placeholder;
        public Sprite weaponplaceholder;
        public Sprite capacityplaceholder;
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
            if (Race_preview == null || Weapon_preview == null)return;
            if (_rolledUnitPrefab == null)
            {
                Race_preview.sprite = placeholder;
                Weapon_preview.sprite = weaponplaceholder;
                Capacity_preview.sprite = capacityplaceholder;
                //preview.color = Color.clear; // cache si pas d’unité
            }
            else
            {
                // Essaye de récupérer un sprite depuis ton prefab
                Unit u;
                if (u = _rolledUnitPrefab.GetComponent<Unit>())
                {
                    Race_preview.sprite = u.icon;
                    Weapon_preview.sprite = u.weapon.Icon1;
                    
                    Capacity_preview.sprite = u.specialCapacity.Icon;
                    Race_preview.color = Color.white;
                }
            }
        }
        
        
    
    }
}
