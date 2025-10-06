using Script;
using UnityEngine;



[CreateAssetMenu(menuName = "SpecialCapacity/StatusApplier")]
public class StatusAbility : SpecialCapacity
{
    public AbilityType Type => AbilityType.Status;
    public StatusEffect statusEffect;
    public override void Apply(EntityBase target)
    {
        statusEffect.Apply(target);
        Debug.Log($"{target.name} est {statusEffect?.name})");
    }

    public override void Remove(EntityBase target)
    {
        statusEffect.Remove(target);
        Debug.Log($"{target.name} n'est plus {statusEffect?.name}");
    }
}