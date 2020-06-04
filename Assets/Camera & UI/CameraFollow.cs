using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] Vector3 offset;
    [SerializeField] float speed = 1f;

    void LateUpdate(){
        Vector3 targetPos = player.transform.position + offset;
        if(!player)
            return;

        if(!player.IsGrounded()){
            transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        }
        else
        {
            if(transform.position != targetPos){
                float dir = targetPos.y - transform.position.y;
                if (dir > 0.15f){
                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                }
                else if(dir < -0.15f){
                    transform.Translate(-Vector3.up * speed * Time.deltaTime);
                }
            }            
            transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        }
    }
}
