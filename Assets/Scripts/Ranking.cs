//EZ AZ ENYEM


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.Utility;

//WaypointCircuit.css, WayPointProgressTracker.css


public class Ranking : MonoBehaviour
{
    public WaypointProgressTracker waypointprogresstracker;

    public float distanceTravelled;
   

    void Update()
    {
       distanceTravelled = waypointprogresstracker.progressDistance;
       
    }
}