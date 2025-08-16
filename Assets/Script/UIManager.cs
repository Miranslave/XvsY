using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text MoneyUI;
  
    public void IncreaseMoneyUI(int amount)
    {
        MoneyUI.SetText(amount.ToString());
    }
}
