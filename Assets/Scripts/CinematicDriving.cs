using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicDriving : MonoBehaviour
{
    public Transform target;
    public float speed = 0.1f;
  
    void Update()
    {
        transform.LookAt(target, Vector3.up);
        transform.position = Vector3.Lerp(transform.position, target.position, speed);
    }

}
