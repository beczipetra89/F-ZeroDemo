using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CollisionManager : MonoBehaviour
{
    public float health;
    public Slider powerSlider;
    
    [Header("VFX")]
    public GameObject damageParticle;
    public GameObject rechargingParticles;
    public Animator shakeAnim;
    public GameObject explosionParticles;

    [Header("SOUND EFFECTS")]
    public AudioSource drainPowerSound;
    public AudioSource hitSound;
    public AudioSource explodeSound;
    public AudioSource chargeSound;

    public bool isDead;
    private bool soundPlayed = false;

    public int startingPitch = 0;
    public float timeToDecrease = 0.1f;

    [Header("NPC BEHAVIOUR SCRIPTS")]
    public NPCBehaviour[] npcScripts;

    [Header("LAP TXT")]
    public TextMeshProUGUI lapTxt;

    [Header("POST PROCESSING EFFECTS")]
    public Volume hurtCameraEffect;
    Bloom hurt_bloom;
    bool isHurt;

    public Volume chargeCameraEffect; [Header("POST PROCESSING EFFECTS")]
    Bloom charge_bloom;
    public static bool isCharging;

    void Start()
    {
        isDead = false;
        damageParticle.SetActive(false);
        rechargingParticles.SetActive(false);
        explosionParticles.SetActive(false);
        chargeSound.pitch = startingPitch;

        // CAMERA POST PROCESSING EFFECTS
        hurtCameraEffect.enabled = false;
        hurtCameraEffect.profile.TryGet(out hurt_bloom);

    
        chargeCameraEffect.profile.TryGet(out charge_bloom);

}

    void FixedUpdate()
    {
       
        if (isHurt)
        {
            PlayHurtCameraEffect(); 
        }

        if (isCharging)
        {
            PlayChargeCameraEffect();
        }

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
            explosionParticles.SetActive(true);
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

            hurtCameraEffect.enabled = true;
            isHurt = true;
        }

        // Play charging particles
        if (other.gameObject.tag == "Charging")
        {
            if (powerSlider.value <= powerSlider.maxValue)
            {
                rechargingParticles.SetActive(true);
            }
            chargeSound.Play();
          
            isCharging = true;
        }

        // Spawn NPCs
         if (other.gameObject.tag == "SpawnNpc1")
         {
            if (lapTxt.text == "1")
            {
                npcScripts[0].spawned = true;
            }
         }

        if (other.gameObject.tag == "SpawnNpc2")
        {
            if (lapTxt.text == "0")
            {
                npcScripts[1].spawned = true;
            }
        }

        if (other.gameObject.tag == "SpawnNpc3")
        {
            if (lapTxt.text == "1")
            {
                npcScripts[2].spawned = true;
            }
        }

        if (other.gameObject.tag == "SpawnNpc4")
        {
            if (lapTxt.text == "0")
            {
                npcScripts[3].spawned = true;
            }
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
            if (!isDead && health <= powerSlider.maxValue)
            {
                health += 25f * Time.deltaTime;
            }

            if (chargeSound.pitch < 3)
            {
                chargeSound.pitch += Time.deltaTime * startingPitch / timeToDecrease;
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

            if (drainPowerSound.isPlaying)
            {
                drainPowerSound.Stop();
            }

            hurtCameraEffect.enabled = false;
            isHurt = false;
        }

        //Stop playing charging particles
        if (other.gameObject.tag == "Charging")
        {
            rechargingParticles.SetActive(false);
            chargeSound.Stop();
            chargeSound.pitch = startingPitch;
            isCharging = false;
        }

      
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "AiCar" || other.gameObject.tag == "NPCDriver")
        {
            hitSound.Play();
            if (!isDead)
            {
                health -= 5f;
            }
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

    void PlayHurtCameraEffect()
    {
        hurt_bloom.intensity.value = Mathf.PingPong(Time.time * 80, 1f);
    }

    void PlayChargeCameraEffect()
    {
        charge_bloom.intensity.value = Mathf.PingPong(Time.time * 20, 6.54f);
    }

}