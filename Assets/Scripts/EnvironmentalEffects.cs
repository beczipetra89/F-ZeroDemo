using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.VFX;

public class EnvironmentalEffects : MonoBehaviour
{

    public GameObject playerController;
    [SerializeField]
    private Rigidbody rb;
    public float forcePower;
    public float nitroCoolDown;

    [Header("Post Processing Volume")]
    public Volume nitroCameraEffect;
    private Vignette _vignette;
    private ChromaticAberration _chromaticAberration;
    public GameObject nitroFlamesVFX;
    bool usingNitro;
   // PP Volume Override Value Flags
    bool isZero1;
    bool isZero2;

    [Header("WrapVFX")]
    public VisualEffect wrapSpeedVFX;
    public float rate = 0.02f;
    private bool wrapActive;
    private bool _hasNitro;
    private bool _pressedBUtton;
    private bool _isSpeeding;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController.GetComponent<ArcadeKart>().baseStats.TopSpeed = 45f;
        playerController.GetComponent<ArcadeKart>().baseStats.Acceleration = 7f;

        nitroCameraEffect.enabled = false;
        nitroCameraEffect.profile.TryGet(out _vignette);
        nitroCameraEffect.profile.TryGet(out _chromaticAberration);
        nitroFlamesVFX.SetActive(false);
        usingNitro = false;
        wrapSpeedVFX.Stop();
        wrapSpeedVFX.SetFloat("WrapAmount", 0);
    }

    void FixedUpdate()
    {
        _hasNitro = GetComponent<NitroManager>().hasNitro;
        _pressedBUtton = GetComponent<NitroManager>().pressedButton;

        if (_hasNitro && _pressedBUtton)
        {
            UseNitro();
        }

        
        if (nitroCameraEffect.enabled)
        {
            if (usingNitro)
            {
                FadeInChromatic();
                PulseVignette();
                wrapActive = true;
                StartCoroutine(ActivateWrapParticles());
            }

            if (!usingNitro)
            {
                FadeOutChromatic();
                FadeOutVignette();
                wrapActive = false;
                StartCoroutine(ActivateWrapParticles());
            }
        }

        if (isZero1 && isZero2)
        {
            ResetAndDisableNitroCameraEffect();
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Slippery")
        {
            // DRIFT
            playerController.GetComponent<ArcadeKart>().baseStats.Braking = 5f;
            playerController.GetComponent<ArcadeKart>().baseStats.CoastingDrag = 0.1f;
            playerController.GetComponent<ArcadeKart>().baseStats.Grip = 0f;
            playerController.GetComponent<ArcadeKart>().DriftGrip = 0.01f;
            playerController.GetComponent<ArcadeKart>().DriftAdditionalSteer = 10f;
            playerController.GetComponent<ArcadeKart>().MinAngleToFinishDrift = 0f;
            playerController.GetComponent<ArcadeKart>().DriftControl = 1f;
            playerController.GetComponent<ArcadeKart>().DriftDampening = 20f;
        }
    }


    void OnTriggerStay(Collider other)
    {
        // SLIDE
        if (other.gameObject.tag == "Slippery")
        {
            rb.AddRelativeForce(Vector3.left * forcePower);
        }

        if (other.gameObject.tag == "Rough")
        {
            // SLOW MOTION
            rb.velocity = rb.velocity * 0.97f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Slippery")
        {
            // GAIN BACK NORMAL CONTROL (GOOD GRIP)
            playerController.GetComponent<ArcadeKart>().baseStats.Braking = 16f;
            playerController.GetComponent<ArcadeKart>().baseStats.CoastingDrag = 5f;
            playerController.GetComponent<ArcadeKart>().baseStats.Grip = 0.97f;
            playerController.GetComponent<ArcadeKart>().DriftGrip = 0.85f;
            playerController.GetComponent<ArcadeKart>().DriftAdditionalSteer = 0f;
            playerController.GetComponent<ArcadeKart>().MinAngleToFinishDrift = 29f;
            playerController.GetComponent<ArcadeKart>().DriftControl = 16f;
            playerController.GetComponent<ArcadeKart>().DriftDampening = 8f;
        }


        if (other.gameObject.tag == "Rough")
        {
            playerController.GetComponent<ArcadeKart>().baseStats.TopSpeed = 45f;
        }
    }

    void UseNitro()
    {
       
        //SPEED BOOST
        playerController.GetComponent<ArcadeKart>().baseStats.TopSpeed = 90f;
        playerController.GetComponent<ArcadeKart>().baseStats.Acceleration = 14f;

        nitroCameraEffect.enabled = true;
        nitroFlamesVFX.SetActive(true);

        usingNitro = true;
        isZero1 = false;
        isZero2 = false;

        StartCoroutine("NitroDuration");
    }

    IEnumerator NitroDuration()
    {
        yield return new WaitForSeconds(nitroCoolDown);
        playerController.GetComponent<ArcadeKart>().baseStats.TopSpeed = 45f;
        playerController.GetComponent<ArcadeKart>().baseStats.Acceleration = 7f;

        usingNitro = false;
    }

  
    void FadeInChromatic()
    {
        if (_chromaticAberration.intensity.value != 1f) {
            _chromaticAberration.intensity.value = Mathf.Lerp(0f, 1f, Mathf.PingPong(Time.time, 6));
        }
    }

    void FadeOutChromatic()
    {
        
        if (_chromaticAberration.intensity.value != 0f)
        {
            _chromaticAberration.intensity.value = Mathf.Lerp(0f, 1f, Mathf.PingPong(Time.time, 1));
        }
        if (_chromaticAberration.intensity.value == 0f)
        {
            isZero1 = true;
        }
    }

   void PulseVignette()
    {
        _vignette.intensity.value = Mathf.Lerp(0.2f, 0.6f, Mathf.PingPong(Time.time, 1));
    }

    void FadeOutVignette()
    {
        if (_vignette.intensity.value != 0f) {
            _vignette.intensity.value = Mathf.Lerp(0f, 0.6f, Mathf.PingPong(Time.time, 1)); 
        }

        if (_vignette.intensity.value == 0f)
        {
            isZero2 = true;
        }
    }

    IEnumerator ActivateWrapParticles()
    {
        if (wrapActive)
        {
            wrapSpeedVFX.Play();

            float amount = wrapSpeedVFX.GetFloat("WrapAmount");
            while (amount < 1 & wrapActive)
            {
                amount += rate;
                wrapSpeedVFX.SetFloat("WrapAmount", amount);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else 
        {
            float amount = wrapSpeedVFX.GetFloat("WrapAmount");
            while (amount > 0 & !wrapActive)
            {
                amount -= rate;
                wrapSpeedVFX.SetFloat("WrapAmount", amount);
                yield return new WaitForSeconds(0.1f);

                if(amount <= 0 + rate) 
                {
                    amount = 0;
                    wrapSpeedVFX.SetFloat("WrapAmount", amount);
                    wrapSpeedVFX.Stop();
                }
            }
        }
          
    }

    void ResetAndDisableNitroCameraEffect()
    {
        _vignette.intensity.value = 0f;
        _chromaticAberration.intensity.value = 0f;
        nitroCameraEffect.enabled = false;
        nitroFlamesVFX.SetActive(false);
        _isSpeeding = GetComponent<NitroManager>().isSpeeding = false;
    }
}
