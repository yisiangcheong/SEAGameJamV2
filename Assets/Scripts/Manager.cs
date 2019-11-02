using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    int postsCounter = 0;
    int followersCounter = 0;
    float followerGainTimer = 0.0f;
    float batteryTimer = 0.0f;
    public float waitTime;
    float getFollowerPercent = 0.0f;
    public float increasePercentPerPost;
    public int EveryNPostsPlusOneFollower;
    public float maxPercent;
    public float BatteryRunOutTime;

    public TextMeshProUGUI PostsCounterTMPro;
    public TextMeshProUGUI FollowersCounterTMPro;
    public GameObject ShakeGO;

    public Image blackPanel;
    bool shake = false;
    Color blackPanelTemp;

    float shakeAmount = 0.0f;

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
        //print(getFollowerPercent.ToString());
        print(postsCounter/EveryNPostsPlusOneFollower);
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
            }
            followerGainTimer = 0.0f;
        }

        if(shake == false)
        {
            batteryTimer += Time.deltaTime;
            blackPanelTemp = blackPanel.color;
            blackPanelTemp.a = (batteryTimer / BatteryRunOutTime);
            blackPanel.color = blackPanelTemp;
        }
       
        if(blackPanel.color.a >= 0.6)
        {
            if(ShakeGO.activeSelf == false)
            {
                ShakeGO.SetActive(true);
                shake = true;
                batteryTimer = 0.0f;
                Shake(10.0f, 6.0f);
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
                }
            }
        }

        print(blackPanel.color.a);
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
    }
}
