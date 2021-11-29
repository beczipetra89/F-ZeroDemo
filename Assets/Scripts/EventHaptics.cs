using System;
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

    [Header("Nitro Haptics")]
    public int nitroMotorIntensity = 255;
    public bool nitroIsOn;
    public bool nitroStarted = false;
    bool nitroSequenceExecuting = false;
    public bool nitroSequenceExecuted = false;
    bool nitroMotorSwitched = false;

    [Header("Slow Motion Haptics")]
    //public GameObject chargingEvent;
    public bool _isInSlowArea;
    bool slowMotion_sequenceExecuting = false;

    void Start()
    {
        Invoke("StartEventHaptics", 8.0f);
    }

    void Update()
    {

        if (turnOnHaptics)
        {
            ///////////////////////////////////// Overtake Haptics \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
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
            /*
            else{
                ResetMotors();
            }
            */
        }

        ///////////////////////////////////// Charging Haptics \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        if (CollisionManager.isCharging)
        {
            _isCharging = true;
            if (!sequenceExecuting)
            {
                sequenceExecuting = true;
                ChargingHaptics();
            }
        }
        else {
            _isCharging = false;
            if (sequenceExecuting)
            {
                StopAllCoroutines();
                StopAllMotors();
                sequenceExecuting = false;
            }
        }

        // Nitro Haptics
        if (NitroManager.isSpeeding)
        { 
           nitroIsOn = true;
        }


        if (NitroManager.isSpeeding)
        {
            if (!nitroStarted)
            {
                nitroStarted = true;
                Debug.Log("started");
                NitroHaptics();
            }
        } else
        {
            if (nitroStarted)
            {
                nitroStarted = false;
                Debug.Log("finished");
            }
        }

        if (nitroIsOn)
        {
            if (nitroSequenceExecuting)
            {
                nitroMotorIntensity = GetNitroMotorIntensity(nitroMotorIntensity);
            }
        }

        ///////////////////////////////////// Slow Motion Haptics \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        if (EnvironmentalEffects.isInSlowMototion)
        {
            _isInSlowArea = true;
            if (!slowMotion_sequenceExecuting)
            {
                slowMotion_sequenceExecuting = true;
                SlowMotionHaptics();
            }
        }
        else
        {
            _isInSlowArea = false;
            if (slowMotion_sequenceExecuting)
            {
                StopAllCoroutines();
                StopAllMotors();
                slowMotion_sequenceExecuting = false;
            }
        }

    }

    // Fade out the intensity decrementally (for using nitro)
    private int GetNitroMotorIntensity(int currentIntensity)
    {
        if (currentIntensity > 0)
        {
            int newIntensity = currentIntensity - (int)(40f * Time.deltaTime);
            if (newIntensity > 0)
                return newIntensity;
            else
                return 0;
        } else
        {
            return 0;
        }
    }

    private void NitroHaptics()
    {
        float timeInterval = 0.5f;

        for (int i =0; i < (int) 6/timeInterval ; i++)
        {
            float delay = timeInterval * (int) i;
            //Debug.Log($"delay: {delay}");
            this.Wait(delay, () => {
                nitroSequenceExecuting = true;
                //Debug.Log($"Motor intensity {nitroMotorIntensity}");
                if (nitroMotorIntensity > 180)
                {
                    if (nitroMotorSwitched)
                        nitroMotorSwitched = false;

                    SendCommands.turnOnMotor(0, nitroMotorIntensity);
                    SendCommands.turnOnMotor(6, nitroMotorIntensity);
                } else
                {
                    if (!nitroMotorSwitched)
                    {
                        SendCommands.turnOffMotor(0);
                        SendCommands.turnOffMotor(6);
                        nitroMotorSwitched = true;
                    }
                    SendCommands.turnOnMotor(3, nitroMotorIntensity);
                }

            });
        }

        // 7.0 seconds for the nitro effect to finish, turn off all motors at that time
        this.Wait(7.0f, () =>
        {
            Debug.Log($"Turn off all motors");
            SendCommands.turnOffMotor(0);
            SendCommands.turnOffMotor(6);
            SendCommands.turnOffMotor(3);
            nitroSequenceExecuting = false;
            nitroSequenceExecuted = true;
            nitroIsOn = false;
            nitroMotorIntensity = 255;
        });
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
        } else 

        // CENTER: Vibrate [B] if racer is coming from behind (center)
        if (overtakerPos > 540.3436f && overtakerPos < 3921.033f)
        {
            SendCommands.turnOnMotor(3, _intensity);
            SendCommands.turnOffMotor(2);
            SendCommands.turnOffMotor(4);
        } else

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

        SendCommands.turnOnMotor(3, 200);                        // 1. BM

        this.Wait(0.3f, () => {                                  // 2. (FML & FMR)
            SendCommands.turnOffMotor(3);
            SendCommands.turnOnMotor(0, 200);
            SendCommands.turnOnMotor(6, 200);

            this.Wait(0.3f, () => {                              // 3. (BL & BR)
                SendCommands.turnOffMotor(0);
                SendCommands.turnOffMotor(6);
                SendCommands.turnOnMotor(2, 200);
                SendCommands.turnOnMotor(4, 200);

                this.Wait(0.3f, () => {                      // 4. (FL & FR)
                    SendCommands.turnOffMotor(2);
                    SendCommands.turnOffMotor(4);
                    SendCommands.turnOnMotor(1, 200);
                    SendCommands.turnOnMotor(5, 200);

                    this.Wait(0.3f, () => {
                        //SEQUENCE EXECUTED"
                        SendCommands.turnOffMotor(1);
                        SendCommands.turnOffMotor(5);
                        sequenceExecuting = false;

                        this.Wait(0.3f, () => { });   // 5. Extra delay
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

    void SlowMotionHaptics() 
    {
        // All Together: FML & BL & BM & BR & FR & MFR
                                                        // 1. Start on low intensity: All(100)
        //Debug.Log("''''''''''''''''''''SEQUENCE STARTED''''''''''''''''''''''''''''''''''''");
        SendCommands.turnOnMotor(0, lowIntensity);
        SendCommands.turnOnMotor(1, lowIntensity);
        SendCommands.turnOnMotor(2, lowIntensity);
        SendCommands.turnOnMotor(3, lowIntensity);
        SendCommands.turnOnMotor(4, lowIntensity);
        SendCommands.turnOnMotor(5, lowIntensity);
        SendCommands.turnOnMotor(6, lowIntensity);

        this.Wait(0.3f, () => {                         //2. Switch off: All
            //Debug.Log("''''''''''''''''''''NEW SEQUENCE; TURN OFF MOTORS'''''''''''''''''''''''");
            SendCommands.turnOffMotor(0);
            SendCommands.turnOffMotor(1);
            SendCommands.turnOffMotor(2);
            SendCommands.turnOffMotor(3);
            SendCommands.turnOffMotor(4);
            SendCommands.turnOffMotor(5);
            SendCommands.turnOffMotor(6);
            //Debug.Log("''''''''''''''''''''SWITCH TO HIGH INTENSITY''''''''''''''''''''''''");

                this.Wait(0.1f, () => {                 //2.  Switch to Medium intensity: All(150)
                    SendCommands.turnOnMotor(0, medIntensity);
                    SendCommands.turnOnMotor(1, medIntensity);
                    SendCommands.turnOnMotor(2, medIntensity);
                    SendCommands.turnOnMotor(3, medIntensity);
                    SendCommands.turnOnMotor(4, medIntensity);
                    SendCommands.turnOnMotor(5, medIntensity);
                    SendCommands.turnOnMotor(6, medIntensity);

                    this.Wait(0.3f, () => {
                        //Debug.Log("''''''''''''''''''''''SEQUENCE EXECUTED''''''''''''''''''''''''");
                        SendCommands.turnOffMotor(0);
                        SendCommands.turnOffMotor(1);
                        SendCommands.turnOffMotor(2);
                        SendCommands.turnOffMotor(3);
                        SendCommands.turnOffMotor(4);
                        SendCommands.turnOffMotor(5);
                        SendCommands.turnOffMotor(6);
                        slowMotion_sequenceExecuting = false;
                    });
                });
        });
    }

}
