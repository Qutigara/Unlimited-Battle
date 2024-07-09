using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CripsSpawner : MonoBehaviour
{

    public GameObject cripPrefab;
    public Transform[] spawnPoints;

    public float spawnInterval = 60f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimerSpawn();
    }

    private void TimerSpawn()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval) 
        {
            SpawnBot();
            timer = 0f;
        }
    }

    private void SpawnBot()
    {
        //Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(cripPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
        }
        
    }
}
