using UnityEngine;

public class testTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogError("bbbbb" + other.gameObject.name);
    }
}
