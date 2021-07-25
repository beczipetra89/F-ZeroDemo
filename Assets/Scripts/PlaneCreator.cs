using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCreator : MonoBehaviour
{
    [SerializeField] Transform planeTransform;

    public Plane plane
    {
        private set;
        get;
    }

   
    void Update()
    {
        plane = new Plane(planeTransform.up, planeTransform.position);
        transform.position = planeTransform.position;
        transform.up = planeTransform.up;
    }
}
