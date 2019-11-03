using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PostingType {
    Positive,
    Negative,
    Scam,
    Default
}

public class MessageScript : MonoBehaviour
{
    /*
     * MessageTimer = ?
     * FollowersToDecrease = ?
     * isScam = ?
     */

    float messageTimer = 0.0f;
    int followersToDecrease = 20;
    public bool isScam = false;
    public PostingType messageType = PostingType.Default;


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

        //This is what i think being better used while using Enum.

        /*
         * I'm using switch case to make it easier to adding 
         * function to each type of the messages
         * 
         * Default's Behaviors:
         * - 
         * 
         * Negative's Behaviors:
         * - minus 50 for the first negative, then x2 after that.
         * 
         * Positive's Behaviors:
         * - increase followers 5% for each positive behavior clicked
         * 
         * Scam's Behaviors:
         * - if tap on 3 times game will end and spawn negative post on the feed for everytime the player clicks on the scam message
         */

        switch(messageType)
        {
            case PostingType.Default:
                print("This is just Default Messages");
                Destroy(gameObject);
                break;
            case PostingType.Negative:
                print("This is just Negative Messages");
                Destroy(gameObject);
                break;
            case PostingType.Positive:
                print("This is just Positive Messages");
                Destroy(gameObject);
                break;
            case PostingType.Scam:
                print("This is just Scam Messages");
                Destroy(gameObject);
                Application.LoadLevel(Application.loadedLevel);
                break;
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
