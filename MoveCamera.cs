using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;
    Vector3 placeholder;

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
