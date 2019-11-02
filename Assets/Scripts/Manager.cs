using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    int postsCounter = 0;
    int followersCounter = 0;
    float getFollowerPercent = 0.0f;

    public TextMeshProUGUI PostsCounterTMPro;
    public TextMeshProUGUI FollowersCounterTMPro;
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
        getFollowerPercent += 0.5f;
        print(getFollowerPercent);

        if(Random.value <= getFollowerPercent/100)
        {
            followersCounter += 1;
            FollowersCounterTMPro.SetText(followersCounter.ToString());
            print("HIT");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
