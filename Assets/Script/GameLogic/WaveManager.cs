using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int spawnRate;
    public float total_duration = 300; // in seconds
    [SerializeField] private float duration;
    public Slider uiSlider;
    public EnemySpawner enemySpawner;
    private bool final_triggered = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetUI();
        enemySpawner.Innit(spawnRate,1);
        enemySpawner.gameObject.SetActive(true);
    }

    void ResetUI()
    {
        
        uiSlider.maxValue = 1;
        uiSlider.minValue = 0;
        uiSlider.value = 0;
        duration = 0;
    }

    void UpdateUI()
    {
        uiSlider.value = duration / total_duration;
    }

    void TriggerFinal()
    {
        enemySpawner.Paused = true;
        final_triggered = true;
        enemySpawner.TriggerFinal();
        Debug.Log("trigger boss or end of level  if no boss");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!final_triggered)
        {
            WaveProgress();
        }
    }

    void WaveProgress()
    {
        if (duration < total_duration)
        {
            duration += Time.deltaTime;
            UpdateUI();
        }
        else
        {
            TriggerFinal();
        }
    }
}
