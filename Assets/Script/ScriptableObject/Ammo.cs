using Script;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewAmmo", menuName = "Scriptables/Ammo")]
public class Ammo : ScriptableObject
{
  [SerializeField] private float speed;
  [SerializeField] private float baseDamage;
  [SerializeField] private bool cross;
  [SerializeField] private StatusEffect statusEffect;
  [SerializeField] private GameObject prefab;
  
  public float Speed => speed;
  public float BaseDamage
  {
    get => baseDamage;
    set => baseDamage = value;
  }
  
  public StatusEffect StatusEffect
  {
    get => statusEffect;
    set => statusEffect = value;
  }

  public bool Cross => cross;
  public GameObject Prefab => prefab;
}
