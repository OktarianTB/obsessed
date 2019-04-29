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
    bool particlesAreActive = true;

    public ParticleSystem particle;
    Vector3 moveDistance;
    new Rigidbody2D rigidbody;
    new BoxCollider2D collider;
    PlayerRewind playerRewind;
    LevelManager levelManager;
    ScoreManager scoreManager;
    SoundManager soundManager;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        playerRewind = FindObjectOfType<PlayerRewind>();
        levelManager = FindObjectOfType<LevelManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        soundManager = FindObjectOfType<SoundManager>();

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
        if (!soundManager)
        {
            Debug.LogWarning("Sound manager hasn't been found");
        }
        if (!particle)
        {
            Debug.LogWarning("Particle system is missing");
        }
    }
    
    void FixedUpdate()
    {
        if (!playerRewind.timeIsRewinding)
        {
            MovePlayer();
        }

        ManageScore();
        ManageParticle();
    }

    private void MovePlayer()
    {
        moveDistance.x = moveSpeed;

        if (PlayerIsGrounded())
        {
            moveDistance.y = 0;
        }
        else
        {
            moveDistance.y += gravity * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Space) && PlayerIsGrounded() && !gameIsFinished)
        {
            moveDistance.y = jumpVelocityY;
            moveDistance.x += jumpVelocityX;
            soundManager.PlayClip(soundManager.randomJumpClip(), 0.1f);
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

    public void ManageParticle()
    {
        if(particlesAreActive && playerRewind.timeIsRewinding)
        {
            particle.Stop();
            particlesAreActive = false;
        }
        if (!particlesAreActive && !playerRewind.timeIsRewinding)
        {
            particle.Play();
            particlesAreActive = true;
        }
    }

    private IEnumerator LoadEnd()
    {
        yield return new WaitForSeconds(timeUntilNextLevel);
        levelManager.LoadNextLevel();
    }

}
