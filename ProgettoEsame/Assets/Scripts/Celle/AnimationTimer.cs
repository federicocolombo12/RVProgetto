using UnityEngine;

public class AnimationTimer : MonoBehaviour
{
    [SerializeField] private Paziente2ScriptNPC paziente2;
    [SerializeField] private Paziente3ScriptNPC paziente3;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private BuzzerSound buzzerSound; // Riferimento al suono del buzzer
    [SerializeField] private GameObject triggerObject;

    private void OnEnable()
    {
        CountdownTimer.OnTimerEnd += HandleTimerEnd;
    }

    private void OnDisable()
    {
        CountdownTimer.OnTimerEnd -= HandleTimerEnd;
    }

    private void HandleTimerEnd()
    {
        // Attiva il trigger dell'oggetto
        triggerObject.SetActive(true);

        // Avvia l'animazione delle porte
        doorAnimator.SetTrigger("OpenDoor");

        // Riproduce entrambi i suoni del buzzer con un piccolo ritardo tra loro
        if (buzzerSound != null)
        {
            buzzerSound.PlayBuzzer1();
            Invoke(nameof(PlaySecondBuzzer), 1.5f); // Il secondo suono parte dopo 1.5 secondi
        }
    }

    private void PlaySecondBuzzer()
    {
        if (buzzerSound != null)
        {
            buzzerSound.PlayBuzzer2();
        }
    }
}
