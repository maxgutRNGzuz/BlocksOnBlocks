using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xMov = 1.5f;
    public float currentSpeed;
    public int score;

    [SerializeField] Animator bodyAnim;
    [SerializeField] SceneHandler sceneHandler;
    [SerializeField] float startSpeed = 2f;
    [SerializeField] float maxSpeed = 8f;
    [SerializeField] int platsToReachMaxSpeed = 20;
    [SerializeField] float jumpForce = 1000f;
    [SerializeField] float duckForce = 4f;
    [SerializeField] float bodyYScale = 1f;

    PlayerUI playerUI;
    Rigidbody rb;
    Touch touch;
    Vector3 touchPos;

    void Start(){
        playerUI = GetComponent<PlayerUI>();
        rb = GetComponentInChildren<Rigidbody>();
        currentSpeed = startSpeed;
        score = PlayerStats.Score;
    }

    void FixedUpdate(){
        rb.MovePosition(transform.position + Vector3.forward.normalized * currentSpeed * Time.deltaTime);
        score += Mathf.RoundToInt(currentSpeed/4f);
        playerUI.UpdateScore(score);
    }

    public void Jump(){
        if(IsGrounded()){
            rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            bodyAnim.SetTrigger("jump"); //to reset duck and transition directly
        }
    }

    public void Duck(){
        bodyAnim.ResetTrigger("jump");
        bodyAnim.SetTrigger("duck");
        if(!IsGrounded()){
            rb.AddForce(Vector3.down*duckForce, ForceMode.Impulse);
        }
    }

    public void ChangeLanes(string direction){
        if(direction == "right"){
            transform.position = new Vector3(transform.position.x+xMov, transform.position.y, transform.position.z);
        }
        else if (direction == "left"){
            transform.position = new Vector3(transform.position.x-xMov, transform.position.y, transform.position.z);
        }
    }

    public bool IsGrounded(){
        return Physics.Raycast(transform.position, -Vector3.up, bodyYScale/2 + 0.1f);
    }

    public void PauseAnim(){
        bodyAnim.speed = 0f;
    }

    public void ContinueAnim(){
        bodyAnim.speed = 1f;
    }

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Finish"){
            //endplatform = collision.transform.root.GetComponent<Endplatform>();
            sceneHandler.NextPhase();
        }
        if(collision.gameObject.tag == "IncreaseSpeed"){
            IncreaseSpeed();
        }
    }

    void IncreaseSpeed(){
        bool isMaxSpeed = false;
        if(currentSpeed == maxSpeed){
            isMaxSpeed = true;
            playerUI.UpdateSpeedText(currentSpeed, isMaxSpeed);
            return;
        }
        currentSpeed += (maxSpeed-startSpeed) / platsToReachMaxSpeed;
        playerUI.UpdateSpeedText(currentSpeed, isMaxSpeed);
    }
}

