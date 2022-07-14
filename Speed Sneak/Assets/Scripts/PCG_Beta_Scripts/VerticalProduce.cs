using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalProduce : MonoBehaviour
{
    
    public int Count = 0;
    public GameObject[] wall = new GameObject[10];
    public GameObject VerticalProducer; 
        // Start is called before the first frame update
 //   public Vector3 curr_Space;
    public Transform CurrSpace;

    void Start() {
        if (Count < 10) {
            CurrSpace = this.transform;
            
        }   
    }


    
}
