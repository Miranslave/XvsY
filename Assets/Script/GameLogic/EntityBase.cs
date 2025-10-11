using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class EntityBase: MonoBehaviour
    {
        [Header("Entity Info")]
        public string entityName;
        public int dmg = 1;
        public float current_speed = 2;
        public float base_speed;
        
        
        [Range(0f, 100f)] public float critChance = 0f;

        [Header("Components")]
        public HealthComponent healthComponent;
        public Rigidbody2D rb;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
        protected bool DmgTaken = false;
        protected bool Attacking = false;

        [Header("Debug")] public List<Coroutine> Status_effects_cd;
        protected virtual void Awake()
        {
            
            current_speed = base_speed;
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (rb != null)
            {
                rb.gravityScale = 0;
                rb.freezeRotation = true;
            }
        }


        public void ResetSpeed()
        {
            current_speed = base_speed;
        }
        
        // ----------------- Combat / Vie -----------------
        public virtual void TakeDmg(float amount,bool iscrit = false)
        {
            DmgTaken = true;
            if (rb != null) rb.linearVelocity = Vector2.zero;
            healthComponent.TakeDamage(amount,iscrit);
            animator.SetTrigger("TakeHit");
        }

        public virtual void TakeDmgOverTime(float duration, float amountpertick, float tickrate)
        {
            DmgTaken = true;
            if (rb != null) rb.linearVelocity = Vector2.zero;
            healthComponent.TakeDamageOverTime(duration,amountpertick,tickrate);
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
                TakeDmg(p.currentDmg,p.IsCriticalStrike);
                p.statusEffect?.Apply(this);
            }
        }

        public bool CheckCrit()
        {
            int crit_roll = Random.Range(0, 100);
            if (crit_roll <= critChance)
            {
                return true;
            }
            return false;
        }
        
        public void ChangeSpriteColor(Color c)
        {
            spriteRenderer.color = c;
        }

        public void ResetSpriteColor()
        {
            spriteRenderer.color = Color.white;
        }

        private void OnDestroy()
        {
            healthComponent.DOKill(healthComponent);
        }
    }
}