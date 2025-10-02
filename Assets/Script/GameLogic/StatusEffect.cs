namespace Script
{
    public abstract class StatusEffect
    {
        public float duration; //durée en secondes
        protected float elapsedTime = 0f;
        
        
        public StatusEffect(float duration)
        {
            this.duration = duration;
        }
        
        public virtual void Apply(EntityBase target) { }        // Quand l’effet est appliqué
        
        public virtual void Update(EntityBase target, float dt) // Chaque frame
        {
            elapsedTime += dt;
        }
        
        public virtual void Remove(EntityBase target) { }       // Quand l’effet expire

        public bool IsExpired() => elapsedTime >= duration;
    }
    
    
}