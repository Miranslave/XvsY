using UnityEngine;

namespace Script.Struct
{
    [System.Serializable]
    public struct WeaponStat
    {
        public bool isRanged; 
        public Collider2D dmgZone;
        public GameObject projectile;
        public float projectilespeed;
        public float frequency;
        public float damage;
    }
}