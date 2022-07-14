using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    
    public GameObject[] Row = new GameObject[225];//15^2
    public GameObject Walls;
    int roomFar = 15;
    int roomSide = 15;
    // Start is called before the first frame update
    char lastDir = 'N';
    float weightContinue = 0.82f;

    void Start()
    {
        for (int i = 0; i < roomFar; i++) {
            for (int j = 0; j<roomSide; j++) {
          

//            Vector3 Position = new Vector3 (1.0f*(float j), 0.0f, 1.0*(float i));
            Vector3 Position = new Vector3 (1.1f*j, 0f, 1.1f*i);

            int f = (i*roomFar)+j; //each row consits of j entries, with i amount of rows      
            Row[f] = Instantiate(Walls,Position,Quaternion.identity);
        //    test();
            }
        }
    FirstMove();
        //TIME TO CUT A PATH
        
    }
    void FirstMove(){
  //      iteration = roomSide/2;
  //      GameObject.Destroy(Row[(roomSide/2)]);
            int firstDir = Random.Range(0,3);
            if (firstDir==0) lastDir='L';
            if (firstDir==1) lastDir='F';
            if (firstDir==2) lastDir='R';
            PathClear();
    }
    
    void PathClear(){
        int iteration = roomFar/2;
        while (iteration < (roomFar*roomSide)-roomSide+roomSide) {
        GameObject.Destroy(Row[iteration]);   
        //iteration=iteration+roomSide;
        iteration = newLocation(iteration);
    }
    }  

    int newLocation (int lastLocal){
        float range = Random.Range(0.0f, 1.0f); 
        if (range>weightContinue){
            weightContinue = 0.82f;
       //     if (lastDir = 'L' || lastDir == 'R') {
            if (lastDir != 'F') {
                lastLocal = lastLocal+roomFar;
                lastDir = 'F';
            }
            else {
                int newDir = Random.Range(0,2);
                if (newDir < 0.5){ lastDir = 'L'; lastLocal=lastLocal-1; 
                } else { lastDir = 'R'; lastLocal=lastLocal+1;
                }
            }
        } else {
            weightContinue = weightContinue*0.8f;
            if (lastDir == 'F') lastLocal=lastLocal+roomFar;
            if (lastDir =='L') { 
                    if (lastLocal % roomFar != 0) lastLocal=lastLocal-1; 
                    else { lastDir = 'F'; lastLocal=lastLocal+roomFar;
                    }
            }
            if (lastDir == 'R') {
                    if (lastLocal % roomFar != roomSide-1) lastLocal=lastLocal+1; 
                    else { lastDir = 'F'; lastLocal=lastLocal+roomFar;
                    }
            }
        }
        return lastLocal;
    }
    

    IEnumerator test(){
       yield return new WaitForSeconds(1);
    }   
    void basicPathclear(){
    for (int i = 0; i<roomFar; i++){
        GameObject.Destroy(Row[(roomSide/2)+i*roomFar]);
        }        
    }
}
