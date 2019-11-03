using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Manager : MonoBehaviour
{
    /*
     * postsCounter = Int Counter for Posts
     * followersCounter = Int Counter for Followers
     * 
     * followerGainTimer = Timer for waitTime
     * batteryTimer = Timer for battery
     * waitTime = Gain followers every N interval
     * getFollowerPercent = Percentage for getting followers every WaitTime
     * increasePercentPerPost = Increase getFollowerPercent by N for every Post()
     * Every N Posts Plus One Follower = Increase the number of possible follower gains by 1 for every N Posts
     * maxPercent = Maximum percentage for getFollowerPercent
     * Battery Run Out Time = Time it takes for battery to run out
     */
    int postsCounter = 0;
    public int followersCounter = 0;
    float followerGainTimer = 0.0f;
    float batteryTimer = 0.0f;
    public float waitTime;
    float getFollowerPercent = 0.0f;
    public float increasePercentPerPost;
    public int EveryNPostsPlusOneFollower;
    public float maxPercent;
    public float BatteryRunOutTime;
    
    /*
     * PostsCounterTMPro
     * FollowerCounterTMPro
     * ShakeGO
     * Canvas
     */

    public TextMeshProUGUI PostsCounterTMPro;
    public TextMeshProUGUI FollowersCounterTMPro;
    public GameObject ShakeGO;
    public GameObject Canvas;

    /*
     * Origin Prefabs
     * blackPanel: ?
     * audioManager : ?
     */

    public GameObject leftMessage_Small;
    public GameObject rightMessage_Small;
    public GameObject leftMessage_Medium;
    public GameObject rightMessage_Medium;
    public GameObject rightMessageOrigin;
    public GameObject leftMessageOrigin;

    //Reply GameObject
    public GameObject replyGO;

    public Image blackPanel;

    public AudioManager audioManager;

    /*
     * This part is the Randomizer Text Messages.
     */

    public string[] shortMessages;
    public string[] mediumMessages;
    public string[] scamMessages;
    public string[] replies;

    //MESSAGE PERCENTAGES
    float chanceToSpawnSmall = 0.70f; //70%
    float chanceToGetMessage = 0.40f;
    float chanceToBeScam = 0.0f;

    public static Manager instance;

    bool shake = false;
    Color blackPanelTemp;

    float shakeAmount = 0.0f;

    public Swipe_Catch SwipeCatch_Manager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PostsCounterTMPro.SetText(postsCounter.ToString());
        FollowersCounterTMPro.SetText(followersCounter.ToString());
    }

    public void Post()
    {
        postsCounter += 1;
        PostsCounterTMPro.SetText(postsCounter.ToString());
        if(getFollowerPercent + increasePercentPerPost <= maxPercent)
        {
            getFollowerPercent += increasePercentPerPost;
        }
    }


    // Update is called once per frame
    void Update()
    {
        followerGainTimer += Time.deltaTime;
        if(followerGainTimer > waitTime)
        {
            if (Random.value <= getFollowerPercent / 100)
            {
                followersCounter += 1 + (postsCounter / EveryNPostsPlusOneFollower);
                FollowersCounterTMPro.SetText(followersCounter.ToString());
                print("HIT");

                for(int i=0;i< 1 + (postsCounter / EveryNPostsPlusOneFollower);i++)
                {
                    if (Random.value <= chanceToGetMessage)
                    {
                        SpawnMessage();
                    }
                }
            }
            followerGainTimer = 0.0f;
        }

        print(chanceToBeScam);

        /*
        if(shake == false)
        {
            batteryTimer += Time.deltaTime;
            blackPanelTemp = blackPanel.color;
            blackPanelTemp.a = (batteryTimer / BatteryRunOutTime);
            blackPanel.color = blackPanelTemp;
        }
       
        if(blackPanel.color.a >= 0.8)
        {
            if(ShakeGO.activeSelf == false)
            {
                ShakeGO.SetActive(true);
                shake = true;
                batteryTimer = 0.0f;
                Shake(10.0f, 60.0f);
                blackPanel.raycastTarget = true;
            }
        }

        if(shake)
        {
            if(Mathf.Abs(Input.acceleration.x)>0.05)
            {
                if(blackPanel.color.a>0.0f)
                {
                    blackPanelTemp = blackPanel.color;
                    blackPanelTemp.a -= Mathf.Abs(Input.acceleration.x * 0.003f);
                    blackPanel.color = blackPanelTemp;
                }
                else if(blackPanel.color.a<=0.0f)
                {
                    shake = false;
                    blackPanelTemp.a = 0.0f;
                    blackPanel.color = blackPanelTemp;
                    blackPanel.raycastTarget = false;
                    StopShake();
                }
            }
        }
        */
    }

    void Shake(float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", length);
    }
    
    
    void DoShake()
    { 
        if (shakeAmount > 0)
        {
            //Quaternion tempRot = ShakeGO.GetComponent<RectTransform>().localRotation;
            //tempRot = Quaternion.Lerp(Quaternion.Euler(tempRot.eulerAngles.x, tempRot.eulerAngles.x, tempRot.eulerAngles.z -20), Quaternion.Euler(tempRot.eulerAngles.x, tempRot.eulerAngles.x, tempRot.eulerAngles.z + 20), Random.value);
            Quaternion tempRot = Quaternion.Euler(0.0f, 0.0f, Random.value * Random.Range(-20, 20));

            ShakeGO.GetComponent<RectTransform>().localRotation = tempRot;
            
        }
    }

    void StopShake()
    {
        CancelInvoke("DoShake");
        ShakeGO.SetActive(false);
    }

    void SpawnMessage()
    {
        if(Random.value >= 0.5) //RIGHT SIDE MESSAGE
        {
            if (Random.value <= chanceToBeScam)
            {
                GameObject Message = Instantiate(rightMessage_Medium, new Vector3(rightMessageOrigin.transform.position.x, rightMessageOrigin.transform.position.y + Random.Range(-500.0f, 500.0f), rightMessageOrigin.transform.position.z), Quaternion.identity);
                Message.GetComponentInChildren<Text>().text = scamMessages[Random.Range(0, scamMessages.Length - 1)];
                Message.GetComponentInChildren<MessageScript>().isScam = true;
                Message.GetComponentInChildren<MessageScript>().startCountdown(5.0f);
                Message.transform.SetParent(Canvas.transform);
                Message.transform.DOScale(1.0f, 0.6f);
                Message.transform.DOLocalMoveX(163.5f, 0.3f);
            }
            else
            {
                if (Random.value <= chanceToSpawnSmall)
                {

                    GameObject Message = Instantiate(rightMessage_Small, new Vector3(rightMessageOrigin.transform.position.x, rightMessageOrigin.transform.position.y + Random.Range(-500.0f, 500.0f), rightMessageOrigin.transform.position.z), Quaternion.identity);
                    Message.GetComponentInChildren<Text>().text = shortMessages[Random.Range(0, shortMessages.Length - 1)];
                    Message.GetComponentInChildren<MessageScript>().startCountdown(5.0f);
                    Message.transform.SetParent(Canvas.transform);
                    Message.transform.DOScale(1.0f, 0.6f);
                    Message.transform.DOLocalMoveX(280, 0.3f);
                }
                else
                {
                    GameObject Message = Instantiate(rightMessage_Medium, new Vector3(rightMessageOrigin.transform.position.x, rightMessageOrigin.transform.position.y + Random.Range(-500.0f, 500.0f), rightMessageOrigin.transform.position.z), Quaternion.identity);
                    Message.GetComponentInChildren<Text>().text = mediumMessages[Random.Range(0, mediumMessages.Length - 1)];
                    Message.GetComponentInChildren<MessageScript>().startCountdown(5.0f);
                    Message.transform.SetParent(Canvas.transform);
                    Message.transform.DOScale(1.0f, 0.6f);
                    Message.transform.DOLocalMoveX(163.5f, 0.3f);
                }
                if (chanceToBeScam + 0.01f < 0.3f)
                {
                    chanceToBeScam += 0.01f;
                }
            }
        }
        else
        {
            if (Random.value <= chanceToBeScam)
            {
                GameObject Message = Instantiate(leftMessage_Medium, new Vector3(leftMessageOrigin.transform.position.x, leftMessageOrigin.transform.position.y + Random.Range(-500.0f, 500.0f), leftMessageOrigin.transform.position.z), Quaternion.identity);
                Message.GetComponentInChildren<Text>().text = scamMessages[Random.Range(0, scamMessages.Length - 1)];
                Message.GetComponentInChildren<MessageScript>().startCountdown(5.0f);
                Message.GetComponentInChildren<MessageScript>().isScam = true;
                Message.transform.SetParent(Canvas.transform);
                Message.transform.DOScale(1.0f, 0.6f);
                Message.transform.DOLocalMoveX(-163.5f, 0.3f);
            }
            else
            {
                if (Random.value <= chanceToSpawnSmall)
                {
                    GameObject Message = Instantiate(leftMessage_Small, new Vector3(leftMessageOrigin.transform.position.x, leftMessageOrigin.transform.position.y + Random.Range(-500.0f, 500.0f), leftMessageOrigin.transform.position.z), Quaternion.identity);
                    Message.GetComponentInChildren<Text>().text = shortMessages[Random.Range(0, shortMessages.Length - 1)];
                    Message.GetComponentInChildren<MessageScript>().startCountdown(5.0f);
                    Message.transform.SetParent(Canvas.transform);
                    Message.transform.DOScale(1.0f, 0.6f);
                    Message.transform.DOLocalMoveX(-280, 0.3f);
                }
                else
                {
                    GameObject Message = Instantiate(leftMessage_Medium, new Vector3(leftMessageOrigin.transform.position.x, leftMessageOrigin.transform.position.y + Random.Range(-500.0f, 500.0f), leftMessageOrigin.transform.position.z), Quaternion.identity);
                    Message.GetComponentInChildren<Text>().text = mediumMessages[Random.Range(0, mediumMessages.Length - 1)];
                    Message.GetComponentInChildren<MessageScript>().startCountdown(5.0f);
                    Message.transform.SetParent(Canvas.transform);
                    Message.transform.DOScale(1.0f, 0.6f);
                    Message.transform.DOLocalMoveX(-163.5f, 0.3f);
                }
                if (chanceToBeScam + 0.01f < 0.3f)
                {
                    chanceToBeScam += 0.01f;
                }
            }
        }
        audioManager.playMessageOnSpawnTone();
    }

}
