using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

namespace Script
{
    public class GridManager : MonoBehaviour
    {
        [Header("Grid Parameter")]
        [Min(1)]
        public int width = 3;
        [Min(1)]
        public int height = 3; 
        [Min(0)]
        public float cellWidth = 1;
        [Min(0)]
        public float cellHeight = 1;
        [Space(10)]
        
        [Header("Appearence")]
        public Sprite[] sprites;
        public GameObject prefab;
        public GameObject highlightPrefab;
        public GameObject highlightInstance;
        private GameObject _flowerToPlace;
        public bool _ishighlightcursor;
        
        
        [Space(10)]
        [Header("User actions input")]
        private InputActionSetBasic Controls;
        [Space(10)]
        [Header("(debug)")]
        public Color lineColor = Color.green;
        [SerializeField] private Vector3 offset;
        [SerializeField] private Vector3 position;
        [SerializeField] private Tile[,] _gridmemory;
        [SerializeField] private PlayerManager _playerManager;
        private PlaceUnit _placeUnit;
        
        private SpriteRenderer prefabSR;
        private Vector2 mouspos;
        private Vector2Int gridpos;
        
        public GridManager(Vector3 position)
        {
            this.position = position;
        }
        
        void OnDestroy()
        {
            // 🔌 Désabonnement propre
            Controls.Basic.Interract.performed -= interract;
            Controls.Basic.Place.performed -= place;
        }
        
        void Awake()
        {
            SetupInput();
        }

        void Start()
        {
            prefabSR = prefab.GetComponent<SpriteRenderer>();
            _playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
            _placeUnit = GameObject.FindGameObjectWithTag("PlaceUnit").GetComponent<PlaceUnit>();
            
            CalculateOffset();
            InitGridData();
            CreateHighlight();
        }

        void Update()
        {
            UpdateHighlight();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void CalculateOffset()
        {
            offset = new Vector2(width * cellWidth / 2f, height * cellHeight / 2f);
        }
        void InitGridData()
        {
            _gridmemory = new Tile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _gridmemory[x, y] = new Tile();
                }
            }
        }
        
        
        void CreateHighlight()
        {
            highlightInstance = Instantiate(highlightPrefab);
            highlightInstance.SetActive(false);

            SpriteRenderer sr = highlightInstance.GetComponent<SpriteRenderer>();
            Vector2 nativeSize = sr.sprite.bounds.size;

            Vector3 newScale = new Vector3(
                cellWidth / nativeSize.x,
                cellHeight / nativeSize.y,
                1f
            );
            _ishighlightcursor = true;
            highlightInstance.transform.localScale = newScale;
        }
        
        void UpdateHighlight()
        {
            mouspos = Controls.Basic.PointerPosition.ReadValue<Vector2>();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouspos);
            worldPos.z = 0f;

            if (GetGridIndexFromWorld(worldPos, out Vector2 pos))
            {
              
                    Vector3 highlightPos = new Vector3(
                        pos.x * cellWidth - offset.x + cellWidth / 2f + transform.position.x,
                        pos.y * cellHeight - offset.y + cellHeight / 2f + transform.position.y,
                        0
                    );

                    highlightInstance.SetActive(true);
                    highlightInstance.transform.position = highlightPos;
                

            }
            else
            {
                highlightInstance.SetActive(false);
               
            }
        }
        void SetupInput()
        {
            Controls = new InputActionSetBasic();
            Controls.Enable();

            if (Mouse.current != null && !Mouse.current.enabled)
            {
                InputSystem.EnableDevice(Mouse.current);
                Debug.Log("Mouse device activé manuellement.");
            }
            
            Controls.Basic.Interract.performed += interract;
            Controls.Basic.Place.performed += place;
        }
        
        
        bool GetGridIndexFromWorld(Vector3 worldPos,out Vector2 pos)
        {
            int x; 
            int y;
            Vector3 localPos = worldPos - transform.position + offset;
            // Décale par rapport au centre
           

            x = (int)(localPos.x / cellWidth);
            y = (int)(localPos.y / cellHeight);
            pos = new Vector2(x, y);
            gridpos = new Vector2Int(x, y);
            // Vérifie si on est bien dans la grille
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                return true;
            }

            x = -1; y = -1;
            pos = new Vector2(x, y);
            return false;
        }

        public void ChangeHighlight(GameObject g)
        {
            highlightInstance.GetComponent<SpriteRenderer>().sprite = g.GetComponentInChildren<SpriteRenderer>().sprite;
            _flowerToPlace = g;
            _ishighlightcursor = false;
        }

        public void Resetcursor()
        {
            _flowerToPlace = null;
            _placeUnit.CleanCurrentPrefab();
            highlightInstance.GetComponent<SpriteRenderer>().sprite =
                highlightPrefab.GetComponent<SpriteRenderer>().sprite;
            _ishighlightcursor = true;
        }
        
        public void interract(InputAction.CallbackContext context)
        {
            Debug.Log("keyboard");
        }
        


        public void place(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (_ishighlightcursor)
                {
                    Debug.LogError("this is just the highlight :V");
                    return;
                }
                
                if (highlightInstance == null)
                {
                    Debug.LogError("highlightInstance est null !");
                    return;
                }

                if (gridpos.x < 0 || gridpos.y < 0 || gridpos.x >= width || gridpos.y >= height)
                {
                    Debug.LogError($"gridpos hors grille : {gridpos}");
                    return;
                }

                Vector3 pos = highlightInstance.transform.position;
                GameObject g_incursor = _flowerToPlace; //Instantiate(_flowerToPlace,pos,Quaternion.identity);
                
                if (_gridmemory[gridpos.x, gridpos.y].IsOccupied)
                {
                    GameObject g_ingrid = _gridmemory[gridpos.x, gridpos.y].occupant;
                    //check if the same unit is there
                    if (CheckSameUnit(g_incursor,g_ingrid))
                    {
                        _gridmemory[gridpos.x, gridpos.y].occupant.GetComponent<Unit>().OnUpgrade();
                        Debug.LogError($"Tile à {gridpos} UPGRADE !");
                        Resetcursor();
                    }
                    else
                    {
                        Debug.LogError($"Tile à {gridpos} est occupé !");
                        return;
                    }
                }
                else
                {
                    g_incursor.SetActive(true);
                    g_incursor.transform.position = pos;
                    g_incursor.transform.rotation = Quaternion.identity;
                
                    _gridmemory[gridpos.x, gridpos.y].occupant = g_incursor;
                    Debug.Log($" cell {gridpos}");
                    Resetcursor();
                }


            }

        }


        bool CheckSameUnit(GameObject g_ingrid,GameObject g_incursor)
        {
            if (g_ingrid.name != g_incursor.name)
                return false;
            if (g_ingrid.GetComponent<BaseUnit>().weapon.name != g_incursor.GetComponent<BaseUnit>().weapon.name)
                return false;
            return true;
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = lineColor;

            Vector3 origin = this.transform.position;
            Vector3 offset  = new Vector3(width * cellWidth / 2f,height * cellHeight / 2f);

            // Lignes verticales
            for (int x = 0; x <= width; x++)
            {
                Vector3 start = new Vector3(x * cellWidth, 0, 0) + origin - offset;
                Vector3 end = start + new Vector3(0, height * cellHeight, 0);
                Gizmos.DrawLine(start, end);
            }

            // Lignes horizontales
            for (int y = 0; y <= height; y++)
            {
                Vector3 start = new Vector3(0, y * cellHeight, 0) + origin - offset;
                Vector3 end = start + new Vector3(width * cellWidth, 0, 0);
                Gizmos.DrawLine(start, end);
            }
        }
