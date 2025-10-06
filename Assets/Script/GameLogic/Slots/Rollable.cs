using UnityEngine;

namespace Script
{
    [System.Serializable]
    public struct Rollable 
    {
        public Sprite icon;
        [Range(0,100)]
        public float probs;
        public GameObject prefab;
        public SpecialCapacity effect;
    }
}