using System.Collections;
using Script;
using TMPro;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    public GameObject BaseUnit;
    
    [Header("UI Elements")]
    public TextMeshProUGUI raceText;
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI abilityText;

    [Header("Options")] public string[] races = { "Humain", "Orc" };//, "Homme-lézard", "Elfe", "Nain" };
    public string[] weapons = { "Arc", "Bouclier"};//,"Magie", "Épée", "Hache" };
    public string[] abilities = { "Furie", "Vol de vie"};//,"AOE", "Poison", "Critique" };

    [Header("Timing")]
    public float spinSpeed = 0.05f; // vitesse de défilement
    public float delayBetweenStops = 0.5f; // délai entre chaque arrêt

    public void StartSpin()
    {
        StartCoroutine(SpinCoroutine());
    }

    private IEnumerator SpinCoroutine()
    {
        // Lancer chaque roue en parallèle
        Coroutine raceSpin = StartCoroutine(SpinWheel(raceText, races, 1f));
        yield return new WaitForSeconds(delayBetweenStops);

        Coroutine weaponSpin = StartCoroutine(SpinWheel(weaponText, weapons, 1f));
        yield return new WaitForSeconds(delayBetweenStops);

        Coroutine abilitySpin = StartCoroutine(SpinWheel(abilityText, abilities, 1f));

        // Attendre que tout soit fini
        yield return raceSpin;
        yield return weaponSpin;
        yield return abilitySpin;

        Debug.Log($"Résultat final : {raceText.text} - {weaponText.text} - {abilityText.text}");
        GameObject g = Instantiate(BaseUnit);
        g.GetComponent<BaseUnit>()?.Innit(raceText.text,weaponText.text,abilityText.text);
        
    }

    private IEnumerator SpinWheel(TextMeshProUGUI textElement, string[] options, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            textElement.text = options[Random.Range(0, options.Length)];
            timer += spinSpeed;
            yield return new WaitForSeconds(spinSpeed);
        }
    }
}
