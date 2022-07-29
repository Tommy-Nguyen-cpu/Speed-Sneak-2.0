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

    int consecutive=0;
    //used to determine length;
    int pathVal=0;
    //used to call routelist, stored in StraightAway

    public Dictionary <int, Vector3> RouteList = new Dictionary <int, Vector3>();
    //dictionary for storing locations of total route
    public Dictionary <int, char> RouteDir = new Dictionary <int, char>();
    //relates route to iterator direcetion
    public Dictionary <int, int> RouteRowPos = new Dictionary <int, int>();
    //relates route to value in array 

    public Dictionary <int, int> StraightAway = new Dictionary <int, int>();
    //will store the first and last entry of a straightaway within grid
    //crucial for valid geometry detection!

    public GameObject BasicMob;
    public GameObject PatrolAiMob;

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
    HallMobs();
        //Finds valid hall mob positions, generates simple ai and hideouts
    SideRoute();
        //Generates Loops, spawns patrol ai
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
        //remove tile


        //STORE VALUES TO DICTIONARIES
        Vector3 Position = new Vector3 (1.0f*(iteration % roomFar), 0f, 1.0f*(iteration / roomFar));
        RouteList.Add(pathVal, Position); //stores locationt to entry
        RouteRowPos.Add(pathVal, iteration); //stores iteration value to entry
        RouteDir.Add(pathVal, lastDir);


        
      
        //ITERATE 
        pathVal++; //used for dictionaries
        iteration = newLocation(iteration); //determines new location of algorithm
    }
    }  

    int newLocation (int lastLocal){
        float range = Random.Range(0.0f, 1.0f);
        //compares against the weighted value. 
        //IF RANGE > WEIGHTCONTINUE 
        if (range>weightContinue){
            weightContinue = 0.82f;

            StraightAway.Add(pathVal,pathVal-consecutive);
       //     Debug.Log(pathVal);
       //     Debug.Log(pathVal-consecutive);
            //confirms distances work


            //stores START AND END POINTS
            consecutive = 0;


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
           
            //keeps stored value
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
        consecutive++;
        return lastLocal;
    }

     void establishStretch(){

     }
    
     void HallMobs(){
        foreach (KeyValuePair<int, int> entry in StraightAway) { 
        int N = entry.Key - entry.Value;
        if (N >= 5) { //VALID 
            //first, determine midpoint
            int midPoint = (entry.Key)-((N/2)+1); 
            //+1 is because we wnat total blocks, not distance from point A to point B.
            //if we had point 6 to point 1, that's 5 blocks difference
            //but the hallway is still 6 blocks long total, so we need to offset it by one!
            char TileDir = RouteDir[midPoint];
            int rowPos = RouteRowPos[midPoint];
            Vector3 AiPos = (RouteList[midPoint]);

            //SPAWN ENTITY
            //TODO
            //Debug.Log(entry.Key);
            //Debug.Log(entry.Value);
            //Debug.Log(N);
            //Debug.Log(midPoint);

            Instantiate(BasicMob,AiPos,Quaternion.identity);
            if (TileDir == 'L' || TileDir == 'R') {
                if (rowPos > (roomFar*roomSide)/2){
                GameObject.Destroy(Row[rowPos-roomSide]);
                GameObject.Destroy(Row[rowPos-roomSide*2]);
                }
                else {
                GameObject.Destroy(Row[rowPos+roomSide]);
                GameObject.Destroy(Row[rowPos+roomSide*2]);
                }
            }
            else {
   //            if (rowPos % roomSide > (roomSide)/2){
   //             GameObject.Destroy(Row[rowPos-1]);
   //             GameObject.Destroy(Row[rowPos-2]);
   //             }
   //             else {
   //             GameObject.Destroy(Row[rowPos+1]);
   //             GameObject.Destroy(Row[rowPos+2]);
   //             } 

            }

        }


        }
     }
    
     void SideRoute(){
      //  for (int i = 0; i<pathVal;i++){ 
    foreach (KeyValuePair<int, int> entry in StraightAway) { 
        int N = entry.Key - entry.Value;
        if (N == 4) { //VALID 
            int Top = entry.Key;
            int Bottom = entry.Value;

            char TopDir = RouteDir[Top];
            int TopPos = RouteRowPos[Top];

            char BottomDir = RouteDir[Top];
            int BottomPos = RouteRowPos[Top];

            if (BottomPos > roomFar*roomSide/4) { 
            //ensures these sequences will not spawn at the start of the map.
                if (BottomDir == 'L' || BottomDir == 'R') {
                    if (BottomPos > (roomFar*roomSide/2 - 2)){
                    GameObject.Destroy(Row[TopPos-roomSide]);
                    GameObject.Destroy(Row[TopPos-roomSide*2]);
                    GameObject.Destroy(Row[BottomPos-roomSide]);
                    GameObject.Destroy(Row[BottomPos-roomSide*2]);
                        if (BottomDir == 'L') {
                        GameObject.Destroy(Row[(BottomPos-roomSide*2)-1]);
                        GameObject.Destroy(Row[(BottomPos-roomSide*2)-2]);
                        }
                        else {
                        GameObject.Destroy(Row[(BottomPos-roomSide*2)+1]);
                        GameObject.Destroy(Row[(BottomPos-roomSide*2)+2]);    
                        }
                    }
                    else {
                     
        //            GameObject.Destroy(Row[TopPos+roomSide]);
        //            GameObject.Destroy(Row[TopPos+roomSide*2]);
        //            GameObject.Destroy(Row[BottomPos+roomSide]);
        //            GameObject.Destroy(Row[BottomPos+roomSide*2]);

                    }
                    }
                else {
          //          if (rowPos % roomSide > (roomSide)/2){
          //          GameObject.Destroy(Row[rowPos-1]);
          //          GameObject.Destroy(Row[rowPos-2]);
          //          }
          //          else {
          //          GameObject.Destroy(Row[rowPos+1]);
          //          GameObject.Destroy(Row[rowPos+2]);
          //          } 

            }
            


            }
        }


        }
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
