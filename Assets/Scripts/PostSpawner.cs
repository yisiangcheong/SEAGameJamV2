using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PostSpawner : MonoBehaviour
{
    public GameObject[] Postings = new GameObject[3];
    public List<GameObject> LibraryPosting = new List<GameObject>();
    public GameObject SpawnerPosition;
    public float DurationAnimationPosting;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool CheckPostingSlotOneEmpty()
    {
        if (Postings[1] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckPostingSlotTwoEmpty()
    {
        if (Postings[2] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SpawnPost()
    {


        int RandomIndex = Random.Range(0, LibraryPosting.Count);
        GameObject Post = Instantiate(LibraryPosting[RandomIndex], new Vector3(SpawnerPosition.transform.position.x,SpawnerPosition.transform.position.y),Quaternion.identity);
        Post.transform.SetParent(Canvas.transform);

        

        if (CheckPostingSlotOneEmpty())
        {
            Post.transform.DOLocalMoveY(-125, DurationAnimationPosting);
            Postings[1] = Post;
        }
        else if (CheckPostingSlotTwoEmpty())
        {
            Post.transform.DOLocalMoveY(-217.5f, DurationAnimationPosting);
            Postings[1].transform.DOLocalMoveY(388, DurationAnimationPosting);
            Postings[2] = Post;
        }
        else
        {
            Postings[1].transform.DOLocalMoveY(1025, DurationAnimationPosting);
            Postings[0] = Postings[1];
            Postings[2].transform.DOLocalMoveY(388, DurationAnimationPosting);
            Postings[1] = Postings[2];
            Post.transform.DOLocalMoveY(-217.5f, DurationAnimationPosting);
            Postings[2] = Post;
            Postings[0].transform.DOKill();
            Destroy(Postings[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
