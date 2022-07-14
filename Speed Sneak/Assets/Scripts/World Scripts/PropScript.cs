using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropScript : MonoBehaviour
{
    public GameObject bucket;
    public GameObject woodBlock;
    public GameObject trashCan;
    public GameObject box;
    List<GameObject> props;
    // Start is called before the first frame update
    void Start()
    {
        // TODO: PCG will take this process and do it themselves.
        // Generate 2-10 repetitive probs.
        int numberOfRepetitiveProps = Random.Range(2, 11);
        props = new List<GameObject>();
        for (int i = 0; i <= numberOfRepetitiveProps; i++)
        {
            props.Add(Instantiate(bucket, new Vector3(Random.Range(0,100), 0.3f, Random.Range(0,100)), Quaternion.identity));
            props.Add(Instantiate(woodBlock, new Vector3(Random.Range(0, 100), 0.3f, Random.Range(0, 100)), Quaternion.identity));
            props.Add(Instantiate(trashCan, new Vector3(Random.Range(0, 100), 0.3f, Random.Range(0, 100)), Quaternion.identity));
            props.Add(Instantiate(box, new Vector3(Random.Range(0, 100), 0.3f, Random.Range(0, 100)), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i< props.Count; i++)
        {
            RaycastHit hit;

            // Vector3.forward works now. Turns out I though the back of the guard was the front, when the front was actually the "blue" side.
            Vector3 propPosition = new Vector3(props[i].transform.position.x, 1.5f, props[i].transform.position.z);

            // Casts a ray that looks for collisions with the ray.
            bool collidedWithPlayer = Physics.Raycast(propPosition, props[i].transform.forward, out hit, 10f);

            // If the player collided with a prop, we'll set the soundDetected flag to true and the source of the sound object to be the prop.
            if (collidedWithPlayer)
            {
                SoundDetection.soundDetected = true;
                SoundDetection.sourceOfSound = props[i];
                Debug.Log("Collided with player!");
                break;
            }
        }
    }
}
