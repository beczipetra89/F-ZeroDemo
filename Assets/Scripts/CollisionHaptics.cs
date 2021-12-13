// VIBRATION FEEDBACK FOR THE COLLISION WITH EDGE AND OTHER CARS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHaptics : MonoBehaviour
{
    void Start() { }
    
    ////////////////////////////// COLLISION WITH NPC \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    void OnCollisionEnter(Collision other)
    {
        if (this.enabled)
        {
            if (other.gameObject.tag == "NPCDriver" || other.gameObject.tag == "AiCar")
            {
                //Turn ON the corresponding motor for NPC collision
                switch (this.gameObject.name)
                {
                    case "F_Col":
                        // F = FML + FMR, motor 0 and motor 6 together
                        SendCommands.turnOnMotor(0, 200);
                        SendCommands.turnOnMotor(6, 200);
                        Debug.Log("F");
                        break;

                    case "FL_Col":
                        SendCommands.turnOnMotor(1, 200);
                        Debug.Log("FL");
                        break;

                    case "BL_Col":
                        SendCommands.turnOnMotor(2, 200);
                        Debug.Log("BL");
                        break;

                    case "B_Col":
                        SendCommands.turnOnMotor(3, 200);
                        Debug.Log("B");
                        break;

                    case "BR_Col":
                        SendCommands.turnOnMotor(4, 200);
                        Debug.Log("BR");
                        break;

                    case "FR_Col":
                        SendCommands.turnOnMotor(5, 200);
                        Debug.Log("FR");
                        break;
                }
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (this.enabled)
        {

            if (other.gameObject.tag == "NPCDriver" || other.gameObject.tag == "AiCar")
            {
                // Turn OFF the corresponding motor for NPC collision
                switch (this.gameObject.name)
                {
                    case "F_Col":
                        // F = FML + FMR, motor 0 and motor 6 together
                        StartCoroutine(TurnOfFrontMotorsWithDelay(0,6));
                        break;

                    case "FL_Col":
                        StartCoroutine(TurnOffAfMotorWithDelay(1));
                        break;

                    case "BL_Col":
                        StartCoroutine(TurnOffAfMotorWithDelay(2));
                        break;

                    case "B_Col":
                        StartCoroutine(TurnOffAfMotorWithDelay(3));
                        break;

                    case "BR_Col":
                        StartCoroutine(TurnOffAfMotorWithDelay(4));
                        break;

                    case "FR_Col":
                        StartCoroutine(TurnOffAfMotorWithDelay(5));
                        break;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (this.enabled)
        {
            ////////////////////////////// COLLISION WITH AI CARS \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            if (other.gameObject.tag == "AiCarCapsule")
            {
                switch (this.gameObject.name)
                {
                    case "F_Col":
                        // F = FML + FMR, motor 0 and motor 6 together
                        SendCommands.turnOnMotor(0, 200);
                        SendCommands.turnOnMotor(6, 200);
                        break;

                    case "FL_Col":
                        SendCommands.turnOnMotor(1, 200);
                        break;

                    case "BL_Col":
                        SendCommands.turnOnMotor(2, 200);
                        break;

                    case "B_Col":
                        SendCommands.turnOnMotor(3, 200);
                        break;

                    case "BR_Col":
                        SendCommands.turnOnMotor(4, 200);
                        break;

                    case "FR_Col":
                        SendCommands.turnOnMotor(5, 200);
                        break;
                }
            }


            ////////////////////////////// COLLISION WITH EDGE L \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            ///TURN ON
            if (other.gameObject.tag == "Hex_Coll_L")
            {
                switch (this.gameObject.name)
                {
                    case "FL_Col":
                        SendCommands.turnOnMotor(1, 200);
                        break;

                    case "BL_Col":
                        SendCommands.turnOnMotor(2, 200);
                        break;
                }
            }
            ////////////////////////////// COLLISION WITH EDGE R \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            ///TURN ON
            if (other.gameObject.tag == "Hex_Coll_R")
            {

                // TURN ON the corresponding motor for EDGE collision
                switch (this.gameObject.name)
                {
                    case "BR_Col":
                        SendCommands.turnOnMotor(4, 200);
                        break;

                    case "FR_Col":
                        SendCommands.turnOnMotor(5, 200);
                        break;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (this.enabled)
        {
            // EXIT FROM COLLISION WITH AI CAR
            if (other.gameObject.tag == "AiCarCapsule")
            {
                //Turn OFF the corresponding motor for AI collision

                switch (this.gameObject.name)
                {
                    case "F_Col":
                        // F = FML + FMR, motor 0 and motor 6 together
                        StartCoroutine(TurnOfFrontMotorsWithDelay(0,6));
                        break;

                    case "FL_Col":
                        StartCoroutine(TurnOffAfMotorWithDelay(1));
                        break;

                    case "BL_Col":
                        StartCoroutine(TurnOffAfMotorWithDelay(2));
                        break;

                    case "B_Col":
                        StartCoroutine(TurnOffAfMotorWithDelay(3));
                        break;

                    case "BR_Col":
                        StartCoroutine(TurnOffAfMotorWithDelay(4));
                        break;

                    case "FR_Col":
                        StartCoroutine(TurnOffAfMotorWithDelay(5));
                        break;
                }
            }

            ////////////////////////////// COLLISION WITH EDGE L \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            // TURN OFF
            if (other.gameObject.tag == "Hex_Coll_L")
            {
                switch (this.gameObject.name)
                {
                    case "FL_Col":
                        SendCommands.turnOffMotor(1);
                        break;

                    case "BL_Col":
                        SendCommands.turnOffMotor(2);
                        break;
                }
            }

            ////////////////////////////// COLLISION WITH EDGE R \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            // TURN OFF
            if (other.gameObject.tag == "Hex_Coll_R")
            {
                switch (this.gameObject.name)
                {
                    case "BR_Col":
                        SendCommands.turnOffMotor(4);
                        break;

                    case "FR_Col":
                        SendCommands.turnOffMotor(5);
                        break;
                }
            }
        }
    }

    IEnumerator TurnOfFrontMotorsWithDelay(int motorID_A, int motorID_B)
    {
        yield return new WaitForSeconds(0.5f);
        SendCommands.turnOffMotor(motorID_A);
        SendCommands.turnOffMotor(motorID_B);
    }

    IEnumerator TurnOffAfMotorWithDelay(int motorID)
    {
        yield return new WaitForSeconds(0.5f);
        SendCommands.turnOffMotor(motorID);
    }
}