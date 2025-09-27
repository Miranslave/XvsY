using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public String Tag;
   [SerializeField] private PlayerManager _playerManager;



    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject g = other.gameObject;
        
        if (g.CompareTag(Tag))
        {
            //Debug.Log($"Collected object with tag: {g.tag}");
            if(Tag == "Enemy")
                SendDmg(g);
            Destroy(g); // ou autre logique
        }
    }

    private void SendDmg(GameObject g)
    {
        Enemy e = g.GetComponent<Enemy>();
        _playerManager.TakeDmg(e.dmg);
    }
}
