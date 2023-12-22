using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraSpeed = 5f;

    private Vector3 offeset;

    void Start()
    {
        offeset = transform.position - target.position;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offeset, Time.deltaTime * cameraSpeed);
        }
    }
}
