using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSelector : MonoBehaviour
{
    [Header("User actions input")]
    private InputActionSetBasic Controls;

    public Button[] Lvl_Buttons;

    


    public void BackMenu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }
    
    
    
}
