using System;
using System.Collections;
using Script;
using UnityEngine;
using Random = UnityEngine.Random;
using UnitBase = Unity.VisualScripting.UnitBase;

public class Enemy : EntityBase
{
    [Header("Enemy info")] 
    [SerializeField] private Transform baseTarget;

    [SerializeField] private Transform currentTarget;
    [SerializeField] private bool isDead = false;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float Attacktimer = 2f;
    [SerializeField] private bool AttackOnCd;
        
    [Header("Raycast")]
    public float rangeRaycast;
    public bool RaycastDebugMod;
    public LayerMask layerMaskToDetect;
    public bool UnitInRange;
    public GameObject UnitRaycasted;
    public int Gold_Reward;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = this.GetComponentInChildren<LineRenderer>();
        Gold_Reward = Random.Range(10, 50);
        baseTarget = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void Update()
    {
        
        if(!paused && !AttackOnCd)
            CheckIfEnemyInLane();
        if (UnitInRange && !AttackOnCd)
        {
            Attack();
        }
    }

    private void FixedUpdate() // utiliser FixedUpdate pour la physique
    {
        if (!paused)
        {
            if (!isDead)
            {
                if (UnitInRange)
                {
                    StopMovement();
                    
                }
                else
                {
                    MoveToTarget(baseTarget);
                }
                    
            }
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
                currentTarget = UnitRaycasted.transform;
                UnitInRange = true;
            }
        }
        else
        {
            currentTarget = baseTarget;
            UnitInRange = false;
        }
            
    }
    // pour une raison obscure on n'a que le player en transform malgrès le passage du dummy en raycast :C
    private void MoveToTarget(Transform target)
    {
        if (target == null) return;
        float distance = Vector2.Distance(this.transform.position, target.position);
        //Debug.Log("Distance :" + distance);
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * base_speed;
        animator.SetBool("IsMoving", rb.linearVelocity != Vector2.zero);
    }

    private void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("IsMoving",false);
    }
    



    
    private void Attack()
    {
        Attacking = true;
        AttackOnCd = true;
        animator.SetTrigger("Attack");
        Invoke(nameof(ResetAttackCd),Attacktimer);
    }

    

    // to modify to get different behavior (keep static) get pushed harder
    public void EnemyHitPhysics()
    {
        isDead = true;
        // 1️⃣ On s’assure d’avoir un Rigidbody2D valide
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        // 2️⃣ On désactive les collisions avec le reste
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false; // plus de collisions

        // 3️⃣ On libère les contraintes du rigidbody (mais bloque la rotation pour le style Paper Mario)
        rb.constraints = RigidbodyConstraints2D.None;
        float ForceInX,ForceInY,TorqueForce;
        ForceInY = Random.Range(5f, 6f);
        ForceInX = Random.Range(1.5f,3f);
        TorqueForce = Random.Range(-9f, 9f);
        // 4️⃣ On applique une impulsion vers le haut (et un peu de côté)// petit aléatoire pour la direction
        rb.AddForce(new Vector2(ForceInX, ForceInY),ForceMode2D.Impulse);
        rb.AddTorque(TorqueForce,ForceMode2D.Impulse);
        rb.gravityScale = 2f;
        rb.linearDamping = 0f; // renforce la chute pour que ce soit visible

    }
    


    public void GiveReward()
    {
        baseTarget.GetComponent<PlayerManager>().AddMoney(Gold_Reward);
    }

    public void MeleAttack()
    {
        UnitRaycasted.GetComponent<Unit>().takedmg(dmg);
    }

    public void TriggerLaser()
    {
        Vector3 target = UnitRaycasted.transform.position - this.transform.position;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1,target);
        UnitRaycasted.GetComponent<Unit>().takedmg(dmg);
        Invoke(nameof(CleanLaser),0.1f);
    }

    private void CleanLaser()
    {
        lineRenderer.enabled = false;
    }

    private void ResetAttackCd()
    {
        AttackOnCd = false;
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
