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

    public static SerialPort sp = new SerialPort("COM4", 115200);
    public string meassage2;
    float timePassed = 0.0f;
    static bool motorIsOn = false;
    static bool[] motorStatus = new bool[7];
    static int[] motorIntensities = { -1, -1, -1, -1, -1, -1, -1 };
    static float lastCommandSentTime = -1.0f;

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

        if (Input.GetKey("escape")) 
        {
            OnApplicationQuit();
            Application.Quit();
        }
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
                sp.ReadTimeout = 25;
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
            ResetAllMotors();
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

    public static void turnOnMotor(int motorId, int intensity)
    {
        if (!motorStatus[motorId]||motorIntensities[motorId]!=intensity)
        {
            try
            {
                //sp.Write($"{motorId} {intensity} 1\n");
                print($"{motorId} {intensity} 1\n");
                motorStatus[motorId] = !motorStatus[motorId];
                motorIntensities[motorId] = intensity;
            }
            catch (System.IO.IOException)
            {
                Debug.Log("Recovered from exception");
                sp.Close();
                sp.Open();
                //sp.Write($"{motorId} {intensity} 1\n");
                print($"{motorId} {intensity} 1\n");
                motorStatus[motorId] = !motorStatus[motorId];
                motorIntensities[motorId] = intensity;
            }

        }
    }

    public static bool getMotorStatus(int motorId)
    {
        return motorStatus[motorId];
    }

    public static void turnOffMotor(int motorId)
    {
        if (motorStatus[motorId])
        {
            /*
            float commandTimeInterval = Time.deltaTime - lastCommandSentTime;
            lastCommandSentTime = Time.deltaTime;
            Debug.Log($"{commandTimeInterval}");
            */
            try
                {
                    //sp.Write($"{motorId} 0 0\n");
                    print($"{motorId} 0 0\n");
                    motorStatus[motorId] = !motorStatus[motorId];
                }
                catch (System.IO.IOException)
                {
                    Debug.Log("Recovered from exception");
                    sp.Close();
                    sp.Open();
                    //sp.Write($"{motorId} 0 0\n");
                    print($"{motorId} 0 0\n");
                    motorStatus[motorId] = !motorStatus[motorId];
                }

            }
    }

    public static void changeIntensity(int motorId, int intensity) 
    {
        if (motorIntensities[motorId] != intensity)
        {
            float commandTimeInterval = Time.deltaTime - lastCommandSentTime;
            lastCommandSentTime = Time.deltaTime;
            if (commandTimeInterval > 0.2f)
            {
                //sp.Write($"{motorId} {intensity} 1\n");
                print($"{motorId} {intensity} 1\n");
                motorIntensities[motorId] = intensity;
            }
        }
    }

    void ResetAllMotors() 
    {
        turnOffMotor(0);
        turnOffMotor(1);
        turnOffMotor(2);
        turnOffMotor(3);
        turnOffMotor(4);
        turnOffMotor(5);
        turnOffMotor(6);
    }
}
