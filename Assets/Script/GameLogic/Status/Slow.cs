namespace Script.Status
{
    public class Slow : StatusEffect
    {
        private float slowFactor;
        
        public Slow(float duration,float slowFactor) : base(duration)
        {
            this.slowFactor = slowFactor;
        }

        public void Apply(Enemy target)
        {
            target.speed *= slowFactor;
        }
        
        public void Remove(Enemy target)
        {
            target.speed /= slowFactor; // remet la vitesse normale
        }
    }
}