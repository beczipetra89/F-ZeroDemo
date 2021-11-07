using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OvertakeIndicator : MonoBehaviour
{

   public GameObject maincamera;
    
    [Header ("Opponent1")]
    [Header("Overtake Attempts Tracking")]
  
    public Transform target1;
    private Vector3 overtakee1; // Takrget 1 on screen
    public float xValue1;

    public Transform target2;
    private Vector3 overtakee2; // Takrget 2 on screen
    public float xValue2;

    public Transform target3;
    private Vector3 overtakee3; // Takrget 3 on screen
    public float xValue3;

    public Transform target4;
    private Vector3 overtakee4; // Takrget 4 on screen
    public float xValue4;

    ///////////////////////////////////////////////////////////// TEST
    public Transform target5;
    private Vector3 overtakee5; 
    public float xValue5;
    public float zValue5;

    [Header("Indicator Sliders")]
    public GameObject slider1;
    public GameObject slider2;
    public GameObject slider3;
    public GameObject slider4;

    ////////////////////////////////////////////////// TEST
    public GameObject slider5;
   

    void Start()
    {
        slider1.SetActive(false);
        slider2.SetActive(false);
        slider3.SetActive(false);
        slider4.SetActive(false);

        ///////////////////////////////////////////////TEST
        slider5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Track Overakee 1
        if (slider1.activeSelf)
        {
            Vector3 screenPos1 = maincamera.GetComponent<Camera>().WorldToScreenPoint(target1.position);
            overtakee1 = new Vector3(screenPos1.x, screenPos1.y, screenPos1.z);
            xValue1 = screenPos1.x;
            slider1.GetComponent<Slider>().value = xValue1;
        }

        // Track Overakee 2
        if (slider2.activeSelf)
        {
            Vector3 screenPos2 = maincamera.GetComponent<Camera>().WorldToScreenPoint(target2.position);
            overtakee2 = new Vector3(screenPos2.x, screenPos2.y, screenPos2.z);
            xValue2 = screenPos2.x;
            slider2.GetComponent<Slider>().value = xValue2;
        }

        // Track Overakee 3
        if (slider3.activeSelf)
        {
            Vector3 screenPos3 = maincamera.GetComponent<Camera>().WorldToScreenPoint(target3.position);
            overtakee3 = new Vector3(screenPos3.x, screenPos3.y, screenPos3.z);
            xValue3 = screenPos3.x;
            slider3.GetComponent<Slider>().value = xValue3;
        }

        // Track Overakee 4
        if (slider4.activeSelf)
        {
            Vector3 screenPos4 = maincamera.GetComponent<Camera>().WorldToScreenPoint(target4.position);
            overtakee4 = new Vector3(screenPos4.x, screenPos4.y, screenPos4.z);
            xValue4 = screenPos4.x;
            slider4.GetComponent<Slider>().value = xValue4;
        }

        ///////////////////////////////////////////// TEST
        ///
        if (slider5.activeSelf)
        {
            Vector3 screenPos5 = maincamera.GetComponent<Camera>().WorldToScreenPoint(target5.position);
            overtakee5 = new Vector3(screenPos5.x, screenPos5.y, screenPos5.z);
            xValue5 = screenPos5.x;
            slider5.GetComponent<Slider>().value = xValue5;

            zValue5 = screenPos5.z;

            // Calculate distance 
           // distance5 = Vector3.Distance(target5.position, transform.position);
        }
       /* else
        {
            xValue1 = 0.0f;
            xValue2 = 0.0f;
            xValue3 = 0.0f;
            xValue4 = 0.0f;
            xValue5 = 0.0f; zValue5 = 0.0f;
        } */
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "TriggerSlider1")
        {
            slider1.SetActive(true);
        }

        if (other.gameObject.tag == "TriggerSlider2")
        {
            slider2.SetActive(true);
        }

        if (other.gameObject.tag == "TriggerSlider3")
        {
            slider3.SetActive(true);
        }

        if (other.gameObject.tag == "TriggerSlider4")
        {
            slider4.SetActive(true);
        }

        ///////////////////////////////////////////TEST CANVAS
        if (other.gameObject.tag == "TriggerSlider5")
        {
            slider5.SetActive(true);
          //  target5IsActive = true;
        }
    }


    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "TriggerSlider1") 
        {
            slider1.SetActive(false);
        }

        if (other.gameObject.tag == "TriggerSlider2")
        {
            slider2.SetActive(false);
        }

        if (other.gameObject.tag == "TriggerSlider3")
        {
            slider3.SetActive(false);
        }

        if (other.gameObject.tag == "TriggerSlider4")
        {
            slider4.SetActive(false);
        }

        ///////////////////////////////////////////TEST CANVAS
        if (other.gameObject.tag == "TriggerSlider5")
        {
            slider5.SetActive(false);
            //target5IsActive = false;
        }
    }
}
