using Script;
using UnityEngine;



[CreateAssetMenu(menuName = "StatusEffects/Slow")]
public class SlowAbility : SpecialCapacity
{
    [Header("Slow settings")]
    [Range(0f,1f)] public float slowFactor = 0.5f; // 0.5 = moitié de la vitesse

    public override void Apply(EntityBase target)
    {
        target.speed *= slowFactor;
        Debug.Log($"{target.name} est ralenti ({slowFactor*100}% de sa vitesse)");
    }

    public override void Remove(EntityBase target)
    {
        target.speed /= slowFactor;
        Debug.Log($"{target.name} n'est plus ralenti.");
    }
}