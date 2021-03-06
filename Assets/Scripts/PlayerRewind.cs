﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerRewind : MonoBehaviour
{
    int rewindScore = 10;
    float recordTime = 5f;
    float currentAlpha = 100f;
    float alphaDecrease;
    float timeInvincible = 2f;

    public bool playerIsInvicible = false;
    public bool timeIsRewinding;
    private bool canJump = true;

    public GameObject rewindCanvas;
    public GameObject infinityIcon;
    GameObject canvasInstance;
    GameObject infinityInstance;
    List<Vector3> positions;
    ScoreManager scoreManager;
    Player player;
 
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        player = FindObjectOfType<Player>();
        positions = new List<Vector3>();
        timeIsRewinding = false;

        if (!rewindCanvas)
        {
            Debug.LogWarning("Rewind canvas is missing");
        }
        if (!scoreManager)
        {
            Debug.LogWarning("Score manager hasn't been found");
        }
        if (!player)
        {
            Debug.LogWarning("Player hasn't been found");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && timeIsRewinding && canJump)
        {
            StopRewind();
        }
    }

    void FixedUpdate()
    {
        if (timeIsRewinding)
        {
            RewindPlayer();
            ManageBackground();
        }
        else
        {
            RecordPositions();
        }
    }

    private void RewindPlayer()
    {
        if(positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    private void RecordPositions()
    {
        if(positions.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            positions.RemoveAt(positions.Count - 1);
        }

        positions.Insert(0, transform.position);
    }

    public void StartRewind()
    {
        if (player.gameIsFinished)
        {
            return;
        }

        timeIsRewinding = true;
        Time.timeScale = 0.6f;
        canvasInstance = Instantiate(rewindCanvas);
        alphaDecrease = 100 / (float) positions.Count;
        scoreManager.AddToScore(-rewindScore);
        playerIsInvicible = true;

        canJump = false;
        StartCoroutine(ResetPlayerJump());
    }

    private void StopRewind()
    {
        Destroy(canvasInstance);
        timeIsRewinding = false;
        Time.timeScale = 1f;
        currentAlpha = 100f;

        ManageInvincibility();
    }

    private void ManageInvincibility()
    {
        infinityInstance = Instantiate(infinityIcon, transform.position, Quaternion.identity);
        StartCoroutine(ResetInvincibility());
    }

    private void ManageBackground()
    {
        GameObject panel = canvasInstance.transform.Find("Panel").gameObject;
        Image image = panel.GetComponent<Image>();

        if (!image)
        {
            Debug.LogWarning("No Image was found");
            return;
        }

        currentAlpha -= alphaDecrease;

        Color fadeColor = new Color(0.87f, 0.86f, 1f, currentAlpha / 255);
        image.color = fadeColor;

    }

    private IEnumerator ResetInvincibility()
    {
        yield return new WaitForSeconds(timeInvincible);
        playerIsInvicible = false;
        Destroy(infinityInstance);
    }

    private IEnumerator ResetPlayerJump()
    {
        yield return new WaitForSeconds(0.4f);
        canJump = true;
    }

}
