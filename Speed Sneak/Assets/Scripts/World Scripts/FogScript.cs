using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScript : MonoBehaviour
{

    /// <summary>
    /// All it does is it moves the "Fog" GameObject up slightly at the end of every frame.
    /// </summary>
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z + .001f);
    }
}
