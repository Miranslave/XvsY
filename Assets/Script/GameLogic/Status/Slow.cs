namespace Script.Status
{
    public class Slow : StatusEffect
    {
        private float slowFactor;
        
        public Slow(float duration,float slowFactor) : base(duration)
        {
            this.slowFactor = slowFactor;
        }

        public override void Apply(Enemy target)
        {
            target.speed *= slowFactor;
        }
        
        public override void Remove(Enemy target)
        {
            target.speed /= slowFactor; // remet la vitesse normale
        }
    }
}