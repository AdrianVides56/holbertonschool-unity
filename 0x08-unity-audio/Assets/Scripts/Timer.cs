using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public Text timerText;
    float currentTime;
    void Start()
    {
        currentTime = 0f;
    }

    void Update()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTime += 1 * Time.deltaTime;
        timerText.text = time.ToString(@"mm\:ss\.ff");
    }
}
