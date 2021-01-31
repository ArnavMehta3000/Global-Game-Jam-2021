using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpacityChange : MonoBehaviour
{
    float lerpTime = 0f;
    public float callTime = 0.2f;
    TextMeshProUGUI tmp;
    bool flag = false;


    private void OnEnable()
    {
        flag = false;
        lerpTime = 0f;
    }


    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.color = Color.clear;
        Invoke("SetFlag", callTime);
    }


    private void Update()
    {
        if (flag && lerpTime <= 1)
        {
            tmp.color = Color.Lerp(Color.clear, Color.white, lerpTime);
            lerpTime += 0.01f;
        }
    }

    void SetFlag()
    {
        flag = true;
    }
}
