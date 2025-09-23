using System;
using Script;
using UnityEngine;
using UnitBase = Unity.VisualScripting.UnitBase;

public class Enemy : MonoBehaviour
{
    public int dmg = 1;
    public float speed = 2;
    [SerializeField] private Transform baseTarget;
    private Rigidbody2D rb;
    private Animator _animator;
    private bool DmgTaken = false;
    private bool Attacking = false;
    [SerializeField] private HealthComponent healthComponent;
    
    [Header("Raycast")]
    public float rangeRaycast;
    public bool RaycastDebugMod;
    public LayerMask layerMaskToDetect;
    public bool UnitInRange;
    public GameObject UnitRaycasted;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        rb.gravityScale = 0; // pas de gravité
        rb.freezeRotation = true; // évite la rotation physique
        baseTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        CheckIfEnemyInLane();
        
    }

    private void FixedUpdate() // utiliser FixedUpdate pour la physique
    {
        if (!DmgTaken && !UnitInRange)
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

    public void CheckIfEnemyInLane()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, -1*transform.right,rangeRaycast,layerMaskToDetect);
        if(RaycastDebugMod)
            Debug.DrawRay(this.gameObject.transform.position, Vector2.left * rangeRaycast, hit ? Color.green : Color.red);
        if (hit)
        {
            //Debug.Log("we hit "+hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Unit"))
            {
                UnitRaycasted = hit.collider.gameObject;
                if(!Attacking)
                    Attack();
                UnitInRange = true;
            }
        }
        else
        {
            UnitInRange = false;
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
            Projectile p = g.GetComponent<Projectile>();
            // test
            if (p == null)
            {
                p = g.GetComponentInParent<Projectile>();
            }
            int _dmg  = p.ammo.Damage;
            TakeDmg(_dmg);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        GameObject g = other.gameObject;
        if (g.CompareTag("Bullet"))
        {
            Projectile p = g.GetComponentInParent<Projectile>();
            int _dmg  = p.ammo.Damage;
            TakeDmg(_dmg);
        }
    }


    private void Attack()
    {
        Attacking = true;
        _animator.SetTrigger("Attack");
         UnitRaycasted.GetComponent<Unit>().takedmg(dmg);
    }

    public void TakeDmg(int dmg)
    {
        DmgTaken = true;
        healthComponent.TakeDamage(dmg);
        //EnemyHitPhysics();
        _animator.SetTrigger("TakeHit");
    }


    // to modify to get different behavior (keep static) get pushed harder
    public void EnemyHitPhysics()
    {
        rb.linearVelocity = new Vector2(1.5f, 0);
    }

    public void endKnockBack()
    {
        
    }

    public void ResetAttackState()
    {
        Attacking = false;
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 origin = this.transform.position;
        if(RaycastDebugMod)
            Gizmos.DrawLine(origin,origin+(-transform.right)*rangeRaycast);
    }

#endif

}
