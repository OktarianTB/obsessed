using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class End : MonoBehaviour
{
    new BoxCollider2D collider;
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        collider = GetComponent<BoxCollider2D>();

        if (!player)
        {
            Debug.LogWarning("Player hasn't been found");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            player.PlayerDeath();
        }
    }
}
