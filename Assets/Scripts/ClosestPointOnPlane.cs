using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestPointOnPlane : MonoBehaviour
{

    [SerializeField] PlaneCreator planeCreator;
    [SerializeField] Transform transformRef;

    void Update()
    {
        transform.position = planeCreator.plane.ClosestPointOnPlane(transformRef.position);
    }
}
