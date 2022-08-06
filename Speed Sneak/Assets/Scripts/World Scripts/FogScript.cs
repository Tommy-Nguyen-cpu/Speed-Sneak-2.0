using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScript : MonoBehaviour
{

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z + .005f);
    }
}
