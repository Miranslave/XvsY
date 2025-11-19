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
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetUI();
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
    }
    
    // Update is called once per frame
    void Update()
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
