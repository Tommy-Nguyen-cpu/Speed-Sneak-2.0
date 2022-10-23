using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleWorldScript : MonoBehaviour
{
    private float timer = 0f;
    public GameObject player;
    public GameObject Indicator;
    private List<GameObject> indicators = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < 20; i++)
        {
            GameObject newIndicator = Instantiate(Indicator);
            newIndicator.SetActive(false);
            indicators.Add(newIndicator);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Find a way to randomize the obstacle the player will face. Should use timer.
        timer += Time.deltaTime;
        if(timer > 2)
        {
            foreach(var myIndicator in indicators.Where(thisIndicator => !thisIndicator.activeInHierarchy))
            {
                int xNegative = (Random.value > .5) ? -1 : 1;
                int zNegative = (Random.value > .5) ? -1 : 1;
                float xDifference = xNegative * Random.value * 10;
                float zDifference = zNegative * Random.value * 10;
                float playerX = player.transform.position.x;
                float playerZ = player.transform.position.z;
                myIndicator.transform.position = new Vector3(playerX + xDifference, myIndicator.transform.position.y, playerZ + zDifference);
                myIndicator.SetActive(true);
            }
            timer = 0f;
        }
    }
}
