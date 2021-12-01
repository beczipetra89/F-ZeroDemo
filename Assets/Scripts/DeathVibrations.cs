using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVibrations : MonoBehaviour
{
    [Header("Death Explosion Haptics")]
    
    public static bool pressedRestart = false;

    public static bool _isBurning;
    public static bool burningStarted = false;
    public static bool explosion_sequenceExecuting = false;
    public static bool explosion_sequenceExecuted = false;

    void Update()
    {
        ///////////////////////////////////// Explosion Haptics \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        if (RaceManager._isDead)
        {
            _isBurning = true;
        }


        if (RaceManager._isDead)
        {
            if (!burningStarted)
            {
                burningStarted = true;
                Debug.Log("started burning");
                DeathHaptics();
            }
        }
        else
        {
            if (burningStarted)
            {
                burningStarted = false;
                Debug.Log("stopped burning");
                pressedRestart = false;
            }
        }

        if (_isBurning)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("PRESSED RESTART!!!!!!!!!!!!!!!!!!!!!!!!");

                pressedRestart = true;
                StopAllCoroutines();
                StopAllMotors();
            }
        } 

    }


    void DeathHaptics()
    {
        explosion_sequenceExecuting = true;

        // All Together: FML & BL & BM & BR & FR & MFR
        // 1. Start on high intensity: All(255)
        Debug.Log("''''''''''''''''''''SEQUENCE STARTED''''''''''''''''''''''''''''''''''''");
        SendCommands.turnOnMotor(0, 255);
        SendCommands.turnOnMotor(1, 255);
        SendCommands.turnOnMotor(2, 255);
        SendCommands.turnOnMotor(3, 255);
        SendCommands.turnOnMotor(4, 255);
        SendCommands.turnOnMotor(5, 255);
        SendCommands.turnOnMotor(6, 255);

        this.Wait(4f, () => {                         //2. Switch off: All
            //Debug.Log("''''''''''''''''''''NEW SEQUENCE; TURN OFF MOTORS'''''''''''''''''''''''");
            SendCommands.turnOffMotor(0);
            SendCommands.turnOffMotor(1);
            SendCommands.turnOffMotor(2);
            SendCommands.turnOffMotor(3);
            SendCommands.turnOffMotor(4);
            SendCommands.turnOffMotor(5);
            SendCommands.turnOffMotor(6);
            //Debug.Log("''''''''''''''''''''SWITCH TO LOW INTENSITY''''''''''''''''''''''''");

            this.Wait(1f, () => {                 //2.  Switch to low intensity: All(100)
                SendCommands.turnOnMotor(0, 100);
                SendCommands.turnOnMotor(1, 100);
                SendCommands.turnOnMotor(2, 100);
                SendCommands.turnOnMotor(3, 100);
                SendCommands.turnOnMotor(4, 100);
                SendCommands.turnOnMotor(5, 100);
                SendCommands.turnOnMotor(6, 100);

                this.Wait(10f, () => {
                    Debug.Log("''''''''''''''''''''''SEQUENCE EXECUTED''''''''''''''''''''''''");
                    SendCommands.turnOffMotor(0);
                    SendCommands.turnOffMotor(1);
                    SendCommands.turnOffMotor(2);
                    SendCommands.turnOffMotor(3);
                    SendCommands.turnOffMotor(4);
                    SendCommands.turnOffMotor(5);
                    SendCommands.turnOffMotor(6);
                    explosion_sequenceExecuting = false;
                    explosion_sequenceExecuted = true;
                    //StopAllCoroutines();
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
}
