using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionCulling : MonoBehaviour
{
    [Header("Objects to Occlude")]
    public GameObject[] racersMesh;
   // public GameObject[] domesMesh; 
    
    void Start()
    {
        for (int i = 0; i < racersMesh.Length; i++)
        {
            racersMesh[i].GetComponent<Renderer>().enabled = false;
        }


       
    }

    void OnTriggerEnter(Collider other)
    {
        // Rendering Cars
        for (int i = 0; i < racersMesh.Length; i++)
        {
            if (other.gameObject == racersMesh[i])
            {
                racersMesh[i].GetComponent<Renderer>().enabled = true;  // Render Racer´s Mesh
                racersMesh[i].transform.GetChild(0).gameObject.SetActive(true); // Enable Shadow
                racersMesh[i].transform.GetChild(1).gameObject.SetActive(true); // Enable Particles
            }
        }

        // Rendering World Deco Parts
     /*   for (int d = 0; d < domesMesh.Length; d++)
        {
            if (other.gameObject == domesMesh[d])
            {
                domesMesh[d].GetComponent<Renderer>().enabled = true;  // Render Dome´s Mesh
            }
        }
        */
    }

 
    void OnTriggerExit(Collider other)
    {
        // Stop Rendering Cars
        for (int i = 0; i < racersMesh.Length; i++)
        {
            if (other.gameObject == racersMesh[i])
            {
                racersMesh[i].GetComponent<Renderer>().enabled = false; // Don´t Render Racer´s Mesh

                racersMesh[i].transform.GetChild(0).gameObject.SetActive(false); // Disable Shadow
                racersMesh[i].transform.GetChild(1).gameObject.SetActive(false);  // Disable Particles
            }
        }

        // Stop Rendering World Deco Parts
    /*    for (int d = 0; d < domesMesh.Length; d++)
        {
            if (other.gameObject == domesMesh[d])
            {
                domesMesh[d].GetComponent<Renderer>().enabled = false;  // Don´t Render Dome´s Mesh
            }
        }
    */
    }
}
