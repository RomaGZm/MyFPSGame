using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnEnable()
    {
       StartCoroutine(DisableTimeOut(5));
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

    
    IEnumerator DisableTimeOut(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
