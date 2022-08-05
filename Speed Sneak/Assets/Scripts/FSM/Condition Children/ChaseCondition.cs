using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCondition : Condition
{
    /// <summary>
    /// If the agent sees the player, the agent will switch to the "chase" state.
    /// </summary>
    /// <returns></returns>
    public override bool Test()
    {
        RaycastHit hit;

        Vector3 NPCPosition = new Vector3(currentNPC.transform.position.x, 1f, currentNPC.transform.position.z);

        // Casts a ray that looks for collisions with the ray.
        bool collidedWithPlayer = Physics.Raycast(NPCPosition, currentNPC.transform.forward, out hit, 100f);

        //Debug.DrawRay(NPCPosition, currentNPC.transform.TransformDirection(Vector3.forward)*100, Color.green, 2, false);
        if (hit.collider != null && hit.collider.name == "Player(Clone)")
        {
            return true;
        }

        return false;
    }
}
