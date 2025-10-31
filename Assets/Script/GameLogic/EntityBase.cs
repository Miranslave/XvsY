using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class EntityBase: MonoBehaviour , IPausable
    {
        [Header("Entity Info")]
        public string entityName;
        public int dmg = 1;
        public float current_speed = 2;
        public float base_speed;
        protected bool paused = false;
        
        [Range(0f, 100f)] public float critChance = 0f;

        [Header("Components")]
        public HealthComponent healthComponent;
        public Rigidbody2D rb;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
        protected bool DmgTaken = false;
        protected bool Attacking = false;
        protected bool CanTakeDmg = true;

        [Header("Debug")] public List<Coroutine> Status_effects_cd;

        public void Innit()
        {
            //spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        
        protected virtual void Awake()
        {
            
            current_speed = base_speed;
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
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
            if (!CanTakeDmg)
            {
                return;
            }
            DmgTaken = true;
            if (rb != null) rb.linearVelocity = Vector2.zero;
            healthComponent.TakeDamage(amount,iscrit);
            animator.SetTrigger("TakeHit");
        }

        public virtual void TakeDmgOverTime(float duration, float amountpertick, float tickrate)
        {
            if (!CanTakeDmg)
            {
                return;
            }
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

        public void Invulnerabilty(float time)
        {
            CanTakeDmg = false;
            //ChangeSpriteColor(Color.black);
            StartCoroutine(Cooldown(time));
        }

        IEnumerator Cooldown(float cd)
        {
            float timer = 0f; 
            while (timer < cd)
            {
                yield return new WaitForSeconds(0.1f);
                timer += 0.1f;
            }
            CanTakeDmg = true;
            //ChangeSpriteColor(Color.yellow);
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



        public void OnPause()
        {
            paused = true;
            animator.speed = 0f;
            if(rb)
                rb.linearVelocity = Vector2.zero;
        }

        public void OnResume()
        {
            paused = false;
            animator.speed = 1f;
        }
    }
}