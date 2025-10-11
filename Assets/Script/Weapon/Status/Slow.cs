using System.Collections;
using UnityEngine;

namespace Script.Status
{
    [CreateAssetMenu(menuName = "StatusEffects/Slow")]
    public class Slow : StatusEffect
    {
        [SerializeField] private float newspeed;
        public Slow(float duration,float slowFactor) : base(duration)
        {
            this.newspeed = slowFactor;
        }

        public override void  Apply(EntityBase target)
        {
            target.StartCoroutine(ApplySlowCoroutine(target));
        }
        
        public override void Remove(EntityBase target)
        {
             // remet la vitesse normale
             target.ResetSpeed();
             target.ResetSpriteColor();
        }

        private IEnumerator ApplySlowCoroutine(EntityBase target)
        {
            //Debug.Log("Applying slow on " + target.name);
            target.current_speed = newspeed;
            target.ChangeSpriteColor(Color.blue);

            yield return new WaitForSeconds(duration); // ✅ attends bien la durée du slow

            Remove(target);
        }
    }
}