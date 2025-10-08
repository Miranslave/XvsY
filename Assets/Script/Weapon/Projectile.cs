using System;
using Script;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Ammo ammo;
    public StatusEffect statusEffect;
    public bool IsCriticalStrike = false;
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
    }
    
}
