using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    [SerializeField] bool hasOffset;
    [SerializeField] float tumbleSpeed = 0.85f;
    [SerializeField] float force = 50f;
    [SerializeField] float minYOffset = 0.25f;
    [SerializeField] float maxYOffset = -0.4f;
    //make asteroids collide with platform

    Rigidbody rb;
    MeshRenderer renderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponentInChildren<MeshRenderer>();

        rb.angularVelocity = Random.insideUnitSphere * tumbleSpeed;

        Vector3 direction;
        if(hasOffset){
            //float yOffset = Random.Range(minYOffset, maxYOffset);
            //direction = new Vector3(-1f, yOffset, 0f);
            direction = -Vector3.right;
        }
        else{
            direction = -Vector3.right;
        }
        rb.AddForce(direction * force);

        StartCoroutine(CheckDestroy());
        print(direction);
    }

    IEnumerator CheckDestroy(){
        yield return new WaitForSeconds(5f);
        while(true){
            yield return new WaitForSeconds(1f);
            if(renderer.isVisible == false){
                Destroy(gameObject);
            }
        }
    }



//not normal distribution 2/3 are outisde range 0,1 and only 1/3 inside
    float NormalDistribution() //https://de.wikipedia.org/wiki/Polar-Methode
    {
        float u = 0f;
        float v = 0f;
        float q = 1f;

        while(q >= 1.0f){
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            q = u * u + v * v;
        }

        float p = (float)Mathf.Sqrt(-2.0f * Mathf.Log(q) / q);
        return u * p;
    }
}