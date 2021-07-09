//EZ AZ ENYEM


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.Utility;
using TMPro;



public class Ranking : MonoBehaviour
{
    public WaypointProgressTracker waypointprogresstracker;
    public float distanceTravelled;

 /*   public float[] car_positions;
    public GameObject Player;
    public float PlayerPosition;
    public GameObject[] AI;
    public int currentPos;
    public TextMeshProUGUI posText;

   */

    void Update()
    {
       distanceTravelled = waypointprogresstracker.progressDistance;
 //      PositionCalc();
   //    posText.text = currentPos.ToString() + " / " + car_positions.Length;
    }
/*
    public void PositionCalc()
    {
        car_positions[0] = Player.GetComponent<CarUserControl>().playerDistance;
        car_positions[1] = AI[0].GetComponent<CarAIControl>().aiDistance;
        car_positions[2] = AI[1].GetComponent<CarAIControl>().aiDistance;
        car_positions[3] = AI[2].GetComponent<CarAIControl>().aiDistance;

        PlayerPosition = Player.GetComponent<CarUserControl>().playerDistance;

        Array.Sort(car_positions);

        int x = Array.IndexOf(car_positions, PlayerPosition);

        switch (x)
        {
            case 0:
                currentPos = 1;
                break;
            case 1:
                currentPos = 2;
                break;
            case 2:
                currentPos = 3;
                break;
            case 3:
                currentPos = 4;
                break;
        }
       
    }*/
}