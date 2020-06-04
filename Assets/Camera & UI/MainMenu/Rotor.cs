using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor : MonoBehaviour
{
    [SerializeField] float speed = 90f;

    void FixedUpdate() {
        transform.Rotate(Vector3.right * speed * Time.fixedDeltaTime);
    }
}
