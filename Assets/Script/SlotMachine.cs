using System;
using System.Collections;
using Script;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotMachine : MonoBehaviour
{
    ///public GameObject BaseUnit;
    
    [Header("UI Elements")]
    public TextMeshProUGUI raceText;
    public GameObject raceSpinUI;
    public TextMeshProUGUI weaponText;
    public GameObject weaponSpinUI;
    public TextMeshProUGUI abilityText;

    [Header("Options")] 
    
    public GameObject[] races;
    
    public GameObject[] weapons;
    
    public GameObject[] abilities;

    [Header("Timing")]
    public float spinSpeed = 0.05f; // vitesse de défilement
    public float delayBetweenStops = 0.5f; // délai entre chaque arrêt

    [Header("Debug")] 
    public UnitFactory factory;
    

    public void StartSpin()
    {
        StartCoroutine(SpinCoroutine());
        if (factory.PlaceUnit.rolledUnitPrefab)
        {
            Destroy(factory.PlaceUnit.rolledUnitPrefab);
        }
    }

    private IEnumerator SpinCoroutine()
    {
        GameObject raceResult = null;
        GameObject weaponResult = null;
        //GameObject abilityResult = null;
        // Lancer chaque roue en parallèle
        Coroutine raceSpin = StartCoroutine(SpinWheel(raceSpinUI,raceText, races, 1f, g => raceResult = g));
        yield return new WaitForSeconds(delayBetweenStops);

        Coroutine weaponSpin = StartCoroutine(SpinWheel(weaponSpinUI,weaponText, weapons, 1f,g => weaponResult = g));
        yield return new WaitForSeconds(delayBetweenStops);

        //Coroutine abilitySpin = StartCoroutine(SpinWheel(abilityText, abilities, 1f, g => abilityResult = g));

        // Attendre que tout soit fini
        yield return raceSpin;
        yield return weaponSpin;
        //yield return abilitySpin;
        factory.Assemble(raceResult,weaponResult);
        //Debug.Log($"Résultat final : {raceText.text} - {weaponText.text} - {abilityText.text}");
        //GameObject g = Instantiate(BaseUnit);
        //g.GetComponent<BaseUnit>()?.Innit(raceText.text,weaponText.text,abilityText.text);
        
    }
    
    private IEnumerator SpinWheel(GameObject uiGameObject,TextMeshProUGUI textElement, GameObject[] options, float duration,Action<GameObject> onFinished)
    {
        float timer = 0f;
        GameObject lastChoice = null;
        while (timer < duration)
        {
            lastChoice = options[Random.Range(0, options.Length)];
            textElement.text = lastChoice.name;
            timer += spinSpeed;
            yield return new WaitForSeconds(spinSpeed);
        }
        uiGameObject.GetComponent<SlotsUI>().LaunchSlot(lastChoice);
        onFinished?.Invoke(lastChoice);
    }
}
