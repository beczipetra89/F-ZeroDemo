
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Ranking : MonoBehaviour
{
    [Header("LIST OF CARS")]
    public GameObject playerCar;
    public GameObject aiCar_1;
    public GameObject aiCar_2;
    public GameObject aiCar_3;

    [Header("Player´s progess")]
    public int playerCar_laps;
    public int playerCar_checkpoints;

    [Header("Opponent 1´s progress")]
    public int aiCar1_laps;
    public int aiCar1_checkpoints;

    [Header("Opponent 2´s progress")]
    public int aiCar2_laps;
    public int aiCar2_checkpoints;

    [Header("Opponent 3´s progress")]
    public int aiCar3_laps;
    public int aiCar3_checkpoints;

 

    public List<GameObject> ranks;

    void Start()
    {
        ranks.Add(playerCar);
        ranks.Add(aiCar_1);
        ranks.Add(aiCar_2);
        ranks.Add(aiCar_3);
    }

    void Update()
    {
       
           // Player´s lap and checkpoint indexes
           playerCar_laps = playerCar.GetComponent<CarLap>().lapNumber;
           playerCar_checkpoints = playerCar.GetComponent<CarLap>().CheckpointIndex;

           // Opponent 1´s lap and checkpoint indexes
           aiCar1_laps =  aiCar_1.GetComponent<CarLap>().lapNumber;
           aiCar1_checkpoints = aiCar_1.GetComponent<CarLap>().CheckpointIndex;

           // Opponent 2´s lap and checkpoint indexes
           aiCar2_laps = aiCar_2.GetComponent<CarLap>().lapNumber;
           aiCar2_checkpoints = aiCar_2.GetComponent<CarLap>().CheckpointIndex;

           // Opponent 3´s lap and checkpoint indexes
           aiCar3_laps = aiCar_3.GetComponent<CarLap>().lapNumber;
           aiCar3_checkpoints = aiCar_3.GetComponent<CarLap>().CheckpointIndex;

           

        ranks.Sort(
            (x, y) =>
            y.GetComponent<CarLap>().getDistanceTravelled().CompareTo(
                x.GetComponent<CarLap>().getDistanceTravelled()
                )
            );
        
        for (int i=0; i < ranks.Count; i++)
        {
            ranks[i].GetComponent<CarLap>().rank = i;
        }
    }
}