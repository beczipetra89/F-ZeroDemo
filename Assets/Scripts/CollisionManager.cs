using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour
{
    public float health;
    public Slider powerSlider;

    void Update()
    {
        powerSlider.value = health;
    }

    void OnCollisionEnter(Collision other)
    {
        
            if (other.gameObject.tag == "Hex_Coll_R" || other.gameObject.tag == "Hex_Coll_L")
        {
            // Decrease health

           // health = health - 10f;
            //play particles
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hex_Coll_R" || other.gameObject.tag == "Hex_Coll_L")
        {
            //play particles
            //Decrease health continuously
            health -= 10f * Time.deltaTime;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hex_Coll_R" || other.gameObject.tag == "Hex_Coll_L")
        {
            //stop playing particles
        }
    }
}
