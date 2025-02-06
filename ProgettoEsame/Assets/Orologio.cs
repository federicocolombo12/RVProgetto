using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Assicurati di includere questo namespace per usare UI elements
using TMPro;  // Solo se usi TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;  // Cambia in 'public Text countdownText;' se non usi TextMeshPro
    public float countdownTime = 60;  // Durata del timer in secondi

    private void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();  // Cambia in 'GetComponent<Text>();' se non usi TextMeshPro
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
    }

    void UpdateTimeDisplay(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
