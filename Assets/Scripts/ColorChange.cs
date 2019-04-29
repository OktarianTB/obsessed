using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorChange : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Color colorA, colorB;
    Color[] colors = {Color.blue, Color.magenta, Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.cyan }; //must be an even length

    int colorIndex;
    public float pingPongSpeedFactor = 0.05f;
    float currentPingPongValue;
    bool pingPongFunctionIsIncreasing;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorA = colors[0];
        colorB = colors[1];
        colorIndex = 0;
        pingPongFunctionIsIncreasing = true;

        if(colors.Length % 2 != 0)
        {
            Debug.LogError("Length of colors array isn't even: the color change logic will not work");
        }
    }
    
    void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        spriteRenderer.color = Color.Lerp(colorA, colorB, Mathf.PingPong(Time.time * pingPongSpeedFactor, 1));
        currentPingPongValue =  Mathf.PingPong(Time.time * pingPongSpeedFactor, 1);
        ManageColorChange();
    }

    private void ManageColorChange()
    {
        if(currentPingPongValue > 0.985f && pingPongFunctionIsIncreasing)
        {
            pingPongFunctionIsIncreasing = false;
            UpdateColor();
        }
        else if(currentPingPongValue < 0.015 && !pingPongFunctionIsIncreasing)
        {
            pingPongFunctionIsIncreasing = true;
            UpdateColor();
        }
    }

    private void UpdateColor()
    {
        if(colorIndex < colors.Length - 2)
        {
            if (!pingPongFunctionIsIncreasing)
            {
                colorA = colors[colorIndex + 2];
            }
            else
            {
                colorB = colors[colorIndex + 2];
            }

            colorIndex++;
        }
        else if (colorIndex == colors.Length - 2)
        {
            colorA = colors[0];
            colorIndex = -1;
        }
    }

}
