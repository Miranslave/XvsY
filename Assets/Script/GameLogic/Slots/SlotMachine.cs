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
    public SlotsUI raceSpinUI;
    public SlotsUI weaponSpinUI;
    public SlotsUI abilitySpinUI;
    
    [Header("Rollables")] 
    [SerializeField] private List<Rollable> RaceListToDraw;
    [SerializeField] private List<Rollable> WeaponListToDraw;
    [SerializeField] private List<Rollable> AbilitiesListToDraw;

    [Header("Slots machine Unit Constructed")]
    [SerializeField] private GameObject raceResult = null;
    [SerializeField] private GameObject weaponResult = null;
    [SerializeField] private SpecialCapacity abilityResult = null;
    //[SerializeField] private Animator _animator;
    /*[SerializeField] private float rollTime = 2f;          // durée totale du spin
    [SerializeField] private float interval = 0.1f;        // vitesse de changement de sprite
    [SerializeField] private float elapsed = 0f;*/
    
    [Header("Timing")]
    public float delayBetweenStops = 0.5f; // délai entre chaque arrêt

    [Header("Debug")] 
    [SerializeField] private GameObject slotmachine_display;
    [SerializeField] private List<SlotsUI> _slotsUis;
    public PresentationBandManager _presentationBandManager;
    
    public bool DuringASpin = false;
    

    [SerializeField] private int slotUi_index_to_stop = 0;
    public UnitFactory factory;
    public PlaceUnitManager placeManager;
    public PlayerManager playerManager;
    
    
    [SerializeField] private List<GameObject> RaceWeightedListToDraw;
    [SerializeField] private List<GameObject> WeaponWeightedListToDraw;
    [SerializeField] private List<SpecialCapacity> AbilitiesWeightedListToDraw;
    [SerializeField] private PausingManager _pausingManager;
    
    private void Awake()
    {
        _slotsUis.Add(raceSpinUI);
        _slotsUis.Add(weaponSpinUI);
        _slotsUis.Add(abilitySpinUI);
        RaceWeightedListToDraw = Normalizing(RaceListToDraw);
        WeaponWeightedListToDraw = Normalizing(WeaponListToDraw);
        AbilitiesWeightedListToDraw = NormalizingScriptable(AbilitiesListToDraw);
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public void StartSpin()
    {
        if (playerManager.Money > 50 && !DuringASpin)
        {

            slotmachine_display.SetActive(true);
            PlaceUnit p;
            if (placeManager.GetFirstUnusedPlaceUnit(out p))
            {
                factory.PlaceUnit_current = p;
                playerManager.AddMoney(-50);
                //_animator.SetTrigger("SlotStart");
                //StartCoroutine(SpinCoroutine());
                trySpinWheel(raceSpinUI,RaceListToDraw);
                raceSpinUI.SetOutlineShader();
                trySpinWheel(weaponSpinUI,WeaponListToDraw);
                trySpinWheel(abilitySpinUI,AbilitiesListToDraw);
                DuringASpin = true;
                GameEvents.RequestPause();
            }
            else
            {
                Debug.LogWarning("not a single slot available");
            }
        }
        else
        {
            Debug.LogWarning("Nicky minaj no Money JUSTINNNNNNNNNNNNNN ");
        }

    }

    private void trySpinWheel(SlotsUI sui,List<Rollable> rollables)
    {
        
        DuringASpin = true;
        sui.LaunchSlot(rollables);
    }

    
   
    public void StopWheel()
    {
        if (!DuringASpin)
        {
            return;
        }
        SlotsUI temp_slotUi = _slotsUis[slotUi_index_to_stop % _slotsUis.Count];
        if (slotUi_index_to_stop == 0)
        {
            temp_slotUi.gDrawn = drawthing(RaceWeightedListToDraw);
            raceResult = (GameObject)temp_slotUi.gDrawn;
            weaponSpinUI.SetOutlineShader();
        } else if(slotUi_index_to_stop == 1)
        {
            temp_slotUi.gDrawn = drawthing(WeaponWeightedListToDraw);
            weaponResult = (GameObject)temp_slotUi.gDrawn;
            abilitySpinUI.SetOutlineShader();
        }else if (slotUi_index_to_stop == 2)
        {
            temp_slotUi.gDrawn = drawthing(WeaponWeightedListToDraw,AbilitiesWeightedListToDraw);
            abilityResult = (SpecialCapacity)temp_slotUi.gDrawn;
        }
        _slotsUis[slotUi_index_to_stop%_slotsUis.Count].StopSpin();
        if (slotUi_index_to_stop%_slotsUis.Count == 2)
        {
            DuringASpin = false;
            BaseUnit CreatedUnit = factory.Assemble(raceResult,weaponResult,abilityResult);
            StartCoroutine(WaitAndHideSlotMachine());
            slotUi_index_to_stop = 0;
            
            // not working as intended
            // Check if unit is new 
            //bool newUnitProc = playerManager.CheckIfNewUnit(factory.toSend.GetComponent<Unit>());       

            bool newUnitProc = playerManager.CheckIfNewUnit(CreatedUnit);
            if (newUnitProc)
            {
                SpriteRenderer srU = CreatedUnit.spriteRenderer;
                SpriteRenderer srW = CreatedUnit.weapon._spriteRenderer;
                _presentationBandManager.new_unit = srU.sprite;
                _presentationBandManager.new_weapon = srW.sprite;
                _presentationBandManager.Innit();
                playerManager.AddANewUnit(CreatedUnit.GetComponent<Unit>());
            }
            else
            {
                GameEvents.RequestResume();
            }
            
            
        }
        else
        {
            slotUi_index_to_stop++;
        }
        
        
    }
    
    private IEnumerator WaitAndHideSlotMachine()
    {
        yield return new WaitForSeconds(1.5f);
        slotmachine_display.SetActive(false);
    }

    private object drawthing(List<GameObject> L_Ra_We,List<SpecialCapacity> L_spe = null)
    {
        object Choice = null;
        if (L_spe == null)
        {
            int  i = Random.Range(0,L_Ra_We.Count);
            //Debug.Log($"🎲 [drawthing] Tirage GameObject index {i}/{L_Ra_We.Count} : {L_Ra_We[i].name}");
            Choice = L_Ra_We[i];
           
        }
        else
        {
            int i = Random.Range(0, L_spe.Count);
            //Debug.Log($"🎲 [drawthing] Tirage Special index {i}/{L_spe.Count} : {L_spe[i].name}");
            Choice = L_spe[i];
        }

        return Choice;
    }
    
    
    /*
    private IEnumerator SpinCoroutine()
    {
        GameObject raceResult = null;
        GameObject weaponResult = null;
        SpecialCapacity abilityResult = null;
        // Lancer chaque roue en parallèle
        Coroutine raceSpin = StartCoroutine(SpinWheel(raceSpinUI,RaceWeightedListToDraw,RaceListToDraw, g => raceResult = g));
        yield return new WaitForSeconds(delayBetweenStops);

        Coroutine weaponSpin = StartCoroutine(SpinWheel(weaponSpinUI,WeaponWeightedListToDraw,WeaponListToDraw,g => weaponResult = g));
        yield return new WaitForSeconds(delayBetweenStops);
        Coroutine abilitySpin = StartCoroutine(SpinWheel(abilitySpinUI, AbilitiesWeightedListToDraw,AbilitiesListToDraw, So => abilityResult = So));

        // Attendre que tout soit fini
        yield return raceSpin;
        yield return weaponSpin;
        yield return abilitySpin;
        
        factory.Assemble(raceResult,weaponResult,abilityResult);
    }*/
    
    private IEnumerator SpinWheel(GameObject uiGameObject,List<GameObject> GameObjectWeightedList,List<Rollable> rollables,Action<GameObject> onFinished)
    {
        GameObject Choice = null;
        Choice = GameObjectWeightedList[Random.Range(0, GameObjectWeightedList.Count)];
        
        if (uiGameObject)
        {
            uiGameObject.GetComponent<SlotsUI>().LaunchSlot(rollables);
        }
        else
        {
            Debug.LogError("UI GAMEOBJECT NOT FOUND");
            yield break;
        }
        onFinished?.Invoke(Choice);
    }
    
    private IEnumerator SpinWheel(GameObject uiGameObject,List<SpecialCapacity> scriptableObjectsWeightedList,List<Rollable> rollables,Action<SpecialCapacity> onFinished)
    {
        SpecialCapacity Choice = null;
        Choice = scriptableObjectsWeightedList[Random.Range(0, scriptableObjectsWeightedList.Count)];
        if (uiGameObject)
        {
            uiGameObject.GetComponent<SlotsUI>().LaunchSlot(rollables);
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

    private List<SpecialCapacity> NormalizingScriptable(List<Rollable> list_r)
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
        return ScriptableObjectsProbLinker(normalized_list,list_r);
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

    private List<SpecialCapacity> ScriptableObjectsProbLinker(List<int> probs,List<Rollable> rollables)
    {
        List<SpecialCapacity> res = new List<SpecialCapacity>();
        for (int i = 0; i < rollables.Count ; i++)
        {
            SpecialCapacity temp = rollables[i].effect;
            for (int j = 0; j < probs[i]; j++)
            {
                res.Add(temp);
            }
        }
        return res;
    }
 
}
