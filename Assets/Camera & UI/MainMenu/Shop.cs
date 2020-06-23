using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Material vantablack;

    Renderer renderer;

    void Start(){
        renderer = player.GetComponent<Renderer>();
    }

    public void ApplyVantablack(){
        renderer.material = vantablack;
    }
}
