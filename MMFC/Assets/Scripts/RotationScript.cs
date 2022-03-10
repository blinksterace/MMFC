using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    GameObject cameraObject;
    Vector3 playerRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cameraObject = GameObject.Find("CameraHolder");

        playerRotation = this.transform.eulerAngles;

        playerRotation.y = cameraObject.transform.eulerAngles.y;

        this.transform.eulerAngles = playerRotation;
    }
}
