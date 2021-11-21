using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHaptics : MonoBehaviour
{
    public GameObject overtakeIndicator;

    public float overtaker1;
    public float overtaker2;
    public float overtaker3;
    public float overtaker4;

    public float overtaker5; // TEST CUBE
    public float distance5;    // TEST CUBE


    public GameObject slider1;
    public GameObject slider2;
    public GameObject slider3;
    public GameObject slider4;

    public GameObject slider5; // TEST CUBE
   
  
    void Start()
    {
       /* slider1 = GameObject.Find("Indicator Pink");
        slider2 = GameObject.Find("Indicator Gold");
        slider3 = GameObject.Find("Indicator Green");
        slider4 = GameObject.Find("Indicator Blue");

        slider5 =  GameObject.Find("Indicator TEST"); // TEST SLIDER */
    }

  /*  void Update()
    {
        OvertakeHaptics();
    }

    */

    ///////////////////////////// OVERTAKER HAPTICS \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    void OvertakeHaptics()
    {
        GetOvertake_XPositions();

        // Decide which modules to vibrate druing an overtaking attempt

        if (slider1.activeSelf) //Pink Car
        {
            // Vibrate [BL]
            if (overtaker1 > -3685.511f && overtaker1 < 540.3436f)
            {
                Debug.Log("BL  Pink");
            }

            // Vibrate [B]
            if (overtaker1 > 540.3436f && overtaker1 < 3921.033f)
            {
                Debug.Log("B   Pink");
            }

            // Vibrate [BR]
            if (overtaker1 > 3921.033f && overtaker1 < 7724.306f)
            {
                Debug.Log("BR  Pink");
            }
        }


        if (slider2.activeSelf) //Gold Car
        {
            // Vibrate [BL]
            if (overtaker2 > -3685.511f && overtaker2 < 540.3436f)
            {
                Debug.Log("BL");
            }

            // Vibrate [B]
            if (overtaker2 > 540.3436f && overtaker2 < 3921.033f)
            {
                Debug.Log("B");
            }

            // Vibrate [BR]
            if (overtaker2 > 3921.033f && overtaker2 < 7724.306f)
            {
                Debug.Log("BR");
            }
        }

        if (slider3.activeSelf) //Green Car
        {
            // Vibrate [BL]
            if (overtaker3 > -3685.511f && overtaker3 < 540.3436f)
            {
                Debug.Log("BL  Green");
            }

            // Vibrate [B]
            if (overtaker3 > 540.3436f && overtaker3 < 3921.033f)
            {
                Debug.Log("B   Green");
            }

            // Vibrate [BR]
            if (overtaker3 > 3921.033f && overtaker3 < 7724.306f)
            {
                Debug.Log("BR  Green");
            }
        }

        if (slider4.activeSelf) //Blue Car
        {
            // Vibrate [BL]
            if (overtaker4 > -3685.511f && overtaker4 < 540.3436f)
            {
                Debug.Log("BL  Blue");
            }

            // Vibrate [B]
            if (overtaker4 > 540.3436f && overtaker4 < 3921.033f)
            {
                Debug.Log("B  Blue");
            }

            // Vibrate [BR]
            if (overtaker4 > 3921.033f && overtaker4 < 7724.306f)
            {
                Debug.Log("BR  Blue");
            }
        }

        if (slider5.activeSelf) //////////////////////////////////// Test car
        {
            // Vibrate [BL]
            if (overtaker5 > -3685.511f && overtaker5 < 540.3436f)
            {
                Debug.Log("BL");
            }

            // Vibrate [B]
            if (overtaker5 > 540.3436f && overtaker5 < 3921.033f)
            {
                Debug.Log("B");
            }

            // Vibrate [BR]
            if (overtaker5 > 3921.033f && overtaker5 < 7724.306f)
            {
                Debug.Log("BR");
            }
        }
    }


    // Get horizontal positions from racers behind the player
   void GetOvertake_XPositions()
    {
        overtaker5 = overtakeIndicator.GetComponent<OvertakeIndicator>().xValue5; // TEST CUBE

        overtaker1 = overtakeIndicator.GetComponent<OvertakeIndicator>().xValue1;
        overtaker2 = overtakeIndicator.GetComponent<OvertakeIndicator>().xValue2;
        overtaker3 = overtakeIndicator.GetComponent<OvertakeIndicator>().xValue3;
        overtaker4 = overtakeIndicator.GetComponent<OvertakeIndicator>().xValue4;

       
    }

    // Get distances of racers behind the player
   void GetDistances() 
    {
        distance5 = overtakeIndicator.GetComponent<OvertakeIndicator>().zValue5;
       
    }

}
