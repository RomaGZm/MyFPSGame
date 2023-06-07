using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 axis = Vector3.up;
    public float speed = 1;

    
    void Update()
    {
        transform.Rotate(axis * speed * Time.deltaTime); 
    }
}
