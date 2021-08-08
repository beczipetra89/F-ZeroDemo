using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarLap : MonoBehaviour
{
    public int lapNumber;
    public int CheckpointIndex; // current checkpoint hit
    public int distanceTravelled;
    public int rank=-1;

    public bool getNitro;

    [Header("Texts")]
    public TextMeshProUGUI rankTxt;

    void Start()
    {
        lapNumber = 0;
        CheckpointIndex = 0;
        getNitro = false;
    }

    void Update()
    {
        rankTxt.text = (rank+1).ToString("0");
    }

    public int getDistanceTravelled()
    {
        distanceTravelled = lapNumber * 41 + CheckpointIndex;
        return distanceTravelled;
    }

    
}
