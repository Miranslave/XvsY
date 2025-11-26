using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class LevelSelector : MonoBehaviour
{
    [Header("User actions input")]
    private InputActionSetBasic Controls;

    public Button[] Lvl_Buttons;

    private void Awake()
    {
        SetupInput();
        
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
        
    }
    
    
    
    
}
