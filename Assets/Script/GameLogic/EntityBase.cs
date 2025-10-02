using UnityEngine;

namespace Script
{
    public class EntityBase: MonoBehaviour
    {
        [Header("Entity Info")]
        public string entityName;
        public int dmg = 1;
        public float speed = 2;

        [Header("Components")]
        public HealthComponent healthComponent;
        public Rigidbody2D rb;
        public Animator animator;

        protected bool DmgTaken = false;
        protected bool Attacking = false;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            if (rb != null)
            {
                rb.gravityScale = 0;
                rb.freezeRotation = true;
            }
        }

        
        
        // ----------------- Combat / Vie -----------------
        public virtual void TakeDmg(int amount)
        {
            DmgTaken = true;
            if (rb != null) rb.linearVelocity = Vector2.zero;

            healthComponent.TakeDamage(amount);
            animator.SetTrigger("TakeHit");
        }

        public void ResetDmgTaken() => DmgTaken = false;

        public void ResetAttackState() => Attacking = false;

        // Collision commune (projectiles)
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                HandleProjectile(other.gameObject);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                HandleProjectile(other.gameObject);
            }
        }

        private void HandleProjectile(GameObject g)
        {
            Projectile p = g.GetComponent<Projectile>() ?? g.GetComponentInParent<Projectile>();
            if (p != null)
            {
                TakeDmg(p.ammo.Damage);
            }
        }
    }
}