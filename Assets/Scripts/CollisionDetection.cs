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

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerRewind = FindObjectOfType<PlayerRewind>();

        if (!playerHealth)
        {
            Debug.LogWarning("Player Health script hasn't been found");
        }
        if (!playerRewind)
        {
            Debug.LogWarning("Player Rewind script hasn't been found");
        }
    }

    void Update()
    {
        ManageRaycast();
    }

    private void ManageRaycast()
    {
        float rayLength = 0.55f;
        rightCenter = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rightCenter, Vector2.right, rayLength, collisionMask);
        Debug.DrawLine(rightCenter, rightCenter + Vector2.right * rayLength, Color.red);

        if (hit && !playerRewind.timeIsRewinding)
        {
            playerHealth.DamagePlayer(damage);
            playerRewind.StartRewind();
        }
    }
}
