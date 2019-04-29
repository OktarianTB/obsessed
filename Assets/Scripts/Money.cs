using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Money : MonoBehaviour
{
    public GameObject explosionParticle;
    float heal = 5f;

    BoxCollider2D collider;
    PlayerHealth playerHealth;
 
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        collider = GetComponent<BoxCollider2D>();

        if (!playerHealth)
        {
            Debug.LogWarning("Player health script hasn't been found");
        }
        if (!explosionParticle)
        {
            Debug.LogWarning("No explosion particle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Player"))){
            playerHealth.HealPlayer(heal);
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }



}
