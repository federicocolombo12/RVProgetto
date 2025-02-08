using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    private TimeSpan startTime = new TimeSpan(12, 29, 0);
    private TimeSpan endTime = new TimeSpan(12, 30, 0);
    public static event Action OnTimerEnd; // Evento per notificare la fine del timer

    private void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        TimeSpan currentTime = startTime;
        bool timerEnded = false;

        while (true)
        {
            currentTime = currentTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
            UpdateTimeDisplay(currentTime);

            if (!timerEnded && currentTime >= endTime)
            {
                OnTimerEnd?.Invoke(); // Invoca l'evento quando il timer scade
                timerEnded = true;
            }

            yield return null;
        }
    }

    void UpdateTimeDisplay(TimeSpan timeToDisplay)
    {
        countdownText.text = string.Format("{0:00}:{1:00}:{2:00}", timeToDisplay.Hours, timeToDisplay.Minutes, timeToDisplay.Seconds);
    }
}
