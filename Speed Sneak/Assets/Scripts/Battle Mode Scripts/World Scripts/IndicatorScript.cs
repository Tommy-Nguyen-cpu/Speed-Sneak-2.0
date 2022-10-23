using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    float timer = 0f;
    public GameObject obstacleBeam;

    private GameObject thisBeam;

    private Material thisMaterial;
    private ObstacleBeamScript thisBeamScript;

    void Awake()
    {
        thisBeam = Instantiate(obstacleBeam);
        thisBeam.SetActive(false);

        thisMaterial = GetComponent<MeshRenderer>().material;
        thisBeamScript = thisBeam.GetComponent<ObstacleBeamScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 5 && !thisBeam.activeInHierarchy)
        {
            thisBeam.transform.position = new Vector3(transform.position.x, -0.17f, transform.position.z);
            thisBeam.SetActive(true);
            timer = 0f;
        }
        else
        {
            thisMaterial.color = Color.Lerp(Color.red, Color.magenta, Mathf.PingPong(Time.time, 1));
        }

        if (thisBeamScript.GetFlag())
        {
            timer = 0f;
            thisBeamScript.SetFlag(false);
            thisBeam.SetActive(false);
            gameObject.SetActive(false);
        }
        timer += Time.deltaTime;
    }
}
