using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    [SerializeField] GameObject water;
    [SerializeField] float firstTileZ = 185f;
    [SerializeField] float distBetweenTiles = 185f;
    [SerializeField] float distBeforeDespawn = 5f;

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
        if(player.position.z - distBeforeDespawn > (zSpawn - 2*distBetweenTiles)){ //2=number of water on scene
            SpawnWater();
            DespawnWater();
        }
    }

    void SpawnWater(){
        Vector3 spawnPos = new Vector3(-10f, -1f, zSpawn);
        GameObject waterInstance = Instantiate(water, spawnPos, water.transform.rotation, transform);
        zSpawn += distBetweenTiles;
        activeTiles.Add(waterInstance);
    }

    void DespawnWater(){
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
