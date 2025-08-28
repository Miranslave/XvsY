
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 1;
    public int dmg = 1;
    public float speed = 2;
    [SerializeField] private Transform baseTarget;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // pas de gravité
        rb.freezeRotation = true; // évite la rotation physique
        baseTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    
    private void FixedUpdate() // utiliser FixedUpdate pour la physique
    {
        Vector2 direction; //= Vector2.left;
        
        // direction en fonction de la position de la base
        direction = (new Vector2(baseTarget.position.x,this.transform.position.y) - rb.position).normalized; 
        // applique une vélocité constante
        rb.linearVelocity = direction * speed;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        GameObject g = other.gameObject;
        if (g.CompareTag("Bullet"))
        {
            int _dmg  = g.GetComponent<Projectile>().ammo.Damage;
            TakeDmg(_dmg);
        }
    }

    public void TakeDmg(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
