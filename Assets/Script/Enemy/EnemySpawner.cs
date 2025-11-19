using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour,IPausable
{


    [SerializeField] private List<Transform> _listSpawnpoint;
    [SerializeField] private List<GameObject> _listEnemyToSpawn;
    [SerializeField] private Coroutine currentwork;
    [SerializeField] private int n = 0;
    [SerializeField] private float SpawnRate;// in second
    [SerializeField] private int maximum_multi_spawn = 1;



    public bool Paused
    {
        get => paused;
        set => paused = value;
    }

    private bool paused = false;

    public bool Debug_mode;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    public void Innit(float spawnrate,int multi_spawn)
    {
        SpawnRate = spawnrate;
        maximum_multi_spawn = multi_spawn;
    }
    
    private void OnDisable()
    {
        StopCoroutine(currentwork);
        currentwork = null;
    }

    public void TriggerFinal()
    {
        foreach (var point in _listSpawnpoint)
        {
            Spawn(point);
        }
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
        if (_listSpawnpoint == null || _listSpawnpoint.Count == 0)
        {
            Debug.LogWarning("⚠️ Aucun spawnpoint défini !");
            return;
        }
        Transform spawnedTransform = _listSpawnpoint[Random.Range(0, _listSpawnpoint.Count)];
        GameObject enemyToSpawn = _listEnemyToSpawn[Random.Range(0, _listEnemyToSpawn.Count)];
        GameObject g = Instantiate(enemyToSpawn, spawnedTransform.position, Quaternion.identity);
        g.name = g.GetComponent<Enemy>().name + n;
        n++;
    }
    void Spawn(int i)
    {
        if (_listSpawnpoint == null || _listSpawnpoint.Count == 0)
        {
            Debug.LogWarning("⚠️ Aucun spawnpoint défini !");
            return;
        }
        Transform spawnedTransform = _listSpawnpoint[0];
        GameObject enemyToSpawn = _listEnemyToSpawn[Random.Range(0, _listEnemyToSpawn.Count)];
        GameObject g = Instantiate(enemyToSpawn, spawnedTransform.position, Quaternion.identity);
        g.name = g.GetComponent<Enemy>().name + n;
        n++;
    }
    
    void Spawn(Transform spawnpoint)
    {
        if (spawnpoint == null)
        {
            Debug.LogWarning("⚠️ Aucun spawnpoint défini !  l115");
            return;
        }
        GameObject enemyToSpawn = _listEnemyToSpawn[Random.Range(0, _listEnemyToSpawn.Count)];
        GameObject g = Instantiate(enemyToSpawn, spawnpoint.position, Quaternion.identity);
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
