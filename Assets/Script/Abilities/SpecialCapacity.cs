using UnityEngine;

namespace Script
{
    public abstract class SpecialCapacity: ScriptableObject
    {
        [Header("Infos générales")] 
        public AbilityType abilityType;
        public string effectName = "New Effect";
        public Sprite Icon;
        [Range(0f, 1f)] public float chance = 1f; // proba d'application

        // Ces fonctions seront overridées dans les effets spécifiques
        public abstract void Apply(EntityBase target);
        public abstract void Remove(EntityBase target);
        
    }
}