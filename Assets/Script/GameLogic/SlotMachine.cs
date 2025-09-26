using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Script;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotMachine : MonoBehaviour
{
    ///public GameObject BaseUnit;
    
    [Header("UI Elements")]
    public GameObject raceSpinUI;
    public GameObject weaponSpinUI;
    
    [Header("Rollables")] 
    [SerializeField] private List<Rollable> RaceListToDraw;
    [SerializeField] private List<Rollable> WeaponListToDraw;
    [SerializeField] private List<Rollable> AbilitiesListToDraw;

    [Header("Slots machine Animator")] 
    [SerializeField] private Animator _animator;
    /*[SerializeField] private float rollTime = 2f;          // durée totale du spin
    [SerializeField] private float interval = 0.1f;        // vitesse de changement de sprite
    [SerializeField] private float elapsed = 0f;*/
    
    [Header("Timing")]
    public float delayBetweenStops = 0.5f; // délai entre chaque arrêt
    
    [Header("Debug")] 
    public UnitFactory factory;

    public PlayerManager playerManager;
    [SerializeField] private List<GameObject> RaceWeightedListToDraw;
    [SerializeField] private List<GameObject> WeaponWeightedListToDraw;
    [SerializeField] private List<GameObject> AbilitiesWeightedListToDraw;
    
    private void Awake()
    {
        RaceWeightedListToDraw = Normalizing(RaceListToDraw);
        WeaponWeightedListToDraw = Normalizing(WeaponListToDraw);
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public void StartSpin()
    {
        if (playerManager.Money > 50)
        {
            playerManager.AddMoney(-50);
            _animator.SetTrigger("SlotStart");
            StartCoroutine(SpinCoroutine());
            if (factory.PlaceUnit.rolledUnitPrefab)
            {
                Destroy(factory.PlaceUnit.rolledUnitPrefab);
            }
        }

    }

    private IEnumerator SpinCoroutine()
    {
        GameObject raceResult = null;
        GameObject weaponResult = null;
        //GameObject abilityResult = null;
        // Lancer chaque roue en parallèle
        Coroutine raceSpin = StartCoroutine(SpinWheel(raceSpinUI,RaceWeightedListToDraw,RaceListToDraw, g => raceResult = g));
        yield return new WaitForSeconds(delayBetweenStops);

        Coroutine weaponSpin = StartCoroutine(SpinWheel(weaponSpinUI,WeaponWeightedListToDraw,WeaponListToDraw,g => weaponResult = g));
        yield return new WaitForSeconds(delayBetweenStops);
        //Coroutine abilitySpin = StartCoroutine(SpinWheel(abilityText, abilities, 1f, g => abilityResult = g));

        // Attendre que tout soit fini
        yield return raceSpin;
        yield return weaponSpin;
        //yield return abilitySpin;
        
        factory.Assemble(raceResult,weaponResult);

        
    }
    
    private IEnumerator SpinWheel(GameObject uiGameObject,List<GameObject> GameObjectWeightedList,List<Rollable> rollables,Action<GameObject> onFinished)
    {
        GameObject Choice = null;
        Choice = GameObjectWeightedList[Random.Range(0, GameObjectWeightedList.Count)];
        
        if (uiGameObject)
        {
            uiGameObject.GetComponent<SlotsUI>().LaunchSlot(Choice,rollables);
        }
        else
        {
            Debug.LogError("UI GAMEOBJECT NOT FOUND");
            yield break;
        }
        onFinished?.Invoke(Choice);
    }
    
    
    // Pure Statistics Luck Base pure RNG BALATRO CODED SHIT I DID
    
    // Statistics
    private List<GameObject> Normalizing(List<Rollable> list_r)
    {
        List<float> list_probs = new List<float>();
        float total = 0;
        foreach (var x in list_r)
        {
            total += x.probs;
            list_probs.Add(x.probs);
        }
        
        var normalized_list = SetTo100sys(list_probs, total);
        if (normalized_list.Count == 0)
        {
            Debug.LogError("Error normalized list is empty");
        }
        return GameObjectProbLinker(normalized_list,list_r);
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

    private List<GameObject> GameObjectProbLinker(List<int> probs,List<Rollable> rollables)
    {
        List<GameObject> res = new List<GameObject>();
        for (int i = 0; i < rollables.Count ; i++)
        {
            GameObject temp = rollables[i].prefab;
            for (int j = 0; j < probs[i]; j++)
            {
                res.Add(temp);
            }
        }
        return res;
    }

    public void SetEndOfRoll()
    {
        _animator.SetTrigger("SlotEnd");
    }
}
