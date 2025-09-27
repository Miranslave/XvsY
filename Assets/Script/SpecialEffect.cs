using UnityEngine;

public class SpecialEffect : MonoBehaviour
{
    public GameObject sparkPrefab;

    public void SpawnSparks()
    {
        GameObject g  = Instantiate(sparkPrefab, this.transform.position, Quaternion.identity);
        g.GetComponent<ParticleSystem>().Play();
       // Destroy(sparks.gameObject, 1f); // Auto clean
    }
}
