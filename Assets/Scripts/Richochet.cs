using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Richochet : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 lastVelocity;

    void Awake()
    {
        rb = rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter(Collision coll)
    {
        float speed = lastVelocity.magnitude;

        // Bounce back / rochichet behaviour
        Vector3 direction = Vector3.Reflect(lastVelocity.normalized, coll.GetContact(0).normal);
        rb.velocity = direction * Mathf.Max(speed, 0f);


        // Don´t fly up when crash
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }

    void OnCollisionExit(Collision coll)
    {
        rb.constraints = RigidbodyConstraints.None;
    }
}

