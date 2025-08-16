using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.WSA;


namespace Script
{
    public class Peashooter : Unit
    {
        public GameObject bulletprefab;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            cost = 50;
            Debug.Log($"{gameObject.name} avec un cooldown de {cooldown}");
            EffectLoopCoroutine = StartCoroutine(StartCooldown());
        }



        public override void Effect()
        {
            Vector3 spawnPosition = transform.position +  new Vector3(1f, 0f, 0f);
            GameObject bullet = Instantiate(bulletprefab, spawnPosition,quaternion.identity);
            Launch(bullet);
        }

        private void Launch(GameObject bullet)
        {
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float xForce = 2f;
                float yForce = 0f;
                rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
            }
        }

    }
}