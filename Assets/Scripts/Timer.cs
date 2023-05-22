using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    bool timeActive = false;
    float currentTime;
    [SerializeField] TextMeshProUGUI currentTimeText;
    [SerializeField] TextMeshProUGUI finalTimeText;

    void Start()
    {
        currentTime = 0;
    }


    void Update()
    {
        if(timeActive)
        {
            currentTime += Time.deltaTime;
        }
        // Displays time every frame
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");

    }

    public void StartTime()
    {
        timeActive = true;
    }

    // Ends and saves time you took to display at the end screen
    public void EndTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        finalTimeText.text = "Total Time: " + time.ToString(@"mm\:ss\:fff");
        timeActive = false;
        currentTime = 0;
    }

}
