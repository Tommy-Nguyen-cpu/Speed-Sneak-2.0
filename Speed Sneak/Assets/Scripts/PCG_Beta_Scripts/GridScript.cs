using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    //Each slow in Row is dedicated to a single chunk, of object wall
    public GameObject[] Row = new GameObject[400];//15^2
    public GameObject Walls;
    //as this is a single array, keep these dimensions square
    int roomFar = 20;
    int roomSide = 20;
    // Start is called before the first frame update
    char lastDir = 'N';
    //Thus weighted value determines how likely the alg will iterate in one direction.
    //It decreases with each successful movement in the same direction, resets after a turn to it's initial value
    float weightContinue = 0.82f;
    
    
    void Start()
    {
        for (int i = 0; i < roomFar; i++) {
            for (int j = 0; j<roomSide; j++) {
          

//            Vector3 Position = new Vector3 (1.0f*(float j), 0.0f, 1.0*(float i));
            Vector3 Position = new Vector3 (1.0f*j, 0f, 1.0f*i);

            int f = (i*roomFar)+j; //each row consits of j entries, with i amount of rows      
            Row[f] = Instantiate(Walls,Position,Quaternion.identity);
        //    test();
            }
        }
    FirstMove();
        //TIME TO CUT A PATH
        
    }
    //
    void FirstMove(){
        //The only true balanced ranodom decision, determines the INITIAL DIRECTION of the iterator.
            int firstDir = Random.Range(0,3);
            if (firstDir==0) lastDir='L';
            if (firstDir==1) lastDir='F';
            if (firstDir==2) lastDir='R';
            //Starts Path Creation Method
            PathClear();
    }
    

    void PathClear(){
        //Initial Position starts in the middle of the bottom row
        int iteration = roomFar/2;
        //while still within the vertical bounds of the grid, continue the algorithm.
        while (iteration < (roomFar*roomSide)) {
        //remove *current* wall piece
        GameObject.Destroy(Row[iteration]);   
        //use newLocation weighted alg to 
        iteration = newLocation(iteration);
    }
    }  

    int newLocation (int lastLocal){
        float range = Random.Range(0.0f, 1.0f);
        //compares against the weighted value. 
        //IF RANGE > WEIGHTCONTINUE 
        if (range>weightContinue){
            weightContinue = 0.82f;
       //     if (lastDir = 'L' || lastDir == 'R') {
            if (lastDir != 'F') { 
                lastLocal = lastLocal+roomFar;
                lastDir = 'F';
                //^ IF WE ARE NOT GOING FORWARD, WE ARE GOING FORWARD NOW. This ensures the path does not double back on itself.
                //IE, if we are traveling left, or right, we will now be traveling up.
                //Move up accordingly.
            }
            else { //if we were traveling forward
                int newDir = Random.Range(0,2);
                if (lastLocal % roomFar == roomSide-1){
                    lastDir = 'L'; lastLocal=lastLocal-1; 
                }
                //^ If on rightmost side go Left
                else if (lastLocal % roomFar == 0){
                    lastDir = 'R'; lastLocal=lastLocal+1;
                }
                //^ If on leftmost side go right
                else if (newDir < 0.5){ lastDir = 'L'; lastLocal=lastLocal-1; 
                } else { lastDir = 'R'; lastLocal=lastLocal+1;
                }
                //Otherwise, we will randomly choose betwen left or right. 
                //move left or right accordingly.
            }
        } else { //IF WE DO NOT CHANGE DIRECTIONS
            weightContinue = weightContinue*0.8f;
            //^ Scale WeightContinue down by 4/5's it's original size, so it's more likely to change next time
            //Then, move the iterator to it's next location.
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
    
     void HallMobs(){
     }
    
     void SideRoute(){
     }



    ///////////////////////////////////////
    //UNUSED METHODS
    IEnumerator test(){
       yield return new WaitForSeconds(1);
    }   
    //Attempt to withhold operation by real time

    void basicPathclear(){
    for (int i = 0; i<roomFar; i++){
        GameObject.Destroy(Row[(roomSide/2)+i*roomFar]);
        }        
    }
    //TestRun Program, designed to show iteration system can work. Will move in a straight line from end to end, to goal.
}
