using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidyAreaVibrations : MonoBehaviour
{
    [Header("Slidy Area Haptics")]
    public bool _isInSlidyArea;
    public static bool slidingFlag = false;
    public bool turnOffSlidingFlag = false;

    void Start()
    {
        
    }

   
    void Update()
    {
        ///////////////////////////////////// Slidy Area Haptics \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        if (EnvironmentalEffects.isInSlipperyArea)
        {
            _isInSlidyArea = true;
            if (!slidingFlag)
            {
                slidingFlag = true;
                SlidyAreaHaptics();
            }
        }
        else
        {
            _isInSlidyArea = false;
            if (slidingFlag)
            {
                StopAllMotors();
                slidingFlag = false;
            }
        }
    }


    void SlidyAreaHaptics()
    {
        if (slidingFlag)
        {
            SendCommands.turnOnMotor(0, 100);
            SendCommands.turnOnMotor(1, 100);
            SendCommands.turnOnMotor(2, 100);
            SendCommands.turnOnMotor(3, 100);
            SendCommands.turnOnMotor(4, 100);
            SendCommands.turnOnMotor(5, 100);
            SendCommands.turnOnMotor(6, 100);
            turnOffSlidingFlag = false;
        }

        if (!slidingFlag && !turnOffSlidingFlag)
        {
            SendCommands.turnOffMotor(0);
            SendCommands.turnOffMotor(1);
            SendCommands.turnOffMotor(2);
            SendCommands.turnOffMotor(3);
            SendCommands.turnOffMotor(4);
            SendCommands.turnOffMotor(5);
            SendCommands.turnOffMotor(6);
            turnOffSlidingFlag = true;
        }
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
