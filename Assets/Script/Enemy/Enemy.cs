using System;
using Script;
using UnityEngine;
using UnitBase = Unity.VisualScripting.UnitBase;

public class Enemy : EntityBase
{
    [Header("Enemy info")] 
    [SerializeField] private Transform baseTarget;
    
    
    [Header("Raycast")]
    public float rangeRaycast;
    public bool RaycastDebugMod;
    public LayerMask layerMaskToDetect;
    public bool UnitInRange;
    public GameObject UnitRaycasted;

    protected override void Awake()
    {
        base.Awake();
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
        animator.SetBool("IsMoving", rb.linearVelocity != Vector2.zero);
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

    
    private void Attack()
    {
        Attacking = true;
        animator.SetTrigger("Attack");
        UnitRaycasted.GetComponent<Unit>().takedmg(dmg);
    }

    

    // to modify to get different behavior (keep static) get pushed harder
    public void EnemyHitPhysics()
    {
        rb.linearVelocity = new Vector2(1.5f, 0);
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
