using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text MoneyUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void IncreaseMoneyUI(int amount)
    {
        MoneyUI.SetText(amount.ToString());
    }
}
