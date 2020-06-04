using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] SceneHandler sceneHandler;
    [SerializeField] Transform player;
    [Header("Platform")]
    [SerializeField] int platformsOnScene = 2;
    [SerializeField] GameObject[] ezPlatforms;
    [SerializeField] int ezPlatformsNum = 2;
    [SerializeField] GameObject[] platforms;
    [SerializeField] GameObject[] specialPlatforms;
    [SerializeField] float firstPlatformZ;
    [SerializeField] float distBetweenPlatforms = 205f;
    [SerializeField] float distBeforePlatformDespawn = 5f;
    [SerializeField] int platformsTillNextLevel = 15;
    [Header("Connector")]
    [SerializeField] GameObject connector;
    [SerializeField] float firstConnectorZ;
    [SerializeField] float connectorDistance = 205f;
    [SerializeField] float[] connectorXSpawnPos;

    List<GameObject> activePlatforms = new List<GameObject>();
    List<GameObject> activeConnectors = new List<GameObject>();
    PlayerMovement playerMovementInstance;
    PlayerUI playerUIInstance;
    float currentPlatformSpawnZ;
    float currentConnectorSpawnZ;
    int platformsSpawned;
    int lastPlatformIndex;
    bool endSpawned = false;

    void Start()
    {     
        playerMovementInstance = player.GetComponent<PlayerMovement>();
        playerUIInstance = player.GetComponent<PlayerUI>();
        currentPlatformSpawnZ = firstPlatformZ;
        currentConnectorSpawnZ = firstConnectorZ;

        int hasPlayed = PlayerPrefs.GetInt("HasPlayed");
        print(PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1));
        if(PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1){
            InitiateTutorial();
        }
        else{
            InitiateNormalStart();
        }
        
        for (int i = 0; i < platformsOnScene; i++)
        {
            SpawnPlatforms();
            SpawnConnectors();
        }

        SpawnConnectors();
    }

    
    void Update()
    {
        if(!sceneHandler.IsLastPhase()){
            if(platformsSpawned == platformsTillNextLevel){
                if(!endSpawned){
                    SpawnEndPlatform();
                }
                return;
            }
        }

        if(player.position.z - distBeforePlatformDespawn > (currentPlatformSpawnZ - platformsOnScene * distBetweenPlatforms)){
            SpawnPlatforms();
            SpawnConnectors();
            DespawnPassedPlatforms();
            DespawnPassedConnectors();
        }
    }

    void InitiateTutorial(){
        GameObject tutorialPlatformInstance = Instantiate(specialPlatforms[0], new Vector3(0f,-1f,30f), Quaternion.identity, transform);
        activePlatforms.Add(tutorialPlatformInstance);        
    }

    void InitiateNormalStart(){
        GameObject startPlatformInstance = Instantiate(specialPlatforms[1], new Vector3(0f,-1f,30f), Quaternion.identity, transform);
        activePlatforms.Add(startPlatformInstance);    
    }

    void SpawnPlatforms(){
        int index = 0;
        GameObject platformInstance = null;
        Vector3 spawnPos = new Vector3(0f, -1f, currentPlatformSpawnZ);
        if(platformsSpawned < ezPlatformsNum){
            platformInstance = Instantiate(ezPlatforms[platformsSpawned], spawnPos, Quaternion.identity, transform);
        }
        else{
            while(index == lastPlatformIndex){ // always a new platform
                index = Random.Range(0, platforms.Length);
            }
            platformInstance = Instantiate(platforms[index], spawnPos, Quaternion.identity, transform);
            lastPlatformIndex = index;
        }
        currentPlatformSpawnZ += distBetweenPlatforms;
        activePlatforms.Add(platformInstance);
        platformsSpawned ++;
    }

    void SpawnConnectors(){
        int index = Random.Range(0, connectorXSpawnPos.Length);
        Vector3 spawnPos = new Vector3(connectorXSpawnPos[index], -1f, currentConnectorSpawnZ);
        GameObject connectorInstance = Instantiate(connector, spawnPos, Quaternion.identity, transform);
        currentConnectorSpawnZ += connectorDistance;
        activeConnectors.Add(connectorInstance);
    }

    void DespawnPassedPlatforms(){
        Destroy(activePlatforms[0]);
        activePlatforms.RemoveAt(0);
    }

    void DespawnPassedConnectors(){
        Destroy(activeConnectors[0]);
        activeConnectors.RemoveAt(0);
    }

    void SpawnEndPlatform(){
        Vector3 spawnPos = new Vector3(0f, -1f, currentPlatformSpawnZ-85f);
        GameObject endPlatformInstance = Instantiate(specialPlatforms[2], spawnPos, Quaternion.identity, transform);
        endSpawned = true;
    }
}
