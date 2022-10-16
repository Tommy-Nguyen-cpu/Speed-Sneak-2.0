using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeamScript : MonoBehaviour
{

    void Update()
    {
        // TODO: At least its moving now. Not in the direction the NPC is looking at though.
        transform.position += transform.forward * Time.deltaTime * 15f;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.name.Contains("Platform"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.name.Contains("Player"))
        {
            HealthBar playerHealth = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            playerHealth.SetHealth(-1f);
        }
    }
}
