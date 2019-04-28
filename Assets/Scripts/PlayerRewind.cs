using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRewind : MonoBehaviour
{
    float recordTime = 5f;
    float currentAlpha = 100f;
    float alphaDecrease;
    public bool timeIsRewinding;

    public GameObject rewindCanvas;
    GameObject canvasInstance;
    List<Vector3> positions;
 
    void Start()
    {
        positions = new List<Vector3>();
        timeIsRewinding = false;

        if (!rewindCanvas)
        {
            Debug.LogWarning("Rewind canvas is missing");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && timeIsRewinding)
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
        timeIsRewinding = true;
        Time.timeScale = 0.6f;
        canvasInstance = Instantiate(rewindCanvas);
        alphaDecrease = 100 / (float) positions.Count;
    }

    private void StopRewind()
    {
        Destroy(canvasInstance);
        timeIsRewinding = false;
        Time.timeScale = 1f;
        currentAlpha = 100f;
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

}
