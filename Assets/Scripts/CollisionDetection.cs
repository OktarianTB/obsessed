using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CollisionDetection : MonoBehaviour
{
    float damage = 20f;

    public LayerMask collisionMask;
    Vector2 rightCenter;
    PlayerHealth playerHealth;
    PlayerRewind playerRewind;
    SoundManager soundManager;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerRewind = FindObjectOfType<PlayerRewind>();
        soundManager = FindObjectOfType<SoundManager>();

        if (!playerHealth)
        {
            Debug.LogWarning("Player Health script hasn't been found");
        }
        if (!playerRewind)
        {
            Debug.LogWarning("Player Rewind script hasn't been found");
        }
        if (!soundManager)
        {
            Debug.LogWarning("Sound manager hasn't been found");
        }
    }

    void Update()
    {
        ManageRaycast();
    }

    private void ManageRaycast()
    {
        float rayLength = 0.53f;
        rightCenter = transform.position;

        for(int i = -1; i < 2; i++)
        {
            Vector2 rayOrigin = rightCenter + Vector2.up * i * 0.2f;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayLength, collisionMask);
            Debug.DrawLine(rayOrigin, rayOrigin + Vector2.right * rayLength, Color.red);

            if (hit && !playerRewind.timeIsRewinding && !playerRewind.playerIsInvicible)
            {
                soundManager.PlayClip(soundManager.bangClip, 1.6f);
                playerHealth.DamagePlayer(damage);
                playerRewind.StartRewind();
            }
        }
    }

}
