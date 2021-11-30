using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionVibrations : MonoBehaviour
{
    [Header("Slow Motion Haptics")]
    public bool _isInSlowArea;
    public static bool slowMotion_sequenceExecuting = false;

   
    void Update()
    {
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

    void SlowMotionHaptics()
    {
        // All Together: FML & BL & BM & BR & FR & MFR
        // 1. Start on low intensity: All(100)
        //Debug.Log("''''''''''''''''''''SEQUENCE STARTED''''''''''''''''''''''''''''''''''''");
        SendCommands.turnOnMotor(0, 100);
        SendCommands.turnOnMotor(1, 100);
        SendCommands.turnOnMotor(2, 100);
        SendCommands.turnOnMotor(3, 100);
        SendCommands.turnOnMotor(4, 100);
        SendCommands.turnOnMotor(5, 100);
        SendCommands.turnOnMotor(6, 100);

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
                SendCommands.turnOnMotor(0, 150);
                SendCommands.turnOnMotor(1, 150);
                SendCommands.turnOnMotor(2, 150);
                SendCommands.turnOnMotor(3, 150);
                SendCommands.turnOnMotor(4, 150);
                SendCommands.turnOnMotor(5, 150);
                SendCommands.turnOnMotor(6, 150);

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
