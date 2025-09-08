using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class SlotsUI : MonoBehaviour
{


    [SerializeField] private SpriteRenderer _result;
    [SerializeField] private SpriteRenderer _top;
    [SerializeField] private SpriteRenderer _bottom;
    public  List<GameObject> _list;
    private Dictionary<Weapon,Sprite> _sprtlist;
    private GameObject g;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inniticonList();
    }

    private void inniticonList()
    {
        _sprtlist = new Dictionary<Weapon, Sprite>();
        foreach (var variabWeapon in _list)
        {
            _sprtlist.Add(variabWeapon.GetComponent<Weapon>(),variabWeapon.GetComponent<Weapon>().Icon1);
        }
    }
    
    public void LaunchSlot(GameObject weapon)
    {
        g = weapon;
        StartCoroutine(Roll(weapon.GetComponent<Weapon>()));
    }

    public void SetEndSprite(Weapon w)
    {
        if(!g) return;
        _result.sprite = _sprtlist[w];

    }

    IEnumerator Roll(Weapon w)
    {
        float rollTime = 2f;          // durée totale du spin
        float interval = 0.1f;        // vitesse de changement de sprite
        float elapsed = 0f;

        while (elapsed < rollTime)
        {
            // Choisit un sprite aléatoire dans la liste
            Sprite randomSprite = _list[Random.Range(0, _list.Count)].GetComponent<Weapon>().Icon1;

            // Mets à jour les 3 slots (optionnel : pour donner l’illusion que ça bouge)
            _result.sprite = randomSprite;
            if (_top) _top.sprite = _list[Random.Range(0, _list.Count)].GetComponent<Weapon>().Icon1;
            if (_bottom) _bottom.sprite = _list[Random.Range(0, _list.Count)].GetComponent<Weapon>().Icon1;

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        // Une fois terminé → fixe le sprite final
        SetEndSprite(w);
    }
    


}
