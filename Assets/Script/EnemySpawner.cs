using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{


    public List<Transform> Spawnpoint;
    public GameObject enemyToSpawn;
    public Coroutine currentwork;
    public int n = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //currentwork = StartCoroutine(Timer());
    }

    private void OnDisable()
    {
        StopCoroutine(currentwork);
        currentwork = null;
    }

    private void OnEnable()
    {
        if(currentwork == null)
            currentwork = StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Timer()
    {
        while (true)
        {
            float t = 5;//Random.Range(3, 5);
            yield return new WaitForSeconds(t);
            Spawn();
        }
    }

    void Spawn()
    {
        Transform spawnedTransform = Spawnpoint[Random.Range(0, Spawnpoint.Count)];
        GameObject g = Instantiate(enemyToSpawn, spawnedTransform.position, Quaternion.identity);
        g.name = "slime " + n;
        n++;
    }
}
