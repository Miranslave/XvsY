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
    private Camera _camera;

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

    public void AddMoney(int toadd)
    {
        Money += toadd;
    }

    void OnMoneyChanged()
    {
        _uiMoneyManager.NewValue(Money);
    }

    void TakeDmg()
    {
        
    }
}
