using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Animator bossAnimation;
    public GameObject bossVision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossAnimation.GetCurrentAnimatorStateInfo(0).IsName("DroneGuard|Idle"))
            transform.LookAt(bossVision.transform);
    }
}
