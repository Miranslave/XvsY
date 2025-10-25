using System;
using Script;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour,IPausable
{
    public Ammo ammo;
    public StatusEffect statusEffect;
    private Rigidbody2D _rb2d;
    private Vector2 _velocity;
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

    public void OnPause()
    {
        _velocity = _rb2d.linearVelocity;
        _rb2d.linearVelocity = Vector2.zero;
    }

    public void OnResume()
    {
        _rb2d.linearVelocity = _velocity;
    }
}
