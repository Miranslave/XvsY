using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{


    [SerializeField] private List<Transform> Spawnpoint;
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private Coroutine currentwork;
    [SerializeField] private int n = 0;
    [SerializeField] private float SpawnRate = 5f;

    public bool Debug_mode;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(currentwork == null)
            currentwork = StartCoroutine(Timer());
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
    

    IEnumerator Timer()
    {
        while (true)
        { 
            yield return new WaitForSeconds(SpawnRate);
            Spawn();
        }
    }

    void Spawn()
    {
        if (Spawnpoint == null || Spawnpoint.Count == 0)
        {
            Debug.LogWarning("⚠️ Aucun spawnpoint défini !");
            return;
        }
        Transform spawnedTransform = Spawnpoint[Random.Range(0, Spawnpoint.Count)];
        GameObject g = Instantiate(enemyToSpawn, spawnedTransform.position, Quaternion.identity);
        g.name = "slime " + n;
        n++;
    }
    void Spawn(int i)
    {
        if (Spawnpoint == null || Spawnpoint.Count == 0)
        {
            Debug.LogWarning("⚠️ Aucun spawnpoint défini !");
            return;
        }
        Transform spawnedTransform = Spawnpoint[0];
        GameObject g = Instantiate(enemyToSpawn, spawnedTransform.position, Quaternion.identity);
        g.name = "slime " + n;
        n++;
    }
}
