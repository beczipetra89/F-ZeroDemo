using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionCulling : MonoBehaviour
{
    [Header("Objects to Occlude")]
    public GameObject[] racersMesh;
    public GameObject[] npcMesh; 
    
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
        for (int r = 0; r < racersMesh.Length; r++)
        {
            if (other.gameObject == racersMesh[r])
            {
                racersMesh[r].GetComponent<Renderer>().enabled = true;  // Render Racer´s Mesh
                racersMesh[r].transform.GetChild(0).gameObject.SetActive(true); // Enable Shadow
                racersMesh[r].transform.GetChild(1).gameObject.SetActive(true); // Enable Particles
            }
        }

        // Render NPC cars
        for (int n = 0; n < npcMesh.Length; n++)
        {
            if (other.gameObject == npcMesh[n])
            {
                npcMesh[n].GetComponent<Renderer>().enabled = true;
                racersMesh[n].transform.GetChild(1).gameObject.SetActive(true); // Enable Engine Particles
            }
        }

    }

 
    void OnTriggerExit(Collider other)
    {
        // Stop Rendering Cars
        for (int r = 0; r < racersMesh.Length; r++)
        {
            if (other.gameObject == racersMesh[r])
            {
                racersMesh[r].GetComponent<Renderer>().enabled = false; // Don´t Render Racer´s Mesh

                racersMesh[r].transform.GetChild(0).gameObject.SetActive(false); // Disable Shadow
                racersMesh[r].transform.GetChild(1).gameObject.SetActive(false);  // Disable Particles
            }
        }

        // Stop Rendering NPC Cars
        for (int n = 0; n < npcMesh.Length; n++)
        {
            if (other.gameObject == npcMesh[n])
            {
                npcMesh[n].GetComponent<Renderer>().enabled = false;
                racersMesh[n].transform.GetChild(1).gameObject.SetActive(false);
            }
        }

    }
}
