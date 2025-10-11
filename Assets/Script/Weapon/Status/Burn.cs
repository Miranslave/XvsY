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
        
        public override void Apply(EntityBase target)
        {
            Debug.Log("Burn this "+ target.name);
            target.TakeDmgOverTime(BurnDuration,BurnDmg,TickRate);
            target.ChangeSpriteColor(Color.red);
        }
        
        

    }
}