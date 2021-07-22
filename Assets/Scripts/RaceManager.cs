using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using TMPro;
using KartGame.KartSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
   
    [Header("USING SCRIPTS OF")]
    public GameObject Player;
    public GameObject playerProgressTracker;
    public GameObject cameraTargetObject;
    [SerializeField] GameObject[] AICars;


    [Header("TEXT OUTPUTS")]
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI goTxt;
    public TextMeshProUGUI lapTxt;
    public TextMeshProUGUI gameOverTxt;
    public TextMeshProUGUI currentPlayerRankTxt;
    public TextMeshProUGUI playAgainTxt;
    public TextMeshProUGUI restartTxt;

    [Header("MUSIC")]
    public AudioSource BigBlueMusic;
    public AudioSource EndingThemeMusic;
    public AudioSource GameOverMusic;
    public GameObject engigeSound;

    [Header("DELETEABLE PARTS")]
    public GameObject partsHolder;

    [Header("RANKMESHES")]
    public GameObject[] rankMesh;

    public Image overlayImage;
    public float rotateSpeed;

    bool racestarted = false;
    bool countDownFinished = false;
    bool raceEnded = false;
    float currentTime = 0f;
    float startingTime = 8.4f;
    public bool _isDead;
    public bool played = false; // for the audio clip state

    void Start()
    {
        Player.GetComponent<CinematicDriving>().enabled = false;
        engigeSound.GetComponent<AudioSource>().mute = true;
        goTxt.enabled = false;
        countdownText.enabled = false;
        gameOverTxt.enabled = false;
        playAgainTxt.enabled = false;
        restartTxt.enabled = false;

        currentTime = startingTime;
        DisableControllers();
        overlayImage.canvasRenderer.SetAlpha(0.0f);
       

        for (int i = 0; i < rankMesh.Length; i++)
        {
            rankMesh[i].SetActive(false);
        }
    }

    void Update()
    {
       
       _isDead = Player.GetComponent<CollisionManager>().isDead;
        
        CountDown();

        if (!racestarted && countDownFinished)
        {
            Race();
        }

      

        if (racestarted && !BigBlueMusic.isPlaying)
        {
            BigBlueMusic.Play();
        }

        
        lapTxt.text = playerProgressTracker.GetComponent<CarLap>().lapNumber.ToString("0");

        // Finish Race
        if (lapTxt.text == "1")
        {
            EndRace();
            StartCoroutine(RotateSideways(Vector3.up * -90, rotateSpeed));
            StartCoroutine(RotateRankMesh(Vector3.up * -90, rotateSpeed));
        }

        if (raceEnded && BigBlueMusic.isPlaying)
        {
            BigBlueMusic.Stop();
            if (!EndingThemeMusic.isPlaying)
            {
                EndingThemeMusic.Play(); 
            }
        }

        if (_isDead)
        {
            GameOver();
            StartCoroutine(RotateSideways(Vector3.up * -90, rotateSpeed));

            // Play Game Over Music
            if (!played)
            {
                StartCoroutine(DelayDeathMusic());
                played = true;
            }
        }

        //Restart Game (Load Menu)
        if (playAgainTxt.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("PickCarScene");
            }
        }
        
        if (restartTxt.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
               Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            }
        }

    }

    void DisableControllers()
    {
        AICars = GameObject.FindGameObjectsWithTag("AiCar");
        foreach (GameObject car in AICars)
        {
            car.GetComponent<CarAIControl>().enabled = false;
        }

        Player.GetComponent<ArcadeKart>().enabled = false; 
    }

    void CountDown()
    {
        currentTime -= 1 * Time.deltaTime;

        //  countdownText.text = currentTime.ToString("0");

        if (currentTime <= 4)
        {
            countdownText.enabled = true;
            countdownText.text = "3";
        }

        if (currentTime <= 3)
        {
            countdownText.text = "2";
        }

        if (currentTime <= 2)
        {
            countdownText.text = "1";
        }

        if (currentTime <= 1)
        {
            countDownFinished = true;
            countdownText.enabled = false;
            Destroy(cameraTargetObject.GetComponent<Animator>());
        }
    }

    void Race()
    {
        foreach (GameObject car in AICars)
        {
            car.GetComponent<CarAIControl>().enabled = true;
        }

        Player.GetComponent<ArcadeKart>().enabled = true;
        engigeSound.GetComponent<AudioSource>().mute = false;

        racestarted = true;
        goTxt.enabled = true;

        if (goTxt.enabled)
        {
            StartCoroutine(DelayDisplayText());
        }
    }

    void EndRace()
    {
        this.GetComponent<Ranking>().enabled = false;
        DisableControllers();
        Player.GetComponent<CollisionManager>().enabled = false;
        Player.GetComponent<KeyboardInput>().enabled = false;
        Destroy(Player.GetComponent<Rigidbody>());

        foreach(Transform child in partsHolder.transform)
        {
            Destroy(child.gameObject);
        }

        raceEnded = true;
        Player.GetComponent<CinematicDriving>().enabled = true;

       // Display Final Rank (as mesh) and prompt to play again
        ActivateRankMesh();
        StartCoroutine(DelayPlayAgainTxt());

    }

    void GameOver()
    {
        Player.GetComponent<ArcadeKart>().enabled = false;
        engigeSound.GetComponent<AudioSource>().mute = true;

        Rigidbody p_rb = Player.GetComponent<Rigidbody>();
        p_rb.velocity = Vector3.zero;

        if (BigBlueMusic.isPlaying)
        {
            BigBlueMusic.Stop();
        }

        // Prompt to restart the game
        StartCoroutine(DelayRestartTxt());
    }
   
    private IEnumerator DelayPlayAgainTxt()
    {
        yield return new WaitForSeconds(5);
        FadeIn();
        playAgainTxt.enabled = true;
    }

    private IEnumerator DelayDeathMusic()
    {
        yield return new WaitForSeconds(4);
        GameOverMusic.Play();
    }

    private IEnumerator DelayRestartTxt()
    {
        yield return new WaitForSeconds(5);
        FadeIn();
        gameOverTxt.enabled = true;
        restartTxt.enabled = true;
    }

    private IEnumerator DelayDisplayText()
    {
        yield return new WaitForSeconds(2);
        goTxt.enabled = false;
    }
    void FadeIn() 
    {
        overlayImage.CrossFadeAlpha(1, 1f, false);
    }

    IEnumerator RotateSideways(Vector3 byAngles, float inTime)
    {
        Quaternion fromAngle = cameraTargetObject.transform.rotation;
        Quaternion toAngle = Quaternion.Euler(cameraTargetObject.transform.eulerAngles + byAngles);
        for (float t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            cameraTargetObject.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    IEnumerator RotateRankMesh(Vector3 byAngles, float inTime)
    {
        for (int i = 0; i < rankMesh.Length; i++) {

            Quaternion fromAngle = rankMesh[i].transform.rotation;
            Quaternion toAngle = Quaternion.Euler(rankMesh[i].transform.eulerAngles + byAngles);
            for (float t = 0f; t < 1; t += Time.deltaTime / inTime)
            {
                rankMesh[i].transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
                yield return null;
            }
        }
    }

    void ActivateRankMesh()
    {
        string playerRank = currentPlayerRankTxt.text;

        switch (currentPlayerRankTxt.text)
        {
            case "1":
                rankMesh[0].SetActive(true);
                break;
            case "2":
                rankMesh[1].SetActive(true);
                break;
            case "3":
                rankMesh[2].SetActive(true);
                break;
            case "4":
                rankMesh[3].SetActive(true);
                break;
        }
    }

}