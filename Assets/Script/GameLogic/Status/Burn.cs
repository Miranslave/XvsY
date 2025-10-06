using UnityEngine;

namespace Script.Status
{
    [CreateAssetMenu(menuName = "StatusEffects/Burn")]
    public class Burn : StatusEffect
    {
        [Header("Burn settings")]
        [Range(0f,100f)] public float BurnDmg = 1f;
        [Range(0f,100f)] public float BurnDuration = 1f;
        [Range(0f, 100f)] public float TickRate;


        public Burn(float duration, float burnDmg) : base(duration)
        {
            BurnDmg = burnDmg;
        }
        
        public void Apply(Enemy target)
        {
            target.TakeDmgOverTime(BurnDuration,BurnDuration,TickRate);
        }
        

    }
}