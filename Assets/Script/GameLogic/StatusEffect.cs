using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    
    public abstract class StatusEffect : ScriptableObject
    {
        public string StatusUIname;
        public float duration = 1f; //durée en secondes
        protected float elapsedTime = 0f;
        
        
        public StatusEffect(float duration)
        {
            this.duration = duration;
        }
        
        public virtual void Apply(EntityBase target) { }        // Quand l’effet est appliqué
        
        
        public virtual void Remove(EntityBase target) { }       // Quand l’effet expire

        public bool IsExpired() => elapsedTime >= duration;
    }
    
    
}