#endif
        
        
        
        
        /*
        void Awake()
        {
            Controls = new InputActionSetBasic();
            Controls.Enable();
            if (Mouse.current != null && !Mouse.current.enabled)
            {
                InputSystem.EnableDevice(Mouse.current);
                Debug.Log("Mouse device activé manuellement.");
            }
        }
        void Start()
        {
            prefabSR = prefab.GetComponent<SpriteRenderer>();
            offset = new Vector2(width * cellWidth / 2f,height * cellHeight / 2f);
            _gridmemory = new Tile[width, height];
            var bounds = prefabSR.bounds;



            //cellWidth = bounds.size.x;
            //cellHeight = bounds.size.y;
            highlightInstance = Instantiate(highlightPrefab);
            highlightInstance.SetActive(false);
            Vector2 spriteSize = highlightInstance.GetComponent<SpriteRenderer>().sprite.bounds.size;
            SpriteRenderer sr = highlightInstance.GetComponent<SpriteRenderer>();
            Vector2 nativeSize = sr.sprite.bounds.size;
            Vector3 newScale = new Vector3(
                cellWidth / nativeSize.x,
                cellHeight / nativeSize.y,
                1f
            );
            highlightInstance.transform.localScale = newScale;
            //Innit();

        }


        // Update is called once per frame
        void Update()
        {
            Vector2 pointerPos = Controls.Basic.PointerPosition.ReadValue<Vector2>();
            //Debug.Log($"PointerPosition = {pointerPos}");
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(pointerPos);
            worldPos.z = 0f; // Tu veux travailler en 2D uniquement

            // 3. Essayer de récupérer l’indice de case (x, y)
            int x, y;
            if (GetGridIndexFromWorld(worldPos, out x, out y))
            {

                Debug.Log($"Tu es sur la case : [{x}, {y}]");
                // On calcule la position logique de la case dans le monde
                Vector3 highlightPos = new Vector3(
                    x * cellWidth - offset.x + cellWidth / 2f + transform.position.x,
                    y * cellHeight - offset.y + cellHeight / 2f + transform.position.y,
                    0
                );

                highlightInstance.SetActive(true);
                highlightInstance.transform.position = highlightPos;
            }
            else
            {
                Debug.Log("La souris est hors grille");
                highlightInstance.SetActive(false);
            }
        }

        public void tracker(InputAction.CallbackContext context)
        {
             mouspos = Controls.Basic.PointerPosition.ReadValue<Vector2>();
            Debug.Log(mouspos);
        }
        public void interract(InputAction.CallbackContext context)
        {
            Debug.Log(context + "keyboard");
        }


        
        void Innit()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    if ((x+y) % 2 == 0)
                    {
                        prefabSR.sprite = sprites[0];
                    }
                    else
                    {
                        prefabSR.sprite = sprites[1];
                    }
                    Tile tempTile = new Tile();
                    tempTile.gridPosition = new Vector2(x * cellWidth - offset.x, y * cellHeight - offset.y);
                    tempTile.Innit(prefab);
                    _gridmemory[x, y] = tempTile;
                    GameObject g = Instantiate(tempTile.occupant, new Vector3(tempTile.gridPosition.x, tempTile.gridPosition.y, 0),Quaternion.identity);
                    g.name = "cell " + x +" - " + y;
                }
            }
        }*/
        
    }
}
