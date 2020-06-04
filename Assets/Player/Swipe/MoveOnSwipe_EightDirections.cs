using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;

public class MoveOnSwipe_EightDirections : MonoBehaviour
{
    bool canSwipeRight = true;
    bool canSwipeLeft = true;
    PlayerMovement playerMovement;

    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void OnSwipeHandler(string id)
    {
        CheckWhichLane();
        switch(id)
        {
            case DirectionId.ID_UP:
                MoveUp();
                break;

            case DirectionId.ID_DOWN:
                MoveDown();
                break;

            case DirectionId.ID_LEFT:
                MoveLeft();
                break;

            case DirectionId.ID_RIGHT:
                MoveRight();
                break;
        }
    }

    void CheckWhichLane(){
        if(transform.position.x >= 1f){//right lane
            canSwipeRight = false;
            canSwipeLeft = true;
        }
        else if (transform.position.x <= -1f){//left lane
            canSwipeLeft = false;
            canSwipeRight = true;
        }
        else{
            canSwipeRight = true;
            canSwipeLeft = true;
        }
    }

    private void MoveRight()
    {
        if (canSwipeRight)
        {
            playerMovement.ChangeLanes("right");
        }
    }

    private void MoveLeft()
    {
        if (canSwipeLeft)
        {
            playerMovement.ChangeLanes("left");
        }
    }

    private void MoveDown()
    {
        playerMovement.Duck();
    }

    private void MoveUp()
    {
        playerMovement.Jump();
    }
}
