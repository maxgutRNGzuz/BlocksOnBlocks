using System;
using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    bool canSwipeRight, canSwipeLeft = true;

    [SerializeField] bool detectSwipeOnlyAfterRelease = false;
    [SerializeField] float minDistanceForSwipe = 20f;
    [SerializeField] float swipeCooldown = 0.0f;

    PlayerMovement playerMovement;
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    float currentSwipeCooldown;

    public static event Action<SwipeData> OnSwipe = delegate { };

    void Start(){
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if(!canSwipeRight && !canSwipeLeft){ //if just swiped
            currentSwipeCooldown -= Time.deltaTime;
            if(currentSwipeCooldown <= 0f){
                CheckWhichLane();
            }
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
        Debug.DrawLine(fingerDownPosition, fingerUpPosition, Color.red);
    }

    void CheckWhichLane(){
        if(transform.position.x >= 1f){//right lane
            canSwipeRight = false;
            canSwipeLeft = true;
            print("rightLane");
        }
        else if (transform.position.x <= -1f){//left lane
            canSwipeLeft = false;
            canSwipeRight = true;
            print("leftLane");
        }
        else{
            canSwipeRight = true;
            canSwipeLeft = true;
        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {
        //changed code
        if(direction == SwipeDirection.Up){
            playerMovement.Jump();
        }
        if(direction == SwipeDirection.Right){
            if(canSwipeRight){
                canSwipeRight = false;
                canSwipeLeft = false;
                currentSwipeCooldown = swipeCooldown;
                playerMovement.ChangeLanes("right");
                print("rightSwipe");
            }
        }
        if(direction == SwipeDirection.Down){
            playerMovement.Duck();
        }
        if(direction == SwipeDirection.Right){
            if(canSwipeLeft){
                playerMovement.ChangeLanes("left");
                canSwipeRight = false;
                canSwipeLeft = false;    
                currentSwipeCooldown = swipeCooldown;   
                print("leftSwipe");
            }
        }
        // switch (direction)
        // {
        //     case SwipeDirection.Up:
        //         playerMovement.Jump();
        //         break;
        //     case SwipeDirection.Right:
        //         if(canSwipeRight){
        //             canSwipeRight = false;
        //             canSwipeLeft = false;
        //             currentSwipeCooldown = swipeCooldown;
        //             playerMovement.ChangeLanes("right");
        //             print("rightSwipe");
        //         }
        //         break;
        //     case SwipeDirection.Down:
        //         playerMovement.Duck();
        //         break;
        //     case SwipeDirection.Left:
        //         if(canSwipeLeft){
        //             playerMovement.ChangeLanes("left");
        //             canSwipeRight = false;
        //             canSwipeLeft = false;    
        //             currentSwipeCooldown = swipeCooldown;   
        //             print("leftSwipe");         
        //         }
        //         break;
        // }
    }
}

public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}