using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Money : MonoBehaviour
{

    float heal = 10f;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Player"))){
            playerHealth.HealPlayer(heal);
            Destroy(gameObject);
        }
    }



}
