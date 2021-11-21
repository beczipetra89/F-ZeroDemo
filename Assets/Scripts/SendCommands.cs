// SEND COMMANDS TO THE ARDUINO BOARD

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;

public class SendCommands : MonoBehaviour
{
    // [Header("LIST OF MOTORS")]
    //  public GameObject[] pie_colliders;

    public static SerialPort sp = new SerialPort("COM3", 9600);
    public string meassage2;
    float timePassed = 0.0f;
    static bool motorIsOn = false;

    // Use this for initialization
    void Start()
    {
        OpenConnection();
    }

    
    void Update()
    {
/*        try
        {
            meassage2 = sp.ReadLine();
            print(meassage2);
        }
        catch (System.TimeoutException) { }*/
    }

    public void OpenConnection()
    {
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                print("Closing port, because it was already open!");
            }
            else
            {
                sp.Open(); // Opens the connection
                sp.ReadTimeout = 25; // Sets the timeout value before reporting
                sp.WriteTimeout = 25;
                print("Port Opened!");
            }
        }
        else
        {
            if (sp.IsOpen)
            {
                print("Port is already open");
            }
            else
            {
                print("Port == null");
            }
        }
    }

    void OnApplicationQuit() 
    {
        print("Closing the port...");
        try
        {
            sp.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    
    /* MAPPING OF MOTORS
        motor 0 = FML
        motor 1 = FL
        motor 2 = BL
        motor 3 = BM
        motor 4 = BR
        motor 5 = FR
        motor 6 = FMR
    */

    // TURN ON MOTORS: sp.Write(" motorID intensitivity state"); where state: 1 = on, 0 = off
    public static void turnOnMotor0() // FML
    {
        if (!motorIsOn)
        {
            sp.Write("0 200 1\n");
            motorIsOn = !motorIsOn;
        }
    }
    public static void turnOnMotor1() // FL
    {
        if (!motorIsOn)
        {
            sp.Write("1 200 1\n");
            motorIsOn = !motorIsOn;
        }
    }

    /*
    public static void turnOnMotor2() // BL
    {
        if (!motorIsOn)
        {
            sp.Write("2 200 1\n");
            motorIsOn = !motorIsOn;
        }
    }

    public static void turnOnMotor3() // BM
    {
        if (!motorIsOn)
        {
            sp.Write("3 200 1\n");
            motorIsOn = !motorIsOn;
        }
    }

    public static void turnOnMotor4() // BR
    {
        if (!motorIsOn)
        {
            sp.Write("4 200 1\n");
            motorIsOn = !motorIsOn;
        }
    }

    public static void turnOnMotor5() // FR
    {
        if (!motorIsOn)
        {
            sp.Write("5 200 1\n");
            motorIsOn = !motorIsOn;
        }
    }

    public static void turnOnMotor6() // FMR
    {
        if (!motorIsOn)
        {
            sp.Write("6 200 1\n");
            motorIsOn = !motorIsOn;
        }
    }
    */

    // TURN OFF MOTORS
    public static void turnOffMotor0() // FML
    {
        if (motorIsOn)
        {
            sp.Write("0 200 0\n");
            motorIsOn = !motorIsOn;
        }
    }
    public static void turnOffMotor1() // FL
    {
        if (motorIsOn)
        {
            sp.Write("1 200 0\n");
            motorIsOn = !motorIsOn;
        }
    }

    /*
    public static void turnOffMotor2() // BL
    {
        if (motorIsOn)
        {
            sp.Write("2 200 0\n");
            motorIsOn = !motorIsOn;
        }
    }

    public static void turnOffMotor3() // BM
    {
        if (motorIsOn)
        {
            sp.Write("3 200 0\n");
            motorIsOn = !motorIsOn;
        }
    }
    public static void turnOffMotor4() // BR
    {
        if (motorIsOn)
        {
            sp.Write("4 200 0\n");
            motorIsOn = !motorIsOn;
        }
    }
    public static void turnOffMotor5() // FR
    {
        if (motorIsOn)
        {
            sp.Write("5 200 0\n");
            motorIsOn = !motorIsOn;
        }
    }
    public static void turnOffMotor6() // FMR
    {
        if (motorIsOn)
        {
            sp.Write("6 200 0\n");
            motorIsOn = !motorIsOn;
        }
    }
    */
}
