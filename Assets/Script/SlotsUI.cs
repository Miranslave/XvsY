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

    public List<GameObject> WeightedListToDraw;
    // Current_G
    private GameObject g;
   // Vector2.x is lower value Vector2.y is highest value

    /*
    public void Awake()
    {
        Normalizing(listToDraw);
    }

    // Statistics
    private void Normalizing(List<Rollable> list_r)
    {
        List<float> list_probs = new List<float>();
        float total = 0;
        foreach (var x in list_r)
        {
            total += x.probs;
            list_probs.Add(x.probs);
        }

        var normalized_list = SetTo100sys(list_probs, total);
        WeightedListToDraw = GameObjectProbLinker(normalized_list);
    }

    
    private List<int> SetTo100sys(List<float> probsList,float total)
    {
        List<int> res = new List<int>();
        for (int i = 0; i < probsList.Count ; i++)
        {
            // 0 to 1 value
            float temp_val = probsList[i] / total;
            //normalized to 100 
            float norm_temp_val = temp_val * 100;
            // Gameobject number to create    
            int NormalizedNbGameObject = Mathf.RoundToInt(norm_temp_val);
            res.Add(NormalizedNbGameObject);
        }
        return res;
    }

    private List<GameObject> GameObjectProbLinker(List<int> probs)
    {
        List<GameObject> res = new List<GameObject>();
        for (int i = 0; i < listToDraw.Count ; i++)
        {
            GameObject temp = listToDraw[i].prefab;
            for (int j = 0; j < probs[i]; j++)
            {
                res.Add(temp);
            }
        }
        return res;
    }
    */
    
    public void LaunchSlot(GameObject gDrawn,List<Rollable> rollables)
    {
        //launch the ROLLLLLLLS
        StartCoroutine(Roll(gDrawn,rollables));
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

    IEnumerator Roll(GameObject gDrawn,List<Rollable>rollables)
    {
        if (listToDraw == null)
        {
            listToDraw = rollables;
        }
        while (elapsed < rollTime)
        {
            // Choisit un sprite aléatoire dans la liste
            Sprite randomSprite = rollables[Random.Range(0, rollables.Count)].icon;
            
            // Mets à jour les 3 slots (optionnel : pour donner l’illusion que ça bouge)
            _result.sprite = randomSprite;
            if (_top) _top.sprite = rollables[Random.Range(0, rollables.Count)].icon;
            if (_bottom) _bottom.sprite = rollables[Random.Range(0, rollables.Count)].icon;

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
