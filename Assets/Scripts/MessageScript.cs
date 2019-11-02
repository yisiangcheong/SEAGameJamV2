using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageScript : MonoBehaviour
{
    float messageTimer = 0.0f;
    int followersToDecrease = 20;
    public bool isScam = false;


    public void DestroySelf()
    {
        if(isScam == false)
        {
            CancelInvoke("decreaseTimer");
            Destroy(gameObject);
        }
        else
        {
            print("GAMEOVER");
        }
    }

    public void startCountdown(float length)
    {
        messageTimer = length;
        InvokeRepeating("decreaseTimer", 0, Time.deltaTime);
        Invoke("Stop", length);
    }

    void decreaseTimer()
    {
        messageTimer -= Time.deltaTime;
    }

    void Stop()
    {
        CancelInvoke("decreaseTimer");
        if(Manager.instance.followersCounter - followersToDecrease >= 0)
        {
            Manager.instance.followersCounter -= followersToDecrease;
        }
        else
        {
            Manager.instance.followersCounter = 0;
            Manager.instance.FollowersCounterTMPro.text = Manager.instance.followersCounter.ToString();
        }
        Destroy(gameObject);
    }
}
