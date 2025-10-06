using System;
using System.Collections;
using JetBrains.Annotations;
using Script.Status;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script
{
    public abstract class Unit : EntityBase
    {
    
        [Header("Unit info")]
        public Sprite icon;
        public int level = 1;
        [SerializeField] public float effect_cooldown = 3f;
        
        
        
        [Header("Components")]
        public Weapon weapon;
        public SpecialCapacity specialCapacity;
        private Coroutine EffectLoopCoroutine;
        
        [Header("Raycast")]
        public bool EnemyInSight = false;
        public GameObject enemy_Gameobject;
        [SerializeField] private LayerMask layerMaskEnemyToDetect;
        [SerializeField] private bool RaycastDebugMod;
        
        protected override void Awake()
        {
            base.Awake();
            
        }
        
        public void Start()
        {
            CapacityAdding();
            EffectLoopCoroutine = StartCoroutine(StartUnitCooldown());
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
                yield return new WaitForSeconds(effect_cooldown); // Attente entre les soleils
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
            animator.SetTrigger("LvlUp");
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

        private void CapacityAdding()
        {
            if (specialCapacity.abilityType == AbilityType.StatModifier)
            {
                StatModifierAbility statModifierAbility = (StatModifierAbility)specialCapacity;
                statModifierAbility.Apply(this);
            }
            if(specialCapacity.abilityType == AbilityType.Status)
            {
                StatusAbility statusAbility = (StatusAbility)specialCapacity;
                if (weapon.GetIsRanged())
                {
                    weapon.SetAmmoStatus(statusAbility.statusEffect);
                }
                weapon.StatusEffect = statusAbility.statusEffect;
            }
        }
    }
}