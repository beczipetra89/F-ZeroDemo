using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHaptics : MonoBehaviour
{
    // public int AVM_ID;
    public bool isCrashing;
    public bool isEdgeColliding;

    // Collision with other cars
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "AiCarCapsule" || other.gameObject.tag == "NPCDriver")
        {
            Debug.Log(this.gameObject.name + " -----------------CAR CRASH ");
            isCrashing = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "AiCar" || other.gameObject.tag == "NPCDriver")
        {
            isCrashing = false;
        }
    }

    // Collision with edge (hexagons)
    void OnTriggerStay(Collider other)
    { 
        if (other.gameObject.tag == "Hex_Coll_L" || other.gameObject.tag == "Hex_Coll_R")
        {
            Debug.Log(this.gameObject.name + " Edge ");
            isEdgeColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hex_Coll_L" || other.gameObject.tag == "Hex_Coll_R")
        {
            isEdgeColliding = false;
        }
    }

    // METHODS: SingleVibration(), ContinuousVibration()
}