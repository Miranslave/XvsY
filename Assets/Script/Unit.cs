using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public abstract class Unit : MonoBehaviour
    {
        public float health = 100;
        public float cooldown = 3f;
        public int cost;
        public FlowerType type;
        public Coroutine EffectLoopCoroutine;
        public Weapon weapon;
        public void Start()
        {
            StartCoroutine(StartUnitCooldown());
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
        
        private void OnDisable()
        {
            if(EffectLoopCoroutine != null)
                StopCoroutine(EffectLoopCoroutine);
        }

    }
}