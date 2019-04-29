using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spikes : MonoBehaviour
{

    float damage = 20f;

    new Collider2D collider;
    PlayerHealth playerHealth;
    PlayerRewind playerRewind;
    SoundManager soundManager;

    void Start()
    {
        collider = GetComponent<Collider2D>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Player")) && !playerRewind.timeIsRewinding)
        {
            playerHealth.DamagePlayer(damage);
            soundManager.PlayClip(soundManager.ouchClip, 0.05f);
            playerRewind.StartRewind();
        }
    }

}
