using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlanetHandler : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject[] planets;
    [SerializeField] float[] rotationSpeeds;
    [SerializeField] float[] zPos;
    [SerializeField] float[] activationRotations;
    [SerializeField] float[] activationDistances;
    [SerializeField] float camCullingRange = 5000f;
    
    float timeToGo = 30f;

    List<PlayableDirector> timelines = new List<PlayableDirector>();

    void FixedUpdate(){
        Orbit();
        HandlePlanetActivation();
        //DoEvery10s();
    }

    void Orbit(){
        for (int i = 0; i < planets.Length; i++)
        {
            planets[i].transform.Rotate(0f, -rotationSpeeds[i] * Time.fixedDeltaTime, 0f);       
        }
    }


    void HandlePlanetActivation(){
        for(int i = 0; i < planets.Length; i++){
            if(player.position.z >= zPos[i]){ 
                planets[i].SetActive(false);
            }
            if(planets[i].transform.eulerAngles.y <= activationRotations[i]){
                if(player.position.z >= activationDistances[i]){
                    planets[i].SetActive(true);
                }
            }
            else if(planets[i].transform.eulerAngles.y <= (360f-activationRotations[i])){
                planets[i].SetActive(false);
            }
        }
    }

    void DoEvery10s(){
        if(Time.fixedTime >= timeToGo){ //running every 10s
            timeToGo = Time.fixedTime + 10f;
        }
    }
}
