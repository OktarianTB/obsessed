using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spikes : MonoBehaviour
{

    float damage = 20f;

    Collider2D collider;
    PlayerHealth playerHealth;
    PlayerRewind playerRewind;

    void Start()
    {
        collider = GetComponent<Collider2D>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Player")) && !playerRewind.timeIsRewinding)
        {
            playerHealth.DamagePlayer(damage);
            playerRewind.StartRewind();
        }
    }

}
