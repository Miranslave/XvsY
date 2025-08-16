using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Currency : MonoBehaviour
{
    void Update()
    {
    
    }

    public void OnClicked(PlayerManager p)
    {
        Debug.Log("Soleil cliqué !");
        p.AddMoney(50);
        // Ajouter la currency ici
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Collector"))
        {
            Destroy(gameObject);
        }
    }
}
