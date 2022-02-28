using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoys : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] saveObjcs = GameObject.FindGameObjectsWithTag("Music");

        if(saveObjcs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
