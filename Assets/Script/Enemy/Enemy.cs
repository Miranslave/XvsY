using System;
using Script;
using UnityEngine;
using Random = UnityEngine.Random;
using UnitBase = Unity.VisualScripting.UnitBase;

public class Enemy : EntityBase
{
    [Header("Enemy info")] 
    [SerializeField] private Transform baseTarget;
    [SerializeField] private bool isDead = false;
    
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
        Gold_Reward = Random.Range(10, 50);
        baseTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        CheckIfEnemyInLane();
        
    }

    private void FixedUpdate() // utiliser FixedUpdate pour la physique
    {
        if (!UnitInRange && !isDead)
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
        rb.linearVelocity = direction * current_speed;
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
