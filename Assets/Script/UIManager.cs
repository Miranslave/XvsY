using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UIElements addable")]
    public TMP_Text uiText;
    public Slider slider;
    public Image uiImage;
    public bool displayBase;
    
    
    private bool haveText = false;
    private bool haveSlider = false;
    private bool haveImage = false;
    
    [SerializeField] private float CurrentValue;
    [SerializeField] private float TotalValue;

    public void Awake()
    {
        if (uiText)
        {
            haveText = true;
        }

        if (slider)
        {
            haveSlider = true;
        }

        if (uiImage)
        {
            haveImage = true;
        }

        if (!haveSlider && !haveText && !haveImage)
        {
            Debug.LogWarning("Care you don't have any UI elements setup in the "+gameObject.name);
        }
    }

    public void Innit(float basevalue)
    {
        TotalValue = basevalue;
        CurrentValue = TotalValue;
        if (haveSlider)
        {
            slider.minValue = 0;
            slider.maxValue = TotalValue;
            slider.value = TotalValue;
        }

        if (haveText)
        {
            if (displayBase)
            {
                //mettre le separator ici 
                uiText.SetText(basevalue+" / "+ TotalValue);
            }
            else
            {
                uiText.SetText(basevalue.ToString());
            }
        }
    }

    // Base function for the UI
    public void NewValue(float amount,Sprite new_sprt = null,String str_separator=null)
    {
        CurrentValue = amount;
        UpdateUI(CurrentValue,new_sprt,str_separator);
    }

    private void UpdateUI(float newvalue,Sprite new_sprt = null,String str=null)
    {
        if (haveText)
        {
            if (displayBase)
            {
                //mettre le separator ici 
                uiText.SetText(newvalue+" / "+ TotalValue);
            }
            else
            {
                uiText.SetText(newvalue.ToString());
            }
        }
        if (haveSlider)
        {
            slider.value = newvalue;
        }
        if (haveImage)
        {
            if(new_sprt)
                uiImage.sprite = new_sprt;
        }
    }
}
