using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{

    public float timeBeforeDestruction = 3f;

    void Start()
    {
        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(timeBeforeDestruction);
        Destroy(gameObject);
    }

}
