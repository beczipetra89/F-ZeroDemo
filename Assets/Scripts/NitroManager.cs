using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NitroManager : MonoBehaviour
{
    [Header("NitroCounter")]
    public GameObject checkPointTracker;
    public static bool hasNitro;
    public int nitros;
    public static bool pressedButton;

    public static bool isSpeeding;

    [Header("Nitro Sprites")]
    public Sprite[] nitroSprites;
    public Image nitroImage;
   

    void Start()
    {
        nitros = 3; //0
        isSpeeding = false;
    }

    void Update()
    {
        nitros = GetNitro();

        if (nitros >=1)
        {
            hasNitro = true;
        }
        
        if (nitros == 0)
        {
            hasNitro = false;
        }

        if ( Input.GetKeyDown("space") && !isSpeeding)
        { 
            if (hasNitro) 
            {
                pressedButton = true;
                nitros = nitros - 1;
                isSpeeding = true;
            }
        }
        else
        {
            pressedButton = false;
        }

        nitroImage.sprite = nitroSprites[nitros];
    }

    public int GetNitro()
    {
        // Increment nitros by 1 if the flag is true
        if (checkPointTracker.GetComponent<CarLap>().getNitro)
        {
            nitros += 1;

            //Reset the flag to false
            checkPointTracker.GetComponent<CarLap>().getNitro = false;
        }
        return nitros;
    }
}
