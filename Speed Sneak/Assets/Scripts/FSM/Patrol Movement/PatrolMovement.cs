using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatrolMovement
{


    public Vector3 NPCOriginalPosition;

    public char rotationDirection;

    public void NPCPatrol(GameObject NPC)
    {
        RaycastHit hitLeft;

        RaycastHit hitForward;

        NPCOriginalPosition = NPC.transform.position;
        Vector3 NPCPosition = new Vector3(NPC.transform.position.x, 0f, NPC.transform.position.z);

        Vector3 raycastDirection = rotationDirection == 'L' ? Vector3.left : Vector3.right;

        // Casts a ray that looks for collisions with the ray.
        bool collidedWithWallForward = Physics.Raycast(NPCPosition, NPC.transform.TransformDirection(Vector3.forward), out hitForward, .5f);
        
        // Checks to see if the NPC hits anything ahead of it. NPC will only change directions once it hits something in front of it.
        if (hitForward.collider != null && (hitForward.collider.name == "BaseTest(Clone)" || hitForward.collider.name.Contains("Wall") || hitForward.collider.name.Contains("Goal")))
        {
            // Casts a ray that looks for collisions with the ray.
            bool collidedWithWallLeft = Physics.Raycast(NPCPosition, NPC.transform.TransformDirection(raycastDirection), out hitLeft, 1f);
            
            // Checks left and right to see if it hits anything. If it isn't hitting anything on the left then it will turn left, otherwise it will turn right.
            if (hitLeft.collider == null)
            {
                NPC.transform.Rotate(Vector3.up, rotationDirection == 'L' ? -90 : 90);
            }
            else
            {
                // If the NPC can't turn left or right, it will turn around and go back.
                NPC.transform.Rotate(Vector3.up, 180);
                rotationDirection = rotationDirection == 'L' ? 'R' : 'L';

            }
        }

        NPC.transform.position += NPC.transform.forward * Time.deltaTime * 2;


    }
}
