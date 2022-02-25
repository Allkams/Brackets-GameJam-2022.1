using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    [SerializeField, Tooltip("The bounce height.")]
    private float height;
    [SerializeField, Tooltip("The rotation speed.")]
    private float speed;
    [SerializeField, Tooltip("The amount of rotation per frame.")]
    private Vector3 rotation;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);

        float bounceY = Mathf.Sin(Time.time * speed) * height + startPosition.y;
        transform.position = new Vector3(startPosition.x, bounceY, startPosition.z);
    }
}
