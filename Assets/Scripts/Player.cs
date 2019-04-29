using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{

    float gravity;
    float jumpVelocityY;
    float jumpVelocityX = 12f;
    float jumpHeight = 2.5f;
    float timeToJumpApex = .3f;
    float moveSpeed = 3f;
    float timeUntilNextLevel = 2f;

    Vector3 moveDistance;
    Rigidbody2D rigidbody;
    BoxCollider2D collider;
    PlayerRewind playerRewind;
    LevelManager levelManager;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        playerRewind = FindObjectOfType<PlayerRewind>();
        levelManager = FindObjectOfType<LevelManager>();

        jumpVelocityY = 2 * jumpHeight / timeToJumpApex;
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);

        if (!playerRewind)
        {
            Debug.LogWarning("Player rewind is missing");
        }
        if (!levelManager)
        {
            Debug.LogWarning("Level Manager wasn't found");
        }
    }
    
    void FixedUpdate()
    {
        if (!playerRewind.timeIsRewinding)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        moveDistance.x = moveSpeed;

        if (PlayerIsGrounded())
        {
            moveDistance.y = 0;
        }

        if(Input.GetKey(KeyCode.Space) && PlayerIsGrounded())
        {
            moveDistance.y = jumpVelocityY;
            moveDistance.x += jumpVelocityX;
        }

        if (!PlayerIsGrounded())
        {
            moveDistance.y += gravity * Time.deltaTime;
        }

        rigidbody.transform.Translate(moveDistance * Time.deltaTime);
    }

    bool PlayerIsGrounded()
    {
        return collider.IsTouchingLayers(LayerMask.GetMask("Objects"));
    }

    public void PlayerDeath()
    {
        Time.timeScale = 0f;
    }

    private IEnumerator LoadEnd()
    {
        yield return new WaitForSeconds(timeUntilNextLevel);
        levelManager.LoadNextLevel();
    }

}
