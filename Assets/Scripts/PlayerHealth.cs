using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
 
    public float currentHealth = 100f;

    public void DamagePlayer(float damage)
    {
        currentHealth -= damage;
    }

    public void HealPlayer(float heal)
    {
        currentHealth += heal;
    }

}
