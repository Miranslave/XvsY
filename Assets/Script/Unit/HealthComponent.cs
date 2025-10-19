using System.Collections;
using System.Collections.Generic;
using Script;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthComponent : MonoBehaviour
{
    [Header("Debug")] [SerializeField] private List<DmgUIManager> dmgprefablist;

    public float max_health;
    [SerializeField] private float current_health;
    [SerializeField] private bool linkedToUI = false;
    [SerializeField] private UIManager healthUiComp;//temporary
    [SerializeField] private GameObject Uihitdmg;
    [SerializeField] private EntityBase _entityBase;
    [SerializeField] private Canvas _Canvas;

    [SerializeField] private bool ui_hit_dmg = false;
    
    
    [Header("Dot and co")]
    [SerializeField]private bool istakingDot = false;
    [SerializeField]private float Dot_timer;
    [SerializeField]private float totalDotDmg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _entityBase = this.GetComponent<EntityBase>();
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
        if (!istakingDot)
        {
            totalDotDmg = CalculateTotalDotDmg(duration, dmgpertick, tickrate);
            StartCoroutine(DotDamage(duration, dmgpertick,tickrate));
        }
        else
        {
            Dot_timer = 0f;
            totalDotDmg = CalculateTotalDotDmg(duration, dmgpertick, tickrate);
            //Debug.Log(this.gameObject.name + " reset dot dmg");
        }
            
    }

    private float CalculateTotalDotDmg(float duration, float dmgpertick, float tickrate)
    {
        return (duration / tickrate )* dmgpertick;
    }

    private void ResetTotalDotDmg()
    {
        totalDotDmg = 0f;
    }

    IEnumerator DotDamage(float duration, float dmgpertick,float tickrate)
    {
        istakingDot = true;
        Dot_timer = 0f;
        while (Dot_timer < duration)
        {
            TakeDamage(dmgpertick,false,true);
            yield return new WaitForSeconds(tickrate);
            Dot_timer += tickrate;
        }
        ResetTotalDotDmg();
        GetComponentInParent<EntityBase>().ResetSpriteColor();
        istakingDot = false;
    }
    
    public void TakeDamage(float dmg,bool iscrit = false,bool isfromstatus = false)
    {

        if (!isfromstatus && !this.gameObject.CompareTag("Player"))
        {
            _entityBase.Invulnerabilty(1f);
        }
        current_health -= dmg;
        if(linkedToUI)
            healthUiComp.NewValue(current_health);
        if(ui_hit_dmg && current_health >= 0)
            UiUpdate(dmg, iscrit,isfromstatus); 
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
        foreach (var t in dmgprefablist)
        {
            t.CleanKill();
        }

        Enemy e = GetComponent<Enemy>();
        if (e)
        {
            e.GiveReward();
            e.EnemyHitPhysics();
        }
        
    }

    private void UiUpdate(float dmg,bool iscrit,bool isfromstatus = false)
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
        dmgprefablist.Add(dmgUI);
        if (dmgUI == null)
        {
            return;
        }
        if (isfromstatus)
        {
            Color c = Color.cyan;
            dmgUI.Setup(dmg,c);
        }
        else
        {
            dmgUI.Setup(dmg,iscrit);
        }

    }
}
