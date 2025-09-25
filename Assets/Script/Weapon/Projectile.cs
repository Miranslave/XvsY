using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Ammo ammo;
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!ammo.Cross)
        {
            if(other.gameObject.CompareTag("Enemy"))
                Destroy(this.gameObject);
        }
    }
    
}
