using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatrolMovement
{
    public float speed = 5f;
    public int height = 10;
    public int width = 10;
    public double timeCounter = 0;


    public Vector3 NPCOriginalPosition;

    public void NPCPatrol(GameObject NPC)
    {
        timeCounter += Time.deltaTime;

        // These two mathematical functions allow the agent to move in a circle (since the values of sine and consines go up but eventually go down).
        float CosX = (float)Math.Cos(timeCounter);
        float SinZ = (float)Math.Sin(timeCounter);

        // We can increase the size of the circle rotation by changing "2f" to some larger value (ex: 10f).
        float x = NPCOriginalPosition.x + (CosX)*2f;
        float z = NPCOriginalPosition.z + (SinZ)*2f;

        NPC.transform.position = new Vector3(x, NPC.transform.position.y, z);
        NPC.transform.Rotate(0.0f, -1*CosX, 0.0f, Space.World);


    }
}
