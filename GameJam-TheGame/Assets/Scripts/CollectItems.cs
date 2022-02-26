using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    [SerializeField, Tooltip("The sound that will be played when picking up coins.")]
    private AudioSource coinPickupSound;
    [SerializeField, Tooltip("CoinHandler, Talks to the Canvas")]
    private CoinsHandler coinHandler;
    private int coins;


    [SerializeField, Tooltip("The time you will be a ghost in seconds.")]
    private float GhostTime = 5.0f;
    private bool GhostMode = false;
    private float activationTime = 0.0f;

    void Update()
    {
        coinHandler.setCurrentCoins(coins);

        if(GhostMode && (activationTime <= GhostTime))
        {
            Debug.Log("GhostMode Activated");
            activationTime++;
        } else
        {
            GhostMode = false;
            activationTime = 0.0f;
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
        }
    }
}
