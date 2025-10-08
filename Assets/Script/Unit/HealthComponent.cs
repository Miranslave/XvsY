using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthComponent : MonoBehaviour
{


    public float max_health;
    [SerializeField] private float current_health;
    [SerializeField] private bool linkedToUI = false;
    [SerializeField] private UIManager healthUiComp;//temporary
    [SerializeField] private GameObject Uihitdmg;

    [SerializeField] private Canvas _Canvas;

    [SerializeField] private bool ui_hit_dmg = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Canvas = this.GetComponentInChildren<Canvas>();
        if (_Canvas)
        {
            ui_hit_dmg = true;
        }
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
        if(ui_hit_dmg && current_health  >= 0)
            UiUpdate(newhealth,false); 
        if (healthUiComp)
        {
            healthUiComp.Innit(max_health); 
            healthUiComp.NewValue(current_health);
        }
    }

    public void TakeDamageOverTime(float duration,float dmgpertick,float tickrate)
    {
        StartCoroutine(DotDamage(duration, dmgpertick,tickrate));
    }

    IEnumerator DotDamage(float duration, float dmgpertick,float tickrate)
    {
        float timer = 0f;
        while (timer < duration)
        {
            TakeDamage(dmgpertick);
            yield return new WaitForSeconds(tickrate);
            timer += tickrate;
        }
    }
    
    public void TakeDamage(float dmg,bool iscrit = false)
    {
        current_health -= dmg;
        if(linkedToUI)
            healthUiComp.NewValue(current_health);
        if(ui_hit_dmg && current_health >= 0)
            UiUpdate(dmg, iscrit); 
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

    private void UiUpdate(float dmg,bool iscrit)
    {
        if (Uihitdmg == null || _Canvas == null)
        {
            Debug.LogWarning("⚠️ Uihitdmg prefab ou Canvas manquant sur " + gameObject.name);
            return;
        }
        
        // Instancie sous le Canvas
        GameObject g = Instantiate(Uihitdmg, _Canvas.transform);
        // Setup l’affichage du texte
        var dmgUI = g.GetComponent<DmgUIManager>();
        if (dmgUI != null)
            dmgUI.Setup(dmg,iscrit);
    }
}
