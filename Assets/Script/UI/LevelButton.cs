using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("Lvl Param")]
    [SerializeField] private string SceneName;
    [SerializeField] private Boolean locked;
    
    [Header("Lvl info")]
    [SerializeField] private string LvlNumber;
    [SerializeField] private string LvlName;
    [SerializeField] private string LvlDescritpion;
    [SerializeField] private Sprite[] IconsList;
    
    [Header("Components")]
    [SerializeField] private GameObject lvlPreview;
    [SerializeField] private TMP_Text _tmpTextLvlDescritpion;
    [SerializeField] private TMP_Text _tmpTextLvlName;
    [SerializeField] private GameObject IconPanel;
    private void Awake()
    {
        lvlPreview.SetActive(false);
        Innit();
        if (locked)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    private void Innit()
    {
        this.GetComponentInChildren<TMP_Text>().text = LvlNumber;
        _tmpTextLvlName.text = LvlName;
        _tmpTextLvlDescritpion.text = LvlDescritpion;
        var i = 0;
        // ajout d'une image par icon  mis pas la peine de les get flemme de codé laisse en dure atm 
        foreach (var sprite in IconsList)
        {
            GameObject go = new GameObject("Icon" + i++, typeof(RectTransform), typeof(Image));
            go.transform.SetParent(IconPanel.transform,false);
            Image img = go.GetComponent<Image>();
            img.preserveAspect = true;
            img.sprite = sprite;
        }
    }



    public void LaunchLevel()
    {
        SceneManager.LoadScene("BaseScene");// a remplacer par la var SceneName
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        lvlPreview.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        lvlPreview.SetActive(false);
    }
}

