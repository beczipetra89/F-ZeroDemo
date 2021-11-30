using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroVibrations : MonoBehaviour
{
    [Header("Nitro Haptics")]
    public int nitroMotorIntensity = 255;
    public static bool nitroIsOn;
    public bool nitroStarted = false;
    bool nitroSequenceExecuting = false;
    public static bool nitroSequenceExecuted = false;
    bool nitroMotorSwitched = false;

    void Update()
    {
        //////////////////////////////////// Nitro Haptics \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
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
        }
        else
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
        }
        else
        {
            return 0;
        }
    }

    private void NitroHaptics()
    {
        float timeInterval = 0.5f;

        for (int i = 0; i < (int)6 / timeInterval; i++)
        {
            float delay = timeInterval * (int)i;
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
                }
                else
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
}
