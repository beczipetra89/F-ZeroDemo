using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHaptics : MonoBehaviour
{
    public bool turnOnHaptics = false;
    [Header("Overtaker Haptics")]
    public bool isACarInRange = false;
    public int _intensity;
    private int lowIntensity = 100;
    private int medIntensity = 150;
    private int hightIntensity = 200;

    private float overtakerPos; // The current overtaker's position
    private float overtakerDistance; // The current overtaker's distance

    public GameObject overtakeIndicator;

    // Positions horizontal
    private float overtaker1;
    private float overtaker2;
    private float overtaker3;
    private float overtaker4;

    private float overtaker5; // TEST CUBE

    // Positions vertical (Distances)
    private float distance1;
    private float distance2;
    private float distance3;
    private float distance4;

    private float distance5;    // TEST CUBE distance

    public GameObject slider1;
    public GameObject slider2;
    public GameObject slider3;
    public GameObject slider4;

    public GameObject slider5; // TEST CUBE

    [Header("Charging Haptics")]
    //public GameObject chargingEvent;
    public bool _isCharging;
    bool sequenceExecuting = false;

    void Start()
    {
        Invoke("StartEventHaptics", 8.0f);
    }

    void Update()
    {
        if (turnOnHaptics) // RaceManager.countDownFinished
        {
            // Overtake Haptics
            if (slider1.activeSelf || slider2.activeSelf || slider3.activeSelf || slider4.activeSelf
                        || slider5.activeSelf) // DELETE SLIDER 5
            {
                isACarInRange = true;
            }
            if (!slider1.activeSelf && !slider2.activeSelf && !slider3.activeSelf && !slider4.activeSelf
                        && !slider5.activeSelf) // DELETE SLIDER 5
            {
                isACarInRange = false;
            }

            if (isACarInRange)
            {
                OvertakeHaptics();
            }
        }

        // Charging Haptics
        if (CollisionManager.isCharging)
        {
            _isCharging = true;
            if (!sequenceExecuting)
            {
                sequenceExecuting = true;
                ChargingHaptics();
            }
        }
        else{
            _isCharging = false;
            if (sequenceExecuting) 
            {
                StopAllCoroutines();
                 StopAllMotors();
                sequenceExecuting = false;
            }
            
        }


    }

    ///////////////////////////// OVERTAKER HAPTICS \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    void OvertakeHaptics()
    {
        overtakerPos = GetOvertakerPosition(overtakerPos);
        _intensity = SetIntensity(_intensity);

        // Decide which modules to vibrate during an overtaking attempt

        // LEFT RANGE: Vibrate [BL] if racer is coming from the left side behind 
        if (overtakerPos > -3685.511f && overtakerPos < 540.3436f)
        {
            SendCommands.turnOnMotor(2, _intensity);
            SendCommands.turnOffMotor(3);
            SendCommands.turnOffMotor(4);
        }

        // CENTER: Vibrate [B] if racer is coming from behind (center)
        if (overtakerPos > 540.3436f && overtakerPos < 3921.033f)
        {
            SendCommands.turnOnMotor(3, _intensity);
            SendCommands.turnOffMotor(2);
            SendCommands.turnOffMotor(4);
        }

        // RIGHT: Vibrate [BR] if racer is coming from the right side behind
        if (overtakerPos > 3921.033f && overtakerPos < 7724.306f)
        {
            SendCommands.turnOnMotor(4, _intensity);
            SendCommands.turnOffMotor(2);
            SendCommands.turnOffMotor(3);
        }
        else
        {
            ResetMotors();
        }

    }

    // Get the horizontal position from a racer behind the player
    public float GetOvertakerPosition(float xPos)
    {
        if (isACarInRange)
        {

            // DELETE THIS TEST CUBE............................................................
            if (slider5.activeSelf)
            {
                overtaker5 = overtakeIndicator.GetComponent<OvertakeIndicator>().xValue5;
                xPos = overtaker5;
            }

            if (slider1.activeSelf)
            {
                overtaker1 = overtakeIndicator.GetComponent<OvertakeIndicator>().xValue1;
                xPos = overtaker1;
            }

            if (slider2.activeSelf)
            {
                overtaker2 = overtakeIndicator.GetComponent<OvertakeIndicator>().xValue2;
                xPos = overtaker2;
            }

            if (slider3.activeSelf)
            {
                overtaker3 = overtakeIndicator.GetComponent<OvertakeIndicator>().xValue3;
                xPos = overtaker3;
            }

            if (slider4.activeSelf)
            {
                overtaker4 = overtakeIndicator.GetComponent<OvertakeIndicator>().xValue4;
                xPos = overtaker4;
            }
        }
        return xPos;
    }

    // Get the distances of a racer behind the player (for intensity change)
    public float GetOvertakerDistance(float zPos)
    {
        if (isACarInRange)
        {
            // DELETE THIS TEST CUBE............................................................
            if (slider5.activeSelf)
            {
                distance5 = overtakeIndicator.GetComponent<OvertakeIndicator>().zValue5;
                zPos = distance5;
            }

            if (slider1.activeSelf)
            {
                distance1 = overtakeIndicator.GetComponent<OvertakeIndicator>().zValue1;
                zPos = distance1;
            }

            if (slider2.activeSelf)
            {
                distance2 = overtakeIndicator.GetComponent<OvertakeIndicator>().zValue2;
                zPos = distance2;
            }

            if (slider3.activeSelf)
            {
                distance3 = overtakeIndicator.GetComponent<OvertakeIndicator>().zValue3;
                zPos = distance3;
            }

            if (slider4.activeSelf)
            {
                distance4 = overtakeIndicator.GetComponent<OvertakeIndicator>().zValue4;
                zPos = distance4;
            }
        }
        return zPos;
    }

    // Change vibration intensity based on overtaker's distance
    public int SetIntensity(int intensity)
    {
        overtakerDistance = GetOvertakerDistance(overtakerDistance);

        // FAR DISTANCE: Low intensity
        if (overtakerDistance > 0.8126047f && overtakerDistance < 4f)
        {
            intensity = lowIntensity;
            Debug.Log("Low Intensity");
        }

        // MEDIUM DISTANCE: Medium intensity
        if (overtakerDistance > 4f && overtakerDistance < 9f)
        {
            intensity = medIntensity;
            Debug.Log("Medium Intensity");
        }

        // CLOSE DISTANCE: High intensity
        if (overtakerDistance > 9f && overtakerDistance < 14f)
        {
            intensity = hightIntensity;
            Debug.Log("High Intensity");
        }

        return intensity;
    }

    void ResetMotors()
    {
        SendCommands.turnOffMotor(2);
        SendCommands.turnOffMotor(3);
        SendCommands.turnOffMotor(4);
    }

    void ChargingHaptics() 
    {
        //Repeat Sequence: BM->(FML & FMR)->(BL & BR)-> (FL & FR)
        
        Debug.Log("turn on BM");
        SendCommands.turnOnMotor(3, 120);                        // 1. BM

        this.Wait(0.5f, () => {                                  // 2. (FML & FMR) 0.5f
                Debug.Log(" turn on FML");
                Debug.Log("turn on FMR");
                SendCommands.turnOffMotor(3);
                SendCommands.turnOnMotor(0, 120);
                SendCommands.turnOnMotor(6, 120); 

            this.Wait(0.5f, () => {                              // 3. (BL & BR)
                Debug.Log("turn on BL");
                Debug.Log("turn on BR");
                SendCommands.turnOffMotor(0);
                SendCommands.turnOffMotor(6);
                SendCommands.turnOnMotor(2, 100);
                SendCommands.turnOnMotor(4, 100); 

                    this.Wait(0.5f, () => {                      // 4. (FL & FR)
                        Debug.Log("turn on FL");
                        Debug.Log("turn on FR");
                        SendCommands.turnOffMotor(2);
                        SendCommands.turnOffMotor(4);
                        SendCommands.turnOnMotor(1, 100);
                        SendCommands.turnOnMotor(5, 100);

                            this.Wait(0.5f, () => {
                                Debug.Log("SEQUENCE EXECUTED");
                                SendCommands.turnOffMotor(1);
                                SendCommands.turnOffMotor(5);
                                sequenceExecuting = false;

                                    this.Wait(1f, () => {});   // 5. Extra delay
                            });
                    });
            });
        });
    }

    void StopAllMotors()
    {
        SendCommands.turnOffMotor(0);
        SendCommands.turnOffMotor(1);
        SendCommands.turnOffMotor(2);
        SendCommands.turnOffMotor(3);
        SendCommands.turnOffMotor(4);
        SendCommands.turnOffMotor(5);
        SendCommands.turnOffMotor(6);
    }
    void StartEventHaptics() 
    {
        turnOnHaptics = true;
    }

}
