using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReplyMessageSCript : MonoBehaviour
{
    Color tempColor;

    public void OnSpawn()
    {
        InvokeRepeating("Opacity", 0, Time.deltaTime * 3.0f);
        Invoke("StopReply", 1.0f);
    }

    void Opacity()
    {
        tempColor = GetComponent<TextMeshProUGUI>().color;
        tempColor.a = tempColor.a * 0.9f;
        GetComponent<TextMeshProUGUI>().color = tempColor;
    }

    void StopReply()
    {
        Destroy(gameObject);
    }
}
