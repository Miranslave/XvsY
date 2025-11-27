using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private InputActionSetBasic _controls;

    public InputActionSetBasic Controls => _controls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SetupInput();
    }
    
    void SetupInput()
    {
        _controls = new InputActionSetBasic();
        _controls.Enable();

        if (Mouse.current != null && !Mouse.current.enabled)
        {
            InputSystem.EnableDevice(Mouse.current);
            Debug.Log("Mouse device activé manuellement.");
        }
        
    }
}
