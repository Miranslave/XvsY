using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour,IPausable
{


    [SerializeField] private List<Transform> Spawnpoint;
    [SerializeField] private List<GameObject> _listEnemyToSpawn;
    [SerializeField] private Coroutine currentwork;
    [SerializeField] private int n = 0;
    [SerializeField] private float SpawnRate = 5f;
    private bool paused = false;

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
            if (!paused)
            {
                if (Debug_mode)
                {
                    Spawn(0);
                    yield break;
                }
                else
                {
                    Spawn();
                }
            }
                
            
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
        GameObject enemyToSpawn = _listEnemyToSpawn[Random.Range(0, _listEnemyToSpawn.Count)];
        GameObject g = Instantiate(enemyToSpawn, spawnedTransform.position, Quaternion.identity);
        g.name = g.GetComponent<Enemy>().name + n;
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
        GameObject enemyToSpawn = _listEnemyToSpawn[Random.Range(0, _listEnemyToSpawn.Count)];
        GameObject g = Instantiate(enemyToSpawn, spawnedTransform.position, Quaternion.identity);
        g.name = g.GetComponent<Enemy>().name + n;
        n++;
    }

    public void OnPause()
    {
        // maybe we could stop couroutine and make it start again somewhere else but taking the easy road now we just put condition
        paused = true;
    }

    public void OnResume()
    {
        paused = false;
    }
}
