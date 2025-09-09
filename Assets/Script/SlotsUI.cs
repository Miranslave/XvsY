using System;
using System.Collections;
using System.Collections.Generic;
using Script;
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
    public List<Rollable> listToDraw;
    
    [Header("Slots machine Parameters")]
    [SerializeField] private float rollTime = 2f;          // durée totale du spin
    [SerializeField] private float interval = 0.1f;        // vitesse de changement de sprite
    [SerializeField] private float elapsed = 0f;
    
    
    // Current_G
    private GameObject g;
    [SerializeField] private List<Vector2> probsList; // Vector2.x is lower value Vector2.y is highest value


    public void Awake()
    {
        Normalizing(listToDraw);
    }

    // Statistics
    private void Normalizing(List<Rollable> list_r)
    {
        probsList = new List<Vector2>();
        float total = 0;
        foreach (var x in list_r)
        {
            total += x.probs;
        }

        if (total <100)
        {
            // divide and add
            Debug.Log("total sup à 100");
        }

        if (total > 100)
        {
            // normalize all shit
            Debug.Log("total sup à 100");
        }

        if (total == 100)
        {
            Debug.Log("100 pile");
            float str_ptr = 0;
            float end_ptr = 0;
            // perfect case 
            Vector2 initVector2 = new Vector2(str_ptr, list_r[0].probs);
            probsList.Add(initVector2);
            for(int i = 1; i<=list_r.Count-1;i++){
                // get higher end of i-1
                end_ptr = probsList[i - 1].y;
                Vector2 current_interval = new Vector2(end_ptr,end_ptr+list_r[i].probs);
                probsList.Add(current_interval);
            }
        }
    }
    
    
    
    
    
    public void LaunchSlot(GameObject gDrawn)
    {
        //launch the ROLLLLLLLS
        StartCoroutine(Roll(gDrawn));
    }

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

    IEnumerator Roll(GameObject gDrawn)
    {
        while (elapsed < rollTime)
        {
            // Choisit un sprite aléatoire dans la liste
            Sprite randomSprite = listToDraw[Random.Range(0, listToDraw.Count)].icon;
            
            // Mets à jour les 3 slots (optionnel : pour donner l’illusion que ça bouge)
            _result.sprite = randomSprite;
            if (_top) _top.sprite = listToDraw[Random.Range(0, listToDraw.Count)].icon;
            if (_bottom) _bottom.sprite = listToDraw[Random.Range(0, listToDraw.Count)].icon;

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        // Une fois terminé → fixe le sprite final
        SetEndSprite(gDrawn);
    }

    private void Reset()
    {
        elapsed = 0;
    }
}
