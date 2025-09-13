using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 1;
    public int dmg = 1;
    public float speed = 2;
    [SerializeField] private Transform baseTarget;
    private Rigidbody2D rb;
    private Animator _animator;
    private bool DmgTaken = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        rb.gravityScale = 0; // pas de gravité
        rb.freezeRotation = true; // évite la rotation physique
        baseTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    
    private void FixedUpdate() // utiliser FixedUpdate pour la physique
    {
        if (!DmgTaken)
        {
            MoveToTarget(baseTarget);
        }
        if(rb.linearVelocity != Vector2.zero){
            _animator.SetBool("IsMoving",true);
        }
        else
        {
            _animator.SetBool("IsMoving",false);
        }
    }



    private void MoveToTarget(Transform target)
    {
        Vector2 direction; //= Vector2.left;
        // direction en fonction de la position de la base
        direction = (new Vector2(target.position.x,this.transform.position.y) - rb.position).normalized; 
        // applique une vélocité constante
        rb.linearVelocity = direction * speed;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        GameObject g = other.gameObject;
        if (g.CompareTag("Bullet"))
        {
            Projectile p = g.GetComponent<Projectile>(); // test
            int _dmg  = p.ammo.Damage;
            TakeDmg(_dmg);
        }
    }

    public void TakeDmg(int dmg)
    {
        DmgTaken = true;
        rb.linearVelocity = new Vector2(1.5f, 0);
        _animator.SetTrigger("TakeHit");
    }




}
