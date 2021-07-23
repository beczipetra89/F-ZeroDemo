using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GraphAnimator : MonoBehaviour
{
    [Header("LIST OF PANELS")]
    public GameObject[] panels;
    private int currentPanelIndex = 0;
   
    [Header("LIST OF CHART MATERIALS")]
    public Material[] charts;
    private float graph_value = 1f;
    private float maxValue = 0f;

    [Header("LIST OF CARS")]
    public GameObject[] cars;
    public Transform[] targets; // Invisible gameobjects the cars looking at

    [Header("SCENE LOADERS")]
    public TextMeshProUGUI[] enterTxts;

    void Start()
    {
        //Hide panels on start (except for the first one)
        for (int i = 1; i<panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        //Set all chart materials alpha threshold clip to 1 (invisible)
        for (int i = 0; i < charts.Length; i++)
        {
            charts[i].SetFloat("cliptresholdref", graph_value);
        }
    }

   
    void Update()
    {

        //Toggle between panels on the right
        if (Input.GetKeyDown("down"))
        {
            graph_value = 1f; // Reset the curve's transparency value

            if (currentPanelIndex <=2)
            {
                panels[currentPanelIndex].SetActive(false);
                currentPanelIndex += 1;
                panels[currentPanelIndex].SetActive(true);
            }
        }

        if (Input.GetKeyDown("up"))
        {
            graph_value = 1f; // Reset the curve's transparency value

            if (currentPanelIndex >= 1)
            {
                panels[currentPanelIndex].SetActive(false);
                currentPanelIndex -= 1;
                panels[currentPanelIndex].SetActive(true);
            }
        }

        //Showcase cars on the left
        StopRotation();
        StartRotation();

        //Fill acceleration curve slowly on active panels
        UVFillClipTresholdValue();

        //Load Next Scene
        SelectSceneToLoad();

    }

    public void StartRotation()
    {
        for (int c = 0, t = 0, p = 0; c < cars.Length && t < targets.Length && p < panels.Length; c++, t++, p++)
        {
            if (panels[p].activeSelf == true)
            {
                if (c == p)
                {
                  cars[c].transform.Rotate(0, 30f * Time.deltaTime, 0); // Rotate the car of the active panel
                  cars[c].transform.localScale = new Vector3(250, 250, 250); // Scale up the size
                }
            }
        }
       

    }

    public void StopRotation()
    {
        for (int c = 0, t = 0, p = 0; c < cars.Length && t < targets.Length && p < panels.Length; c++, t++, p++)
        {
            if (panels[p].activeSelf == false)
            {
                if (c == p)
                {
                    cars[c].transform.LookAt(targets[t], Vector3.up); // Don't rotate cars of inactive panels
                    cars[c].transform.localScale = new Vector3(200, 200, 200); // Scale back the size to original
                }
            }
        }

    }

    public void UVFillClipTresholdValue()
    {
        graph_value -= 0.1f * Time.deltaTime * 6;

        for (int p = 0, ch = 0; p < panels.Length && ch < charts.Length; p++, ch++)
        {
            if (panels[p].activeSelf == true)
            {
                if(p == ch)
                {
                    if (graph_value >= maxValue)
                    {
                        charts[ch].SetFloat("cliptresholdref", graph_value);
                    }
                }
            }
        }
    }

    void SelectSceneToLoad()
    {
       // Load Racing for Golden Fox
        if (enterTxts[1].isActiveAndEnabled && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("RacingScene");
        }
    }

}
