using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour
{
    public float health;
    public Slider powerSlider;
    public GameObject damageParticle;
    public GameObject rechargingParticles;

    void Start()
    {
        damageParticle.SetActive(false);
        rechargingParticles.SetActive(false);
    }

    void Update()
    {
        powerSlider.value = health;

        if(powerSlider.value == powerSlider.maxValue)
        {
            rechargingParticles.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Play damage particles
        if (other.gameObject.tag == "Hex_Coll_L" || other.gameObject.tag == "Hex_Coll_R")
        {
            damageParticle.SetActive(true);
        }

        // Play charging particles
        if (other.gameObject.tag == "Charging")
        {
            if (powerSlider.value <= powerSlider.maxValue)
                rechargingParticles.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        //DEDUCT HEALTH
        if (other.gameObject.tag == "Hex_Coll_L" || other.gameObject.tag == "Hex_Coll_R")
        {
            //Decrease health continuously
            health -= 10f * Time.deltaTime;
        }

        //INCREASE HEALTH
        if (other.gameObject.tag == "Charging")
        {
            health += 10f * Time.deltaTime;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hex_Coll_L" || other.gameObject.tag == "Hex_Coll_R")
        {
            //Stop playing particles
            damageParticle.SetActive(false);
        }

        //Stop playing charging particles
        if (other.gameObject.tag == "Charging")
        {
            rechargingParticles.SetActive(false);
        }
    }
}