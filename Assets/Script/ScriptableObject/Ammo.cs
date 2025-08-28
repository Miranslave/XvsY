using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmo", menuName = "Scriptables/Ammo")]
public class Ammo : ScriptableObject
{
  [SerializeField] private float speed;
  [SerializeField] private int damage;
  [SerializeField] private GameObject prefab;
  
  public float Speed => speed;
  public int Damage => damage;
  public GameObject Prefab => prefab;
}
