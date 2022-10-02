using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVision : MonoBehaviour
{
    public GameObject player;
    // Script used to move an empty game object, which the boss will look at to follow player.

    Vector3 playerPosition;

    void Update()
    {
        playerPosition = player.transform.position;
    }
    void LateUpdate()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * 5);
        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
    }
}
