using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public abstract class Unit : MonoBehaviour
    {
        public HealthComponent healthComponent;
        public float cooldown = 3f;
        public int level = 1;
        public Coroutine EffectLoopCoroutine;
        public Weapon weapon;
        public bool EnemyInSight = false;
        public LayerMask layerMask;
        public bool RaycastDebugMod;
        
        public void Start()
        {
            StartCoroutine(StartUnitCooldown());
        }

        public void Update()
        {
            CheckIfEnemyInLane();
        }

        public abstract void Effect();
        
        [NotNull]
        public IEnumerator StartUnitCooldown()
        {
            while (true)
            {
                yield return new WaitForSeconds(cooldown); // Attente entre les soleils
                Effect();                                
            }
        }
        
        
        public void CheckIfEnemyInLane()
        {
            float attackrange = weapon.GetRange();
            RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, transform.right,attackrange,layerMask);
            if(RaycastDebugMod)
                Debug.DrawRay(this.gameObject.transform.position, Vector2.right * attackrange, hit ? Color.green : Color.red);
            if (hit)
            {
                //Debug.Log("we hit "+hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    EnemyInSight = true;
                }
                
            }
            else
            {
                EnemyInSight = false;
            }
            
        }

        public void OnUpgrade()
        {
            level++;
            healthComponent.SetNewHealth(healthComponent.max_health * 1.5f);
            weapon.OnUpgrade();
        }
        
        private void OnDisable()
        {
            if(EffectLoopCoroutine != null)
                StopCoroutine(EffectLoopCoroutine);
        }

    }
}