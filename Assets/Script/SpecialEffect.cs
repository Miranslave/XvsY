using UnityEngine;

public class SpecialEffect : MonoBehaviour
{
    public GameObject sparkPrefab;

    public void SpawnSparks()
    {
        GameObject g  = Instantiate(sparkPrefab, this.transform.position, Quaternion.identity);
        ParticleSystem p = g.GetComponent<ParticleSystem>();
        p.Play();
        Destroy(g, 2f); // Auto clean
    }
}
