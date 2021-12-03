using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    [Header("SCENE LOADERS")]
    public TextMeshProUGUI[] enterTxts;
    public GameObject loadingPanel;
    public Slider loadingBar;
    
    void Update()
    {
        if (enterTxts[0].isActiveAndEnabled && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoadAsynchronously("RacingScene_Blue")); // Blue Falcon
        }

        if (enterTxts[1].isActiveAndEnabled && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoadAsynchronously("RacingScene")); // Golden Fox
        }

        if (enterTxts[2].isActiveAndEnabled && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoadAsynchronously("RacingSceneGreen")); // Wild Goose
        }

        if (enterTxts[3].isActiveAndEnabled && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoadAsynchronously("RacingScenePink")); // Fire Stingray
        }

    }

    IEnumerator LoadAsynchronously(string sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingPanel.SetActive(true);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;
            yield return null;
        }
    }

}
