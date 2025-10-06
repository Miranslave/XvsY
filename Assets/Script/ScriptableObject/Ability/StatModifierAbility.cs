using UnityEngine;

namespace Script.Status
{
    [CreateAssetMenu(menuName = "SpecialCapacity/StatModifier")]
    public class StatModifierAbility: SpecialCapacity

    {
        public AbilityType Type => AbilityType.StatModifier;
        public StatType statToModify;   // <-- choisis dans l'inspecteur Unity
        public float value;
        public override void Apply(EntityBase target)
        {
            switch (statToModify)
            {
                case StatType.Health:
                    target.healthComponent.SetNewHealth(target.healthComponent.max_health+value);
                    break;
                case StatType.Attack:
                    target.dmg += (int)value;
                    break;
                case StatType.CritChance:
                    target.critChance += value;
                    break;
                case StatType.Cooldown:
                    target.GetComponent<Unit>().effect_cooldown -= value;
                    break;
            }
        }

        public override void Remove(EntityBase target)
        {
            switch (statToModify)
            {
                case StatType.Health:
                    target.healthComponent.SetNewHealth(target.healthComponent.max_health-value);
                    break;
                case StatType.Attack:
                    target.dmg -= (int)value;
                    break;
                case StatType.CritChance:
                    target.critChance -= value;
                    break;
                case StatType.Cooldown:
                    target.GetComponent<Unit>().effect_cooldown += value;
                    break;
            }
        }
    }
}