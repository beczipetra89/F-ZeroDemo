using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

using TMPro;
using KartGame.KartSystems;

public class RaceManager : MonoBehaviour
{
    bool racestarted = false;
    bool countDownFinished = false;

    GameObject[] AICars;
    public GameObject Player;
    public GameObject playerLapTracker;

    float currentTime = 0f;
    float startingTime = 4f;

    [Header("TEXT OUTPUTS")]
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI goTxt;
    public TextMeshProUGUI lapTxt;

    void Start()
    {
        goTxt.enabled = false;
      
        AICars = GameObject.FindGameObjectsWithTag("AiCar");
        foreach (GameObject car in AICars)
        {
            car.GetComponent<CarAIControl>().enabled = false;
        }

        Player.GetComponent<ArcadeKart>().enabled = false;
        currentTime = startingTime;

    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 1)
        {
            countDownFinished = true;
            countdownText.enabled = false;
        }


        if (!racestarted && countDownFinished)
        {
            foreach (GameObject car in AICars)
            {
                car.GetComponent<CarAIControl>().enabled = true;
            }

            Player.GetComponent<ArcadeKart>().enabled = true;

            racestarted = true;
            goTxt.enabled = true;
        }

        if (goTxt.enabled)
        {
             StartCoroutine(ExampleCoroutine());
        }

        lapTxt.text = playerLapTracker.GetComponent<CarLap>().lapNumber.ToString("0");
    }


    private IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(2);
        goTxt.enabled = false;
    }

  
}