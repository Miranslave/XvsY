using System;
using Script;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Ammo ammo;
    public StatusEffect statusEffect;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!ammo.Cross)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy e = other.gameObject.GetComponent<Enemy>();
                if (statusEffect)
                {
                    statusEffect.Apply(e);
                }
            }
            Destroy(this.gameObject);
        }
    }
    
}
