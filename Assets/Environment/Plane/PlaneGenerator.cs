using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlaneGenerator : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject[] planes;
    [SerializeField] float minSpawnTime = 15f;
    [SerializeField] float maxSpawnTime = 35f;
    [SerializeField] float timelineDuration = 5f;

    float countdown;

    void Start()
    {
        countdown = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        countdown -= Time.deltaTime;

        if(countdown <= 0f){
            SpawnPlane();
            countdown = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void SpawnPlane(){
        int i = Random.Range(0, planes.Length);
        GameObject planeInstance = Instantiate(planes[i], player.position, Quaternion.identity, transform);
        Destroy(planeInstance, 5f);
    }
}
