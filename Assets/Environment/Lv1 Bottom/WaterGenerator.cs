using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    [SerializeField] GameObject water;
    [SerializeField] float firstTileZ = 90f;
    [SerializeField] float distBetweenTiles = 200f;
    [SerializeField] float distBeforeDespawn = 10f;

    List<GameObject> activeTiles = new List<GameObject>();
    Transform player;
    float zSpawn;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        zSpawn = firstTileZ;
        SpawnWater();
        SpawnWater();
    }

    void Update()
    {
        if(player.position.z - distBeforeDespawn > (zSpawn - 1.5f* distBetweenTiles)){
            SpawnWater();
            DespawnWater();
        }
    }

    void SpawnWater(){
        Vector3 spawnPos = new Vector3(0, -1f, zSpawn);
        GameObject waterInstance = Instantiate(water, spawnPos, water.transform.rotation, transform);
        zSpawn += distBetweenTiles;
        activeTiles.Add(waterInstance);
    }

    void DespawnWater(){
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
