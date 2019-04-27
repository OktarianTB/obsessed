using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{

    float gravity;
    float jumpVelocity;
    float jumpHeight = 2.5f;
    float timeToJumpApex = .4f;
    float moveSpeed = 2;
    float accelerationTimeAirborne = 0.15f;
    float accelerationTimeGrounded = 0.1f;
    float velocitySmoothing;

    Vector3 moveDistance;
    Controller2D controller;


    void Start()
    {
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = 2 * jumpHeight / timeToJumpApex;

        controller = GetComponent<Controller2D>();
    }
    
    void Update()
    {
        if(controller.collisionInfo.above || controller.collisionInfo.below)
        {
            moveDistance.y = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisionInfo.below)
        {
            moveDistance.y += jumpVelocity;
        }

        float smoothingTime = controller.collisionInfo.below ? accelerationTimeGrounded : accelerationTimeAirborne;
        moveDistance.x = Mathf.SmoothDamp(moveDistance.x, moveSpeed, ref velocitySmoothing, smoothingTime);

        moveDistance.y += gravity * Time.deltaTime;

        controller.MovePlayer(moveDistance * Time.deltaTime);

    }
}
