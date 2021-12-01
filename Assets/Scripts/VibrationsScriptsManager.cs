using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Toggle vibration scripts to avoid concurrent data flow, interference
public class VibrationsScriptsManager : MonoBehaviour
{

    // Haptics colliders that has the "CollisionHaptics" script
    public GameObject F_Col;
    public GameObject FL_Col;
    public GameObject FR_Col;
    public GameObject BL_Col;
    public GameObject BR_Col;
    public GameObject B_Col;

    // Player which has all the "EventHaptics" scripts attached
    public GameObject player;

    // Collection Container
    public MonoBehaviour[] scripts;

    // Flags
    bool charging_enabled;
    bool nitro_enabled;
    bool slidy_enabled;
    bool slow_enabled;
    bool overtake_enabled;
    public bool death_enabled;


    void Start()
    {
        // Gather Collision Haptics scripts
        scripts[0] = F_Col.GetComponent<CollisionHaptics>();
        scripts[1] = FL_Col.GetComponent<CollisionHaptics>();
        scripts[2] = FR_Col.GetComponent<CollisionHaptics>();
        scripts[3] = BL_Col.GetComponent<CollisionHaptics>();
        scripts[4] = BR_Col.GetComponent<CollisionHaptics>();
        scripts[5] = B_Col.GetComponent<CollisionHaptics>();

        // Gather Event Haptics scripts
        scripts[6] = player.GetComponent<ChargingVibrations>();
        scripts[7] = player.GetComponent<NitroVibrations>();
        scripts[8] = player.GetComponent<SlidyAreaVibrations>();
        scripts[9] = player.GetComponent<SlowMotionVibrations>();
        scripts[10] = player.GetComponent<OvertakeVibrations>();
        // Death script
        scripts[11] = player.GetComponent<DeathVibrations>();

        Turn_Off_DeathVibrationsScript();
    }

    // Update is called once per frame
    void Update()
    {

        //DEATH
        if (RaceManager._isDead)
        {
            death_enabled = true;
            Turn_On_DeathVibrationsScript();
        }

        if (DeathVibrations.pressedRestart)
        {
            death_enabled = false;
            Turn_Off_DeathVibrationsScript();
        }

        if (!death_enabled)
        {
            // CHARGING
            if (CollisionManager.isCharging)
            {
                if (!nitro_enabled)
                {
                    Debug.Log("Charging script");
                    charging_enabled = true;
                    Turn_On_ChargingVibrationsScript();
                }
            }
            if (!CollisionManager.isCharging && !ChargingVibrations.sequenceExecuting)
            {
                charging_enabled = false;
                Turn_Off_ChargingVibrationsScript();
            }

            // NITRO
            if (NitroManager.isSpeeding)
            {
                if (!charging_enabled && !slidy_enabled && !slow_enabled)
                {
                    Debug.Log("Nitro script");
                    nitro_enabled = true;
                    Turn_On_NitroVibrationsScript();
                }
            }

            if (!NitroManager.isSpeeding && NitroVibrations.nitroSequenceExecuted && !NitroVibrations.nitroIsOn)
            {
                nitro_enabled = false;
                Turn_Off_NitroVibrationsScript();
            }


            // SLIDY AREA
            if (EnvironmentalEffects.isInSlipperyArea)
            {
                if (!nitro_enabled)
                {
                    Debug.Log("Slidy script");
                    slidy_enabled = true;
                    Turn_On_SlidyAreaVibrationsScript();
                }
            }
            if (!EnvironmentalEffects.isInSlipperyArea && !SlidyAreaVibrations.slidingFlag)
            {
                slidy_enabled = false;
                Turn_Off_SlidyAreaVibrationsScript();
            }


            // SLOW AREA
            if (EnvironmentalEffects.isInSlowMototion)
            {
                if (!nitro_enabled)
                {
                    Debug.Log("Slow  script");
                    slow_enabled = true;
                    Turn_On_SlowMotionVibrationsScript();
                }

            }
            if (!EnvironmentalEffects.isInSlowMototion && !SlowMotionVibrations.slowMotion_sequenceExecuting)
            {
                slow_enabled = false;
                Turn_Off_SlowMotionVibrationsScript();
            }


            // DEFAULT: Collision Haptics on and off if other events are playing, not playing
            if (charging_enabled || nitro_enabled || slidy_enabled || slow_enabled)                  // or explosion             
            {
                Turn_Off_CollisiontHapticsScript();
            }
            if (!charging_enabled && !nitro_enabled && !slidy_enabled && !slow_enabled)      // and explosion
            {
                Turn_On_CollisiontHapticsScript();
            }

        }


    }


    // CollisionHaptics script
    void Turn_On_CollisiontHapticsScript()
    {
        for (int i = 0; i <= 5; i++)
        {
            scripts[i].enabled = true;
        }

        for (int j = 6; j <= 9; j++)
        {
            scripts[j].enabled = false; // turn off all other scripts except for CollisionHaptics and OvertakeVibrationHaptics
        }
    }

    void Turn_Off_CollisiontHapticsScript()
    {
        for (int i = 0; i <= 5; i++)
        {
            scripts[i].enabled = false;
        }
    }


    // Events

    // Charging script
    void Turn_On_ChargingVibrationsScript()
    {
        scripts[6].enabled = true;
    }
    void Turn_Off_ChargingVibrationsScript()
    {
        scripts[6].enabled = false;
    }

    // Nitro script
    void Turn_On_NitroVibrationsScript()
    {
        scripts[7].enabled = true;
    }
    void Turn_Off_NitroVibrationsScript()
    {
        scripts[7].enabled = false;
    }

    // Slidy Area script
    void Turn_On_SlidyAreaVibrationsScript()
    {
        scripts[8].enabled = true;
    }
    void Turn_Off_SlidyAreaVibrationsScript()
    {
        scripts[8].enabled = false;
    }

    //SlowMotion script
    void Turn_On_SlowMotionVibrationsScript()
    {
        scripts[9].enabled = true;
    }
    void Turn_Off_SlowMotionVibrationsScript()
    {
        scripts[9].enabled = false;
    }

    // Overtake script
    void Turn_On_OvertakeVibrationsScript()
    {
        scripts[10].enabled = true;
    }
    void Turn_Off_OvertakeVibrationsScript()
    {
        scripts[10].enabled = false;
    }

    void Turn_On_DeathVibrationsScript()
    {
        scripts[11].enabled = true;

        for (int i = 0; i <= 10; i++)
        {
            scripts[i].enabled = false;
        }


    }
    void Turn_Off_DeathVibrationsScript()
    {
        scripts[11].enabled = false;
    }
}
