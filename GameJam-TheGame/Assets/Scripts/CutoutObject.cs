using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField, Tooltip("The targetObject")]
    private Transform targetObject;

    [SerializeField, Tooltip("Test")]
    private LayerMask wallMask;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for(int i = 0; i < hitObjects.Length; ++i)
        {
            if(hitObjects[i].transform.GetComponent<Renderer>() != null)
            {
                Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

                for(int j = 0; j < materials.Length; ++j)
                {
                    materials[j].SetVector("_CutoutPos", cutoutPos);
                    materials[j].SetFloat("_CutoutSize", 0.1f);
                    materials[j].SetFloat("_FalloffSize", 0.05f);
                }
            }
        }
    }
}
