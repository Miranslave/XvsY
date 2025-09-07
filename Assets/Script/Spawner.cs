using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public bool blaunch;
    public float delay;

    private float _delay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _delay = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
        }
        else
        {
            Spawn();
            _delay = delay;

        }
        
    }

    void Spawn()
    {
        GameObject g = Instantiate(prefab,this.transform);
        g.transform.position = this.transform.position;
        Launch(g);
    }
    
    private void Launch(GameObject g)
    {
        Rigidbody rb;
        Rigidbody2D rb2D;
        
        float xForce = Random.Range(-1.5f, 1.5f);
        float yForce = Random.Range(0, 0);
        
        if (rb = g.GetComponent<Rigidbody>())
        {
            rb.AddForce(new Vector2(xForce, yForce), ForceMode.Impulse);
        }

        if (rb2D = g.GetComponent<Rigidbody2D>())
        {
            rb2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }
        

        
    }
    
}
