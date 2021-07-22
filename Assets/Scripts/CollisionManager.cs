using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour
{
    public float health;
    public Slider powerSlider;
    
    [Header("VFX")]
    public GameObject damageParticle;
    public GameObject rechargingParticles;
    public Animator shakeAnim;

    [Header("SOUND EFFECTS")]
    public AudioSource drainPowerSound;
    public AudioSource hitSound;
    public AudioSource explodeSound;

    public bool isDead;
    private bool soundPlayed = false;

    void Start()
    {
        isDead = false;
        damageParticle.SetActive(false);
        rechargingParticles.SetActive(false);
    }

    void Update()
    {
        powerSlider.value = health;

        /*  if(powerSlider.value == powerSlider.maxValue)
          {
              rechargingParticles.SetActive(false);
          }*/

        if (powerSlider.value == powerSlider.minValue)
        {
            // Die if drained health down to 0
            isDead = true;
            StopOtherVFXWhenDead();
            if (!soundPlayed)
            {
                explodeSound.Play();
                soundPlayed = true;
            }
        }

        if (soundPlayed)
        {
            if (!explodeSound.isPlaying)
            {
                explodeSound.Stop();
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //Play damage particles
        if (other.gameObject.tag == "Hex_Coll_L" || other.gameObject.tag == "Hex_Coll_R")
        {
            damageParticle.SetActive(true);
            //Start shaking the car
            shakeAnim.SetTrigger("Shake");

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
            if (!isDead)
            {
                health -= 10f * Time.deltaTime;
            }
           

        }

        //INCREASE HEALTH
        if (other.gameObject.tag == "Charging")
        {
            if (!isDead)
            {
                health += 10f * Time.deltaTime;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hex_Coll_L" || other.gameObject.tag == "Hex_Coll_R")
        {
            //Stop playing particles
            damageParticle.SetActive(false);
            // Stop shaking the car
            shakeAnim.SetTrigger("StopShake");

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

    void StopOtherVFXWhenDead()
    {
        damageParticle.SetActive(false);
        shakeAnim.SetTrigger("StopShake");
        hitSound.Stop();
        drainPowerSound.Stop();
        rechargingParticles.SetActive(false);
    }

}