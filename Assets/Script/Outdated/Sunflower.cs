using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class Sunflower : Unit
    {

        public GameObject sunprefab;
        
        
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Starting()
        {
            cost = 50;
            Debug.Log($"{gameObject.name} avec un cooldown de {cooldown}");
            //EffectLoopCoroutine = StartCoroutine(StartCooldown());
        }
        
        public override void Effect()
        {
            Vector3 spawnPosition = transform.position +  new Vector3(0f, 1f, 0f); 
            GameObject sun = Instantiate(sunprefab, spawnPosition,quaternion.identity);
            Launch(sun);
        }

        private void Launch(GameObject sun)
        {
            Rigidbody2D rb = sun.GetComponent<Rigidbody2D>();
            float xForce = Random.Range(-0.5f, 0.5f);
            float yForce = Random.Range(0.5f, 1f);
            rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }
        

    }
}
