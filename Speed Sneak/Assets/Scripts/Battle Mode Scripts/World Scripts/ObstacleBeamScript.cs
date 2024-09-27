using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBeamScript : MonoBehaviour
{
    private bool reachedTop = false;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 30)
        {
            reachedTop = true;
        }
        else
        {
            transform.position += transform.up * Time.deltaTime * 5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            HealthBar playerHealth = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            playerHealth.SetHealth(-3f);
        }
    }

    public void SetFlag(bool newFlagValue)
    {
        reachedTop = newFlagValue;
    }

    public bool GetFlag()
    {
        return reachedTop;
    }
}
