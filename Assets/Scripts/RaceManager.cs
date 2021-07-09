using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using TMPro;


public class RaceManager : MonoBehaviour
{
    bool racestarted = false;
    bool countDownFinished = false;
   
    GameObject[] AICars;

    float currentTime = 0f;
    float startingTime = 4f;

    public TextMeshProUGUI countdownText;

    void Start()
    {
        AICars = GameObject.FindGameObjectsWithTag("AiCar");
        foreach(GameObject car in AICars)
        {
            car.GetComponent<CarAIControl>().enabled = false;
        }

        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            countDownFinished = true;
        }

        if (!racestarted && countDownFinished)
        {
            foreach(GameObject car in AICars)
            {
                car.GetComponent<CarAIControl>().enabled = true;
            }
            racestarted = true;
        }
    }
}
