using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CripsSpawner : MonoBehaviour
{
    // Crips prefabs
    public GameObject purpleCripPrefab;
    public GameObject pinkCripPrefab;
    public GameObject brownCripPrefab;

    // Team side for spawn crips
    public GameObject purpleMidSide;
    public GameObject pinkMidSide;
    public GameObject brownMidSide;


    public float spawnInterval = 9f;
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
        // Purple team crips spawner
        Transform[] purpleSpawnPoints = purpleMidSide.GetComponentsInChildren<Transform>();

        foreach (Transform spawnPoint in purpleSpawnPoints)
        {
            Instantiate(purpleCripPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        // Pink team crips spawner
        Transform[] pinkSpawnPoints = pinkMidSide.GetComponentsInChildren<Transform>();

        foreach (Transform spawnPoint in pinkSpawnPoints)
        {
            Instantiate(pinkCripPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        // Brown team crips spawner
        Transform[] brownSpawnPoints = brownMidSide.GetComponentsInChildren<Transform>();

        foreach (Transform spawnPoint in brownSpawnPoints)
        {
            Instantiate(brownCripPrefab, spawnPoint.position, spawnPoint.rotation);
        }


    }
}
