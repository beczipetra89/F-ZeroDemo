using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvasByRange : MonoBehaviour
{
    public GameObject canvasObject1;
    public GameObject canvasObject2;
    public GameObject canvasObject3;
    public GameObject canvasObject4;

    // Update is called once per frame
    void Start()
    {
        canvasObject1.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "canvas1")
        {
            canvasObject1.SetActive(true);
        }

        if (other.gameObject.tag == "canvas2")
        {
            canvasObject2.SetActive(true);
        }

        if (other.gameObject.tag == "canvas3")
        {
            canvasObject3.SetActive(true);
        }

        if (other.gameObject.tag == "canvas4")
        {
            canvasObject4.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "canvas1")
        {
            canvasObject1.SetActive(false);
        }

        if (other.gameObject.tag == "canvas2")
        {
            canvasObject2.SetActive(false);
        }

        if (other.gameObject.tag == "canvas3")
        {
            canvasObject3.SetActive(false);
        }

        if (other.gameObject.tag == "canvas4")
        {
            canvasObject4.SetActive(false);
        }
    }
}
