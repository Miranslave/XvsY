using System;
using Script;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Ammo ammo;
    public StatusEffect statusEffect;
    public float currentDmg;
    public bool IsCriticalStrike = false;

    private void Awake()
    {
        currentDmg = ammo.BaseDamage;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!ammo.Cross)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetCriticalStrike()
    {
        IsCriticalStrike = true;
        currentDmg *= 1.5f;
    }
    
}
