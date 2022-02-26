using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsHandler : MonoBehaviour
{
    [SerializeField, Tooltip("The Canvas Text where the scoreText will be displayed")]
    private TextMeshProUGUI scoreText;

    int coins;
    int TotalCoins;

    public void setCurrentCoins(int coins)
    {
        this.coins = coins;
    }


    void Start()
    {
        TotalCoins = GameObject.FindGameObjectsWithTag("Coins").Length;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Coins: " + coins + "/" + TotalCoins;
    }
}
