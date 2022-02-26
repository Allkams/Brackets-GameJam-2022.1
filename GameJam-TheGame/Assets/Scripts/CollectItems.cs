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
                Debug.Log("GhostMode Active");

                DisplayTime(GhostTime);
                this.GetComponent<MeshRenderer>().material = ghostMaterial;
                for(int i = 0; i < seeWalls.Length; i++)
                {
                    seeWalls[i].GetComponent<MeshRenderer>().material = regularWallMaterial;
                    seeWalls[i].GetComponent<Collider>().enabled = true;
                }
                
                GhostTime -= Time.deltaTime;
            } else
            {
                Debug.Log("Ghostmode Disabled");

                this.GetComponent<MeshRenderer>().material = nonGhostMaterial;
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

        if(other.tag == "GhostPower")
        {
            //Trigger ghostmode;
            GhostMode = true;
            timerIsRunning = true;
        }

        if(other.tag == "Finish")
        {
            SceneManager.LoadScene(0);
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
