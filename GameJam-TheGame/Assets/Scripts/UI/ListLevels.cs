using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Linq;

public class ListLevels : MonoBehaviour
{
    [SerializeField]
    private GameObject LevelButton;
    [SerializeField]
    private String[] GameLevels;

    private int LevelID;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < GameLevels.Length; i++)
        {
           GameObject level = Instantiate(LevelButton, new Vector3(this.transform.position.x, this.transform.position.y-(80+62*i)) ,new Quaternion(0,0,0,0),this.transform);

            level.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameLevels[i];
            level.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "0/0";
            level.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "00:00:000";
        }

        this.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(1088.29f, 100 + (63 * LevelID) + 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
