using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeamScript : MonoBehaviour
{

    void Update()
    {
        // TODO: At least its moving now. Not in the direction the NPC is looking at though.
        transform.position += transform.forward * Time.deltaTime * 10f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains("Platform"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.collider.name.Contains("Player"))
        {
            Debug.Log("Hit Player!");        
        }
    }
}
