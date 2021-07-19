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
    float startingTime = 8.4f;

    [Header("TEXT OUTPUTS")]
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI goTxt;
    public TextMeshProUGUI lapTxt;
    public int laps;

    public GameObject cameraTargetObject;

    public AudioSource audioSource;
  
    

    void Start()
    {
        goTxt.enabled = false;
        countdownText.enabled = false;
      
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

        //  countdownText.text = currentTime.ToString("0");

        if (currentTime <= 4)
        {
            countdownText.enabled = true;
            countdownText.text = "3";
        }

        if (currentTime <= 3)
        {
            countdownText.text = "2";
        }

        if (currentTime <= 2)
        {
            countdownText.text = "1";
        }
        
        if (currentTime <= 1)
        {
            countDownFinished = true;
            countdownText.enabled = false;
            Destroy(cameraTargetObject.GetComponent<Animator>());
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

        if (racestarted && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (goTxt.enabled)
        {
             StartCoroutine(ExampleCoroutine());
        }

        lapTxt.text = playerLapTracker.GetComponent<CarLap>().lapNumber.ToString("0");

        if (lapTxt.text == "1")
        {
            goTxt.enabled = true;
            goTxt.text = "Game Over";
        }
        


    }


    private IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(2);
        goTxt.enabled = false;
    }

}