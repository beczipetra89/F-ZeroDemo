using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingVibrations : MonoBehaviour
{
    [Header("Charging Haptics")]
    public bool _isCharging;
    public static bool sequenceExecuting = false;

   
    void Update()
    {
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
        else
        {
            _isCharging = false;
            if (sequenceExecuting)
            {
                StopAllCoroutines();
                StopAllMotors();
                sequenceExecuting = false;
            }
        }
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
}

