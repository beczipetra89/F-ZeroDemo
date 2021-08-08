using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCheckPoint : MonoBehaviour
{
    public int Index;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.GetComponent<CarLap>())
        {
            CarLap car = other.GetComponent<CarLap>();

            if (car.CheckpointIndex == Index - 1)
            {
                car.CheckpointIndex = Index;
            }
        }
    }
}

