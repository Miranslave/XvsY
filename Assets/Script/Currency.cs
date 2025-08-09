using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Currency : MonoBehaviour
{
    void Update()
    {
        /*
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPos.z = 0f;

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OnClicked();
            }
        }*/
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
