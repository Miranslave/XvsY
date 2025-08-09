using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public abstract class Flower : MonoBehaviour
    {
        public float health = 100;
        public float cooldown = 3f;
        public int cost;
        public FlowerType type;
        public Coroutine EffectLoopCoroutine;
        
        public abstract void Effect();
        
        public IEnumerator StartCooldown()
        {
            while (true)
            {
                yield return new WaitForSeconds(cooldown); // Attente entre les soleils
                Effect();                                
            }
        }
        
        private void OnDisable()
        {
            StopCoroutine(EffectLoopCoroutine);
        }

    }
}