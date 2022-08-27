using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : Condition
{


    /// <summary>
    /// Tests to see if the agent collided with the player, if so, then we will go to the "win" state.
    /// </summary>
    /// <returns></returns>
    public override bool Test()
    {
        RaycastHit hit;

        Vector3 NPCPosition = new Vector3(currentNPC.transform.position.x, 1f, currentNPC.transform.position.z);

        // Casts a ray that looks for collisions with the ray.
        bool collidedWithPlayer = Physics.Raycast(NPCPosition, currentNPC.transform.forward, out hit, .5f);

        //Debug.DrawRay(NPCPosition, currentNPC.transform.TransformDirection(Vector3.forward), Color.green, 2, false);
        if (hit.collider != null && hit.collider.name == "Player(Clone)")
        {
            return true;
        }

        return false;
    }
}
