using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraScript : MonoBehaviour
{
    private GameObject Camera;
    private float PlayerZPosition;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Camera");
        PlayerZPosition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float differenceInZValue = transform.position.z - PlayerZPosition;
        if(differenceInZValue > 5 || differenceInZValue < -5)
        {
            Debug.Log("Difference In Z Value: " + differenceInZValue);
            int incrementCameraZPosition = differenceInZValue > 0 ? 5 : -5;
            PlayerZPosition = transform.position.z;
            Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, Camera.transform.position.z + incrementCameraZPosition);
        }
    }
}
