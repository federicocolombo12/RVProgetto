using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    public float countdownTime = 60;
    public static event Action OnTimerEnd; // Evento per notificare la fine del timer

    private void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        float currentTime = countdownTime;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimeDisplay(currentTime);
            yield return null;
        }
        countdownText.text = "Finito!";
        OnTimerEnd?.Invoke(); // Invoca l'evento quando il timer scade
    }

    void UpdateTimeDisplay(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
