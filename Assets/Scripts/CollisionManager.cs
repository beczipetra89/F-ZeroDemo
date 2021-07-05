using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "HexCollider")
        {
            //play particles
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "HexCollider")
        {
            //play particles
            //Decrease health by seconds
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "HexCollider")
        {
            //stop playing particles
        }
    }
}
