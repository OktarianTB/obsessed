using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spikes : MonoBehaviour
{

    float damage = 25f;

    new Collider2D collider;
    PlayerHealth playerHealth;
    PlayerRewind playerRewind;
    SoundManager soundManager;
    public GameObject spikeParticle;

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
        if (!spikeParticle)
        {
            Debug.LogWarning("Spike particle is missing");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Player")) && !playerRewind.timeIsRewinding && !playerRewind.playerIsInvicible)
        {
            Instantiate(spikeParticle, transform.position, Quaternion.identity);
            playerHealth.DamagePlayer(damage);
            soundManager.PlayClip(soundManager.ouchClip, 0.55f);
            playerRewind.StartRewind();
        }
    }

}
