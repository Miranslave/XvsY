using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        public GameObject enemy_Gameobject;
        public LayerMask layerMaskEnemyToDetect;
        public bool RaycastDebugMod;
        public Animator unit_animator;
        public Sprite icon;
        public void Start()
        {
            StartCoroutine(StartUnitCooldown());
            unit_animator = GetComponent<Animator>();
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
            RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, transform.right,attackrange,layerMaskEnemyToDetect);
            if(RaycastDebugMod)
                Debug.DrawRay(this.gameObject.transform.position, Vector2.right * attackrange, hit ? Color.green : Color.red);
            if (hit)
            {
                //Debug.Log("we hit "+hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    enemy_Gameobject = hit.collider.gameObject;
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
            unit_animator.SetTrigger("LvlUp");
            healthComponent.SetNewHealth(healthComponent.max_health * 1.5f);
            weapon.OnUpgrade();
        }
        
        private void OnDisable()
        {
            if(EffectLoopCoroutine != null)
                StopCoroutine(EffectLoopCoroutine);
        }

        public void takedmg(float dmg)
        {
            healthComponent.TakeDamage(dmg);
        }

    }
}