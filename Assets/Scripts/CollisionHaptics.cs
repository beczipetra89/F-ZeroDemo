// VIBRATION FEEDBACK FOR THE COLLISION WITH EDGE AND OTHER CARS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHaptics : MonoBehaviour
{
 
    ////////////////////////////// COLLISION WITH NPC \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "NPCDriver")
        {
            //Turn ON the corresponding motor for NPC collision
            switch (this.gameObject.name)
            {
                case "F_Col":
                    // F = FML + FMR, motor 0 and motor 1 together
                    SendCommands.turnOnMotor(0, 200);
                    SendCommands.turnOnMotor(6, 200);
                    break;

                case "FL_Col":
                    SendCommands.turnOnMotor(1, 200);
                    break;

                case "BL_Col":
                     SendCommands.turnOnMotor(2,200);
                     break;

                case "B_Col":
                      SendCommands.turnOnMotor(3,200);
                      break;

                case "BR_Col":
                      SendCommands.turnOnMotor(4,200);
                      break;

                case "FR_Col":
                      SendCommands.turnOnMotor(5,200);
                      break;
            }
        }
    }

    void OnCollisionExit(Collision other)
    {

        if (other.gameObject.tag == "NPCDriver") // aiCar // AiCarCapsule
        {
            //isCrashing = false;

            // Turn OFF the corresponding motor for NPC collision
            switch (this.gameObject.name)
            {
                case "F_Col":
                    // F = FML + FMR, motor 0 and motor 1 together
                    SendCommands.turnOffMotor(0);
                    SendCommands.turnOffMotor(6);
                    break;

                case "FL_Col":
                    SendCommands.turnOffMotor(1);
                    break;

                case "BL_Col":
                     SendCommands.turnOffMotor(2);
                     break;

                case "B_Col":
                      SendCommands.turnOffMotor(3);
                      break;

                case "BR_Col":
                      SendCommands.turnOffMotor(4);
                      break;

                case "FR_Col":
                      SendCommands.turnOffMotor(5);
                      break;
            }
        }
    }
   

    void OnTriggerEnter(Collider other)
    {
        ////////////////////////////// COLLISION WITH AI CARS \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        if (other.gameObject.tag == "AiCarCapsule" )
        {
            switch (this.gameObject.name)
            {
                case "F_Col":
                    // F = FML + FMR, motor 0 and motor 1 together
                    SendCommands.turnOnMotor(0, 200);
                    SendCommands.turnOnMotor(6, 200);
                    break;

                case "FL_Col":
                    SendCommands.turnOnMotor(1,200);
                    break;

                case "BL_Col":
                    SendCommands.turnOnMotor(2,200);
                    break;

                case "B_Col":
                    SendCommands.turnOnMotor(3,200);
                    break;

                case "BR_Col":
                    SendCommands.turnOnMotor(4,200);
                    break;

                case "FR_Col":
                    SendCommands.turnOnMotor(5,200);
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

   

    
    void OnTriggerExit(Collider other)
    {
        // EXIT FROM COLLISION WITH AI CAR
        if (other.gameObject.tag == "AiCarCapsule") 
        {
            //isCrashing = false;

            //Turn OFF the corresponding motor for AI collision

            switch (this.gameObject.name)
            {
                case "F_Col":
                    // F = FML + FMR, motor 0 and motor 1 together
                     SendCommands.turnOffMotor(0);
                     SendCommands.turnOffMotor(6);
                     break;

                case "FL_Col":
                      SendCommands.turnOffMotor(1);
                      break;

                case "BL_Col":
                      SendCommands.turnOffMotor(2);
                      break;

                case "B_Col":
                      SendCommands.turnOffMotor(3);
                      break;

                case "BR_Col":
                      SendCommands.turnOffMotor(4);
                      break;

                case "FR_Col":
                      SendCommands.turnOffMotor(5);
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