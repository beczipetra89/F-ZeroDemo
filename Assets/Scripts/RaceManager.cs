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
    public GameObject Player;

    float currentTime = 0f;
    float startingTime = 4f;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI goTxt;

    void Start()
    {
        goTxt.enabled = false;
        AICars = GameObject.FindGameObjectsWithTag("AiCar");
        foreach (GameObject car in AICars)
        {
            car.GetComponent<CarAIControl>().enabled = false;
        }

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
            racestarted = true;
            goTxt.enabled = true;
        }

        if (goTxt.enabled)
        {
             StartCoroutine(ExampleCoroutine());
        }

    }


    private IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(2);
        goTxt.enabled = false;
    }

}