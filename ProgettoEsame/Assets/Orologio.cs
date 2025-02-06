using System;
using UnityEngine;
using UnityEngine.UI;

public class DigitalClock : MonoBehaviour
{
    public Text textClock;
    public int countdownTime = 60; // Tempo di conto alla rovescia in secondi
    private float remainingTime;

    void Awake()
    {
        textClock = GetComponent<Text>();
        remainingTime = countdownTime;
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
            string hour = LeadingZero(timeSpan.Hours);
            string minute = LeadingZero(timeSpan.Minutes);
            string second = LeadingZero(timeSpan.Seconds);
            textClock.text = $"{hour}:{minute}:{second}";
        }
        else
        {
            textClock.text = "00:00:00";
        }
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}
