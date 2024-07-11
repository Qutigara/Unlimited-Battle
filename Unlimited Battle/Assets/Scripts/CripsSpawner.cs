using FischlWorks_FogWar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CripsSpawner : MonoBehaviour
{
    // Префабы крипов трёх команд
    [BigHeader("Crips prefabs")]
    public GameObject purpleCripPrefab;
    public GameObject pinkCripPrefab;
    public GameObject brownCripPrefab;

    // Точки спавна трёх команд для каждой их стороны
    [BigHeader("Team side for spawn crips")]
    [Header("Purple team")]
    public GameObject purpleMidSideSpawnPoints;
    public GameObject purpleLeftTopSideSpawnPoints;
    public GameObject purpleBottomSideSpawnPoints;

    [Header("Pink team")]
    public GameObject pinkMidSideSpawnPoints;
    public GameObject pinkLeftTopSideSpawnPoints;
    public GameObject pinkRightTopSideSpawnPoints;

    [Header("Brown team")]
    public GameObject brownMidSideSpawnPoints;
    public GameObject brownRightTopSideSpawnPoints;
    public GameObject brownBottomSideSpawnPoints;

    // Side impact points
    [BigHeader("Side impact points")]
    public GameObject middle;
    public GameObject bottom;
    public GameObject leftTop;
    public GameObject rightTop;

    [BigHeader("Spawn interval (sec)")]
    [SerializeField]
    private float spawnInterval = 14f;
    private float timer = 0f;

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
            // Middle
            SpawnCrips(purpleCripPrefab, purpleMidSideSpawnPoints, middle);
            SpawnCrips(pinkCripPrefab, pinkMidSideSpawnPoints, middle);
            SpawnCrips(brownCripPrefab, brownMidSideSpawnPoints, middle);

            // Bottom
            SpawnCrips(purpleCripPrefab, purpleBottomSideSpawnPoints, bottom);
            SpawnCrips(brownCripPrefab, brownBottomSideSpawnPoints, bottom);

            // Left top
            SpawnCrips(purpleCripPrefab, purpleLeftTopSideSpawnPoints, leftTop);
            SpawnCrips(pinkCripPrefab, pinkLeftTopSideSpawnPoints, leftTop);


            // Right top
            SpawnCrips(pinkCripPrefab, pinkRightTopSideSpawnPoints, rightTop);
            SpawnCrips(brownCripPrefab, brownRightTopSideSpawnPoints, rightTop);


            timer = 0f;
        }
    }

    /// <summary>
    ///     Метод для спавна крипов, в который передаётся префаб крипа определённой команды,
    ///     точки спавна крипов и их точка назначения.
    /// </summary>
    /// <param name="prefab"> Префаб крипа одной из команд. </param>
    /// <param name="spawnSide"> Точки спавна крипов. </param>
    /// <param name="impactPoint"> Точка назначения (столкновения с другой командой) крипов. </param>
    private void SpawnCrips(GameObject prefab, GameObject spawnSide, GameObject impactPoint)
    {
 
        // Team crips spawner
        Transform[] spawnPoints = spawnSide.GetComponentsInChildren<Transform>();

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject crip = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            crip.GetComponent<NavMeshAgent>().SetDestination(impactPoint.transform.position);
        }
    }
}
