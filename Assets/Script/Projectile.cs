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
            Destroy(this.gameObject);
        }
    }
}
