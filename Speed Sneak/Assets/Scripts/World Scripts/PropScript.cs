using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropScript : MonoBehaviour
{
    public GameObject bucket;
    public GameObject woodBlock;
    public GameObject trashCan;
    public GameObject box;
    GameObject[] props = new GameObject[6];
    // Start is called before the first frame update
    void Start()
    {
        // TODO: PCG will take this process and do it themselves.
        // Generate 2-10 repetitive probs.
        int numberOfRepetitiveProps = Random.Range(2, 11);

        for (int i = 0; i <= numberOfRepetitiveProps; i++)
        {
            Instantiate(bucket, new Vector3(Random.Range(0,1000), 0.3f, Random.Range(0,1000)), Quaternion.identity);
            Instantiate(woodBlock, new Vector3(Random.Range(0, 1000), 0.3f, Random.Range(0, 1000)), Quaternion.identity);
            Instantiate(trashCan, new Vector3(Random.Range(0, 1000), 0.3f, Random.Range(0, 1000)), Quaternion.identity);
            Instantiate(box, new Vector3(Random.Range(0, 1000), 0.3f, Random.Range(0, 1000)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Check to see if the player collides with any of the prefabs using Raycasting.
    }
}
