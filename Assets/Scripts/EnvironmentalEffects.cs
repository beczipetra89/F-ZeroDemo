using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class EnvironmentalEffects : MonoBehaviour
{
    public GameObject playerController;
    [SerializeField]
    private Rigidbody rb;
    public float forcePower;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Slippery")
        {
            // DRIFT
            playerController.GetComponent<ArcadeKart>().baseStats.Braking = 5f;
            playerController.GetComponent<ArcadeKart>().baseStats.CoastingDrag = 0.1f;
            playerController.GetComponent<ArcadeKart>().baseStats.Grip = 0f;
            playerController.GetComponent<ArcadeKart>().DriftGrip = 0.01f;
            playerController.GetComponent<ArcadeKart>().DriftAdditionalSteer = 10f;
            playerController.GetComponent<ArcadeKart>().MinAngleToFinishDrift = 0f;
            playerController.GetComponent<ArcadeKart>().DriftControl = 1f;
            playerController.GetComponent<ArcadeKart>().DriftDampening = 20f;
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Slippery")
        {
            rb.AddRelativeForce(Vector3.left * forcePower);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Slippery")
        {
            // GAIN BACK NORMAL CONTROL (GOOD GRIP)
            playerController.GetComponent<ArcadeKart>().baseStats.Braking = 16f;
            playerController.GetComponent<ArcadeKart>().baseStats.CoastingDrag = 5f;
            playerController.GetComponent<ArcadeKart>().baseStats.Grip = 0.97f;
            playerController.GetComponent<ArcadeKart>().DriftGrip = 0.85f;
            playerController.GetComponent<ArcadeKart>().DriftAdditionalSteer = 0f;
            playerController.GetComponent<ArcadeKart>().MinAngleToFinishDrift = 29f;
            playerController.GetComponent<ArcadeKart>().DriftControl = 16f;
            playerController.GetComponent<ArcadeKart>().DriftDampening = 8f;
        }
    }

}
