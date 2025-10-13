using Script;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int _money;
    [SerializeField] private HealthComponent _healthComponent;
    public int Money
    {
        get => _money;
        set
        {
            if (_money != value)
            {
                _money = value;
                OnMoneyChanged();
            }
        }
    }

    [SerializeField] private GridManager _gridManager;
    [SerializeField] private UIManager _uiMoneyManager;
    [SerializeField] private SlotMachine _slotMachine;
    [Header("User actions input")]
    private InputActionSetBasic Controls;
    private Camera _camera;

    
    void OnDestroy()
    {
        // 🔌 Désabonnement propre
        Controls.Basic.Interract.performed -= interract;
    }
        
    void Awake()
    {
        SetupInput();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _money = Money;
        _camera = Camera.main;
        _gridManager = FindFirstObjectByType<GridManager>();
        _uiMoneyManager.Innit(Money);
    }

    // Update is called once per frame
    void Update()
    {
        MouseAction();
    }


    void MouseAction()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mouseWorldPos = _camera!.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPos.z = 0f;
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Currency") )
            {
                Currency currency = hit.collider.gameObject.gameObject.GetComponent<Currency>();
                currency.OnClicked(this);
            }
        }
    }
    
    
    
    void SetupInput()
    {
        Controls = new InputActionSetBasic();
        Controls.Enable();
        Controls.Basic.Interract.performed += interract;
    }
    public void interract(InputAction.CallbackContext context)
    {
        Debug.Log("keyboard stop slots");
        
        _slotMachine.StopWheel();
    }

    public void AddMoney(int toadd)
    {
        Money += toadd;
    }

    void OnMoneyChanged()
    {
        _uiMoneyManager.NewValue(Money);
    }

    public void TakeDmg(float dmg)
    {
        _healthComponent.TakeDamage(dmg);
    }
    
}
