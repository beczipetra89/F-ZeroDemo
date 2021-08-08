using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapHandle : MonoBehaviour
{
    public int TotalCheckpoints;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CarLap>())
        {
            CarLap car = other.GetComponent<CarLap>();

            if (car.CheckpointIndex == TotalCheckpoints)
            {
                // the car reached the final checkpoint
                // reset the cars checkpoint index and finish this lap

                car.CheckpointIndex = 0;
                car.lapNumber++;
                car.getNitro = true;
                 
            }
            else
            {
                car.getNitro = false;
            }
        }
    }
}
