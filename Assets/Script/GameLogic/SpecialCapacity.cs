using UnityEngine;

namespace Script
{
    public abstract class SpecialCapacity: ScriptableObject
    {
        [Header("Infos générales")]
        public string effectName = "New Effect";
        public Sprite Icon;
        
        [Range(0f, 1f)] public float chance = 1f; // proba d'application

        // Ces fonctions seront overridées dans les effets spécifiques
        public abstract void Apply(Unit target);
        public abstract void Remove(Unit target);
    }
}