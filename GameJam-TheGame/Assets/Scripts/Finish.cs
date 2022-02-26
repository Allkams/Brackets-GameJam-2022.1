using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
    }
}
