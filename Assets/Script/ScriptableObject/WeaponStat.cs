using UnityEngine;

namespace Script.Struct
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Scriptables/WeaponStat")]
    public class WeaponStat: ScriptableObject
    {
        public bool isRanged; 
        public float frequency;
        public int damage;
    }
}