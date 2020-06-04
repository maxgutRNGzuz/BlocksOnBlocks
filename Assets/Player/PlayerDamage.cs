using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] ParticleSystem deathFX;
    [SerializeField] Vector3 deathFXOffset = new Vector3(0f,0.6f,0f);
    [SerializeField] float dieHeight = -0.5f;
    
    PlayerUI playerUI;
    PlayerMovement playerMovement;

    void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update(){
        if(transform.position.y <= dieHeight){
            Die();
        }
    }

    void OnCollisionEnter(Collision collider) {
        if(collider.gameObject.tag == "Obstacle"){
            Die();
            print(collider.transform.name);
        }
    }

    void Die(){
        ParticleSystem deathFXInstance = Instantiate(deathFX, transform.position+deathFXOffset, Quaternion.identity);
        Destroy(deathFXInstance, deathFXInstance.duration);
        PlayerStats.Score = playerMovement.score;
        playerUI.FadeInDeadScreen();
        gameObject.SetActive(false);
    }
}
