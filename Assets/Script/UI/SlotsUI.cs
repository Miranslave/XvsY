using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotsUI : MonoBehaviour
{
    
    [Header("SpritesRenderers")]
    [SerializeField] private SpriteRenderer _result;
    [SerializeField] private SpriteRenderer _top;
    [SerializeField] private SpriteRenderer _bottom;
    
    
    [Header("Rollables")]
    private List<Rollable> listToDraw;
    
    [Header("Slots machine Parameters")]
    [SerializeField] private float rollTime = 2f;          // durée totale du spin
    [SerializeField] private float interval = 0.1f;        // vitesse de changement de sprite
    [SerializeField] private float elapsed = 0f;
    [SerializeField] private bool Spinning = false;


    public object gDrawn;
    
    // Current_G
    private GameObject g;
   
    

    public void LaunchSlot(List<Rollable> rollables)
    {
        //launch the ROLLLLLLLS
        StartCoroutine(Roll(rollables));
    }

    public void StopSpin()
    {
        Spinning = false;
        SetEndSprite();
    }
    
    


    IEnumerator Roll(List<Rollable>rollables)
    {
        if (listToDraw == null)
        {
            listToDraw = rollables;
        }

        Spinning = true;
        while (Spinning)
        {
            // Choisit un sprite aléatoire dans la liste
            Sprite randomSprite = rollables[Random.Range(0, rollables.Count)].icon;
            
            // Mets à jour les 3 slots (optionnel : pour donner l’illusion que ça bouge)
            _result.sprite = randomSprite;
            if (_top) _top.sprite = rollables[Random.Range(0, rollables.Count)].icon;
            if (_bottom) _bottom.sprite = rollables[Random.Range(0, rollables.Count)].icon;
            yield return new WaitForSeconds(interval);
        }
       
    }
    

    public void SetEndSprite()
    {
        
        if (gDrawn == null) return;

        foreach (var vaRollable in listToDraw)
        {
            if ((gDrawn is GameObject go && vaRollable.prefab == go) ||
                (gDrawn is ScriptableObject so && vaRollable.effect == so))
            {
                _result.sprite = vaRollable.icon;
                return;
            }
        }
    }
    /*
    public void SetEndSprite(GameObject gDrawn)
    {
        Reset();
        if(!gDrawn) return;
        foreach (var vaRollable in listToDraw)
        {
            if (vaRollable.prefab == gDrawn)
            {
                _result.sprite = vaRollable.icon;
                return;
            }
        }
    }
    
    public void SetEndSprite(ScriptableObject gDrawn)
    {
        Reset();
        if(!gDrawn) return;
        foreach (var vaRollable in listToDraw)
        {
            if (vaRollable.effect == gDrawn)
            {
                _result.sprite = vaRollable.icon;
                return;
            }
        }
    }
    */

}
