using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        transform.Rotate(0f,0f,rotationSpeed*Time.fixedDeltaTime);
    }
}
