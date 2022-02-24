using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField, Tooltip("The object the camera will follow.")]
    private GameObject ObjectToFollow;
    [SerializeField, Tooltip("Cameras offset from GameObject")]
    private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = ObjectToFollow.transform.position + offset;

        transform.LookAt(ObjectToFollow.transform.position);
    }
}
