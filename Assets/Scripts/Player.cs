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
    float jumpVelocityX = 5f;
    float jumpHeight = 2.5f;
    float timeToJumpApex = .3f;
    float moveSpeed = 3f;

    int scorePerSecond = 1;
    int numberOfFramesPerSecond;
    int currentCount = 0;

    float timeUntilNextLevel = 2f;
    bool gameIsFinished = false;

    Vector3 moveDistance;
    new Rigidbody2D rigidbody;
    new BoxCollider2D collider;
    PlayerRewind playerRewind;
    LevelManager levelManager;
    ScoreManager scoreManager;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        playerRewind = FindObjectOfType<PlayerRewind>();
        levelManager = FindObjectOfType<LevelManager>();
        scoreManager = FindObjectOfType<ScoreManager>();

        jumpVelocityY = 2 * jumpHeight / timeToJumpApex;
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);

        numberOfFramesPerSecond = Mathf.RoundToInt(1 / Time.fixedDeltaTime);

        scoreManager.ResetScore();

        if (!playerRewind)
        {
            Debug.LogWarning("Player rewind is missing");
        }
        if (!levelManager)
        {
            Debug.LogWarning("Level Manager wasn't found");
        }
        if (!scoreManager)
        {
            Debug.LogWarning("Score manager hasn't been found");
        }
    }
    
    void FixedUpdate()
    {
        if (!playerRewind.timeIsRewinding)
        {
            MovePlayer();
        }

        ManageScore();
    }

    private void MovePlayer()
    {
        moveDistance.x = moveSpeed;

        if (PlayerIsGrounded())
        {
            moveDistance.y = 0;
        }

        if(Input.GetKey(KeyCode.Space) && PlayerIsGrounded() && !gameIsFinished)
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

    private void ManageScore()
    {
        if (!playerRewind.timeIsRewinding && !gameIsFinished)
        {
            if(currentCount >= numberOfFramesPerSecond)
            {
                currentCount = 0;
                scoreManager.AddToScore(scorePerSecond);
            }

            currentCount++;
        }
    }

    public void PlayerDeath()
    {
        gameIsFinished = true;
        StartCoroutine(LoadEnd());
        scoreManager.UpdateHighScore();
    }

    private IEnumerator LoadEnd()
    {
        yield return new WaitForSeconds(timeUntilNextLevel);
        levelManager.LoadNextLevel();
    }

}
