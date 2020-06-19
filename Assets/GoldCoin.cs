using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    [SerializeField] float minRotationSpeed;
    [SerializeField] float maxRotationSpeed;

    float rotationSpeed;

    void Start(){
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }
    
    void FixedUpdate()
    {
        transform.Rotate(0f,0f,-rotationSpeed * Time.fixedDeltaTime);
    }
}
