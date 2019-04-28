using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Transform bar;
    PlayerHealth playerHealth;

    float currentBarHealth;
    float increasePerPeriod = 0.25f;
    float period = 0.1f;
    float time;

    void Start()
    {
        bar = transform.Find("Bar");
        playerHealth = FindObjectOfType<PlayerHealth>();

        if (!bar)
        {
            Debug.LogWarning("Bar child wasn't found");
            return;
        }
        if (!playerHealth)
        {
            Debug.LogWarning("Player health wasn't found");
            return;
        }

        bar.localScale = new Vector3(1f, 1f, 1f);
        currentBarHealth = 100f;
        time = 0f;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= period)
        {
            time = 0f;
            UpdateBar();
        }
    }

    public void UpdateBar()
    {
        if(currentBarHealth == playerHealth.currentHealth)
        {
            return;
        }
        else if(currentBarHealth < playerHealth.currentHealth)
        {
            currentBarHealth += increasePerPeriod;
        }
        else if(currentBarHealth > playerHealth.currentHealth)
        {
            currentBarHealth -= increasePerPeriod;
        }

        float scaleX = currentBarHealth / 100;
        float clampedScaleX = Mathf.Clamp(scaleX, 0f, 1f);
        bar.localScale = new Vector3(clampedScaleX, 1f, 1f);

    }

}
