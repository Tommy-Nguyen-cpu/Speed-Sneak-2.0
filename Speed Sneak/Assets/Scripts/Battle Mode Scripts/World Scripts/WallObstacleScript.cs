using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObstacleScript : MonoBehaviour
{
    public GameObject otherWall;
    public Vector3 OriginalPosition;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y >= 4.40)
        {
            transform.position = Vector3.MoveTowards(transform.position, otherWall.transform.position, 1f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 4.5f, transform.position.z), .05f);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.name == otherWall.name)
        {
            gameObject.transform.position = OriginalPosition;
            gameObject.SetActive(false);
        }
        else if (collider.name == "Player")
        {
            Debug.Log("Collided with player!");
            HealthBar playerHealth = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            playerHealth.SetHealth(-6f);
        }
    }
}
