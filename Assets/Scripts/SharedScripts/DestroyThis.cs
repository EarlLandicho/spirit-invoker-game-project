using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    [SerializeField] private bool isTimedDestroy;
    [SerializeField] private float timedDestroyTime;

    // Called in Animator
    public void DestroyThisObject()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if(isTimedDestroy)
        {
            isTimedDestroy = false;
            StartCoroutine("DestroyTimedObject");
        }
    }

    private IEnumerator DestroyTimedObject()
    {
        yield return new WaitForSeconds(timedDestroyTime);
        Destroy(gameObject);
    }
}