using UnityEngine;
using UnityEngine.Serialization;

public class HealthComponent : MonoBehaviour
{


    public float max_health;
    [SerializeField] private float current_health;
    [SerializeField] private bool linkedToUI = false;
    [SerializeField] private UIManager healthUiComp; //temporary
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_health = max_health;
        if (healthUiComp)
        {
            linkedToUI = true;
            healthUiComp.Innit(max_health); 
        }
           
    }

    public void SetNewHealth(float newhealth)
    {
        max_health = newhealth;
        current_health = newhealth;
        if (healthUiComp)
        {
            healthUiComp.Innit(max_health); 
            healthUiComp.NewValue(current_health);
        }
    }
    
    
    public void TakeDamage(float dmg)
    {
        current_health -= dmg;
        if(linkedToUI)
            healthUiComp.NewValue(current_health);
        if (current_health <= 0)
        {
            Death();
        }
    }

    public float getCurrentHealth()
    {
        return current_health;
    }
    
    private void Death()
    {
        Destroy(gameObject);
    }
}
