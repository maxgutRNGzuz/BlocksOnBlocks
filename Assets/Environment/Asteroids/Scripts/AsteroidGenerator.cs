using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject[] asteroids;
    [SerializeField] Transform[] spawns;

    [SerializeField] int minAsteroids = 1;
    [SerializeField] int maxAsteroids = 10;

    [SerializeField] float minFirstSpawnTime = 60f;
    [SerializeField] float maxFirstSpawnTime = 120f;
    [SerializeField] float minSpawnTime = 30f;
    [SerializeField] float maxSpawnTime = 90f;

    [SerializeField] float intraSpawnTime = 3f;
    [SerializeField] float spawnZ = 120f;
    
    Vector3 spawnOffset;

    void Start()
    {
        StartCoroutine(Wait(minFirstSpawnTime, maxFirstSpawnTime));
    }

    IEnumerator Wait(float minTime, float maxTime){
        float time = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids(){
        int numAsteroids = Random.Range(minAsteroids, maxAsteroids + 1);
        int w = 0;
        spawnOffset = new Vector3(0f,0f, player.position.z + spawnZ);
        for (int i = 0; i < numAsteroids; i++)
        {
            int index = Random.Range(0, asteroids.Length);
            GameObject asteroid = Instantiate(asteroids[index], spawns[w].position + spawnOffset, Quaternion.identity, transform);
        }
        if(Random.value < 0.5f){
            for(int i = 0; i < numAsteroids; i++){
                if(w > 2){
                    w = 0;
                    yield return new WaitForSeconds(3f);
                }
                int index = Random.Range(0, asteroids.Length);
                GameObject asteroid = Instantiate(asteroids[index], spawns[w].position + spawnOffset, Quaternion.identity, transform);
                yield return new WaitForSeconds(/*Random.Range(0f, intraSpawnTime)*/1f);
                w += 1;
            }
        }
        else{
            for (int i = 0; i < numAsteroids; i++)
            {
                int index = Random.Range(0, asteroids.Length);
                GameObject asteroid = Instantiate(asteroids[index], spawns[w].position + spawnOffset, Quaternion.identity, transform);
                yield return new WaitForSeconds(/*Random.Range(0f, intraSpawnTime)*/5f);
            }
        }
        StartCoroutine(Wait(minSpawnTime, maxSpawnTime));
    }
}
