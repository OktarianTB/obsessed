using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Money : MonoBehaviour
{
    public GameObject explosionParticle;
    float heal = 5f;
    int scorePerMoney = 10;

    new BoxCollider2D collider;
    PlayerHealth playerHealth;
    ScoreManager scoreManager;
    SoundManager soundManager;
 
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        collider = GetComponent<BoxCollider2D>();
        scoreManager = FindObjectOfType<ScoreManager>();
        soundManager = FindObjectOfType<SoundManager>();

        if (!playerHealth)
        {
            Debug.LogWarning("Player health script hasn't been found");
        }
        if (!explosionParticle)
        {
            Debug.LogWarning("No explosion particle");
        }
        if (!scoreManager)
        {
            Debug.LogWarning("Score manager hasn't been found");
        }
        if (!soundManager)
        {
            Debug.LogWarning("Sound manager hasn't been found");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            playerHealth.HealPlayer(heal);
            scoreManager.AddToScore(scorePerMoney);
            soundManager.PlayClip(soundManager.moneyClip, 0.4f);

            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }



}
