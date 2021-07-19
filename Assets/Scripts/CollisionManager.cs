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

   public Animator hexAnim;

    public AudioSource drainPowerSound;
    public AudioSource hitSound;
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
            //Start shaking the car
            hexAnim.SetTrigger("Shake");

            drainPowerSound.Play();
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
           // drainPower.Play();

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
            // Stop shaking the car
            hexAnim.SetTrigger("StopShake");

            if (drainPowerSound.isPlaying) {
                drainPowerSound.Stop();
            }
        }

        //Stop playing charging particles
        if (other.gameObject.tag == "Charging")
        {
            rechargingParticles.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "AiCar")
        {
            hitSound.Play();
        }

        if (other.gameObject.tag == "Wall_L" || other.gameObject.tag == "Wall_R")
        {
            hitSound.Play();
        }
    }
}