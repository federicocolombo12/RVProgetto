using UnityEngine;

public class AnimationTimer : MonoBehaviour
{
    [SerializeField] private Paziente2ScriptNPC paziente2;
    [SerializeField] private Paziente3ScriptNPC paziente3;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private BuzzerSound buzzerSound; // Aggiunto riferimento al suono del buzzer
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
        // Imposta i flag su true
        triggerObject.SetActive(true);

        // Avvia l'animazione delle porte
        doorAnimator.SetTrigger("OpenDoor");

        // Riproduce il suono del buzzer
        if (buzzerSound != null)
        {
            buzzerSound.PlayBuzzer();
        }
    }
}
