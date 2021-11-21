// VIBRATION FEEDBACK FOR THE COLLISION WITH EDGE AND OTHER CARS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHaptics : MonoBehaviour
{
    // public int AVM_ID;
    public bool isCrashing;
    public bool isEdgeColliding;

    ////////////////////////////// COLLISION WITH NPC \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
   
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "NPCDriver")
        {
            Debug.Log(this.gameObject.name + " -----------------CAR CRASH ");
            isCrashing = true;

            // SendCommands.turnOnMotor1();

            //Turn ON the corresponding motor for NPC collision
            switch (this.gameObject.name)
            {
                case "F_Col":
                    // F = FML + FMR, motor 0 and motor 1 together
                    SendCommands.turnOnMotor0();
                    // SendCommands.turnOnMotor6();
                    break;

                case "FL_Col":
                    SendCommands.turnOnMotor1();
                    break;

                    /*   case "BL_Col":
                           SendCommands.turnOnMotor2();
                           break;

                       case "B_Col":
                           SendCommands.turnOnMotor3();
                           break;

                       case "BR_Col":
                           SendCommands.turnOnMotor4();
                           break;

                       case "FR_Col":
                           SendCommands.turnOnMotor5();
                           break;
                    */
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "NPCDriver") // aiCar // AiCarCapsule
        {
            isCrashing = false;

            //SendCommands.turnOffMotor1();

            // Turn OFF the corresponding motor for NPC collision
            switch (this.gameObject.name)
            {
                case "F_Col":
                    // F = FML + FMR, motor 0 and motor 1 together
                    SendCommands.turnOffMotor0();
                    // SendCommands.turnOffMotor6();
                    break;

                case "FL_Col":
                    SendCommands.turnOffMotor1();
                    break;

                    /*   case "BL_Col":
                           SendCommands.turnOffMotor2();
                           break;

                       case "B_Col":
                           SendCommands.turnOffMotor3();
                           break;

                       case "BR_Col":
                           SendCommands.turnOffMotor4();
                           break;

                       case "FR_Col":
                           SendCommands.turnOffMotor5();
                           break;
                    */

            }
        }
    }

    //////////////////////////////COLLISION WITH AI CARS and EDGE \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    void OnTriggerEnter(Collider other)
    {
        ////////////////////////////// Entering Collision with AI CARS \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        if (other.gameObject.tag == "AiCarCapsule")
        {
            Debug.Log(this.gameObject.name + " -----------------CAR CRASH ");
            isCrashing = true;

            //SendCommands.turnOnMotor1();

            //Turn ON the corresponding motor for AI Collision
            switch (this.gameObject.name)
            {
                case "F_Col":
                    // F = FML + FMR, motor 0 and motor 1 together
                    SendCommands.turnOnMotor0();
                    // SendCommands.turnOnMotor6();
                    break;

                case "FL_Col":
                    SendCommands.turnOnMotor1();
                    break;

                    /*   case "BL_Col":
                           SendCommands.turnOnMotor2();
                           break;

                       case "B_Col":
                           SendCommands.turnOnMotor3();
                           break;

                       case "BR_Col":
                           SendCommands.turnOnMotor4();
                           break;

                       case "FR_Col":
                           SendCommands.turnOnMotor5();
                           break;
                    */
            }
        }
    }

    //Collision with edge (hexagons) 
    void OnTriggerStay(Collider other)
    { 
        if (other.gameObject.tag == "Hex_Coll_L" || other.gameObject.tag == "Hex_Coll_R")
        {
            Debug.Log(this.gameObject.name + " Edge ");
            isEdgeColliding = true;

            
           //SendCommands.turnOnMotor1();

            // TURN ON the corresponding motor for EDGE collision
            switch (this.gameObject.name)
            {
                case "F_Col": 
                    // F = FML + FMR, motor 0 and motor 1 together
                    SendCommands.turnOnMotor0();
                 // SendCommands.turnOnMotor6();
                    break;

                case "FL_Col":
                    SendCommands.turnOnMotor1();
                    break;

             /*   case "BL_Col":
                    SendCommands.turnOnMotor2();
                    break;

                case "B_Col":
                    SendCommands.turnOnMotor3();
                    break;

                case "BR_Col":
                    SendCommands.turnOnMotor4();
                    break;

                case "FR_Col":
                    SendCommands.turnOnMotor5();
                    break;
             */

            }
        }
    }

    // EXIT FROM COLLISION WITH AI CAR AND EDGE
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AiCarCapsule") 
        {
            isCrashing = false;

           // SendCommands.turnOffMotor1();

            //Turn OFF the corresponding motor for AI collision

            switch (this.gameObject.name)
            {
                case "F_Col":
                    // F = FML + FMR, motor 0 and motor 1 together
                    SendCommands.turnOffMotor0();
                    // SendCommands.turnOffMotor6();
                    break;

                case "FL_Col":
                    SendCommands.turnOffMotor1();
                    break;

                    /*   case "BL_Col":
                           SendCommands.turnOffMotor2();
                           break;

                       case "B_Col":
                           SendCommands.turnOffMotor3();
                           break;

                       case "BR_Col":
                           SendCommands.turnOffMotor4();
                           break;

                       case "FR_Col":
                           SendCommands.turnOffMotor5();
                           break;
                    */

            }
        }

        if (other.gameObject.tag == "Hex_Coll_L" || other.gameObject.tag == "Hex_Coll_R")
        {
            isEdgeColliding = false;

            //SendCommands.turnOffMotor1();

            // Turn OFF the corresponding motor for EDGE collision
            switch (this.gameObject.name)
            {
                case "F_Col":
                    // F = FML + FMR, motor 0 and motor 1 together
                    SendCommands.turnOffMotor0();
                 // SendCommands.turnOffMotor6();
                    break;

                case "FL_Col":
                    SendCommands.turnOffMotor1();
                    break;

                    /*   case "BL_Col":
                           SendCommands.turnOffMotor2();
                           break;

                       case "B_Col":
                           SendCommands.turnOffMotor3();
                           break;

                       case "BR_Col":
                           SendCommands.turnOffMotor4();
                           break;

                       case "FR_Col":
                           SendCommands.turnOffMotor5();
                           break;
                    */

            }
        }
    }

  
}