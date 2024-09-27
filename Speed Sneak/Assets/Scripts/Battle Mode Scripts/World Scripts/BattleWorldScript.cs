using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleWorldScript : MonoBehaviour
{
    private float timer = 0f;
    public GameObject player;
    public Animator bossAnimation;
    public GameObject Indicator;
    private List<GameObject> indicators = new List<GameObject>();

    public GameObject WallObstacle1;
    public GameObject WallObstacle2;

    private float BattleModeTimer = 60f;
    public Text timerText;
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
        BattleModeTimer -= Time.deltaTime;
        System.TimeSpan timeConverter = System.TimeSpan.FromSeconds(BattleModeTimer);
        string timeConverted = $"{(int)timeConverter.TotalMinutes}:{timeConverter.Seconds:00}";
        timerText.text = "Time Remaining: " + timeConverted;
        if(BattleModeTimer <= 0)
        {
            SceneManager.LoadScene(1);
        }
        if (bossAnimation.GetCurrentAnimatorStateInfo(0).IsName("DroneGuard|Idle"))
        {
            timer += Time.deltaTime;
            int randomObstacleGenerator = (int)(Random.value * 10);
            if (timer > 2 && randomObstacleGenerator % 2 == 0)
            {
                foreach (var myIndicator in indicators.Where(thisIndicator => !thisIndicator.activeInHierarchy))
                {
                    (float newPositionX, float newPositionZ, _, _) = GenerateNewObstaclePosition();
                    myIndicator.transform.position = new Vector3(newPositionX, myIndicator.transform.position.y, newPositionZ);
                    myIndicator.SetActive(true);
                }
                timer = 0f;
            }
            else if(timer > 2 && randomObstacleGenerator % 2 != 0)
            {
                if (!WallObstacle1.activeInHierarchy && !WallObstacle2.activeInHierarchy)
                {


                    bool sameX = Random.value > .5;
                    (float newPositionX, float newPositionZ, float newPosX2, float newPosZ2) = GenerateNewObstaclePosition();

                    float xPos = sameX ? player.transform.position.x : newPositionX;
                    float zPos = sameX ? newPositionZ : player.transform.position.z;

                    WallObstacle1.transform.position = new Vector3(xPos, WallObstacle1.transform.position.y, zPos);
                    float xScale = sameX ? 0 : .5f;
                    float zScale = sameX ? .5f : 0;
                    WallObstacle1.transform.localScale += new Vector3(xScale, 0, zScale);

                    float xPos2 = sameX ? player.transform.position.x : (newPosX2);
                    float zPos2 = sameX ? (newPosZ2) : player.transform.position.z;
                    WallObstacle2.transform.position = new Vector3(xPos2, WallObstacle2.transform.position.y, zPos2);

                    WallObstacle1.SetActive(true);
                    WallObstacle2.SetActive(true);

                    timer = 0f;
                }
            }
        }
    }

    (float, float, float, float) GenerateNewObstaclePosition()
    {
        int xNegative = (Random.value > .5) ? -1 : 1;
        int zNegative = (Random.value > .5) ? -1 : 1;
        float xDifference = xNegative * Random.value * 10;
        float zDifference = zNegative * Random.value * 10;
        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;
        return ((playerX + xDifference), (playerZ + zDifference), (playerX - xDifference), (playerZ - zDifference));
    }
}
