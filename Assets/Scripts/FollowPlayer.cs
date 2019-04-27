using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();

        if (!player)
        {
            Debug.LogWarning("Player is missing");
        }
    }

    void Update()
    {
        if (!player)
        {
            Debug.LogWarning("CameraFollow script Update isn't running because an error has been detected");
            return;
        }

        FollowPlayerPosition();

    }

    private void FollowPlayerPosition()
    {
        float playerPositionX = player.transform.position.x;
        transform.position = new Vector3(playerPositionX, transform.position.y, transform.position.z);
    }
}
