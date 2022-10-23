using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BossScript : MonoBehaviour
{
    public Animator bossAnimation;
    public GameObject bossVision;
    public GameObject bossPosition;

    public List<GameObject> beams;
    public GameObject beamObject;

    public float timer = 0f;

    void Awake()
    {
        beams = new List<GameObject>();
        for(int i = 0; i < 10; i++)
        {
            GameObject newBeam = Instantiate(beamObject);
            newBeam.SetActive(false);
            beams.Add(newBeam);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bossPosition.transform.position = Vector3.MoveTowards(bossPosition.transform.position, transform.position, 0);
        if (bossAnimation.GetCurrentAnimatorStateInfo(0).IsName("DroneGuard|Idle"))
        {
            timer += Time.deltaTime;
            transform.LookAt(bossVision.transform);

            if(timer > 1f)
            {
                GameObject notEnabledBeam = beams.Where(beam => !beam.activeInHierarchy).FirstOrDefault();
                if (notEnabledBeam != null)
                {
                    notEnabledBeam.transform.position = bossPosition.transform.position;
                    notEnabledBeam.transform.rotation = transform.rotation;

                    notEnabledBeam.SetActive(true);
                }
                timer = 0f;
            }
        }
    }
}
