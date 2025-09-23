using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public String Tag;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject g = other.gameObject;
        
        if (g.CompareTag(Tag))
        {
            //Debug.Log($"Collected object with tag: {g.tag}");
            Destroy(g); // ou autre logique
        }
    }
}
