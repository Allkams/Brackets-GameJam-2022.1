using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CollectItems : MonoBehaviour
{
    [SerializeField, Tooltip("The sound that will be played when picking up coins.")]
    private AudioSource coinPickupSound;
    [SerializeField, Tooltip("CoinHandler, Talks to the Canvas")]
    private CoinsHandler coinHandler;
    private int coins;

    [Space]

    [SerializeField, Tooltip("The sound when entering ghostmode")]
    private AudioSource ghostModeSound;
    [SerializeField, Tooltip("The time you will be a ghost in seconds.")]
    private float GhostTime = 10.0f;
    [SerializeField]
    private Material nonGhostMaterial;
    [SerializeField]
    private Material ghostMaterial;

    [SerializeField]
    private Material regularWallMaterial;
    [SerializeField]
    private Material seeThroughMaterial;

    private bool GhostMode = false;
    private bool timerIsRunning = false;
    private GameObject[] seeWalls;

    

    [Space]

    [SerializeField]
    private TextMeshProUGUI TimerText;

    [Space]

    [SerializeField, Tooltip("Sound when Finish.")]
    private AudioSource finishSound;
    [SerializeField]
    private TextMeshProUGUI FinishText;
    [SerializeField]
    private TextMeshProUGUI ThanksText;
    private bool Finished = false;
    private float beforeTransition = 5.0f;
    private bool FtimerIsRunning = false;

    private void Start()
    {
        seeWalls = GameObject.FindGameObjectsWithTag("SeeThroughWall");
        for (int i = 0; i < seeWalls.Length; i++)
        {
            seeWalls[i].GetComponent<Collider>().enabled = false;
        }

        TimerText.alpha = 0;
    }

    void Update()
    {
        coinHandler.setCurrentCoins(coins);

        if(GhostMode && timerIsRunning)
        {
            if(GhostTime > 0)
            {
                DisplayTime(GhostTime);
                GhostTime -= Time.deltaTime;
            } else
            {
                Debug.Log("Ghostmode Disabled");

                this.GetComponent<MeshRenderer>().material = nonGhostMaterial;
                this.GetComponent<TrailRenderer>().startColor = nonGhostMaterial.color;
                this.GetComponent<TrailRenderer>().endColor = nonGhostMaterial.color;
                for (int i = 0; i < seeWalls.Length; i++)
                {
                    seeWalls[i].GetComponent<MeshRenderer>().material = seeThroughMaterial;
                    seeWalls[i].GetComponent<Collider>().enabled = false;
                }

                TimerText.alpha = 0;
                GhostTime = 10.0f;
                GhostMode = false;
                timerIsRunning = false;
            }
        }

        if(Finished && FtimerIsRunning)
        {
            if(beforeTransition > 0)
            {
                beforeTransition -= Time.deltaTime;
            } else
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coins")
        {
            Destroy(other.gameObject);
            if(coinPickupSound != null)
            {
                coinPickupSound.Play();
            }
            coins++;
        }

        if(other.tag == "GhostPower" && !timerIsRunning)
        {
            //Trigger ghostmode;
            if(ghostModeSound != null)
            {
                ghostModeSound.Play();
            }
            GhostMode = true;
            timerIsRunning = true;
            Debug.Log("GhostMode Active");
            this.GetComponent<MeshRenderer>().material = ghostMaterial;
            this.GetComponent<TrailRenderer>().startColor = ghostMaterial.color;
            this.GetComponent<TrailRenderer>().endColor = ghostMaterial.color;
            for (int i = 0; i < seeWalls.Length; i++)
            {
                seeWalls[i].GetComponent<MeshRenderer>().material = regularWallMaterial;
                seeWalls[i].GetComponent<Collider>().enabled = true;
            }

        }

        if (other.tag == "Finish")
        {
            if(finishSound != null)
            {
                finishSound.Play();
            }
            Finished = true;
            FtimerIsRunning = true;

            FinishText.enabled = true;
            ThanksText.enabled = true;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimerText.alpha = 1;
        TimerText.text = string.Format("{0:00}:{1:00}", minutes,seconds);
    }
}
